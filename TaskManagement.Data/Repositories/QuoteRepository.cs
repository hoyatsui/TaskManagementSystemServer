using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Core.Models;
using TaskManagementSystem.Core.Repositories;

namespace TaskManagement.Data.Repositories
{
    public class QuoteRepository : IQuoteRepository
    {
        private readonly TaskManagementDbContext _context;
        public QuoteRepository(TaskManagementDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Quote>> GetAllQuotesAsync()
        {
            return await _context.Quotes.ToListAsync();
        }

        public async Task<Quote> GetQuoteByIdAsync(int id)
        {
            return await _context.Quotes.FindAsync(id);
        }

        public async Task<Quote> AddQuoteAsync(Quote quote)
        {
            await _context.Quotes.AddAsync(quote);
            await _context.SaveChangesAsync();
            return quote;
        }

        public async Task<bool> UpdateQuoteAsync(Quote quote)
        {
            _context.Quotes.Update(quote);
            int rows = await _context.SaveChangesAsync();
            return rows > 0;
        }

        public async Task<bool> DeleteQuoteAsync(int id)
        {
            Quote quote = await GetQuoteByIdAsync(id);
            if (quote == null) return false;
            _context.Quotes.Remove(quote);
            int rows = await _context.SaveChangesAsync();
            return rows > 0;
        }

        public async Task<IEnumerable<Quote>> SearchQuotesAsync(string term)
        {
            return await _context.Quotes.Where(q => q.Description.Contains(term)).ToListAsync();
        }
    }
}
