using MigraDocCore.DocumentObjectModel;
using MigraDocCore.Rendering;

namespace TestPDFSharp;

public class PdfContent
{
    public double X { get; set; }
    public double Y { get; set; }
    public string FontName { get; set; } = "Arial";
    public double FontSize { get; set; } = 12;
    public bool IsBold { get; set; } = false;
    public bool IsItalic { get; set; } = false;
    public string Color { get; set; } = "black";
    public string Text { get; set; } = string.Empty;
}

public static class GeneratePDFService
{
    public static byte[] GeneratePDFFromInput(List<PdfContent> contents)
    {
        // Kích thước A4: 595 x 842 point
        double pageWidth = 595;
        double pageHeight = 842;
        return GeneratePDFFromInput(contents, pageWidth, pageHeight);
    }

    public static byte[] GeneratePDFFromInput(List<PdfContent> contents, double pageWidth, double pageHeight)
    {
        // Tạo document MigraDoc
        var document = new Document();
            
        // Tạo section
        var section = document.AddSection();
        section.PageSetup.PageWidth = Unit.FromPoint(pageWidth);
        section.PageSetup.PageHeight = Unit.FromPoint(pageHeight);

        // Thêm nội dung vào document
        foreach (var content in contents)
        {
            var paragraph = section.AddParagraph();
            paragraph.Format.LeftIndent = Unit.FromPoint(content.X);
            paragraph.Format.SpaceBefore = Unit.FromPoint(content.Y);
                
            // Định dạng font
            paragraph.Format.Font.Name = content.FontName;
            paragraph.Format.Font.Size = Unit.FromPoint(content.FontSize);
            paragraph.Format.Font.Bold = content.IsBold;
            paragraph.Format.Font.Italic = content.IsItalic;
            paragraph.Format.Font.Color = ParseColor(content.Color);

            // Thêm nội dung text
            paragraph.AddText(content.Text);
        }

        // Render document sang PDF
        var renderer = new PdfDocumentRenderer(true)
        {
            Document = document
        };
        renderer.RenderDocument();

        // Lưu PDF vào MemoryStream
        using var memoryStream = new MemoryStream();
        renderer.PdfDocument.Save(memoryStream);
        return memoryStream.ToArray();
    }
    
    private static Color ParseColor(string color)
    {
        return color.ToLower() switch
        {
            "black" => Colors.Black,
            "red" => Colors.Red,
            "blue" => Colors.Blue,
            "green" => Colors.Green,
            _ => Colors.Black // Default color
        };
    }
}