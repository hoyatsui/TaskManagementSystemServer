using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Core.DTOs;
using TaskManagementSystem.Core.Repositories;
using TaskManagementSystem.Core.Services;

namespace TaskManagementSystem.API.Contollers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class QuoteController : ControllerBase
    {
        private readonly IQuoteService _quoteService;
        private readonly IMapper _mapper;

        public QuoteController(IQuoteService quoteService, IMapper mapper)
        {
            _quoteService = quoteService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllQuotes()
        {
            var quotes = await _quoteService.GetAllQuotesAsync();
            return Ok(quotes);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuoteById(int id)
        {
            var quote = await _quoteService.GetQuoteByIdAsync(id);
            if (quote == null) return NotFound();
            return Ok(quote);
        }
        [HttpPost]
        public async Task<IActionResult> AddQuote(QuoteCreationDTO quoteCreationDTO)
        {
            var quote = await _quoteService.AddQuoteAsync(quoteCreationDTO);
            return CreatedAtAction(nameof(GetQuoteById), new { id = quote.QuoteID }, quote);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateQuote(QuoteDTO quoteDTO)
        {
            var result = await _quoteService.UpdateQuoteAsync(quoteDTO);
            if (!result) return NotFound();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuote(int id)
        {
            var result = await _quoteService.DeleteQuoteAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchQuotes(string term)
        {
            var quotes = await _quoteService.SearchQuotesAsync(term);
            return Ok(quotes);
        }
    }
}
