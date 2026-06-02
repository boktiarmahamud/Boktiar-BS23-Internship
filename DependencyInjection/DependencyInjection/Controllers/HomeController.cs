using DependencyInjection.Models;
using DependencyInjection.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text;

namespace DependencyInjection.Controllers
{
    public class HomeController : Controller
    {
        private readonly IsingletonGuideService _singleton1;
        private readonly IsingletonGuideService _singleton2;

        private readonly IScopedGuideService _scoped1;
        private readonly IScopedGuideService _scoped2;

        private readonly ITransientGuideService _transient1;
        private readonly ITransientGuideService _transient2;

        public HomeController(IScopedGuideService scoped1, IScopedGuideService scoped2,
            ITransientGuideService transient1, ITransientGuideService transient2,
            IsingletonGuideService singleton1, IsingletonGuideService singleton2)
        {
            _singleton1 = singleton1;
            _singleton2 = singleton2;
            _scoped1 = scoped1;
            _scoped2 = scoped2;
            _transient1 = transient1;
            _transient2 = transient2;
        }
        public IActionResult Index()
        {
           StringBuilder message = new StringBuilder();
            message.AppendLine($"Singleton 1: {_singleton1.GetGuid()}");
            message.AppendLine($"Singleton 2: {_singleton2.GetGuid()}");
            _ = message.AppendLine($"Scoped 1: {_scoped1.GetGuid()}");
            message.AppendLine($"Scoped 2: {_scoped2.GetGuid()}");
            message.AppendLine($"Transient 1: {_transient1.GetGuid()}");
            message.AppendLine($"Transient 2: {_transient2.GetGuid()}");
            return Ok(message.ToString());
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
