using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Core.DTOs;
using TaskManagementSystem.Core.Models;

namespace TaskManagementSystem.Core.Services
{
    public interface IQuoteService
    {
        Task<IEnumerable<QuoteDTO>> GetAllQuotesAsync();
        Task<QuoteDTO> GetQuoteByIdAsync(int id);
        Task<QuoteDTO> AddQuoteAsync(QuoteCreationDTO quote);
        Task<bool> UpdateQuoteAsync(QuoteDTO quote);
        Task<bool> DeleteQuoteAsync(int id);
        Task<IEnumerable<Quote>> SearchQuotesAsync(string term);
    }
}
