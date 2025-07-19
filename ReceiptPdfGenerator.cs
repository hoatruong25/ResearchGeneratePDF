using MigraDocCore.DocumentObjectModel;
using MigraDocCore.Rendering;

namespace TestPDFSharp;

public class ReceiptPdfGenerator
{
    public void GenerateReceiptPdf(ReceiptData data, string filename, string projectRoot)
    {
        // var finalPath = GetUniqueFilePath(filename, projectRoot);
        var doc = InitDocument();
        var section = InitSection(doc);
        
        // Tiêu đề
        var titlePara = section.AddParagraph($"Phiếu thu #{data.ReceiptNumber}");
        titlePara.Style = "CustomTitle";
        titlePara.Format.SpaceAfter = 2;

        // Company name
        var companyNamePara = section.AddParagraph(data.CompanyName);
        companyNamePara.Style = "CustomHeader";
        companyNamePara.Format.SpaceAfter = 2;

        // Thông tin công ty (dưới tên công ty)
        var pCompany = section.AddParagraph();
        pCompany.Style = "CustomBold";
        pCompany.Format.SpaceAfter = 6;
        var ft1 = pCompany.AddFormattedText("Công ty cổ phần Educator\n Nguyễn Văn Sang");
        
        // pCompany.Format.LeftIndent = Unit.FromCentimeter(1);
        // pCompany.Format.SpaceBefore = 6;
        
        
        
        
        
        pCompany.AddText($"MST: {data.CompanyTaxCode}\n");
        pCompany.AddText($"Địa chỉ: {data.CompanyAddress}\n");
        pCompany.AddText("Chi nhánh: ");
        var ft2 = pCompany.AddFormattedText(data.Branch);
        ft2.Style = "CustomBold";

        var test = doc.Styles["CustomBold"];
        // Không set style cho paragraph này để giữ được style cho từng đoạn nhỏ

        
        
        
        
        
        
        // Table 2 cột lớn: Trái (học viên), Phải (hóa đơn)
        var mainTable = section.AddTable();
        mainTable.Borders.Width = 0;
        mainTable.AddColumn(Unit.FromCentimeter(11)); // Trái 60%
        mainTable.AddColumn(Unit.FromCentimeter(7)); // Phải 40%
        var mainRow = mainTable.AddRow();
        var leftCell = mainRow.Cells[0];
        var rightCell = mainRow.Cells[1];

        // Table con bên trái (thông tin học viên)
        var leftTable = leftCell.Elements.AddTable();
        leftTable.Borders.Width = 0;
        leftTable.AddColumn(Unit.FromCentimeter(11));
        var lrow1 = leftTable.AddRow();
        var stuPara = lrow1.Cells[0].AddParagraph();
        var ftStu1 = stuPara.AddFormattedText("HÓA ĐƠN ĐẾN\n");
        ftStu1.Font.Bold = true;
        var ftStu2 = stuPara.AddFormattedText(data.StudentName + "\n");
        ftStu2.Font.Bold = true;
        stuPara.AddText($"Số điện thoại: {data.StudentPhone}\n");
        stuPara.AddText($"Địa chỉ: {data.StudentAddress}\n");
        stuPara.AddText(
            $"CMND: {data.StudentId} Nơi cấp: {data.StudentIdPlace} Ngày cấp: {data.StudentIdDate:dd/MM/yyyy}\n");
        stuPara.AddText($"Đầu ra: {data.OutputCommit}\n");
        stuPara.AddText($"Ghi chú: {data.Note}");
        stuPara.Style = "Normal";
        stuPara.Format.SpaceAfter = 6;

        // Table con bên phải (thông tin hóa đơn)
        var rightTable = rightCell.Elements.AddTable();
        rightTable.Borders.Width = 0;
        rightTable.AddColumn(Unit.FromCentimeter(7));
        var rrow1 = rightTable.AddRow();
        var pr = rrow1.Cells[0].AddParagraph();
        var ftPr1 = pr.AddFormattedText("THÔNG TIN HÓA ĐƠN\n");
        ftPr1.Font.Bold = true;
        pr.AddText($"Hóa đơn số\n");
        var ftPr2 = pr.AddFormattedText(data.ReceiptNumber + "\n");
        ftPr2.Font.Bold = true;
        pr.AddText($"Ngày thanh toán\n");
        var ftPr3 = pr.AddFormattedText(data.PaymentDate.ToString("dd/MM/yyyy HH:mm") + "\n");
        ftPr3.Font.Bold = true;
        pr.AddText($"Hình thức thanh toán\n");
        var ftPr4 = pr.AddFormattedText(data.PaymentMethod);
        ftPr4.Font.Bold = true;
        pr.Style = "Normal";
        pr.Format.SpaceAfter = 6;

        // Vùng dưới: full width
        section.AddParagraph().Format.SpaceAfter = 2;

        // Bảng tổng kết tài chính
        var sumTable = section.AddTable();
        sumTable.Borders.Width = 0;
        sumTable.AddColumn(Unit.FromCentimeter(11));
        sumTable.AddColumn(Unit.FromCentimeter(7));
        var sumRow = sumTable.AddRow();
        sumRow.Cells[0].AddParagraph();
        var sumCell = sumRow.Cells[1];
        sumCell.AddParagraph($"Tổng học phí\t{data.TotalFee:N0}").Style = "CustomBold";
        sumCell.AddParagraph($"Ưu đãi\t{data.Discount:N0}").Style = "Normal";
        sumCell.AddParagraph($"Tổng đã đóng\t{data.Paid:N0}").Style = "Normal";
        sumCell.AddParagraph($"Thực đóng hôm nay\t{data.PaidToday:N0}").Style = "Normal";
        sumCell.AddParagraph($"Học phí còn lại\t{data.Remaining:N0}").Style = "Normal";
        sumCell.AddParagraph($"Ngày hẹn thu\t{data.DueDate}").Style = "Normal";
        sumCell.Format.Alignment = ParagraphAlignment.Right;
        sumCell.Format.SpaceAfter = 6;

        // Lưu ý
        var note = section.AddParagraph("LƯU Ý");
        note.Style = "Normal";
        note.Format.SpaceBefore = 6;
        foreach (var n in data.GeneralNotes)
        {
            var noteItem = section.AddParagraph("• " + n);
            noteItem.Style = "CustomSmall";
        }

        section.AddParagraph().Format.SpaceAfter = 6;

        // Ký tên
        var signTable = section.AddTable();
        signTable.Borders.Width = 0;
        signTable.AddColumn(Unit.FromCentimeter(8));
        signTable.AddColumn(Unit.FromCentimeter(8));
        var signRow = signTable.AddRow();
        var payerCell = signRow.Cells[0].AddParagraph("Người nộp tiền\n(Ký và ghi rõ họ tên)");
        payerCell.Style = "Normal";
        var cashierCell = signRow.Cells[1].AddParagraph($"Người thu tiền\n(Ký và ghi rõ họ tên)\n\n{data.Cashier}");
        cashierCell.Style = "Normal";

        // Render PDF
        ExportDocument.ExportMigraDocCore(filename, projectRoot, doc);
    }

