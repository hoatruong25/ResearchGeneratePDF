namespace TestPDFSharp.Models;

public class BlockContent
{
    public int Page { get; set; } = 1;
    public string Text { get; set; } = string.Empty;
    public double X { get; set; }
    public double Y { get; set; }
    public double Width { get; set; }
    public double Height { get; set; }
    public string FontName { get; set; } = "Times New Roman";
    public double FontSize { get; set; } = 12;
    public bool IsBold { get; set; } = false;
    public bool IsItalic { get; set; } = false;
    public string Color { get; set; } = "black";
}