using TestPDFSharp.Models;

namespace TestPDFSharp;

class Program
{
    static void Main(string[] args)
    {
        // Lấy đường dẫn gốc project (lên 3 cấp từ thư mục thực thi)
        string projectRoot = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", ".."));

        var blocks = GenerateBlockContent();
        GenerateWithBlock.GeneratePdfSharpCore("TestBlock.pdf", projectRoot, blocks);
                
        Console.WriteLine("File PDF đã được tạo trong thư mục Reports của project.");
    }

    private static List<BlockContent> GenerateBlockContent()
    {
        const int width = 200;
        const int xLeft = 30;
        const string fontName = "Times New Roman";
        const string color = "black";
        
        return new List<BlockContent>
        {
            new BlockContent
            {
                Page = 1,
                Text = "THOẢ THUẬN CAM KẾT ĐẦU RA\n" +
                       "Số: ADV... - ZIM/TTCKĐR",
                X = xLeft,
                Y = 30,
                Width = width,
                Height = 50,
                FontName = fontName,
                FontSize = 10,
                IsBold = true,
                IsItalic = false,
                Color = color,
            },
            new BlockContent
            {
                Page = 1,
                Text = "Căn cứ Bộ luật Dân sự số 91/2015/QH13 ngày 24/11/2015 và các văn bản pháp luật liên quan;\n" +
                       "Căn cứ nhu cầu và khả năng của các bên;",
                X = xLeft,
                Y = 60,
                Width = width,
                Height = 50,
                FontName = fontName,
                FontSize = 7,
                IsBold = false,
                IsItalic = true,
                Color = color,
            },
            new BlockContent
            {
                Page = 1,
                Text = "Hôm nay ngày .. tháng ... năm …. tại Trung tâm Anh ngữ ZIM, số \r\nnhà …………….., Phường ….……., Quận ……..….., Thành phố \r\n……..….",
                X = xLeft,
                Y = 90,
                Width = width,
                Height = 50,
                FontName = fontName,
                FontSize = 7,
                IsBold = false,
                IsItalic = true,
                Color = color,
            },
        };
    }
}