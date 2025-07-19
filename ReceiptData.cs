using System;
using System.Collections.Generic;

namespace TestPDFSharp
{
    public class ReceiptData
    {
        // Thông tin chung
        public string ReceiptNumber { get; set; } = "";
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; } = "";
        public string Branch { get; set; } = "";
        public string QrImagePath { get; set; } = ""; // Đường dẫn ảnh QR code, nếu rỗng thì không chèn

        // Thông tin công ty
        public string CompanyName { get; set; } = "";
        public string CompanyTaxCode { get; set; } = "";
        public string CompanyAddress { get; set; } = "";
        public string CompanyBranch { get; set; } = "";

        // Thông tin học viên
        public string StudentName { get; set; } = "";
        public string StudentPhone { get; set; } = "";
        public string StudentAddress { get; set; } = "";
        public string StudentId { get; set; } = "";
        public string StudentIdPlace { get; set; } = "";
        public DateTime StudentIdDate { get; set; }
        public string Note { get; set; } = "";
        public string OutputCommit { get; set; } = "";

        // Thông tin khóa học
        public List<ReceiptCourseItem> Courses { get; set; } = new();

        // Thông tin tài chính
        public decimal TotalFee { get; set; }
        public decimal Discount { get; set; }
        public decimal Paid { get; set; }
        public decimal PaidToday { get; set; }
        public decimal Remaining { get; set; }
        public string DueDate { get; set; } = "";

        // Ghi chú chung
        public List<string> GeneralNotes { get; set; } = new();

        // Người thu tiền
        public string Cashier { get; set; } = "";
    }

    public class ReceiptCourseItem
    {
        public string Type { get; set; } = ""; // Loại: Đăng ký, Gia hạn, ...
        public string Name { get; set; } = "";
        public string Skills { get; set; } = "";
        public decimal Amount { get; set; }
    }
} 