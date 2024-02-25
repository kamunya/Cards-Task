using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Cards.Core.Interfaces;
using Cards.Core.Specifications;
using Cards.Errors;
using Cards.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cards.Extensions;
using Cards.Core.Entities.Enum;
using cardDto = Cards.Dtos.CardDto;
using System;
using Cards.Core.Entities;
using Cards.Infrastracture.Repositories;

namespace Cards.Controllers
{

    public class CardController : BaseApiController
    {
        private readonly IGenericRepository<Card> _genericCardRepository;
        private readonly ICardRepository _cardRepository;
        public IMapper _Mapper;
        public CardController(IGenericRepository<Card> genericCardRepository, CardRepository cardRepository,
             IMapper mapper)
        {
            _genericCardRepository = genericCardRepository;
            _cardRepository = cardRepository;
            _Mapper = mapper;
        }
        [HttpGet(nameof(GetCards))]
        public async Task<ActionResult<Pagination<CardDto>>> GetCards(
            [FromQuery] CardSpecPrams cardSpecPrams)
        {
            var spec = new FilterCardSpecification(cardSpecPrams);
            var total = await _genericCardRepository.CountAsync(spec);
            var cards = await _genericCardRepository.ListAsync(spec);
            var data = _Mapper.Map<IReadOnlyList<Card>, IReadOnlyList<CardDto>>(cards);
            return Ok(new Pagination<CardDto>(cardSpecPrams.pageIndex, cardSpecPrams.pageSize, total, data));
        }

        [HttpGet(nameof(GetCardById))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIResponce), StatusCodes.Status404NotFound)]
        public async Task<Card> GetCardById([FromQuery] int Id)
        {
            var spec = new SortCardSpecification(Id);
            var cards = await _genericCardRepository.GetByIdAsync(Id);
            return _Mapper.Map<Card>(cards);
        }

        [HttpPost(nameof(CreateOrder))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIResponce), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Card>> CreateOrder(cardDto cardDto)
        {
            var user = HttpContext.User.ReteriveEmailFromPrincipal();
            var card = await _cardRepository.CreateCardAsync
                (   
                 cardDto.Name,
                 user,
                 cardDto.Description,
                 cardDto.Color,
                 DateTime.Now,
                 CardStatus.ToDo
                );
            if (card == null)
            {
                return BadRequest(new APIResponce(400, "Failed creating new card"));
            }
            return Ok(card);
        }

        [HttpPost(nameof(UpdateCard))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIResponce), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CardDto>> UpdateCard(CardDto card)
        {
            var data = await _cardRepository.UpdateCardAsync(card);
            if (data == null)
            {
                return BadRequest(new APIResponce(400, "Failed updating card"));
            }
            return Ok(data);
        }

        [HttpDelete(nameof(DeleteCard))]
        public async Task DeleteCard(string Id)
        {
            await _cardRepository.DeleteCardAsync(Id);
        }
    }
}

