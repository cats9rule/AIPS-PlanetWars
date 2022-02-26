using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using PlanetWars.Core.Configuration;
using PlanetWars.Data.Models;
using PlanetWars.DTOs;

namespace PlanetWars.Services.ConcreteServices
{
    public class PlayerColorService : IPlayerColorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public PlayerColorService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> Add(PlayerColorDto pcDto)
        {
            using (_unitOfWork)
            {
                var retval = await _unitOfWork.PlayerColors.Add(DtoToModel(pcDto));
                await _unitOfWork.CompleteAsync();
                return retval;
            }
        }

        public async Task<bool> Delete(Guid id)
        {
            using (_unitOfWork)
            {
                var retval = await _unitOfWork.PlayerColors.Delete(id);
                await _unitOfWork.CompleteAsync();
                return retval;
            }
        }

        public async Task<IEnumerable<PlayerColorDto>> GetAll()
        {
            using (_unitOfWork)
            {
                IEnumerable<PlayerColor> playerColors = await _unitOfWork.PlayerColors.GetAll();
                List<PlayerColorDto> pcDtos = new List<PlayerColorDto>();
                foreach (PlayerColor pc in playerColors)
                {
                    pcDtos.Add(ModelToDto(pc));
                }
                return pcDtos;
            }
        }

        public async Task<PlayerColorDto> GetById(Guid id)
        {
            using (_unitOfWork)
            {
               var pc = await _unitOfWork.PlayerColors.GetById(id);
               if (pc != null)
               {
                   return ModelToDto(pc);
               }
               return null;
            }
        }

        public async Task<bool> Update(PlayerColorDto pcDto)
        {
            using (_unitOfWork)
            {
                var retval = await _unitOfWork.PlayerColors.Update(DtoToModel(pcDto));
                await _unitOfWork.CompleteAsync();
                return retval;
            }
        }

        #region Mappers
        //TODO: implement Automapper
        public static PlayerColorDto ModelToDto(PlayerColor model)
        {
            return new PlayerColorDto
            {
                ID = model.ID,
                HexColor = model.ColorHexValue,
                TurnIndex = model.TurnIndex
            };
        }
        public static PlayerColor DtoToModel(PlayerColorDto dto)
        {
            return new PlayerColor
            {
                ID = dto.ID,
                ColorHexValue = dto.HexColor,
                TurnIndex = dto.TurnIndex
            };
        }
        #endregion
    }
}