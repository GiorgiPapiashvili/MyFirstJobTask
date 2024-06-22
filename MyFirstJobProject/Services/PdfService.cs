using iText.Kernel.Pdf;
using iText.Layout.Element;
using MyFirstJobProject.Models;
using iText.Layout;
using iText.Layout.Properties;
using iText.Kernel.Colors;
using static iText.Kernel.Pdf.Colorspace.PdfPattern;

namespace MyFirstJobProject.Services
{
    public class PdfService
    {
        public byte[] GenerateRegistrationPDFContent(ProofOfUseModel model)
        {
            using (var memoryStream = new MemoryStream())
            {
                var writer = new PdfWriter(memoryStream);
                var pdfDocument = new PdfDocument(writer);
                var document = new Document(pdfDocument);

                AddRegistrationDetailsToPDF(document, model);

                document.Close();
                return memoryStream.ToArray();
            }
        }

        public void AddRegistrationDetailsToPDF(Document document, ProofOfUseModel model)
        {
            string miscellanous = !string.IsNullOrEmpty(model.Miscellaneous) ? $"Miscellaneous: {model.Miscellaneous}" : "";



            //document.Add(new Paragraph("Name:")
            //  .SetFontSize(12)
            //  .SetBold());
            //document.Add(new Paragraph(model.Name)
            //            .SetFontSize(12));

            //document.Add(new Paragraph("Contact Person: ").SetFontSize(12).SetBold());
            //document.Add(new Paragraph(model.ContactPerson)
            //            .SetFontSize(12));


            document.Add(new Paragraph("Customer Details").SetFontSize(16)
               .SetBold());
            document.Add(new Paragraph("Contact Person: " + model.ContactPerson));
            document.Add(new Paragraph("Location: " + model.Location));
            document.Add(new Paragraph("File Number/Name of person: " + model.FileNumber));
            document.Add(new Paragraph("Language: " + model.Language));
            document.Add(new Paragraph("Date: " + model.Date));
            document.Add(new Paragraph("Arrival: " + model.Arrival));
            document.Add(new Paragraph("Start: " + model.Start));
            document.Add(new Paragraph("End: " + model.End));
            document.Add(new Paragraph("Departure: " + model.Departure));
            if (miscellanous != "")
            {
                document.Add(new Paragraph(miscellanous));
            }
            document.Add(new Paragraph("Email: " + model.Location));
            document.Add(new Paragraph("Signature: " + model.Signature));
            document.Add(new Paragraph("Satisfiction: " + model.Satisfaction));


            //private void AddRegistrationDetailsToPDF(Document document, RegisterFormModel model)
            //{
            //    document.Add(new Heading("Registration Details")
            //                .SetFontSize(16)
            //                .SetBold());

            //    document.Add(new Paragraph("Name:")
            //                .SetFontSize(12)
            //                .SetBold());
            //    document.Add(new Paragraph(model.Name)
            //                .SetFontSize(12));

            //    document.Add(new Paragraph("Email:")
            //                .SetFontSize(12)
            //                .SetBold());
            //    document.Add(new Paragraph(model.Email)
            //                .SetFontSize(12));
            //}
        }
    }
}