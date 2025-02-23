using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.Model
{
    public class MonAn
    {
        public string thu {  get; set; }    
        public string Sang { get; set; }
        public string Trua { get; set; }
        public string Chieu { get; set; }
        public string MaMonAn { get; set; }  // Thêm mã món ăn
        public string Ten { get; set; }      // Tên món ăn
        public double ChiPhi { get; set; }   // Chi phí
        public double Calo { get; set; }     // Calo
        public double Protein { get; set; }  // Protein
        public double Carb { get; set; }     // Carb
        public double Fat { get; set; }      // Fat
        public string LoaiMon { get; set; }  // Loại món ăn (ví dụ: chính, phụ)
        public List<MonAn> MonAnLienQuan { get; set; }

        // Constructor để khởi tạo các giá trị
        public MonAn( string ten, double chiPhi, double calo, double protein, double carb, double fat, string loaiMon)
        {
           
            Ten = ten;
            ChiPhi = chiPhi;
            Calo = calo;
            Protein = protein;
            Carb = carb;
            Fat = fat;
            LoaiMon = loaiMon;
        }
        public MonAn() { }  
    }
}
