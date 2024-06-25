using iText.Kernel.Colors;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using MyFirstJobProject.Models;

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

        private void AddRegistrationDetailsToPDF(Document document, ProofOfUseModel model)
        {
            string miscellaneous = !string.IsNullOrEmpty(model.Miscellaneous) ? $"{model.Miscellaneous}" : "";

            document.Add(new Paragraph("Customer Details")
                .SetFontSize(18)
                .SetBold()
                .SetTextAlignment(TextAlignment.CENTER)
                .SetMarginBottom(20));

            Table detailsTable = new Table(new float[] { 1, 2 }).UseAllAvailableWidth();
            detailsTable.SetMarginBottom(20);
            detailsTable.SetHorizontalAlignment(HorizontalAlignment.CENTER);

            AddDetailRow(detailsTable, "Contact Person:", model.ContactPerson);
            AddDetailRow(detailsTable, "Location:", model.Location);
            AddDetailRow(detailsTable, "File Number/Name of person:", model.FileNumber);
            AddDetailRow(detailsTable, "Language:", model.Language);
            AddDetailRow(detailsTable, "Date:", model.Date);
            AddDetailRow(detailsTable, "Arrival:", model.Arrival);
            AddDetailRow(detailsTable, "Start:", model.Start);
            AddDetailRow(detailsTable, "End:", model.End);
            AddDetailRow(detailsTable, "Departure:", model.Departure);

            if (!string.IsNullOrEmpty(miscellaneous))
            {
                AddDetailRow(detailsTable, "Miscellaneous:", miscellaneous);
            }

            AddDetailRow(detailsTable, "Signature:", model.Signature, true);
            AddDetailRow(detailsTable, "Satisfaction:", model.Satisfaction);

            document.Add(detailsTable);
        }

        private void AddDetailRow(Table table, string label, object value, bool highlightSignature = false)
        {
            Color lightBlue = new DeviceRgb(173, 216, 230); // Light blue for label cells
            Color lightYellow = new DeviceRgb(255, 255, 224); // Light yellow for value cells
            Color highlightColor = new DeviceRgb(255, 239, 213); // Light peach for signature highlight

            Cell labelCell = new Cell().Add(new Paragraph(label).SetFontSize(12).SetBold())
                                      .SetBackgroundColor(lightBlue)
                                      .SetBorder(new SolidBorder(ColorConstants.BLACK, 1))
                                      .SetPadding(5);

            Cell valueCell = new Cell().Add(new Paragraph(value.ToString()).SetFontSize(12))
                                      .SetBackgroundColor(lightYellow)
                                      .SetBorder(new SolidBorder(ColorConstants.BLACK, 1))
                                      .SetPadding(5);

            if (highlightSignature)
            {
                labelCell.SetFontSize(14)
                         .SetBackgroundColor(lightBlue)
                         .SetPadding(10);
                valueCell.SetBackgroundColor(highlightColor)
                         .SetPadding(10);
            }

            table.AddCell(labelCell);
            table.AddCell(valueCell);
        }
    }
}