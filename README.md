# Hướng dẫn Khởi chạy & Kiểm thử dự án Webbanhang (MVC & Web API)

Dự án này tích hợp song song cả hai nền tảng:
- **Trang web bán hàng thông thường (ASP.NET Core MVC)**.
- **Hệ thống Web API & bảo mật bằng JWT Token (kèm giao diện thử nghiệm Swagger UI)**.

---

## 1. Cách Khởi chạy Dự án
Bạn có thể khởi chạy ứng dụng bằng một trong hai cách dưới đây:

### Cách 1: Sử dụng terminal (Dòng lệnh)
1. Mở terminal tại thư mục dự án `NgoManhHung_Tuan345`.
2. Chạy lệnh:
   ```bash
   dotnet run
   ```
3. Trình duyệt sẽ tự động mở trang web bán hàng bình thường.

### Cách 2: Sử dụng IDE (Visual Studio / VS Code)
- Nhấn nút **Play / Start (F5)** trên thanh công cụ để chạy dự án. Trình duyệt sẽ tự động mở trang web bán hàng.

---

## 2. Liên kết truy cập
Sau khi dự án đã chạy, bạn sử dụng các địa chỉ sau trên trình duyệt:

- **Trang web bán hàng (Mặc định)**:
  - HTTP: [http://localhost:5167](http://localhost:5167)
  - HTTPS: [https://localhost:7105](https://localhost:7105)

- **Trang kiểm thử Web API (Swagger UI)**:
  - HTTP: [http://localhost:5167/swagger](http://localhost:5167/swagger)
  - HTTPS: [https://localhost:7105/swagger](https://localhost:7105/swagger)

---

## 3. Hướng dẫn các bước Kiểm thử Web API bằng Swagger

### Bước 1: Đăng ký tài khoản mới (Register API)
1. Truy cập trang Swagger UI (`/swagger`).
2. Tìm nhóm **Authenticate** và chọn **`POST /api/Authenticate/register`**.
3. Nhấp nút **Try it out** ở góc phải.
4. Sửa Request Body mẫu thành thông tin của bạn. Ví dụ:
   ```json
   {
     "username": "hungtest1",
     "email": "hungtest1@gmail.com",
     "password": "HungPassword@123",
     "fullName": "Ngô Mạnh Hùng",
     "address": "Hồ Chí Minh",
     "age": "20",
     "initials": "NMH",
     "role": "User"
   }
   ```
5. Nhấp nút **Execute**. Trả về **`200 OK`** kèm thông báo thành công là hoàn thành.

### Bước 2: Đăng nhập & Lấy JWT Token (Login API)
1. Chọn **`POST /api/Authenticate/login`** trong Swagger UI.
2. Nhấp **Try it out**.
3. Điền tài khoản vừa đăng ký ở Bước 1:
   ```json
   {
     "username": "hungtest1",
     "password": "HungPassword@123"
   }
   ```
4. Nhấp **Execute**.
5. Sao chép (Copy) toàn bộ chuỗi ký tự Token trong Response body (không copy dấu ngoặc kép).

### Bước 3: Xác thực Token trên Swagger (Authorize)
1. Kéo lên đầu trang Swagger UI, nhấp vào nút **Authorize** màu xanh lá.
2. Tại ô **Value**, nhập theo đúng cú pháp sau (có dấu cách sau chữ Bearer):
   ```text
   Bearer <Chuỗi_Token_Của_Bạn>
   ```
   *Ví dụ:* `Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...`
3. Nhấp **Authorize** rồi nhấn **Close**.

### Bước 4: Gọi thử nghiệm API Sản phẩm (Product API)
1. Chọn bất kỳ API nào dưới nhóm **ProductApi** (Ví dụ: **`GET /api/ProductApi`**).
2. Nhấp **Try it out** -> **Execute**.
3. Bạn sẽ nhận được danh sách sản phẩm phản hồi thành công dưới dạng JSON.
