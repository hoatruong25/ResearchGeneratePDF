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
                FontName = "Times New Roman",
                FontSize = 10,
                IsBold = false,
                IsItalic = false,
                Color = "black",
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
                FontName = "Times New Roman",
                FontSize = 6,
                IsBold = false,
                IsItalic = true,
                Color = "black",
            }
        };
    }
}