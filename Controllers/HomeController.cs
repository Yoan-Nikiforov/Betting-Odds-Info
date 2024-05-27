using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UltraPlayTask.Data.DbHandlers;
using UltraPlayTask.Models;
using UltraPlayTask.ViewModels;

namespace UltraPlayTask.Controllers
{
    public class HomeController : Controller
    {
        private readonly DbHandler dbHandler;

        public HomeController(DbHandler dbHandler)
        {
            this.dbHandler = dbHandler;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var sportViewModel = dbHandler.GetSports().Select(x => new SportViewModel(x)).ToList();
            return View(sportViewModel);
        }

        [HttpGet]
        [Route("Home/Match/{matchId}")]
        public async Task<IActionResult> Match(string matchId)
        {
            var matchViewModel = new MatchViewModel(dbHandler.GetMatch(matchId));
            return View(matchViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
