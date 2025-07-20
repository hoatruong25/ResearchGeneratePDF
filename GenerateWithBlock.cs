using MigraDocCore.DocumentObjectModel;
using PdfSharpCore.Drawing;
using PdfSharpCore.Drawing.Layout;
using PdfSharpCore.Drawing.Layout.enums;
using TestPDFSharp.Models;
using TestPDFSharp.Utility;

namespace TestPDFSharp;

public static class GenerateWithBlock
{
    public static void GenerateMigraDocCore(string filename, string projectRoot, List<BlockContent> contents)
    {
        var document = new Document();
        var section = document.AddSection();
        section.PageSetup.PageFormat = PageFormat.A4;
        // section.PageSetup.TopMargin = Unit.FromCentimeter(1.5);
        // section.PageSetup.LeftMargin = Unit.FromCentimeter(2);
        // section.PageSetup.RightMargin = Unit.FromCentimeter(2);
        
        foreach (var content in contents)
        {
            var frame = section.AddTextFrame();
            frame.Left = Unit.FromPoint(content.X);
            frame.Top = Unit.FromPoint(content.Y);
            frame.Width = Unit.FromPoint(content.Width);
            frame.Height = Unit.FromPoint(content.Height);

            var paragraph = frame.AddParagraph();
            paragraph.Format.Font.Name = content.FontName;
            paragraph.Format.Font.Size = Unit.FromPoint(content.FontSize);
            paragraph.Format.Font.Bold = content.IsBold;
            paragraph.Format.Font.Italic = content.IsItalic;
            paragraph.Format.Font.Color = ParseColor(content.Color);
            
            paragraph.AddText(content.Text);
        }
        
        ExportDocument.ExportMigraDocCore(filename, projectRoot, document);
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

    public static void GeneratePdfSharpCore(string filename, string projectRoot, List<BlockContent> contents)
    {
        var pdf = new PdfSharpCore.Pdf.PdfDocument();
        var countPage = contents.Max(x => x.Page);
        for (var i = 1; i <= countPage; i++)
        {
            var contentInPage = contents.Where(x => x.Page == i).ToList();
            var page = pdf.AddPage();
            page.Size = PdfSharpCore.PageSize.A4;
            var gfx = XGraphics.FromPdfPage(page);
        
            var tf = new XTextFormatter(gfx);
            foreach (var content in contentInPage)
            {
                var fontStyle = XFontStyle.Regular;
                if (content.IsBold && content.IsItalic)
                    fontStyle = XFontStyle.BoldItalic;
                else if (content.IsBold)
                    fontStyle = XFontStyle.Bold;
                else if (content.IsItalic)
                    fontStyle = XFontStyle.Italic;
            
                var font = new XFont(content.FontName, content.FontSize, fontStyle);
            
                var brush = ParseXBrush(content.Color);
            
                var rect = new XRect(content.X, content.Y, content.Width, content.Height);

                var alignment = new TextFormatAlignment
                {
                    Horizontal = XParagraphAlignment.Left,
                    Vertical = XVerticalAlignment.Top
                };
                tf.DrawString(content.Text, font, brush, rect, alignment);
            }
        }
        
        
        

        ExportDocument.ExportPdfSharpCore(filename, projectRoot, pdf);
    }

    private static XBrush ParseXBrush(string color)
    {
        switch (color.ToLower())
        {
            case "black": return XBrushes.Black;
            case "red": return XBrushes.Red;
            case "blue": return XBrushes.Blue;
            case "green": return XBrushes.Green;
            default: return XBrushes.Black;
        }
    }
}