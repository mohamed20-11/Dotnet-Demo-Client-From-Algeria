global using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Dotnet_Project.Services.CharacterServices
{
    public class CharacterService : ICharacterService
    {
        static private List<Character> characters = new List<Character> {
          new Character{Class =RpgClass.Cleric},
          new Character{Id=1, Name="Mohamed" , Class=RpgClass.Mage}
        };
        private readonly IMapper _mapper;
        private readonly DataContext _dataContext; 

        public CharacterService(IMapper mapper , DataContext dataContext)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto character)
        {
            var servieceResponse = new ServiceResponse<List<GetCharacterDto>>();
            var newCharacter = _mapper.Map<Character>(character);
            _dataContext.Characters.Add(newCharacter);
           await _dataContext.SaveChangesAsync();
            servieceResponse.Data =await _dataContext.Characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToListAsync();
            return servieceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
             var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
             try 
             {
                var dbCharacters = await _dataContext.Characters.FirstOrDefaultAsync(c=>c.Id==id);
                if(dbCharacters is null)
                {
                    throw new Exception($"Character with Id '{id}'  not found.");
                }
                _dataContext.Characters.Remove(dbCharacters);
                _dataContext.SaveChangesAsync();
                serviceResponse.Data= characters.Select(c=>_mapper.Map<GetCharacterDto>(c)).ToList();
             }
             catch(Exception ex)
             {
                serviceResponse.Success=false;
                serviceResponse.Msg=ex.Message;
             }
             return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            var servieceResponse = new ServiceResponse<GetCharacterDto>();
            var dbCharacters = await _dataContext.Characters.FirstOrDefaultAsync(c => c.Id == id);
            servieceResponse.Data = _mapper.Map<GetCharacterDto>(dbCharacters);
            return servieceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetCharacters()
        {
            var servieceResponse = new ServiceResponse<List<GetCharacterDto>>();
            var dbCharacters=await _dataContext.Characters.ToListAsync();
            servieceResponse.Data = dbCharacters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            return servieceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updatedcharacter)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            try
            {
                var dbCharacters = await _dataContext.Characters.FirstOrDefaultAsync(c => c.Id == updatedcharacter.Id);
                if(dbCharacters is null)
                {
                    throw new Exception($"Character with Id {updatedcharacter.Id}  not found.");
                }
                dbCharacters.Name = updatedcharacter.Name;
                dbCharacters.Defense = updatedcharacter.Defense;
                dbCharacters.Intellegence = updatedcharacter.Intellegence;
                dbCharacters.Strength = updatedcharacter.Strength;
                dbCharacters.Class = updatedcharacter.Class;
                dbCharacters.HitPoints = updatedcharacter.HitPoints;
                await _dataContext.SaveChangesAsync();
                serviceResponse.Data = _mapper.Map<GetCharacterDto>(dbCharacters);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Msg = ex.Message;
            }
            return serviceResponse;
        }
    }
}