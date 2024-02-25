using Cards.Core.Entities;
using Cards.Core.Entities.Enum;
using System;
using System.Threading.Tasks;

namespace Cards.Core.Interfaces
{
    public interface ICardRepository
    {
        Task<CardDto> CreateCardAsync(
            string Name,
            string UserId,
            string Description,
            string Color,
            DateTime CreatedOn,
            CardStatus status);
        Task<CardDto> GetCardAsync(string CardId);
        Task<CardDto> UpdateCardAsync(CardDto Card);
        Task<bool> DeleteCardAsync(string CardId);
    }
}
