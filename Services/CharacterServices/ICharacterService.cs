using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Dotnet_Project.Services.CharacterServices
{
    public interface ICharacterService
    {
        Task<ServiceResponse<List<GetCharacterDto>>> GetCharacters();
        Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id);
        Task<ServiceResponse<List<GetCharacterDto>>>AddCharacter(AddCharacterDto character);
        Task <ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updatedcharacter);
         Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id);

    }
}