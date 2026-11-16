-- Nâng cấp database đã có; chạy trước 02_DuLieuMau.sql. Không xóa dữ liệu cũ.
USE quanlynhahang;
-- MySQL 8 không hỗ trợ ADD COLUMN IF NOT EXISTS: kiểm tra trước khi thêm cột.
SET @co_cot = (SELECT COUNT(*) FROM information_schema.COLUMNS
    WHERE TABLE_SCHEMA=DATABASE() AND TABLE_NAME='monnuoc' AND COLUMN_NAME='soluongton');
SET @lenh = IF(@co_cot=0,
    'ALTER TABLE monnuoc ADD COLUMN soluongton INT NOT NULL DEFAULT 0 CHECK (soluongton >= 0)',
    'SELECT 1');
PREPARE lenh_nang_cap FROM @lenh;
EXECUTE lenh_nang_cap;
DEALLOCATE PREPARE lenh_nang_cap;
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
