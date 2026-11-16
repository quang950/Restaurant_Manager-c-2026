-- Chạy nguyên file trong MySQL Workbench. Script không xóa database/bảng hiện có.
-- Dữ liệu mẫu chỉ được thêm nếu mã tương ứng chưa tồn tại (không ghi đè dữ liệu đã sửa).
CREATE DATABASE IF NOT EXISTS quanlynhahang CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
USE quanlynhahang;
CREATE TABLE IF NOT EXISTS nhanvien (
    manv INT PRIMARY KEY AUTO_INCREMENT,
    hoten VARCHAR(100) NOT NULL,
    sdt VARCHAR(20) NOT NULL,
    tendangnhap VARCHAR(50) NOT NULL UNIQUE,
    matkhau VARCHAR(150) NOT NULL,
    vaitro VARCHAR(20) NOT NULL,
    CHECK (vaitro IN ('Quản lý', 'Nhân viên'))
) ENGINE=InnoDB;
CREATE TABLE IF NOT EXISTS monnuoc (
    mamon INT PRIMARY KEY AUTO_INCREMENT,
    tenmon VARCHAR(100) NOT NULL UNIQUE,
    loai VARCHAR(20) NOT NULL,
    donvi VARCHAR(30) NOT NULL,
    gia INT NOT NULL,
    soluongton INT NOT NULL DEFAULT 0 CHECK (soluongton >= 0),
    CHECK (gia > 0), CHECK (loai IN ('Món ăn','Nước uống'))
) ENGINE=InnoDB;
CREATE TABLE IF NOT EXISTS ban (
    maban INT PRIMARY KEY AUTO_INCREMENT,
    tenban VARCHAR(50) NOT NULL UNIQUE,
    socho INT NOT NULL,
    CHECK (socho > 0)
) ENGINE=InnoDB;
CREATE TABLE IF NOT EXISTS nhacungcap (
    mancc INT PRIMARY KEY AUTO_INCREMENT,
    tenncc VARCHAR(100) NOT NULL,
    sdt VARCHAR(20) NOT NULL,
    diachi VARCHAR(250) NOT NULL
) ENGINE=InnoDB;
CREATE TABLE IF NOT EXISTS hoadon (
    mahd INT PRIMARY KEY AUTO_INCREMENT,
    maban INT NOT NULL,
    manv INT NOT NULL,
    ngaylap DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    ngaythanhtoan DATETIME NULL,
    trangthai VARCHAR(30) NOT NULL DEFAULT 'Chưa thanh toán',
    tongtien INT NOT NULL DEFAULT 0,
    FOREIGN KEY (maban) REFERENCES ban(maban),
    FOREIGN KEY (manv) REFERENCES nhanvien(manv),
    CHECK (tongtien >= 0), CHECK (trangthai IN ('Chưa thanh toán','Đã thanh toán')),
    INDEX idx_hoadon_ban (maban, trangthai),
    INDEX idx_hoadon_ngay (ngaythanhtoan)
) ENGINE=InnoDB;
CREATE TABLE IF NOT EXISTS chitiethoadon (
    mahd INT NOT NULL,
    mamon INT NOT NULL,
    soluong INT NOT NULL,
    dongia INT NOT NULL,
    PRIMARY KEY (mahd,mamon),
    FOREIGN KEY (mahd) REFERENCES hoadon(mahd),
    FOREIGN KEY (mamon) REFERENCES monnuoc(mamon),
    CHECK (soluong > 0), CHECK (dongia > 0)
) ENGINE=InnoDB;
CREATE TABLE IF NOT EXISTS phieunhap (
    mapn INT PRIMARY KEY AUTO_INCREMENT,
    mancc INT NOT NULL,
    manv INT NOT NULL,
    ngaynhap DATETIME NOT NULL,
    tongtien INT NOT NULL,
    maxacnhan CHAR(32) NOT NULL UNIQUE,
    FOREIGN KEY (mancc) REFERENCES nhacungcap(mancc),
    FOREIGN KEY (manv) REFERENCES nhanvien(manv),
    CHECK (tongtien > 0)
) ENGINE=InnoDB;
CREATE TABLE IF NOT EXISTS chitietphieunhap (
    mapn INT NOT NULL,
    mamon INT NOT NULL,
    soluong INT NOT NULL,
    dongianhap INT NOT NULL,
    PRIMARY KEY (mapn,mamon),
    FOREIGN KEY (mapn) REFERENCES phieunhap(mapn),
    FOREIGN KEY (mamon) REFERENCES monnuoc(mamon),
    CHECK (soluong > 0), CHECK (dongianhap > 0)
) ENGINE=InnoDB;

INSERT INTO nhanvien (manv,hoten,sdt,tendangnhap,matkhau,vaitro)
SELECT 1,'Nguyễn Minh Quản','0901234567','admin','UUxOSGFkbWluU2FsdDAwMSE=:hlZ465+nHZjqDu6Fe2BK7aTNtBRejwxpKkjFenVBG2A=','Quản lý'
WHERE NOT EXISTS (SELECT 1 FROM nhanvien WHERE manv=1 OR tendangnhap='admin');
INSERT INTO nhanvien (manv,hoten,sdt,tendangnhap,matkhau,vaitro)
SELECT 2,'Trần Thu Ngân','0912345678','nv01','UUxOSHN0YWZmU2FsdDAwMSE=:ljeOlB0K0dWnUzyNBUrAXGvxI4rW4OQ5rj1ewSYJMMc=','Nhân viên'
WHERE NOT EXISTS (SELECT 1 FROM nhanvien WHERE manv=2 OR tendangnhap='nv01');
INSERT IGNORE INTO monnuoc (mamon,tenmon,loai,donvi,gia) VALUES
(1,'Cơm chiên hải sản','Món ăn','Đĩa',65000),
(2,'Bò lúc lắc','Món ăn','Đĩa',95000),
(3,'Gà chiên nước mắm','Món ăn','Đĩa',85000),
(4,'Canh chua cá','Món ăn','Tô',75000),
(5,'Rau muống xào tỏi','Món ăn','Đĩa',35000),
(6,'Trà đào','Nước uống','Ly',25000),
(7,'Nước suối','Nước uống','Chai',12000),
(8,'Coca Cola','Nước uống','Lon',20000);
INSERT IGNORE INTO ban VALUES (1,'Bàn 01',4),(2,'Bàn 02',4),(3,'Bàn 03',6),(4,'Bàn 04',6),(5,'Bàn 05',8),(6,'Bàn 06',2);
INSERT IGNORE INTO nhacungcap VALUES
(1,'Thực phẩm An Nhiên','0281234567','12 Nguyễn Trãi, TP.HCM'),
(2,'Đồ uống Minh Phát','0287654321','45 Lê Lợi, TP.HCM');
-- Hóa đơn cũ trước khi theo dõi tồn kho. Không trừ tồn hồi tố.
INSERT IGNORE INTO hoadon (mahd,maban,manv,ngaylap,ngaythanhtoan,trangthai,tongtien)
VALUES (1,1,1,CURRENT_DATE + INTERVAL 10 HOUR,CURRENT_DATE + INTERVAL 11 HOUR,'Đã thanh toán',180000);
INSERT IGNORE INTO chitiethoadon VALUES (1,1,2,65000),(1,6,2,25000);

-- Cài mới: tiếp tục chạy 02_DuLieuMau.sql để có 20 món/nước, 5 NCC, 100 hóa đơn mẫu.
