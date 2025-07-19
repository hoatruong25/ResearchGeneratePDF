namespace TestPDFSharp;

class Program
{
    static void Main(string[] args)
    {
        // Lấy đường dẫn gốc project (lên 3 cấp từ thư mục thực thi)
        string projectRoot = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", ".."));

        var blocks = GenerateBlockContent();
        GenerateWithBlock.GeneratePDFSharpCore("TestBlock.pdf", projectRoot, blocks);
                
        Console.WriteLine("File PDF đã được tạo trong thư mục Reports của project.");
    }

    private static List<BlockContent> GenerateBlockContent()
    {
        return new List<BlockContent>
        {
            new BlockContent
            {
                Text = "THOẢ THUẬN CAM KẾT ĐẦU RA" +
                       "Số: ADV... - ZIM/TTCKĐR",
                X = 0,
                Y = 0,
                Width = 150,
                Height = 100,
                FontName = "Times New Roman",
                FontSize = 6,
                IsBold = true,
                IsItalic = true,
                Color = "black",
            },
            // new BlockContent
            // {
            //     Text = "3.2 Trường hợp chỉ có Bên B thay đổi lịch, Bên A sẽ đổi lớp tương đương  trình độ của Bên B.",
            //     X = 0,
            //     Y = 0,
            //     Width = 100,
            //     Height = 500,
            //     FontName = "Times New Roman",
            //     FontSize = 6,
            //     IsBold = true,
            //     IsItalic = true,
            //     Color = "black",
            // }
        };
    }
}