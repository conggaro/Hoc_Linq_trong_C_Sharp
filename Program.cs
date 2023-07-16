using System.Security.Cryptography.X509Certificates;
// Linq là gì?
// Linq = Language integrated query
// ngôn ngữ truy vấn tích hợp

// nó được viết bằng class IEnumerable, IEnumerable<T>

// nguồn dữ liệu: Array, List, Stack, Queue

using System;
using System.Linq;

namespace MyApp{
    public class Product {
        public int ID {set; get;}
        public string Name {set; get;}         // tên
        public double Price {set; get;}        // giá
        public string[] Colors {set; get;}     // các màu sắc
        public int id_Brand {set; get;}           // ID Nhãn hiệu, hãng
        
        public Product(int id, string name, double price, string[] colors, int brand)
        {
            ID = id; Name = name; Price = price; Colors = colors; id_Brand = brand;
        }
        
        // Lấy chuỗi thông tin sản phẳm gồm ID, Name, Price
        // dùng -3, -15, -6, -2 để căn lề bên trái
        // dùng 3, 15, 6, 2 để căn lề bên phải
        public override string ToString()
        => $"[{ID, -3} {Name, -15} {Price, -6} {id_Brand, -2} ({string.Join(", ", Colors)})]";
    }

    // tạo lớp thương hiệu
    public class Brand {
        public string Name {set; get;}
        public int ID {set; get;}

        public override string ToString()
        => $"[{ID, -1}, {Name, -10}]";
    }

