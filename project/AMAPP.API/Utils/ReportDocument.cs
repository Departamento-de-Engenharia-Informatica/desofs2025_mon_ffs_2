using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using AMAPP.API.DTOs;  // Import the DTO for report parameters

namespace AMAPP.API.Utils
{
    public class ReportDocument : IDocument
    {
        private readonly ReportParametersDto _parameters;

        public ReportDocument(ReportParametersDto parameters)
        {
            _parameters = parameters;
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

        public void Compose(IDocumentContainer container)
        {
            var title = _parameters.Title;
            var date = _parameters.Date;
            var software = _parameters.Software;

            // Title Page
            container.Page(page =>
            {
                page.Margin(50);
                page.Content().Column(col =>
                {
                    col.Item().AlignCenter().Text(title).FontSize(28).Bold();
                    col.Item().Height(20);
                    col.Item().AlignCenter().Text(date.ToString("yyyy-MM-dd")).FontSize(14);
                    col.Item().Height(10);
                    col.Item().AlignCenter().Text(software).FontSize(12);
                });
            });

            // Reservations Page
            container.Page(page =>
            {
                page.Margin(50);
                // Single Header call with both title and date
                page.Header().Column(col =>
                {
                    col.Item().AlignCenter().Text("Reservations").FontSize(20).Bold();
                    col.Item().AlignCenter().Text(date.ToString("yyyy-MM-dd")).FontSize(12);
                });
                page.Content().PaddingTop(20).Column(col =>
                {
                    col.Item().Text("<< Table will go here >>").FontSize(10).Italic();
                });
            });

            // Deliveries Page
            container.Page(page =>
            {
                page.Margin(50);
                page.Header().Column(col =>
                {
                    col.Item().AlignCenter().Text("Deliveries").FontSize(20).Bold();
                    col.Item().AlignCenter().Text(date.ToString("yyyy-MM-dd")).FontSize(12);
                });
                page.Content().PaddingTop(20).Column(col =>
                {
                    col.Item().Text("<< Table will go here >>").FontSize(10).Italic();
                });
            });
        }
    }
}