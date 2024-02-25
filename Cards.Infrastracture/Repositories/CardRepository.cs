using AutoMapper;
using Cards.Core.Entities;
using Cards.Core.Entities.Enum;
using Cards.Core.Interfaces;
using StackExchange.Redis;
using System;
using System.Text.Json;
using System.Threading.Tasks;
using static StackExchange.Redis.Role;

namespace Cards.Infrastracture.Repositories
{
    public class CardRepository : ICardRepository
    {
        private readonly IDatabase _database;
        private readonly IMapper _mapper;

        public CardRepository(IConnectionMultiplexer radis, IMapper mapper)
        {
            _database = radis.GetDatabase();
            _mapper = mapper;
        }

        public async Task<CardDto> CreateCardAsync(string Name,
            string UserId,
            string Description,
            string Color,
            DateTime CreatedOn,
            CardStatus status)
        {
            var cardDto = new CardDto();
            cardDto.Items[0].Name = Name;
            cardDto.Items[0].Description = Description;
            cardDto.Items[0].Color = Color;
            cardDto.Items[0].CreatedOn = CreatedOn;
            cardDto.Items[0].Status = status;
            cardDto = _mapper.Map<CardDto, CardDto>(cardDto);
            var created = await _database.StringSetAsync(cardDto.Id,
                JsonSerializer.Serialize(cardDto), TimeSpan.FromDays(15));
            if (!created)
            {
                return null;
            }
            return await GetCardAsync(cardDto.Id);
        }

        public async Task<bool> DeleteCardAsync(string cardId)
        {
            return await _database.KeyDeleteAsync(cardId);
        }

        public async Task<CardDto> GetCardAsync(string cardId)
        {
            var data = await _database.StringGetAsync(cardId);
            return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CardDto>(data);
        }

        public async Task<CardDto> UpdateCardAsync(CardDto cardId)
        {
            var created = await _database.StringSetAsync(cardId.Id,
                JsonSerializer.Serialize(cardId), TimeSpan.FromDays(15));
            if (!created)
            {
                return null;
            }
            return await GetCardAsync(cardId.Id);
        }
    }
}