    public class Program{
        public static void Main(string[] args){
            // xóa màn hình console cũ
            Console.Clear();

            // tạo đối tượng
            List<Brand> ds_ThuongHieu = new List<Brand>() {
                new Brand{ID = 1, Name = "Cong ty AAA"},
                new Brand{ID = 2, Name = "Cong ty BBB"},
                new Brand{ID = 4, Name = "Cong ty CCC"},
            };

            List<Product> ds_SanPham = new List<Product>(){
                new Product(1, "Ban tra",    400, new string[] {"Xam", "Xanh"},         2),
                new Product(2, "Tranh treo", 400, new string[] {"Vang", "Xanh"},        1),
                new Product(3, "Den chum",   500, new string[] {"Trang"},               3),
                new Product(4, "Ban hoc",    200, new string[] {"Trang", "Xanh"},       1),
                new Product(5, "Tui da",     300, new string[] {"Do", "Den", "Vang"},   2),
                new Product(6, "Giuong ngu", 500, new string[] {"Trang"},               2),
                new Product(7, "Tu ao",      600, new string[] {"Trang"},               3),
            };

            // lấy ra sản phẩm có giá 400
            var dt_query1 = from item in ds_SanPham
                            where item.Price == 400
                            select new {
                                Ten = item.Name,
                                Gia = item.Price
                            };

            // in ra màn hình những sản phẩm có giá 400
            Console.WriteLine($"Danh sach san pham co gia 400:");
            for(int i = 0; i < dt_query1.Count(); i++){
                Console.WriteLine($"{i + 1}. [{dt_query1.ToList()[i].Ten}, {dt_query1.ToList()[i].Gia}]");
            }

            // sử dụng phương thức Select()
            // Select() trả về như 1 danh sách
            // tham số của nó là Func
            // tức là bạn phải truyền vào 1 cái hàm (thật là ra truyền vào 1 cái Lambda)
            var dt_query2 = ds_SanPham.Select((Product item) => {
                return item.Name;
            });

            // in ra tên tất cả sản phẩm
            Console.WriteLine($"\nTen tat ca san pham:");
            for(int i = 0; i < dt_query2.Count(); i++){
                Console.WriteLine($"{i + 1}. [{dt_query2.ToList()[i]}]");
            }

            // sử dụng phương thức Where()
            // trả về kiểu bool
            // nhận tham số là Func
            // trả về cái phần tử được return là true
            var dt_query3 = ds_SanPham.Where((Product item) => {
                bool ketQua = false;

                // kiểm tra trong Name có chứa chuỗi "tr"
                // sử dụng Contains
                // Contains nghĩa là chứa
                if (item.Name.Contains("tr") == true)
                {
                    ketQua = true;
                }
                else
                {
                    ketQua = false;
                }

                return ketQua;
            });

            // in ra những sản phẩm
            // trong Name có chứa chuỗi "tr"
            Console.WriteLine("\nSan pham chua \"tr\":");
            for(int i = 0; i < dt_query3.Count(); i++){
                Console.WriteLine($"{i + 1}. {dt_query3.ToList()[i].ToString()}");
            }

            // Yêu cầu:
            // tìm sản phẩm
            // có giá lớn hơn 200 và nhỏ hơn 300
            // sử dụng Where()
            var dt_query4 = ds_SanPham.Where((Product item) => {
                return 200 <= item.Price && item.Price <= 300;
            });

            Console.WriteLine($"\nDanh sach san pham co gia lon hon 200 va nho hon 300:");
            for(int i = 0; i < dt_query4.Count(); i++){
                Console.WriteLine($"{i + 1}. {dt_query4.ToList()[i]}");
            }

            // Học sách sử dụng SelectMany()
            // nó có tác dụng
            // giống như gộp các mảng thành 1 mảng
            var dt_query5 = ds_SanPham.SelectMany((Product item) => {
                return item.Colors;
            });

            // sử dụng SelectMany()
            // để gộp tất cả phần tử màu sắc
            // ở các mảng khác nhau
            // về 1 mảng (kiểu kiểu vậy)
            Console.WriteLine($"\nDanh sach mau:");
            for(int i = 0; i < dt_query5.Count(); i++){
                Console.WriteLine($"{i + 1}. {dt_query5.ToList()[i]}");
            }

            // sử dụng phương thức Min()
            // có tác dụng trả về giá trị nhỏ nhất
            
            // in ra giá nhỏ nhất
            var dt_query6 = from item in ds_SanPham
                            select item.Price;
            Console.WriteLine($"\nGia nho nhat: {dt_query6.Min()}");

            // sử dụng phương thức Max()
            // có tác dụng trả về giá trị lớn nhất

            // in ra giá lớn nhất
            Console.WriteLine($"\nGia lon nhat: {dt_query6.Max()}");

            // sử dụng phương thức Sum()
            // có tác dụng tính tổng tất cả phần tử trong mảng (kiểu kiểu vậy)
            
            // in ra tổng giá tiền của tất cả sản phẩm
            Console.WriteLine($"\nTong gia tien cua tat ca san pham: {dt_query6.Sum()}");

            // sử dụng phương thức Average()
            // có tác dụng tính trung bình cộng

            // int ra trung bình cộng giá tiền của tất cả sản phẩm
            Console.WriteLine($"\nTrung binh cong gia tien cua tat ca san pham: {dt_query6.Average()}");

            // sử dụng phương thức Join()
            // để kết hợp dữ liệu từ 2 bảng
            // Join() có 4 tham số
            // tham số thứ 1 là nguồn dữ liệu tham chiếu vào ds_SanPham, Ví dụ: ds_ThuongHieu
            // tham số thứ 2 là Func (giống key chính)
            // tham số thứ 3 là Func (giống key phụ)
            // tham số thứ 4 là Func (1 bản ghi của Product ghép với 1 bản ghi của Brand)
            var dt_query7 = ds_SanPham.Join(ds_ThuongHieu,
                            (Product item_sp) => {
                                return item_sp.id_Brand;
                            },
                            (Brand item_th) => {
                                return item_th.ID;
                            },
                            (Product item_sp, Brand item_th) => {
                                return new {
                                    San_Pham = item_sp,
                                    Thuong_Hieu = item_th
                                };
                            }
            );

            // in ra tên sản phẩm và tên thương hiệu ứng với sản phẩm đó
            Console.WriteLine($"\nTen san pham va ten thuong hieu ung voi san pham do:");
            for(int i = 0; i < dt_query7.Count(); i++){
                Console.WriteLine($"{i + 1}. {dt_query7.ToList()[i].San_Pham}");
                Console.WriteLine($"{i + 1}. {dt_query7.ToList()[i].Thuong_Hieu}\n");
            }

            // sử dụng phương thức GroupJoin()
            // hơi giống Join thôi
            // tuy nhiên phần tử trả về là 1 nhóm, nhóm lại theo nguồn ban đầu

            // để kết hợp dữ liệu từ 2 bảng
            // GroupJoin() có 4 tham số
            // tham số thứ 1 là nguồn dữ liệu phụ để ghép với dữ liệu chính
            // tham số thứ 2 là Func (giống key chính)
            // tham số thứ 3 là Func (giống key phụ)
            // tham số thứ 4 là Func (1 bản ghi của Brand ghép với N bản ghi của Product nếu thỏa mãn điều kiện id_Brand bằng nhau)
            var dt_query8 = ds_ThuongHieu.GroupJoin(ds_SanPham,
                            (Brand item) => {
                                return item.ID;
                            },
                            (Product item) => {
                                return item.id_Brand;
                            },
                            (Brand item_th,IEnumerable<Product> item_sp) => {
                                return new {
                                    Ten_TH = item_th.Name,

                                    // tức là mỗi cái item_sp
                                    // thì nó có đủ 5 trường dữ liệu luôn
                                    Nhieu_Truong_SP = item_sp
                                };
                            }
            );

            // bây giờ
            // in ra 1 bản ghi chính (trong bản ghi chính, chỉ hiển thị tên_thương_hiệu cũng được)
            // ghép với N bản ghi phụ
            // nếu thỏa mãn điều kiện id_Brand bằng nhau
            foreach(var item in dt_query8){
                Console.WriteLine($"\n{item.Ten_TH}");

                foreach(var san_pham in item.Nhieu_Truong_SP){
                    Console.WriteLine(san_pham);
                }
            }

            // sử dụng phương thức Take()
            // có tác dụng lấy ra N bản ghi đầu tiên trong 1 bảng
            var dt_query9 = ds_SanPham.Take(4);

            Console.WriteLine($"\nLay ra 4 ban ghi dau tien:");
            foreach(var item in dt_query9){
                Console.WriteLine(item.ToString());
            }

            // sử dụng phương thức Skip()
            // có tác dụng bỏ qua N bản ghi đầu tiên trong 1 bảng
            // chỉ lấy các bản ghi còn lại
            var dt_query10 = ds_SanPham.Skip(2);

            Console.WriteLine($"\nBo qua 2 ban ghi dau tien, chi lay cac ban ghi con lai:");
            foreach(var item in dt_query10){
                Console.WriteLine(item.ToString());
            }

            // sử dụng phương thức OrderBy()
            // có tác dụng sắp xếp tăng dần (tiếng Anh là Ascending)
            // tham số là Func (giống 1 hàm)
            // nó trả về giống như 1 mảng chứa các phần tử kiểu Product
            var dt_query11 = ds_SanPham.OrderBy((Product item) => {
                return item.Price;
            });

            Console.WriteLine($"\nSap xep tang dan theo gia san pham:");
            foreach(var item in dt_query11){
                Console.WriteLine(item.ToString());
            }

            // sử dụng phương thức OrderByDescending()
            // có tác dụng sắp xếp giảm dần (tiếng Anh là Descending)
            // tham số là Func (giống 1 hàm)
            // nó trả về giống như 1 mảng chứa các phần tử kiểu Product
            var dt_query12 = ds_SanPham.OrderByDescending((Product item) => {
                return item.Price;
            });

            Console.WriteLine($"\nSap xep giam dan theo gia san pham:");
            foreach(var item in dt_query12){
                Console.WriteLine(item.ToString());
            }

            // sử dụng phương thức Reverse()
            // có tác dụng đảo ngược các phần tử trong mảng
            var dt_query13 = ds_SanPham.ToArray().Reverse();

            Console.WriteLine($"\nIn ra danh sach bi dao nguoc:");
            foreach(var item in dt_query13){
                Console.WriteLine(item.ToString());
            }

            // sử dụng phương thức GroupBy()
            // có tác dụng nhóm các bản ghi có giá_sản_phẩm giống nhau vào 1 nhóm
            // tham số là Func
            // trả về 1 chuỗi dữ liệu
            var dt_query14 = ds_SanPham.GroupBy((Product item) => {
                return item.Price;
            });

            Console.WriteLine($"\nCac nhom ban ghi theo gia san pham:");
            foreach(var item in dt_query14){
                Console.WriteLine($"____________________ Nhom gia {item.Key} ____________________");

                foreach(var san_pham in item){
                    Console.WriteLine($"{san_pham.ToString()}");
                }

                Console.Write("\n");
            }

            // Bình luận:
            // Nếu các bạn chỉ muốn in ra các sản phẩm
            // có giá bằng 400
            // thì không cần dùng GroupBy() đâu
            // dùng Where là được rồi

            
            // sử dụng phương thức Distinct()
            // có tác dụng loại bỏ các bản ghi giống nhau
            // chỉ giữ lại các bản ghi khác nhau
            var dt_query15 = ds_SanPham.SelectMany((Product item) => {
                return item.Colors;
            }).Distinct();

            Console.WriteLine($"Danh sach cac mau khac nhau:");
            for(int i = 0; i < dt_query15.Count(); i++){
                Console.WriteLine($"{i + 1}. {dt_query15.ToList()[i]}");
            }

            // sử dụng phương thức Single()
            // kiểm tra các phần tử
            // nếu có 1 phần tử thỏa mãn điều kiện
            // thì trả về cái phần tử đó

            // nếu không có phần tử nào thỏa mãn điều kiện
            // thì nó báo lỗi (nó báo ngoại lệ Exception)

            // nếu có nhiều phần tử thỏa mãn điều kiện
            // thì nó cũng báo lỗi (nó báo ngoại lệ Exception)
            var dt_query16 = ds_SanPham.Single((Product item) => {
                return item.Price == 600;
            });

            Console.WriteLine("\nTim 1 san pham, nhung no phai co gia 600:");
            Console.WriteLine(dt_query16.ToString());

            // sử dụng phương thức SingleOrDefault()
            // kiểm tra các phần tử
            // nếu có 1 phần tử thỏa mãn điều kiện
            // thì trả về cái phần tử đó

            // nếu không có phần tử nào thỏa mãn điều kiện
            // thì nó trả về null

            // nếu có nhiều phần tử thỏa mãn điều kiện
            // thì nó báo lỗi (nó báo ngoại lệ Exception)

            var dt_query17 = ds_SanPham.SingleOrDefault((Product item) => {
                return item.Name == "Ban hoc";
            });

            Console.WriteLine("\nTim 1 san pham, nhung no phai co ten \"Ban hoc\":");
            if(dt_query17 != null){
                Console.WriteLine(dt_query17.ToString());
            }
            else if(dt_query17 == null){
                Console.WriteLine("Khong ton tai (san_pham is null)");
            }

            // Bình luận:
            // cái SingleOrDefault() phù hợp 
            // để kiểm tra tài khoản đăng nhập đấy
            // nếu tài khoản tồn tại => đăng nhập thành công
            // nếu tài khoản chưa tạo => thông báo tai_khoan is null


            // sử dụng phương thức Any()
            // có tác dụng kiểm tra
            // tham số là Func
            // trả về true hoặc false
            var dt_query18 = ds_SanPham.Any((Product item) => {
                return item.Price == 700;
            });

            Console.WriteLine("\nKiem tra xem co san pham nao co gia 700 khong:");
            Console.WriteLine(dt_query18);

            // sử dụng phương thức All()
            // nó có tác dụng kiểm tra
            // cụ thể là kiểm tra tất cả bản ghi
            // tham số là Func
            // trả về true hoặc false

            // ví dụ:
            // kiểm tra tất cả sản phẩm
            // xem giá của tất cả sản phẩm có nhỏ hơn 1000 không
            var dt_query19 = ds_SanPham.All((Product item) => {
                return item.Price < 1000;
            });

            Console.WriteLine("\nKiem tra tat ca san pham, xem gia cua tat ca san pham co nho hon 1000 khong:");
            Console.WriteLine(dt_query19);

            // sử dụng phương thức Count()
            // có tác dụng đếm bản ghi
            int dt_query20 = ds_SanPham.Count((Product item) => {
                return item.Price > 400;
            });

            Console.WriteLine($"\nCo {dt_query20} san pham, co gia lon hon 400\n");
        }
    }
}