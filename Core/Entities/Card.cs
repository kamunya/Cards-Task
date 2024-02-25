using Cards.Core.Entities.Enum;
using System;
using System.ComponentModel.DataAnnotations;

namespace Cards.Core.Entities
{
    public class Card : BaseEntity
    {

        public required string Name { get; set; }
        public string Description { get; set; }
        [RegularExpression("^#[0-9A-Fa-f]{6}$", ErrorMessage = "Color must be in the format #RRGGBB")]
        public string Color { get; set; }
        public DateTime CreatedOn { get; set; }
        public CardStatus Status { get; set; } = CardStatus.ToDo;
    }
}
