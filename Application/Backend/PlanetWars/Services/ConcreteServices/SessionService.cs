using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using PlanetWars.Communication;
using PlanetWars.Core.Configuration;
using PlanetWars.Data.Models;
using PlanetWars.DTOs;
using PlanetWars.DTOs.Communication;
using PlanetWars.Services.Strategy;
using PlanetWars.Services.Exceptions;

namespace PlanetWars.Services.ConcreteServices
{
    public class SessionService : ISessionService
    {
        #region Attributes
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly HubService _hubService;

        private readonly ITurnActionContext _turnActionContext;
        #endregion

        public SessionService(IUnitOfWork uow, IMapper mapper, HubService hubService, ITurnActionContext context)
        {
            _unitOfWork = uow;
            _mapper = mapper;
            _hubService = hubService;
            _turnActionContext = context;
        }

        public async Task<bool> Add(Session session)
        {
            using (_unitOfWork)
            {
                return await _unitOfWork.Sessions.Add(session);
            }
        }

        public async Task<IEnumerable<SessionDto>> GetAllSessions()
        {
            using (_unitOfWork)
            {
                return _mapper.Map<List<Session>, List<SessionDto>>(new List<Session>(await _unitOfWork.Sessions.GetAll()));
            }
        }

        public async Task<SessionDto> GetById(Guid sessionId)
        {
            using (_unitOfWork)
            {
                return _mapper.Map<Session, SessionDto>(await _unitOfWork.Sessions.GetById(sessionId));
            }
        }

        public async Task<SessionDto> GetByNameAndCode(string name, string code)
        {

            return _mapper.Map<Session, SessionDto>(await _unitOfWork.Sessions.GetByNameAndCode(name, code));

        }

        public async Task<bool> Update(UpdateSessionDto sessionDto)
        {
            using (_unitOfWork)
            {
                var session = await _unitOfWork.Sessions.GetById(sessionDto.SessionID);

                if (sessionDto.HasLostPlanet == true && sessionDto.HasWonPlanet)
                {
                    var loser = await _unitOfWork.Players.GetById(sessionDto.PlanetLoserID);
                    var planet = await _unitOfWork.Planets.GetById(sessionDto.PlanetID);
                    loser.Planets.Remove(planet);

                    await _unitOfWork.Players.Update(loser);
                }

                if (sessionDto.HasWonPlanet == true)
                {
                    var winner = await _unitOfWork.Players.GetById(sessionDto.PlanetWinnerID);
                    var planet = await _unitOfWork.Planets.GetById(sessionDto.PlanetID);
                    winner.Planets.Add(planet);
                    planet.OwnerID = winner.ID;
                    planet.Owner = winner;

                    await _unitOfWork.Planets.Update(planet);
                    await _unitOfWork.Players.Update(winner);
                }

                if (sessionDto.NextPlayer == true)
                {
                    session.CurrentTurnIndex = (session.CurrentTurnIndex++) % session.Players.Count;
                }

                var retval = await _unitOfWork.Sessions.Update(session);
                await _unitOfWork.CompleteAsync();
                return retval;
            }
        }

        public async Task<bool> Delete(Guid id)
        {
            using (_unitOfWork)
            {
                List<Planet> sessionPlanets = new List<Planet>(await _unitOfWork.Planets.GetForSession(id));
                List<PlanetPlanet> relations = new List<PlanetPlanet>();
                foreach (Planet planet in sessionPlanets)
                {
                    relations.AddRange(await _unitOfWork.PlanetPlanets.DeleteAllRelationsForPlanet(planet.ID));
                }
                await _unitOfWork.CompleteAsync();
                var retval = await _unitOfWork.Sessions.Delete(id);
                if (retval)
                {
                    try
                    {
                        await _unitOfWork.CompleteAsync();
                    }
                    catch (Exception e)
                    {
                        await _unitOfWork.PlanetPlanets.AddAll(relations);
                        Console.Error.Write(e.Message);
                        Console.Error.Write(e.StackTrace);
                    }

                }
                return retval;
            }
        }

        public async Task<bool> DeleteAll()
        {
            using (_unitOfWork)
            {
                var relations = await _unitOfWork.PlanetPlanets.DeleteAll();
                await _unitOfWork.CompleteAsync();
                await _unitOfWork.Players.DeleteAll();
                await _unitOfWork.Planets.DeleteAll();
                await _unitOfWork.Galaxies.DeleteAll();
                await _unitOfWork.Sessions.DeleteAll();
                try
                {
                    await _unitOfWork.CompleteAsync();
                }
                catch (Exception e)
                {
                    await _unitOfWork.PlanetPlanets.AddAll(relations.ToList());
                    Console.Error.Write(e.Message);
                    Console.Error.Write(e.StackTrace);
                }
                return true;
            }
        }

