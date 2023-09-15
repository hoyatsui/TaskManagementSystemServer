using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Core.DTOs;
using TaskManagementSystem.Core.Models;
using TaskManagementSystem.Core.Repositories;
using TaskManagementSystem.Core.Services;

namespace TaskManagement.Service
{
    public class QuoteService : IQuoteService
    {
        private readonly IQuoteRepository _quoteRepository;
        private readonly IMapper _mapper;
        public QuoteService(IQuoteRepository quoteRepository, IMapper mapper) 
            {
            this._quoteRepository = quoteRepository;
            this._mapper = mapper;
        }

        public async Task<QuoteDTO> AddQuoteAsync(QuoteCreationDTO quoteCreationDTO)
        {
            var quote = _mapper.Map<Quote>(quoteCreationDTO);
            var addedQuote =  await _quoteRepository.AddQuoteAsync(quote);
            var quoteDTO = _mapper.Map<QuoteDTO>(addedQuote);
            return quoteDTO;
            
        }

        public async Task<bool> DeleteQuoteAsync(int id)
        {
            return await _quoteRepository.DeleteQuoteAsync(id);
        }

        public async Task<IEnumerable<QuoteDTO>> GetAllQuotesAsync()
        {
            var quoteDTOs = new List<QuoteDTO>();
            var quotes = await _quoteRepository.GetAllQuotesAsync();
            foreach (var quote in quotes)
            {
                quoteDTOs.Add(_mapper.Map<QuoteDTO>(quote));
            }
            return quoteDTOs;
        }

        public async Task<QuoteDTO> GetQuoteByIdAsync(int id)
        {
            var quote = await _quoteRepository.GetQuoteByIdAsync(id);
            var quoteDTO = _mapper.Map<QuoteDTO>(quote);
            return quoteDTO;
        }

        public async Task<bool> UpdateQuoteAsync(QuoteDTO quoteDTO)
        {
            var quote = _mapper.Map<Quote>(quoteDTO);
            return await _quoteRepository.UpdateQuoteAsync(quote);
        }

        public async Task<IEnumerable<Quote>> SearchQuotesAsync(string term)
        {
            return await _quoteRepository.SearchQuotesAsync(term);
        }
    }
}
