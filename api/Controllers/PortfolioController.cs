using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Extensions;
using api.Interface;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/portfolio")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IStockRepository _stockRepository;
        private readonly IPortfolioRepository _porfolioRepository;
        public PortfolioController(UserManager<AppUser> userManager, IStockRepository tockRepository, IPortfolioRepository porfolioRepository)
        {
            _userManager = userManager;
            _stockRepository = tockRepository;
            _porfolioRepository = porfolioRepository;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserPortfolio()
        {
            var username = User.GetUsername();
            if (username != null)
            {
                Console.WriteLine($"Hello {username}");
            }
            else
            {
                return NotFound("Not Found claim GivenName");
            }

            var appUSer = await _userManager.FindByNameAsync(username);
            var userPortfolio = await _porfolioRepository.GetUserPortfolio(appUSer);
            return Ok(userPortfolio);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddPortfolio(string symbol)
        {
            var username = User.GetUsername();
            var appUSer = await _userManager.FindByNameAsync(username);
            var stock = await _stockRepository.GetBySymbolAsync(symbol);

            if (stock == null)
                return BadRequest("Stock not found");

            var userPortfolio = await _porfolioRepository.GetUserPortfolio(appUSer);
            if (userPortfolio.Any(e => e.Symbol.ToLower() == symbol.ToLower()))
                return BadRequest("Can not add same portfolio");

            var portfolioModel = new Portfolio
            {
                StockId = stock.Id,
                AppUserId = appUSer.Id,
            };

            await _porfolioRepository.CreateAsync(portfolioModel);

            if (portfolioModel == null)
            {
                return StatusCode(500, "Could not create");
            }
            else
            {
                return Created();
            }
        }
    }
}