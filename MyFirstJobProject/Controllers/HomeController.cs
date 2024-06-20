using Microsoft.AspNetCore.Mvc;
using MyFirstJobProject.Models;
using System.Diagnostics;
using iText.Html2pdf;
using iText.Kernel.Pdf;
using System.Net;
using System.Net.Mail;

namespace MyFirstJobProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //private readonly IWebHostEnvironment _webHostEnvironment;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment webHostEnviroment)
        {
            //_webHostEnvironment = webHostEnviroment;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Save(ProofOfUseModel model)
        {
            byte[] pdfBytes;

            using (MemoryStream ms = new MemoryStream())
            {
                using (var writer = new PdfWriter(ms))
                {
                    using (var pdf = new PdfDocument(writer))
                    {
                        HtmlConverter.ConvertToPdf(model.ContactPerson, pdf, converterProperties: new ConverterProperties());
                    }
                }
                pdfBytes = ms.ToArray();
            }

            SendPdfByEmail(pdfBytes);
  
            //File(pdfBytes, "application/pdf", "FormData.pdf")
            return Ok("Email Sent!");
        }

        private void SendPdfByEmail(byte[] pdfBytes)
        {
            // Set up the email message
            var fromAddress = new MailAddress("Giushki77@Gmail.Com", "Giorgi Papiashvili");
            var toAddress = new MailAddress("GiorgioPapiashvili77@Gmail.com", "Giorgi Papiashvili");
            const string fromPassword = "_Billion$";
            const string subject = "PDF Document";
            const string body = "Please find the attached PDF document.";

            //{
            //    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
            //                                      | SecurityProtocolType.Tls11
            //                                      | SecurityProtocolType.Tls12;
            //}

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)

            };

            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            }
            )
            {
               
                using (var stream = new MemoryStream(pdfBytes))
                {
                    stream.Position = 0;
                    var attachment = new Attachment(stream, "FormData.pdf", "application/pdf");
                    message.Attachments.Add(attachment);

                    smtp.Send(message);
                }
            }
        }


        //ViewToString
        //    private async Task<string> RenderViewToStringAsync(string viewName, object model)
        //    {
        //        var viewEngine = _webHostEnvironment.ContentRootFileProvider;
        //        var viewPath = $"Views/Pdf/{viewName}.cshtml";

        //        var view = viewEngine.GetFileInfo(viewPath);
        //        if (!view.Exists)
        //        {
        //            throw new FileNotFoundException($"View {viewName} not found at {viewPath}");
        //        }

        //        using var reader = new StreamReader(view.CreateReadStream());
        //        var viewContent = await reader.ReadToEndAsync();

        //        var viewResult = ViewEngines.Engines.FindView(ControllerContext, viewName, null);

        //        if (viewResult.View == null)
        //        {
        //            throw new FileNotFoundException($"View {viewName} not found.");
        //        }

        //        ViewData.Model = model;

        //        using var sw = new StringWriter();
        //        var viewContext = new ViewContext(
        //            ControllerContext,
        //            viewResult.View,
        //            ViewData,
        //            TempData,
        //            sw,
        //            new HtmlHelperOptions()
        //        );

        //        await viewResult.View.RenderAsync(viewContext);
        //        return sw.ToString();
        //    }
        //}


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