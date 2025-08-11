using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOs.Stock;
using api.Interface;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class Stockrepository : IStockRepository
    {
        private readonly ApplicationDBContext _applicationDBContext;

        public Stockrepository(ApplicationDBContext applicationDBContext)
        {
            _applicationDBContext = applicationDBContext;
        }

        public async Task<List<Stock>> GetAllAsync()
        {
            return await _applicationDBContext.Stocks.Include(c => c.Comments).ToListAsync();
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            return await _applicationDBContext.Stocks.Include(c => c.Comments).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Stock> CreateAsync(Stock stock)
        {
            await _applicationDBContext.Stocks.AddAsync(stock);
            await _applicationDBContext.SaveChangesAsync();

            return stock;
        }

        public async Task<Stock?> UpdateAsync(int id, UpdateStockResquestDto stock)
        {
            var stockModel = await _applicationDBContext.Stocks.FirstOrDefaultAsync(x => x.Id == id);
            if (stockModel == null)
            {
                return null;
            }
            stockModel.Symbol = stock.Symbol;
            stockModel.Company = stock.Company;
            stockModel.Purchase = stock.Purchase;
            stockModel.LastDiv = stock.LastDiv;
            stockModel.Industry = stock.Industry;
            stockModel.MarketCap = stock.MarketCap;

            await _applicationDBContext.SaveChangesAsync();
            return stockModel;
        }

        public async Task<Stock?> DeleteAsync(int id)
        {
            var stockModel = await _applicationDBContext.Stocks.FirstOrDefaultAsync(x => x.Id == id);
            if (stockModel == null)
            {
                return null;
            }

            _applicationDBContext.Remove(stockModel);
            await _applicationDBContext.SaveChangesAsync();
            return stockModel;
        }

        public async Task<bool> StockExist(int stockId)
        {
            return await _applicationDBContext.Stocks.AnyAsync(x => x.Id == stockId);
        }
    }
}