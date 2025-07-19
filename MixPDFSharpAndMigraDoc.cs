using MigraDocCore.DocumentObjectModel;
using MigraDocCore.Rendering;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;

namespace TestPDFSharp;

public class MixPDFSharpAndMigraDoc
{
    public static void GenerateReceiptPdf(ReceiptData data, string filename, string projectRoot)
    {
        // Tạo tài liệu MigraDoc
        Document document = new Document();
        Section section = document.AddSection();
        section.AddParagraph("Hóa đơn thanh toán", "Heading1");
        var table = section.AddTable();
        table.AddColumn("200pt");
        table.AddColumn("200pt");
        var row = table.AddRow();
        row.Cells[0].AddParagraph("Phí tín dụng:");
        row.Cells[1].AddParagraph("5%");
        row = table.AddRow();
        row.Cells[0].AddParagraph("Tổng thanh toán:");
        row.Cells[1].AddParagraph("1,050,000 VND");

        // Render tài liệu MigraDoc thành PDF
        PdfDocumentRenderer renderer = new PdfDocumentRenderer();
        renderer.Document = document;
        renderer.RenderDocument();
        PdfDocument pdf = renderer.PdfDocument;

        // Tùy chỉnh trên cùng trang bằng PDFSharpCore
        PdfPage page = pdf.Pages[0]; // Lấy trang đầu tiên
        using (XGraphics gfx = XGraphics.FromPdfPage(page))
        {
            XFont font = new XFont("Arial", 12, XFontStyle.Bold);
            gfx.DrawString("Watermark: Công ty XYZ", font, XBrushes.Red, 
                new XRect(100, 100, page.Width, page.Height), XStringFormats.TopLeft);
        }

        // ExportDocument.Export(filename, projectRoot, document);
        
        // Lưu tài liệu PDF
        pdf.Save("invoice.pdf");
        Console.WriteLine("PDF đã được tạo thành công!");
    }
}