using Games.Card.TexasHoldEm.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Syslib.Games.Card;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TexasQuery.Models;

namespace TexasQuery.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly ITexasDb db;

		public HomeController(ILogger<HomeController> logger, ITexasDb db)
		{
			_logger = logger;
			this.db = db;
		}

		public IActionResult Index()
		{
			return View();
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
