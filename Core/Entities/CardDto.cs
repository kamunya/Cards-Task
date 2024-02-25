using Cards.Core.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cards.Core.Entities
{
    public class CardDto
    {
        public CardDto()
        {
                
        }
        public CardDto(string id)
        {
            Id = id;
        }
        public string Id { get; set; }
        public string UserId { get; set; }
        public List<Card> Items { get; set; } 
    }
}
