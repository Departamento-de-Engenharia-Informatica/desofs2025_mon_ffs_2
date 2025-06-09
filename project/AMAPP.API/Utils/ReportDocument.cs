using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using AMAPP.API.DTOs;
using AMAPP.API.DTOs.Reservation;
using AMAPP.API.DTOs.Delivery;
using Microsoft.Extensions.Logging;

namespace AMAPP.API.Utils
{
    public class ReportDocument : IDocument
    {
        private readonly ReportParametersDto _parameters;
        private readonly ILogger<ReportDocument> _logger;

        public ReportDocument(
            ReportParametersDto parameters,
            ILogger<ReportDocument> logger)
        {
            _parameters = parameters;
            _logger     = logger;
        }

        public DocumentMetadata GetMetadata() =>
            DocumentMetadata.Default;

        public void Compose(IDocumentContainer container)
        {
            try
            {
                ComposeTitlePage(container);
                ComposeReservationsPage(container);
                ComposeDeliveriesPage(container);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to generate PDF report");
                throw new ReportGenerationException(
                    "An error occurred while generating the report.", ex);
            }
        }

        void ComposeTitlePage(IDocumentContainer container)
        {
            var title    = _parameters.Title;
            var dateText = _parameters.Date.ToString("yyyy-MM-dd");
            var software = _parameters.Software;
            var username    = _parameters.Username;

            container.Page(page =>
            {
                page.Margin(50);
                page.Content().Column(col =>
                {
                    col.Item().AlignCenter().Text(title).FontSize(28).Bold(); 
                    
                    if (!string.IsNullOrWhiteSpace(username))
                    {
                        col.Item().Height(8);
                        col.Item().AlignCenter()
                                 .Text($"CoProducer: {username}")
                                 .FontSize(14)
                                 .SemiBold();
                    }
                    
                    col.Item().Height(20);
                    col.Item().AlignCenter().Text(dateText).FontSize(14);
                    col.Item().Height(10);
                    col.Item().AlignCenter().Text(software).FontSize(12);
                });
                page.Footer().AlignCenter().Text(txt =>
                {
                    txt.Span("Page ");
                    txt.CurrentPageNumber();
                    txt.Span(" of ");
                    txt.TotalPages();
                });
            });
        }

        void ComposeReservationsPage(IDocumentContainer container)
        {
            var dateText = _parameters.Date.ToString("yyyy-MM-dd");
            container.Page(page =>
            {
                page.Margin(50);
                page.Header().Column(col =>
                {
                    col.Item().AlignCenter().Text("Reservations").FontSize(20).Bold();
                    col.Item().AlignCenter().Text(dateText).FontSize(12);
                });
                page.Content().PaddingTop(20).Element(ctx =>
                {
                    try
                    {
                        ComposeReservationsTable(ctx);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error rendering reservations table");
                        ctx.Text("<< Error rendering reservations >>")
                           .Italic().FontSize(10).AlignCenter();
                    }
                });
                page.Footer().AlignCenter().Text(txt =>
                {
                    txt.Span("Page ");
                    txt.CurrentPageNumber();
                    txt.Span(" of ");
                    txt.TotalPages();
                });
            });
        }

        void ComposeDeliveriesPage(IDocumentContainer container)
        {
            var dateText = _parameters.Date.ToString("yyyy-MM-dd");
            container.Page(page =>
            {
                page.Margin(50);
                page.Header().Column(col =>
                {
                    col.Item().AlignCenter().Text("Deliveries").FontSize(20).Bold();
                    col.Item().AlignCenter().Text(dateText).FontSize(12);
                });
                page.Content().PaddingTop(20).Element(ctx =>
                {
                    try
                    {
                        ComposeDeliveriesTable(ctx);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error rendering deliveries table");
                        ctx.Text("<< Error rendering deliveries >>")
                           .Italic().FontSize(10).AlignCenter();
                    }
                });
                page.Footer().AlignCenter().Text(txt =>
                {
                    txt.Span("Page ");
                    txt.CurrentPageNumber();
                    txt.Span(" of ");
                    txt.TotalPages();
                });
            });
        }

        private void ComposeReservationsTable(IContainer container)
        {
            var data = _parameters.Reservations ?? new List<ReservationDto>();

            container.Table(table =>
            {
                table.ColumnsDefinition(cols =>
                {
                    cols.ConstantColumn(50);
                    cols.RelativeColumn(2);
                    cols.RelativeColumn(2);
                    cols.RelativeColumn(3);
                    cols.RelativeColumn(5);
                });

                table.Header(header =>
                {
                    header.Cell().Text("Order").Bold();
                    header.Cell().Text("Method").Bold();
                    header.Cell().Text("Date").Bold();
                    header.Cell().Text("Location").Bold();
                    header.Cell().Text("Notes").Bold();
                });

                if (data.Any())
                {
                    foreach (var r in data)
                    {
                        table.Cell().Text(r.OrderId.ToString());
                        table.Cell().Text(r.Method.ToString());
                        table.Cell().Text(r.ReservationDate.ToString("yyyy-MM-dd"));
                        table.Cell().Text(r.Location);
                        table.Cell().Text(r.Notes);
                    }
                }
                else
                {
                    table.Cell().ColumnSpan(5)
                         .Text("No reservations found.")
                         .Italic().AlignCenter();
                }
            });
        }

        private void ComposeDeliveriesTable(IContainer container)
        {
            var data = _parameters.Deliveries ?? new List<DeliveryDto>();

            container.Table(table =>
            {
                table.ColumnsDefinition(cols =>
                {
                    cols.ConstantColumn(50);
                    cols.RelativeColumn(2);
                    cols.RelativeColumn(3);
                    cols.RelativeColumn(2);
                });

                table.Header(header =>
                {
                    header.Cell().Text("Order").Bold();
                    header.Cell().Text("Date").Bold();
                    header.Cell().Text("Location").Bold();
                    header.Cell().Text("Status").Bold();
                });

                if (data.Any())
                {
                    foreach (var d in data)
                    {
                        table.Cell().Text(d.OrderId.ToString());
                        table.Cell().Text(d.DeliveryDate.ToString("yyyy-MM-dd"));
                        table.Cell().Text(d.DeliveryLocation);
                        table.Cell().Text(d.Status.ToString());
                    }
                }
                else
                {
                    table.Cell().ColumnSpan(4)
                         .Text("No deliveries found.")
                         .Italic().AlignCenter();
                }
            });
        }
    }
}