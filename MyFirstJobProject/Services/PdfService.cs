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

        public void AddRegistrationDetailsToPDF(Document document, ProofOfUseModel model)
        {
            string miscellanous = !string.IsNullOrEmpty(model.Miscellaneous) ? $"{model.Miscellaneous}" : "";

            document.Add(new Paragraph("Customer Details")
                .SetFontSize(16)
                .SetBold()
                .SetTextAlignment(TextAlignment.CENTER)
                .SetMarginBottom(20));

            Table detailsTable = new Table(new float[] { 1, 2 }).UseAllAvailableWidth();
            detailsTable.SetMarginBottom(20);

            AddDetailRow(detailsTable, "Contact Person:", model.ContactPerson);
            AddDetailRow(detailsTable, "Location:", model.Location);
            AddDetailRow(detailsTable, "File Number/Name of person:", model.FileNumber);
            AddDetailRow(detailsTable, "Language:", model.Language);
            AddDetailRow(detailsTable, "Date:", model.Date);
            AddDetailRow(detailsTable, "Arrival:", model.Arrival);
            AddDetailRow(detailsTable, "Start:", model.Start);
            AddDetailRow(detailsTable, "End:", model.End);
            AddDetailRow(detailsTable, "Departure:", model.Departure);

            if (!string.IsNullOrEmpty(miscellanous))
            {
                AddDetailRow(detailsTable, "Miscellanous", miscellanous);
            }

            AddDetailRow(detailsTable, "Signature:", model.Signature, true);
            AddDetailRow(detailsTable, "Satisfaction:", model.Satisfaction);

            document.Add(detailsTable);
        }

        private void AddDetailRow(Table table, string label, object value, bool highlightSignature = false)
        {
            Cell labelCell = new Cell().Add(new Paragraph(label));
            Cell valueCell = new Cell().Add(new Paragraph(value.ToString()));

            if (highlightSignature)
            {
                labelCell.SetFontSize(14) 
                         .SetBold()
                         .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
                         .SetBorder(Border.NO_BORDER)
                         .SetPadding(5);
                valueCell.SetBackgroundColor(ColorConstants.YELLOW) // Highlight signature cell
                         .SetBorder(Border.NO_BORDER)
                         .SetPadding(5);
            }
            else
            {
                labelCell.SetBold()
                         .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
                         .SetBorder(Border.NO_BORDER)
                         .SetPadding(5);
                valueCell.SetBorder(Border.NO_BORDER)
                         .SetPadding(5);
            }

            table.AddCell(labelCell);
            table.AddCell(valueCell);
        }
    }
}