using Microsoft.Extensions.Logging;
using Cards.Core.Entities;
using Cards.Infrastracture.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Cards.Infrastracture.SeedData
{
    public class CardsSeed
    {
        public static async Task SeedAsync(CardsContext CardsContext, ILoggerFactory loggerFactory)
        {
            try
            {
                if(!CardsContext.Cards.Any())
                {
                    var carddata = File.ReadAllText("../Cards.Infrastracture/SeedData/card.json");
                    var cards = JsonSerializer.Deserialize<List<Card>>(carddata);
                    foreach (var cardItem in cards)
                    {
                        await CardsContext.Cards.AddAsync(cardItem);
                    }
                    await CardsContext.SaveChangesAsync();
                }
                
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<CardsSeed>();
                logger.LogError(ex, "Failed processing request.");
            }
        }
    }
}