        public async Task<SessionDto> CreateSession(CreateGameDto dto)
        {
            using (_unitOfWork)
            {
                GameMap gameMap = await _unitOfWork.GameMaps.GetById(dto.GameMapID);

                User user = await _unitOfWork.Users.GetById(dto.UserID);
                if (gameMap == null || user == null) return null;

                Session session = await InitSession(dto.Name, gameMap.MaxPlayers);
                if (session == null) return null;

                Player player = new Player()
                {
                    User = user,
                    UserID = user.ID,
                    PlayerColor = await _unitOfWork.PlayerColors.GetByTurnIndex(0),
                    IsActive = true,
                    Session = session,
                    IsSessionOwner = true,
                    SessionID = session.ID,
                    TotalArmies = 5, //TODO: define this parameter
                    Planets = new List<Planet>()
                };

                session.Players.Add(player);
                session.PlayerCount++;

                Galaxy galaxy = new Galaxy()
                {
                    ResourcePlanetRatio = gameMap.ResourcePlanetRatio,
                    PlanetCount = gameMap.PlanetCount,
                    Session = session,
                    SessionID = session.ID,
                    Planets = new List<Planet>(),
                    GameMap = gameMap
                };
                var result = await _unitOfWork.Galaxies.Add(galaxy);
                if (result)
                {
                    session.Galaxy = galaxy;
                }
                else return null;

                await _unitOfWork.Sessions.Update(session);
                await _unitOfWork.CompleteAsync();
                var s = _mapper.Map<Session, SessionDto>(session);
                return s;
            }
        }

        public async Task<SessionDto> AddPlayer(Guid sessionID, Guid userID)
        {
            using (_unitOfWork)
            {
                User user = await _unitOfWork.Users.GetById(userID);
                Session session = await _unitOfWork.Sessions.GetById(sessionID);

                if (session.PlayerCount == session.MaxPlayers) return null;

                int turnIndex = session.PlayerCount;

                Player player = new Player()
                {
                    User = user,
                    UserID = userID,
                    PlayerColor = await _unitOfWork.PlayerColors.GetByTurnIndex(turnIndex),
                    IsActive = true,
                    SessionID = sessionID,
                    Session = session,
                    Planets = new List<Planet>()
                };

                session.Players.Add(player);
                session.PlayerCount++;
                await _unitOfWork.Sessions.Update(session);
                await _unitOfWork.CompleteAsync();

                return _mapper.Map<SessionDto>(session);
            }
        }

        public async Task<bool> PlayMove(TurnDto turn)
        {
            using (_unitOfWork)
            {
                var session = await _unitOfWork.Sessions.GetById(new Guid(turn.SessionID));
                if (session == null) throw new InvalidActionException("Session with given ID does not exist.");
                var player = await _unitOfWork.Players.GetById(new Guid(turn.PlayerID));
                if (player == null) throw new InvalidActionException("Player with given ID does not exist.");
                if (session.TurnsPlayed <= player.PlayerColor.TurnIndex) {
                    if (!ValidatePlacedArmies(turn.Actions, player, true)) 
                        throw new InvalidActionException("Invalid number of armies placed.");
                }
                else if (!ValidatePlacedArmies(turn.Actions, player)) 
                    throw new InvalidActionException("Invalid number of armies placed.");

                var connections = await _unitOfWork.PlanetPlanets.GetAllRelationsForSession(session.ID);

                session = ProcessActions(turn.Actions, session, player, connections);

                var nextPlayer = session.Players.Find(p => p.PlayerColor.TurnIndex == session.CurrentTurnIndex);

                if (nextPlayer == null) throw new Exception("Next player could not be found.");
                int armiesNextTurn = CalculateNewArmies(
                    nextPlayer, 
                    session.CurrentTurnIndex >= ++session.TurnsPlayed
                );

                nextPlayer.TotalArmies += armiesNextTurn; 

                session.Galaxy.Planets = session.Galaxy.Planets.OrderBy(p => p.IndexInGalaxy).ToList();
                await _unitOfWork.Sessions.Update(session);
                await _unitOfWork.CompleteAsync();

                Console.WriteLine("\n\n" + session.CurrentTurnIndex);

                GameUpdateDto gud = new GameUpdateDto()
                {
                    ArmiesNextTurn = armiesNextTurn,
                    Session = _mapper.Map<SessionDto>(session)
                };
                var result = await _hubService.NotifyOnGameChanges(gud);
                if (result.IsSuccessful)
                {
                    var winner = FindWinner(session);
                    if (winner != null)
                    {
                        GameOverDto god = new GameOverDto()
                        {
                            SessionID = session.ID,
                            winner = _mapper.Map<PlayerDto>(winner)
                        };
                        result = await _hubService.NotifyOnWinner(god);
                    }
                }
                return result.IsSuccessful;
            }
        }

