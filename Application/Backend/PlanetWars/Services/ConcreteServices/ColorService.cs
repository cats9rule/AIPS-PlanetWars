using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PlanetWars.Core.Configuration;
using PlanetWars.Data.Models;
using PlanetWars.DTOs;

namespace PlanetWars.Services.ConcreteServices
{
    public class ColorService : IColorService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ColorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Add(ColorDto colorDto)
        {
            using (_unitOfWork)
            {
                return await _unitOfWork.Colors.Add(DtoToModel(colorDto));
            }
        }

        public async Task<bool> Delete(Guid id)
        {
            using (_unitOfWork)
            {
                return await _unitOfWork.Colors.Delete(id);
            }
        }

        public async Task<IEnumerable<ColorDto>> GetAll()
        {
            using (_unitOfWork)
            {
                IEnumerable<Color> colors = await _unitOfWork.Colors.GetAll();
                List<ColorDto> colorDtos = new List<ColorDto>();
                foreach (Color c in colors)
                {
                    colorDtos.Add(ModelToDto(c));
                }
                return colorDtos;
            }
        }

        public async Task<ColorDto> GetByHexValue(string hexValue)
        {
            using (_unitOfWork)
            {
               var color = await _unitOfWork.Colors.GetByHexValue(hexValue);
               if (color != null)
               {
                   return ModelToDto(color);
               }
               return null;
            }
        }

        public async Task<ColorDto> GetById(Guid id)
        {
            using (_unitOfWork)
            {
               var color = await _unitOfWork.Colors.GetById(id);
               if (color != null)
               {
                   return ModelToDto(color);
               }
               return null;
            }
        }

        public async Task<bool> Update(ColorDto colorDto)
        {
            using (_unitOfWork)
            {
                return await _unitOfWork.Colors.Update(DtoToModel(colorDto));
            }
        }

        #region Mappers
        public static ColorDto ModelToDto(Color model)
        {
            return new ColorDto
            {
                ID = model.ID,
                HexValue = model.HexValue
            };
        }
        public static Color DtoToModel(ColorDto dto)
        {
            return new Color
            {
                ID = dto.ID,
                HexValue = dto.HexValue
            };
        }
        #endregion
    }
}