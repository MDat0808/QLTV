create database qltv;

use qltv;

create table nguoidung (
	`id` INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    `HoTen` VARCHAR(50) NOT NULL,
	TaiKhoan varchar(100) not null unique,
    `MatKhau` VARCHAR(255) NOT NULL,
    `email` VARCHAR(100) NOT NULL,
	NgaySinh date not null,
	DiaChi varchar(100) not null ,
    quyen varchar(10) not null default 'user',
    `NgayTao` date DEFAULT CURRENT_TIMESTAMP
);

create table sach (
	`MaSach` INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    SoLuong int not null,
	TenSach varchar(100) not null ,
	TacGia varchar(150) not null ,
    NamXuatBan date not null,
    NhaXuatBan varchar(100) not null ,
    Gia float not null,
    NgayNhap date not null
);

create table phieumuonsach (
	MaPhieuMuon INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    MaNguoiMuon int not null,
    MaSach int not null,
    NgayMuon date not null,
    CONSTRAINT fk_1
    FOREIGN KEY (MaNguoiMuon) REFERENCES nguoidung(id),
    CONSTRAINT fk_2
    FOREIGN KEY (MaSach) REFERENCES sach(MaSach)
);

CREATE TABLE hoadon (
  MaHoaDon INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
  MaNguoiMuon INT NOT NULL,
  NgayLapHoaDon DATE NOT NULL,
	MaSach int not null,
  TongTien FLOAT NOT NULL,
  TrangThai VARCHAR(20) DEFAULT 'Chưa thanh toán',
  CONSTRAINT fk_hoadon_nguoidung FOREIGN KEY (MaNguoiMuon) REFERENCES nguoidung(id),
      CONSTRAINT fk_masach
    FOREIGN KEY (MaSach) REFERENCES sach(MaSach)
);

create table phieuthutien (
	MaPhieu INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
	MaNguoiThanhToan INT NOT NULL,
  NgayThanhToan DATE NOT NULL,
	MaHD int not null,
  CONSTRAINT fk_thutien FOREIGN KEY (MaNguoiThanhToan) REFERENCES nguoidung(id),
      CONSTRAINT fk_hd
    FOREIGN KEY (MaHD) REFERENCES hoadon(MaHoaDon)
);

INSERT INTO nguoidung (HoTen, TaiKhoan, MatKhau, email, NgaySinh, DiaChi, quyen)
VALUES
('Nguyen Van A', 'admin1', '123456', 'nguyenvana@example.com', '1990-01-01', '123 Nguyen Trai, Hanoi', 'admin'),
('Tran Thi B', 'user1', '1234', 'tranthib@example.com', '1992-02-02', '456 Le Loi, HCMC', 'user'),
('Le Van C', 'user2', '1234', 'levanc@example.com', '1994-03-03', '789 Tran Phu, Da Nang', 'user');

INSERT INTO sach (SoLuong, TenSach, TacGia, NamXuatBan, NhaXuatBan, Gia, NgayNhap)
VALUES
(10, 'Book A', 'Author A', '2022-01-01', 'Publisher A', 9.99, '2023-01-01'),
(5, 'Book B', 'Author B', '2021-02-02', 'Publisher B', 19.99, '2023-02-02'),
(7, 'Book C', 'Author C', '2020-03-03', 'Publisher C', 14.99, '2023-03-03');