        //TODO: test this
        public async Task<bool> LeaveGame(LeaveGameDto dto)
        {
            using (_unitOfWork)
            {
                Session session = await _unitOfWork.Sessions.GetById(dto.SessionID);
                Player player = session.Players.Where(player => player.ID == dto.PlayerID).FirstOrDefault();
                if (player != null)
                {
                    User user = await _unitOfWork.Users.GetById(player.UserID);
                    var success = await _unitOfWork.Players.Delete(dto.PlayerID);
                    session.PlayerCount--;
                    if (success)
                    {
                        await _unitOfWork.CompleteAsync();
                        var notification = new LeaveGameNotificationDto()
                        {
                            Message = $"{user.Username}#{user.Tag} has left the game.",
                            PlayerID = player.ID,
                            SessionID = dto.SessionID
                        };
                        await _hubService.NotifyPlayerLeft(notification);
                        return true;

                    }
                }
                return false;
            }
        }

        private string GetRandomString(int size)
        {
            char[] chars =
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            byte[] data = new byte[4 * size];
            using (var crypto = RandomNumberGenerator.Create())
            {
                crypto.GetBytes(data);
            }
            StringBuilder result = new StringBuilder(size);
            for (int i = 0; i < size; i++)
            {
                var rnd = BitConverter.ToUInt32(data, i * 4);
                var idx = rnd % chars.Length;
                result.Append(chars[idx]);
            }
            return result.ToString();
        }

        private async Task<Session> InitSession(string name, int maxPlayers)
        {
            Session session = new Session()
            {
                Name = name,
                MaxPlayers = maxPlayers,
                GameCode = GetRandomString(6),
                Players = new List<Player>(),
                PlayerCount = 0,
                CurrentTurnIndex = 0
            };
            var result = await _unitOfWork.Sessions.Add(session);
            await _unitOfWork.CompleteAsync();
            return result ? session : null;
        }

        private int CalculateNewArmies(Player player, bool isInitial)
        {
            return isInitial ? 5 : player.Planets.Count / 2;
        }

        private bool ValidatePlacedArmies(List<ActionDto> actions, Player player, bool isInitial = false)
        {
            int armies = CalculateNewArmies(player, isInitial);
            Console.WriteLine("\n\n" + armies + "\n\n");
            if (armies > 0)
            {
                actions.ForEach(a =>
                {
                    if (a.Type == ActionType.Placement)
                    {
                        armies -= a.Armies;
                    }
                });
                Console.WriteLine("\n\nAfter validate: " + armies + "\n\n");
                if (armies == 0)
                {
                    return true;
                }

            }
            return false;
        }

        private Session ProcessActions(List<ActionDto> actions, Session session, Player player, List<PlanetPlanet> connections)
        {
            actions.ForEach(action =>
            {
                if (SetStrategyForAction(action, player))
                {
                    session = _turnActionContext.DoAction(action, session, connections);
                }
                else throw new InvalidActionException("Invalid action type.");
            });
            session.Players.ForEach(player =>
            {
                if (player.Planets.Count == 0) player.IsActive = false;
            });

            ProcessTurnIndex(session);

            return session;
        }
        private bool SetStrategyForAction(ActionDto action, Player player)
        {
            switch (action.Type)
            {
                case ActionType.Attack:
                    {
                        if (player.Planets.Where(p => p.ID.ToString() == action.PlanetFrom).FirstOrDefault().Extras.Contains("atk"))
                            _turnActionContext.SetStrategy(new BoostedAttackStrategy());
                        else
                            _turnActionContext.SetStrategy(new AttackStrategy());
                        break;
                    }
                case ActionType.Movement:
                    {
                        if (player.Planets.Where(p => p.ID.ToString() == action.PlanetFrom).FirstOrDefault().Extras.Contains("mov"))
                            _turnActionContext.SetStrategy(new BoostedMoveStrategy());
                        else
                            _turnActionContext.SetStrategy(new MovementStrategy());
                        break;
                    }
                case ActionType.Placement:
                    {
                        _turnActionContext.SetStrategy(new PlacementStrategy());
                        break;
                    }
                default:
                    {
                        return false;
                    }
            }
            return true;
        }

        private Session ProcessTurnIndex(Session session)
        {
            bool foundNext = false;
            while (!foundNext)
            {
                session.CurrentTurnIndex = (session.CurrentTurnIndex + 1) % session.PlayerCount;
                var player = session.Players.Where(player => player.PlayerColor.TurnIndex == session.CurrentTurnIndex).First();
                Console.WriteLine(player.User.Username);
                foundNext = player.IsActive;
            }
            return session;
        }

        private Player FindWinner(Session session)
        {
            Player winner = null;
            for (int i = 0; i < session.PlayerCount; i++)
            {
                Player player = session.Players[i];
                if (player.IsActive)
                {
                    if (winner == null) winner = player;
                    else return null;
                }
            }
            return winner;
        }
    }

    public static class ActionType
    {
        public const string Attack = "attack";
        public const string Movement = "movement";
        public const string Placement = "placement";
    }
}