using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Core.Models;

namespace TaskManagementSystem.Core.Repositories
{
    public interface IQuoteRepository
    {
        Task<IEnumerable<Quote>> GetAllQuotesAsync();
        Task<Quote> GetQuoteByIdAsync(int id);
        Task<Quote> AddQuoteAsync(Quote quote);
        Task<bool> UpdateQuoteAsync(Quote quote);
        Task<bool> DeleteQuoteAsync(int id);
        Task<IEnumerable<Quote>> SearchQuotesAsync(string term);
    }
}