    private Document InitDocument()
    {
        Document doc = new Document();
        // Font mặc định Times New Roman
        Style normal = doc.Styles["Normal"];
        normal.Font.Name = "Times New Roman";
        normal.Font.Size = 9; // nhỏ hơn

        // Style CustomTitle (rất to, in đậm, căn giữa)
        Style title = doc.Styles.AddStyle("CustomTitle", "Normal");
        title.Font.Size = 20;
        title.Font.Bold = true;

        // Style CustomHeader (to, in đậm)
        Style header = doc.Styles.AddStyle("CustomHeader", "Normal");
        header.Font.Size = 20;
        header.Font.Bold = true;
        header.ParagraphFormat.Alignment = ParagraphAlignment.Center;

        // Style CustomSubHeader (vừa, in đậm)
        Style subHeader = doc.Styles.AddStyle("CustomSubHeader", "Normal");
        subHeader.Font.Size = 12;
        subHeader.Font.Bold = true;
        subHeader.ParagraphFormat.Alignment = ParagraphAlignment.Left;

        // Style CustomSmall (nhỏ, không đậm)
        Style small = doc.Styles.AddStyle("CustomSmall", "Normal");
        small.Font.Size = 6;
        small.Font.Bold = false;

        // Style CustomBold (vừa, in đậm)
        Style bold = doc.Styles.AddStyle("CustomBold", "Normal");
        bold.Font.Bold = true;

        return doc;
    }

    private Section InitSection(Document doc)
    {
        Section section = doc.AddSection();
        section.PageSetup.PageFormat = PageFormat.A4;
        section.PageSetup.TopMargin = Unit.FromCentimeter(1.5);
        section.PageSetup.LeftMargin = Unit.FromCentimeter(2);
        section.PageSetup.RightMargin = Unit.FromCentimeter(2);

        return section;
    }
}
