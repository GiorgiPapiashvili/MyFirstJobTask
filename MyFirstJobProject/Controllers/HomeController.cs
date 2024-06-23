using Microsoft.AspNetCore.Mvc;
using MyFirstJobProject.Models;
using System.Diagnostics;
using MyFirstJobProject.Services;

namespace MyFirstJobProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly EmailServiceNet _emailService;
        private readonly PdfService _pdfService;

        public HomeController(ILogger<HomeController> logger, EmailServiceNet emailService, PdfService pdfService)
        {
            _pdfService = pdfService;
            _emailService = emailService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Save(ProofOfUseModel model)
        {

            byte[] pdfBytes = _pdfService.GenerateRegistrationPDFContent(model);

            _emailService.SendMail(pdfBytes);

            return Ok("Email Sent!");
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
