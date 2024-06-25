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
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, EmailServiceNet emailService, PdfService pdfService, IConfiguration configuration)
        {
            _configuration = configuration;
            _pdfService = pdfService;
            _emailService = emailService;
            _logger = logger;
        }


        private string SenderEmail => _configuration["EmailSettings:SenderEmail"];
        private string SenderEmailPassword => _configuration["EmailSettings:SenderEmailPassword"];
        private string ReportEmail => _configuration["EmailSettings:ReportEmail"];

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Save(ProofOfUseModel model)
        {

            byte[] pdfBytes = _pdfService.GenerateRegistrationPDFContent(model);

            _emailService.SendMail(SenderEmail, SenderEmailPassword, ReportEmail,pdfBytes);

            return RedirectToAction("SuccessPageIndex");
        }

        public IActionResult SuccessPageIndex()
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
