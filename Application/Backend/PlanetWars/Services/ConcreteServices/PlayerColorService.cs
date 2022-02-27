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
                var retval = await _unitOfWork.PlayerColors.Add(_mapper.Map<PlayerColorDto, PlayerColor>(pcDto));
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
                return _mapper.Map<List<PlayerColor>, List<PlayerColorDto>>(new List<PlayerColor>(await _unitOfWork.PlayerColors.GetAll()));
                // IEnumerable<PlayerColor> playerColors = await _unitOfWork.PlayerColors.GetAll();
                // List<PlayerColorDto> pcDtos = new List<PlayerColorDto>();
                // foreach (PlayerColor pc in playerColors)
                // {
                //     pcDtos.Add(ModelToDto(pc));
                // }
                // return pcDtos;
            }
        }

        public async Task<PlayerColorDto> GetById(Guid id)
        {
            using (_unitOfWork)
            {
               var pc = await _unitOfWork.PlayerColors.GetById(id);
               if (pc != null)
               {
                   return _mapper.Map<PlayerColor, PlayerColorDto>(pc);
               }
               return null;
            }
        }

        public async Task<bool> Update(PlayerColorDto pcDto)
        {
            using (_unitOfWork)
            {
                var retval = await _unitOfWork.PlayerColors.Update(_mapper.Map<PlayerColorDto, PlayerColor>(pcDto));
                await _unitOfWork.CompleteAsync();
                return retval;
            }
        }
    }
}