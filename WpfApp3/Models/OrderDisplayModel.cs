using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeNguyenAnNinhWpfApp.Models
{
    public class OrderDisplayModel
    {
        public int OrderId { get; set; }
        public string CustomerName { get; set; } = "";
        public string EmployeeName { get; set; } = "";
        public DateTime OrderDate { get; set; }

        public string ProductNames { get; set; } = ""; // Gom tên sản phẩm lại
        public string Quantities { get; set; } = "";   // Gom số lượng lại
        public decimal TotalAmount { get; set; }
        // Property hiển thị định dạng sẵn
        public string TotalAmountDisplay => $"{TotalAmount:N0} ₫";
    }
}
