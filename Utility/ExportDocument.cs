using MigraDocCore.DocumentObjectModel;
using MigraDocCore.Rendering;
using PdfSharpCore.Pdf;

namespace TestPDFSharp.Utility;

public static class ExportDocument
{
    public static void ExportMigraDocCore(string filename, string projectRoot, Document doc)
    {
        // Đảm bảo thư mục Reports tồn tại trong projectRoot
        string reportsDir = Path.Combine(projectRoot, "Reports");
        if (!Directory.Exists(reportsDir))
            Directory.CreateDirectory(reportsDir);

        // Tách tên file và phần mở rộng
        string baseName = Path.GetFileNameWithoutExtension(filename);
        string ext = Path.GetExtension(filename);
        string finalPath = Path.Combine(reportsDir, baseName + ext);
        int count = 1;
        while (File.Exists(finalPath))
        {
            finalPath = Path.Combine(reportsDir, $"{baseName}({count}){ext}");
            count++;
        }
        
        // Render PDF
        PdfDocumentRenderer renderer = new PdfDocumentRenderer(true);
        renderer.Document = doc;
        renderer.RenderDocument();
        renderer.PdfDocument.Save(finalPath);
    }
    
    public static void ExportPdfSharpCore(string filename, string projectRoot, PdfDocument pdf)
    {
        // Đảm bảo thư mục Reports tồn tại trong projectRoot
        string reportsDir = Path.Combine(projectRoot, "Reports");
        if (!Directory.Exists(reportsDir))
            Directory.CreateDirectory(reportsDir);

        // Tách tên file và phần mở rộng
        string baseName = Path.GetFileNameWithoutExtension(filename);
        string ext = Path.GetExtension(filename);
        string finalPath = Path.Combine(reportsDir, baseName + ext);
        int count = 1;
        while (File.Exists(finalPath))
        {
            finalPath = Path.Combine(reportsDir, $"{baseName}({count}){ext}");
            count++;
        }
        
        
        pdf.Save(finalPath);
    }
}