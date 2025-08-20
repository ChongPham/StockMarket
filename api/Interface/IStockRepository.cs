using api.DTOs.Stock;
using api.Helpers;
using api.Models;

namespace api.Interface
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync(QueryObject queryObject);
        Task<Stock?> GetByIdAsync(int id);
        Task<Stock?> GetBySymbolAsync(string symbol);
        Task<Stock> CreateAsync(Stock stock);
        Task<Stock?> UpdateAsync(int id, UpdateStockResquestDto stock);
        Task<Stock?> DeleteAsync(int id);
        Task<bool> StockExist(int stockId);
    }
}