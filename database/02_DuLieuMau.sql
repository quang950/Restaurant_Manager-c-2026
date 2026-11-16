-- Dữ liệu mẫu bổ sung: 10 món ăn, 10 nước uống, 5 NCC, 5 phiếu nhập, 100 hóa đơn.
-- Cài mới: chạy quanlynhahang.sql trước. Database cũ: chạy 01_BoSungTonKho_NhapHang.sql trước.
-- Không xóa/ghi đè bản ghi hiện có. Các món tự thêm được giữ ngoài bộ 20 món mẫu.
-- Dành mã phiếu nhập 1001..1005 và mã hóa đơn 1001..1100 cho bộ mẫu.
-- Script chỉ nhập/trừ tồn khi tạo mới chứng từ, chạy lại không cộng/trừ lần nữa.
-- Cần có ít nhất một nhân viên và một bàn. Không dùng các dải mã trên cho dữ liệu tự tạo trước khi nạp mẫu.
USE quanlynhahang;
START TRANSACTION;
SET @nvmau=(SELECT manv FROM nhanvien ORDER BY manv LIMIT 1);
SET @ngaybatdau=CURRENT_DATE-INTERVAL 29 DAY;

INSERT INTO monnuoc (tenmon,loai,donvi,gia) SELECT 'Cơm chiên hải sản','Món ăn','Đĩa',65000 WHERE NOT EXISTS (SELECT 1 FROM monnuoc WHERE tenmon='Cơm chiên hải sản');
INSERT INTO monnuoc (tenmon,loai,donvi,gia) SELECT 'Bò lúc lắc','Món ăn','Đĩa',95000 WHERE NOT EXISTS (SELECT 1 FROM monnuoc WHERE tenmon='Bò lúc lắc');
INSERT INTO monnuoc (tenmon,loai,donvi,gia) SELECT 'Gà chiên nước mắm','Món ăn','Đĩa',85000 WHERE NOT EXISTS (SELECT 1 FROM monnuoc WHERE tenmon='Gà chiên nước mắm');
INSERT INTO monnuoc (tenmon,loai,donvi,gia) SELECT 'Canh chua cá','Món ăn','Tô',75000 WHERE NOT EXISTS (SELECT 1 FROM monnuoc WHERE tenmon='Canh chua cá');
INSERT INTO monnuoc (tenmon,loai,donvi,gia) SELECT 'Rau muống xào tỏi','Món ăn','Đĩa',35000 WHERE NOT EXISTS (SELECT 1 FROM monnuoc WHERE tenmon='Rau muống xào tỏi');
INSERT INTO monnuoc (tenmon,loai,donvi,gia) SELECT 'Sườn nướng mật ong','Món ăn','Đĩa',65000 WHERE NOT EXISTS (SELECT 1 FROM monnuoc WHERE tenmon='Sườn nướng mật ong');
INSERT INTO monnuoc (tenmon,loai,donvi,gia) SELECT 'Mì xào bò','Món ăn','Đĩa',55000 WHERE NOT EXISTS (SELECT 1 FROM monnuoc WHERE tenmon='Mì xào bò');
INSERT INTO monnuoc (tenmon,loai,donvi,gia) SELECT 'Tôm rang muối','Món ăn','Đĩa',95000 WHERE NOT EXISTS (SELECT 1 FROM monnuoc WHERE tenmon='Tôm rang muối');
INSERT INTO monnuoc (tenmon,loai,donvi,gia) SELECT 'Lẩu thái hải sản','Món ăn','Nồi',180000 WHERE NOT EXISTS (SELECT 1 FROM monnuoc WHERE tenmon='Lẩu thái hải sản');
INSERT INTO monnuoc (tenmon,loai,donvi,gia) SELECT 'Cá chiên xù','Món ăn','Đĩa',85000 WHERE NOT EXISTS (SELECT 1 FROM monnuoc WHERE tenmon='Cá chiên xù');
INSERT INTO monnuoc (tenmon,loai,donvi,gia) SELECT 'Trà đào','Nước uống','Ly',25000 WHERE NOT EXISTS (SELECT 1 FROM monnuoc WHERE tenmon='Trà đào');
INSERT INTO monnuoc (tenmon,loai,donvi,gia) SELECT 'Nước suối','Nước uống','Chai',12000 WHERE NOT EXISTS (SELECT 1 FROM monnuoc WHERE tenmon='Nước suối');
INSERT INTO monnuoc (tenmon,loai,donvi,gia) SELECT 'Coca Cola','Nước uống','Lon',20000 WHERE NOT EXISTS (SELECT 1 FROM monnuoc WHERE tenmon='Coca Cola');
INSERT INTO monnuoc (tenmon,loai,donvi,gia) SELECT 'Cam vắt','Nước uống','Ly',30000 WHERE NOT EXISTS (SELECT 1 FROM monnuoc WHERE tenmon='Cam vắt');
INSERT INTO monnuoc (tenmon,loai,donvi,gia) SELECT 'Nước chanh','Nước uống','Ly',25000 WHERE NOT EXISTS (SELECT 1 FROM monnuoc WHERE tenmon='Nước chanh');
INSERT INTO monnuoc (tenmon,loai,donvi,gia) SELECT 'Trà tắc','Nước uống','Ly',15000 WHERE NOT EXISTS (SELECT 1 FROM monnuoc WHERE tenmon='Trà tắc');
INSERT INTO monnuoc (tenmon,loai,donvi,gia) SELECT 'Trà sữa','Nước uống','Ly',30000 WHERE NOT EXISTS (SELECT 1 FROM monnuoc WHERE tenmon='Trà sữa');
INSERT INTO monnuoc (tenmon,loai,donvi,gia) SELECT 'Cà phê đen','Nước uống','Ly',20000 WHERE NOT EXISTS (SELECT 1 FROM monnuoc WHERE tenmon='Cà phê đen');
INSERT INTO monnuoc (tenmon,loai,donvi,gia) SELECT 'Cà phê sữa','Nước uống','Ly',25000 WHERE NOT EXISTS (SELECT 1 FROM monnuoc WHERE tenmon='Cà phê sữa');
INSERT INTO monnuoc (tenmon,loai,donvi,gia) SELECT '7 Up','Nước uống','Lon',20000 WHERE NOT EXISTS (SELECT 1 FROM monnuoc WHERE tenmon='7 Up');
INSERT INTO nhacungcap (tenncc,sdt,diachi) SELECT 'Thực phẩm An Nhiên','0281234567','12 Nguyễn Trãi, TP.HCM' WHERE NOT EXISTS (SELECT 1 FROM nhacungcap WHERE tenncc='Thực phẩm An Nhiên');
INSERT INTO nhacungcap (tenncc,sdt,diachi) SELECT 'Đồ uống Minh Phát','0287654321','45 Lê Lợi, TP.HCM' WHERE NOT EXISTS (SELECT 1 FROM nhacungcap WHERE tenncc='Đồ uống Minh Phát');
INSERT INTO nhacungcap (tenncc,sdt,diachi) SELECT 'Thực phẩm Hoàng Gia','0903456789','86 Trần Hưng Đạo, TP.HCM' WHERE NOT EXISTS (SELECT 1 FROM nhacungcap WHERE tenncc='Thực phẩm Hoàng Gia');
INSERT INTO nhacungcap (tenncc,sdt,diachi) SELECT 'Nông sản Xanh','0914567890','25 Điện Biên Phủ, TP.HCM' WHERE NOT EXISTS (SELECT 1 FROM nhacungcap WHERE tenncc='Nông sản Xanh');
INSERT INTO nhacungcap (tenncc,sdt,diachi) SELECT 'Thực phẩm Phú An','0925678901','101 Phan Văn Trị, TP.HCM' WHERE NOT EXISTS (SELECT 1 FROM nhacungcap WHERE tenncc='Thực phẩm Phú An');

-- Phiếu nhập mẫu 1001: 500 đơn vị cho mỗi mặt hàng trong phiếu.
INSERT INTO phieunhap (mapn,mancc,manv,ngaynhap,tongtien,maxacnhan) SELECT 1001,(SELECT MIN(mancc) FROM nhacungcap WHERE tenncc='Thực phẩm An Nhiên'),@nvmau,@ngaybatdau-INTERVAL 1 DAY+INTERVAL 8 HOUR,1,'MAU_NHAP_HANG_20261018_000000001' WHERE NOT EXISTS (SELECT 1 FROM phieunhap WHERE mapn=1001);
SET @themphieu=ROW_COUNT();
INSERT INTO chitietphieunhap (mapn,mamon,soluong,dongianhap) SELECT 1001,mamon,500,GREATEST(1,gia*60 DIV 100) FROM monnuoc WHERE tenmon='Cơm chiên hải sản' AND @themphieu=1;
UPDATE monnuoc SET soluongton=soluongton+500 WHERE tenmon='Cơm chiên hải sản' AND @themphieu=1;
INSERT INTO chitietphieunhap (mapn,mamon,soluong,dongianhap) SELECT 1001,mamon,500,GREATEST(1,gia*60 DIV 100) FROM monnuoc WHERE tenmon='Sườn nướng mật ong' AND @themphieu=1;
UPDATE monnuoc SET soluongton=soluongton+500 WHERE tenmon='Sườn nướng mật ong' AND @themphieu=1;
INSERT INTO chitietphieunhap (mapn,mamon,soluong,dongianhap) SELECT 1001,mamon,500,GREATEST(1,gia*60 DIV 100) FROM monnuoc WHERE tenmon='Trà đào' AND @themphieu=1;
UPDATE monnuoc SET soluongton=soluongton+500 WHERE tenmon='Trà đào' AND @themphieu=1;
INSERT INTO chitietphieunhap (mapn,mamon,soluong,dongianhap) SELECT 1001,mamon,500,GREATEST(1,gia*60 DIV 100) FROM monnuoc WHERE tenmon='Trà tắc' AND @themphieu=1;
UPDATE monnuoc SET soluongton=soluongton+500 WHERE tenmon='Trà tắc' AND @themphieu=1;
UPDATE phieunhap SET tongtien=(SELECT SUM(soluong*dongianhap) FROM chitietphieunhap WHERE mapn=1001) WHERE mapn=1001 AND @themphieu=1;

-- Phiếu nhập mẫu 1002: 500 đơn vị cho mỗi mặt hàng trong phiếu.
INSERT INTO phieunhap (mapn,mancc,manv,ngaynhap,tongtien,maxacnhan) SELECT 1002,(SELECT MIN(mancc) FROM nhacungcap WHERE tenncc='Đồ uống Minh Phát'),@nvmau,@ngaybatdau-INTERVAL 1 DAY+INTERVAL 9 HOUR,1,'MAU_NHAP_HANG_20261018_000000002' WHERE NOT EXISTS (SELECT 1 FROM phieunhap WHERE mapn=1002);
SET @themphieu=ROW_COUNT();
INSERT INTO chitietphieunhap (mapn,mamon,soluong,dongianhap) SELECT 1002,mamon,500,GREATEST(1,gia*60 DIV 100) FROM monnuoc WHERE tenmon='Bò lúc lắc' AND @themphieu=1;
UPDATE monnuoc SET soluongton=soluongton+500 WHERE tenmon='Bò lúc lắc' AND @themphieu=1;
INSERT INTO chitietphieunhap (mapn,mamon,soluong,dongianhap) SELECT 1002,mamon,500,GREATEST(1,gia*60 DIV 100) FROM monnuoc WHERE tenmon='Mì xào bò' AND @themphieu=1;
UPDATE monnuoc SET soluongton=soluongton+500 WHERE tenmon='Mì xào bò' AND @themphieu=1;
INSERT INTO chitietphieunhap (mapn,mamon,soluong,dongianhap) SELECT 1002,mamon,500,GREATEST(1,gia*60 DIV 100) FROM monnuoc WHERE tenmon='Nước suối' AND @themphieu=1;
UPDATE monnuoc SET soluongton=soluongton+500 WHERE tenmon='Nước suối' AND @themphieu=1;
INSERT INTO chitietphieunhap (mapn,mamon,soluong,dongianhap) SELECT 1002,mamon,500,GREATEST(1,gia*60 DIV 100) FROM monnuoc WHERE tenmon='Trà sữa' AND @themphieu=1;
UPDATE monnuoc SET soluongton=soluongton+500 WHERE tenmon='Trà sữa' AND @themphieu=1;
UPDATE phieunhap SET tongtien=(SELECT SUM(soluong*dongianhap) FROM chitietphieunhap WHERE mapn=1002) WHERE mapn=1002 AND @themphieu=1;

-- Phiếu nhập mẫu 1003: 500 đơn vị cho mỗi mặt hàng trong phiếu.
INSERT INTO phieunhap (mapn,mancc,manv,ngaynhap,tongtien,maxacnhan) SELECT 1003,(SELECT MIN(mancc) FROM nhacungcap WHERE tenncc='Thực phẩm Hoàng Gia'),@nvmau,@ngaybatdau-INTERVAL 1 DAY+INTERVAL 10 HOUR,1,'MAU_NHAP_HANG_20261018_000000003' WHERE NOT EXISTS (SELECT 1 FROM phieunhap WHERE mapn=1003);
SET @themphieu=ROW_COUNT();
INSERT INTO chitietphieunhap (mapn,mamon,soluong,dongianhap) SELECT 1003,mamon,500,GREATEST(1,gia*60 DIV 100) FROM monnuoc WHERE tenmon='Gà chiên nước mắm' AND @themphieu=1;
UPDATE monnuoc SET soluongton=soluongton+500 WHERE tenmon='Gà chiên nước mắm' AND @themphieu=1;
INSERT INTO chitietphieunhap (mapn,mamon,soluong,dongianhap) SELECT 1003,mamon,500,GREATEST(1,gia*60 DIV 100) FROM monnuoc WHERE tenmon='Tôm rang muối' AND @themphieu=1;
UPDATE monnuoc SET soluongton=soluongton+500 WHERE tenmon='Tôm rang muối' AND @themphieu=1;
INSERT INTO chitietphieunhap (mapn,mamon,soluong,dongianhap) SELECT 1003,mamon,500,GREATEST(1,gia*60 DIV 100) FROM monnuoc WHERE tenmon='Coca Cola' AND @themphieu=1;
UPDATE monnuoc SET soluongton=soluongton+500 WHERE tenmon='Coca Cola' AND @themphieu=1;
INSERT INTO chitietphieunhap (mapn,mamon,soluong,dongianhap) SELECT 1003,mamon,500,GREATEST(1,gia*60 DIV 100) FROM monnuoc WHERE tenmon='Cà phê đen' AND @themphieu=1;
UPDATE monnuoc SET soluongton=soluongton+500 WHERE tenmon='Cà phê đen' AND @themphieu=1;
UPDATE phieunhap SET tongtien=(SELECT SUM(soluong*dongianhap) FROM chitietphieunhap WHERE mapn=1003) WHERE mapn=1003 AND @themphieu=1;

-- Phiếu nhập mẫu 1004: 500 đơn vị cho mỗi mặt hàng trong phiếu.
INSERT INTO phieunhap (mapn,mancc,manv,ngaynhap,tongtien,maxacnhan) SELECT 1004,(SELECT MIN(mancc) FROM nhacungcap WHERE tenncc='Nông sản Xanh'),@nvmau,@ngaybatdau-INTERVAL 1 DAY+INTERVAL 11 HOUR,1,'MAU_NHAP_HANG_20261018_000000004' WHERE NOT EXISTS (SELECT 1 FROM phieunhap WHERE mapn=1004);
SET @themphieu=ROW_COUNT();
INSERT INTO chitietphieunhap (mapn,mamon,soluong,dongianhap) SELECT 1004,mamon,500,GREATEST(1,gia*60 DIV 100) FROM monnuoc WHERE tenmon='Canh chua cá' AND @themphieu=1;
UPDATE monnuoc SET soluongton=soluongton+500 WHERE tenmon='Canh chua cá' AND @themphieu=1;
INSERT INTO chitietphieunhap (mapn,mamon,soluong,dongianhap) SELECT 1004,mamon,500,GREATEST(1,gia*60 DIV 100) FROM monnuoc WHERE tenmon='Lẩu thái hải sản' AND @themphieu=1;
UPDATE monnuoc SET soluongton=soluongton+500 WHERE tenmon='Lẩu thái hải sản' AND @themphieu=1;
INSERT INTO chitietphieunhap (mapn,mamon,soluong,dongianhap) SELECT 1004,mamon,500,GREATEST(1,gia*60 DIV 100) FROM monnuoc WHERE tenmon='Cam vắt' AND @themphieu=1;
UPDATE monnuoc SET soluongton=soluongton+500 WHERE tenmon='Cam vắt' AND @themphieu=1;
INSERT INTO chitietphieunhap (mapn,mamon,soluong,dongianhap) SELECT 1004,mamon,500,GREATEST(1,gia*60 DIV 100) FROM monnuoc WHERE tenmon='Cà phê sữa' AND @themphieu=1;
UPDATE monnuoc SET soluongton=soluongton+500 WHERE tenmon='Cà phê sữa' AND @themphieu=1;
UPDATE phieunhap SET tongtien=(SELECT SUM(soluong*dongianhap) FROM chitietphieunhap WHERE mapn=1004) WHERE mapn=1004 AND @themphieu=1;

-- Phiếu nhập mẫu 1005: 500 đơn vị cho mỗi mặt hàng trong phiếu.
INSERT INTO phieunhap (mapn,mancc,manv,ngaynhap,tongtien,maxacnhan) SELECT 1005,(SELECT MIN(mancc) FROM nhacungcap WHERE tenncc='Thực phẩm Phú An'),@nvmau,@ngaybatdau-INTERVAL 1 DAY+INTERVAL 12 HOUR,1,'MAU_NHAP_HANG_20261018_000000005' WHERE NOT EXISTS (SELECT 1 FROM phieunhap WHERE mapn=1005);
SET @themphieu=ROW_COUNT();
INSERT INTO chitietphieunhap (mapn,mamon,soluong,dongianhap) SELECT 1005,mamon,500,GREATEST(1,gia*60 DIV 100) FROM monnuoc WHERE tenmon='Rau muống xào tỏi' AND @themphieu=1;
UPDATE monnuoc SET soluongton=soluongton+500 WHERE tenmon='Rau muống xào tỏi' AND @themphieu=1;
INSERT INTO chitietphieunhap (mapn,mamon,soluong,dongianhap) SELECT 1005,mamon,500,GREATEST(1,gia*60 DIV 100) FROM monnuoc WHERE tenmon='Cá chiên xù' AND @themphieu=1;
UPDATE monnuoc SET soluongton=soluongton+500 WHERE tenmon='Cá chiên xù' AND @themphieu=1;
INSERT INTO chitietphieunhap (mapn,mamon,soluong,dongianhap) SELECT 1005,mamon,500,GREATEST(1,gia*60 DIV 100) FROM monnuoc WHERE tenmon='Nước chanh' AND @themphieu=1;
UPDATE monnuoc SET soluongton=soluongton+500 WHERE tenmon='Nước chanh' AND @themphieu=1;
INSERT INTO chitietphieunhap (mapn,mamon,soluong,dongianhap) SELECT 1005,mamon,500,GREATEST(1,gia*60 DIV 100) FROM monnuoc WHERE tenmon='7 Up' AND @themphieu=1;
UPDATE monnuoc SET soluongton=soluongton+500 WHERE tenmon='7 Up' AND @themphieu=1;
UPDATE phieunhap SET tongtien=(SELECT SUM(soluong*dongianhap) FROM chitietphieunhap WHERE mapn=1005) WHERE mapn=1005 AND @themphieu=1;

-- Hóa đơn mẫu 001: 1 dòng chi tiết.
INSERT INTO hoadon (mahd,maban,manv,ngaylap,ngaythanhtoan,trangthai,tongtien) SELECT 1001,(SELECT maban FROM ban ORDER BY maban LIMIT 1 OFFSET 0),@nvmau,@ngaybatdau+INTERVAL 0 DAY+INTERVAL 8 HOUR+INTERVAL 0 MINUTE,@ngaybatdau+INTERVAL 0 DAY+INTERVAL 8 HOUR+INTERVAL 20 MINUTE,'Đã thanh toán',0 WHERE NOT EXISTS (SELECT 1 FROM hoadon WHERE mahd=1001);
SET @themhoadon=ROW_COUNT();
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1001,mamon,1,gia FROM monnuoc WHERE tenmon='Cơm chiên hải sản' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-1 WHERE tenmon='Cơm chiên hải sản' AND @themhoadon=1;
UPDATE hoadon SET tongtien=(SELECT SUM(soluong*dongia) FROM chitiethoadon WHERE mahd=1001) WHERE mahd=1001 AND @themhoadon=1;

-- Hóa đơn mẫu 002: 2 dòng chi tiết.
INSERT INTO hoadon (mahd,maban,manv,ngaylap,ngaythanhtoan,trangthai,tongtien) SELECT 1002,(SELECT maban FROM ban ORDER BY maban LIMIT 1 OFFSET 1),@nvmau,@ngaybatdau+INTERVAL 0 DAY+INTERVAL 8 HOUR+INTERVAL 10 MINUTE,@ngaybatdau+INTERVAL 0 DAY+INTERVAL 8 HOUR+INTERVAL 30 MINUTE,'Đã thanh toán',0 WHERE NOT EXISTS (SELECT 1 FROM hoadon WHERE mahd=1002);
SET @themhoadon=ROW_COUNT();
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1002,mamon,2,gia FROM monnuoc WHERE tenmon='Tôm rang muối' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-2 WHERE tenmon='Tôm rang muối' AND @themhoadon=1;
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1002,mamon,3,gia FROM monnuoc WHERE tenmon='Trà sữa' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-3 WHERE tenmon='Trà sữa' AND @themhoadon=1;
UPDATE hoadon SET tongtien=(SELECT SUM(soluong*dongia) FROM chitiethoadon WHERE mahd=1002) WHERE mahd=1002 AND @themhoadon=1;

-- Hóa đơn mẫu 003: 3 dòng chi tiết.
INSERT INTO hoadon (mahd,maban,manv,ngaylap,ngaythanhtoan,trangthai,tongtien) SELECT 1003,(SELECT maban FROM ban ORDER BY maban LIMIT 1 OFFSET 2),@nvmau,@ngaybatdau+INTERVAL 0 DAY+INTERVAL 8 HOUR+INTERVAL 20 MINUTE,@ngaybatdau+INTERVAL 0 DAY+INTERVAL 8 HOUR+INTERVAL 40 MINUTE,'Đã thanh toán',0 WHERE NOT EXISTS (SELECT 1 FROM hoadon WHERE mahd=1003);
SET @themhoadon=ROW_COUNT();
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1003,mamon,3,gia FROM monnuoc WHERE tenmon='Nước chanh' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-3 WHERE tenmon='Nước chanh' AND @themhoadon=1;
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1003,mamon,1,gia FROM monnuoc WHERE tenmon='Canh chua cá' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-1 WHERE tenmon='Canh chua cá' AND @themhoadon=1;
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1003,mamon,2,gia FROM monnuoc WHERE tenmon='Coca Cola' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-2 WHERE tenmon='Coca Cola' AND @themhoadon=1;
UPDATE hoadon SET tongtien=(SELECT SUM(soluong*dongia) FROM chitiethoadon WHERE mahd=1003) WHERE mahd=1003 AND @themhoadon=1;

-- Hóa đơn mẫu 004: 1 dòng chi tiết.
INSERT INTO hoadon (mahd,maban,manv,ngaylap,ngaythanhtoan,trangthai,tongtien) SELECT 1004,(SELECT maban FROM ban ORDER BY maban LIMIT 1 OFFSET 3),@nvmau,@ngaybatdau+INTERVAL 0 DAY+INTERVAL 8 HOUR+INTERVAL 0 MINUTE,@ngaybatdau+INTERVAL 0 DAY+INTERVAL 8 HOUR+INTERVAL 20 MINUTE,'Đã thanh toán',0 WHERE NOT EXISTS (SELECT 1 FROM hoadon WHERE mahd=1004);
SET @themhoadon=ROW_COUNT();
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1004,mamon,1,gia FROM monnuoc WHERE tenmon='Bò lúc lắc' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-1 WHERE tenmon='Bò lúc lắc' AND @themhoadon=1;
UPDATE hoadon SET tongtien=(SELECT SUM(soluong*dongia) FROM chitiethoadon WHERE mahd=1004) WHERE mahd=1004 AND @themhoadon=1;

-- Hóa đơn mẫu 005: 2 dòng chi tiết.
INSERT INTO hoadon (mahd,maban,manv,ngaylap,ngaythanhtoan,trangthai,tongtien) SELECT 1005,(SELECT maban FROM ban ORDER BY maban LIMIT 1 OFFSET 4),@nvmau,@ngaybatdau+INTERVAL 1 DAY+INTERVAL 8 HOUR+INTERVAL 10 MINUTE,@ngaybatdau+INTERVAL 1 DAY+INTERVAL 8 HOUR+INTERVAL 30 MINUTE,'Đã thanh toán',0 WHERE NOT EXISTS (SELECT 1 FROM hoadon WHERE mahd=1005);
SET @themhoadon=ROW_COUNT();
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1005,mamon,2,gia FROM monnuoc WHERE tenmon='Lẩu thái hải sản' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-2 WHERE tenmon='Lẩu thái hải sản' AND @themhoadon=1;
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1005,mamon,3,gia FROM monnuoc WHERE tenmon='Cà phê đen' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-3 WHERE tenmon='Cà phê đen' AND @themhoadon=1;
UPDATE hoadon SET tongtien=(SELECT SUM(soluong*dongia) FROM chitiethoadon WHERE mahd=1005) WHERE mahd=1005 AND @themhoadon=1;

-- Hóa đơn mẫu 006: 3 dòng chi tiết.
INSERT INTO hoadon (mahd,maban,manv,ngaylap,ngaythanhtoan,trangthai,tongtien) SELECT 1006,(SELECT maban FROM ban ORDER BY maban LIMIT 1 OFFSET 5),@nvmau,@ngaybatdau+INTERVAL 1 DAY+INTERVAL 8 HOUR+INTERVAL 20 MINUTE,@ngaybatdau+INTERVAL 1 DAY+INTERVAL 8 HOUR+INTERVAL 40 MINUTE,'Đã thanh toán',0 WHERE NOT EXISTS (SELECT 1 FROM hoadon WHERE mahd=1006);
SET @themhoadon=ROW_COUNT();
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1006,mamon,3,gia FROM monnuoc WHERE tenmon='Trà tắc' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-3 WHERE tenmon='Trà tắc' AND @themhoadon=1;
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1006,mamon,1,gia FROM monnuoc WHERE tenmon='Rau muống xào tỏi' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-1 WHERE tenmon='Rau muống xào tỏi' AND @themhoadon=1;
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1006,mamon,2,gia FROM monnuoc WHERE tenmon='Cam vắt' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-2 WHERE tenmon='Cam vắt' AND @themhoadon=1;
UPDATE hoadon SET tongtien=(SELECT SUM(soluong*dongia) FROM chitiethoadon WHERE mahd=1006) WHERE mahd=1006 AND @themhoadon=1;

-- Hóa đơn mẫu 007: 1 dòng chi tiết.
INSERT INTO hoadon (mahd,maban,manv,ngaylap,ngaythanhtoan,trangthai,tongtien) SELECT 1007,(SELECT maban FROM ban ORDER BY maban LIMIT 1 OFFSET 0),@nvmau,@ngaybatdau+INTERVAL 1 DAY+INTERVAL 8 HOUR+INTERVAL 0 MINUTE,@ngaybatdau+INTERVAL 1 DAY+INTERVAL 8 HOUR+INTERVAL 20 MINUTE,'Đã thanh toán',0 WHERE NOT EXISTS (SELECT 1 FROM hoadon WHERE mahd=1007);
SET @themhoadon=ROW_COUNT();
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1007,mamon,1,gia FROM monnuoc WHERE tenmon='Gà chiên nước mắm' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-1 WHERE tenmon='Gà chiên nước mắm' AND @themhoadon=1;
UPDATE hoadon SET tongtien=(SELECT SUM(soluong*dongia) FROM chitiethoadon WHERE mahd=1007) WHERE mahd=1007 AND @themhoadon=1;

-- Hóa đơn mẫu 008: 2 dòng chi tiết.
INSERT INTO hoadon (mahd,maban,manv,ngaylap,ngaythanhtoan,trangthai,tongtien) SELECT 1008,(SELECT maban FROM ban ORDER BY maban LIMIT 1 OFFSET 1),@nvmau,@ngaybatdau+INTERVAL 2 DAY+INTERVAL 8 HOUR+INTERVAL 10 MINUTE,@ngaybatdau+INTERVAL 2 DAY+INTERVAL 8 HOUR+INTERVAL 30 MINUTE,'Đã thanh toán',0 WHERE NOT EXISTS (SELECT 1 FROM hoadon WHERE mahd=1008);
SET @themhoadon=ROW_COUNT();
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1008,mamon,2,gia FROM monnuoc WHERE tenmon='Cá chiên xù' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-2 WHERE tenmon='Cá chiên xù' AND @themhoadon=1;
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1008,mamon,3,gia FROM monnuoc WHERE tenmon='Cà phê sữa' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-3 WHERE tenmon='Cà phê sữa' AND @themhoadon=1;
UPDATE hoadon SET tongtien=(SELECT SUM(soluong*dongia) FROM chitiethoadon WHERE mahd=1008) WHERE mahd=1008 AND @themhoadon=1;

-- Hóa đơn mẫu 009: 3 dòng chi tiết.
INSERT INTO hoadon (mahd,maban,manv,ngaylap,ngaythanhtoan,trangthai,tongtien) SELECT 1009,(SELECT maban FROM ban ORDER BY maban LIMIT 1 OFFSET 2),@nvmau,@ngaybatdau+INTERVAL 2 DAY+INTERVAL 8 HOUR+INTERVAL 20 MINUTE,@ngaybatdau+INTERVAL 2 DAY+INTERVAL 8 HOUR+INTERVAL 40 MINUTE,'Đã thanh toán',0 WHERE NOT EXISTS (SELECT 1 FROM hoadon WHERE mahd=1009);
SET @themhoadon=ROW_COUNT();
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1009,mamon,3,gia FROM monnuoc WHERE tenmon='Trà sữa' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-3 WHERE tenmon='Trà sữa' AND @themhoadon=1;
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1009,mamon,1,gia FROM monnuoc WHERE tenmon='Sườn nướng mật ong' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-1 WHERE tenmon='Sườn nướng mật ong' AND @themhoadon=1;
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1009,mamon,2,gia FROM monnuoc WHERE tenmon='Nước chanh' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-2 WHERE tenmon='Nước chanh' AND @themhoadon=1;
UPDATE hoadon SET tongtien=(SELECT SUM(soluong*dongia) FROM chitiethoadon WHERE mahd=1009) WHERE mahd=1009 AND @themhoadon=1;

-- Hóa đơn mẫu 010: 1 dòng chi tiết.
INSERT INTO hoadon (mahd,maban,manv,ngaylap,ngaythanhtoan,trangthai,tongtien) SELECT 1010,(SELECT maban FROM ban ORDER BY maban LIMIT 1 OFFSET 3),@nvmau,@ngaybatdau+INTERVAL 2 DAY+INTERVAL 8 HOUR+INTERVAL 0 MINUTE,@ngaybatdau+INTERVAL 2 DAY+INTERVAL 8 HOUR+INTERVAL 20 MINUTE,'Đã thanh toán',0 WHERE NOT EXISTS (SELECT 1 FROM hoadon WHERE mahd=1010);
SET @themhoadon=ROW_COUNT();
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1010,mamon,1,gia FROM monnuoc WHERE tenmon='Canh chua cá' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-1 WHERE tenmon='Canh chua cá' AND @themhoadon=1;
UPDATE hoadon SET tongtien=(SELECT SUM(soluong*dongia) FROM chitiethoadon WHERE mahd=1010) WHERE mahd=1010 AND @themhoadon=1;

-- Hóa đơn mẫu 011: 2 dòng chi tiết.
INSERT INTO hoadon (mahd,maban,manv,ngaylap,ngaythanhtoan,trangthai,tongtien) SELECT 1011,(SELECT maban FROM ban ORDER BY maban LIMIT 1 OFFSET 4),@nvmau,@ngaybatdau+INTERVAL 3 DAY+INTERVAL 8 HOUR+INTERVAL 10 MINUTE,@ngaybatdau+INTERVAL 3 DAY+INTERVAL 8 HOUR+INTERVAL 30 MINUTE,'Đã thanh toán',0 WHERE NOT EXISTS (SELECT 1 FROM hoadon WHERE mahd=1011);
SET @themhoadon=ROW_COUNT();
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1011,mamon,2,gia FROM monnuoc WHERE tenmon='Trà đào' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-2 WHERE tenmon='Trà đào' AND @themhoadon=1;
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1011,mamon,3,gia FROM monnuoc WHERE tenmon='7 Up' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-3 WHERE tenmon='7 Up' AND @themhoadon=1;
UPDATE hoadon SET tongtien=(SELECT SUM(soluong*dongia) FROM chitiethoadon WHERE mahd=1011) WHERE mahd=1011 AND @themhoadon=1;

-- Hóa đơn mẫu 012: 3 dòng chi tiết.
INSERT INTO hoadon (mahd,maban,manv,ngaylap,ngaythanhtoan,trangthai,tongtien) SELECT 1012,(SELECT maban FROM ban ORDER BY maban LIMIT 1 OFFSET 5),@nvmau,@ngaybatdau+INTERVAL 3 DAY+INTERVAL 8 HOUR+INTERVAL 20 MINUTE,@ngaybatdau+INTERVAL 3 DAY+INTERVAL 8 HOUR+INTERVAL 40 MINUTE,'Đã thanh toán',0 WHERE NOT EXISTS (SELECT 1 FROM hoadon WHERE mahd=1012);
SET @themhoadon=ROW_COUNT();
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1012,mamon,3,gia FROM monnuoc WHERE tenmon='Cà phê đen' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-3 WHERE tenmon='Cà phê đen' AND @themhoadon=1;
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1012,mamon,1,gia FROM monnuoc WHERE tenmon='Mì xào bò' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-1 WHERE tenmon='Mì xào bò' AND @themhoadon=1;
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1012,mamon,2,gia FROM monnuoc WHERE tenmon='Trà tắc' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-2 WHERE tenmon='Trà tắc' AND @themhoadon=1;
UPDATE hoadon SET tongtien=(SELECT SUM(soluong*dongia) FROM chitiethoadon WHERE mahd=1012) WHERE mahd=1012 AND @themhoadon=1;

-- Hóa đơn mẫu 013: 1 dòng chi tiết.
INSERT INTO hoadon (mahd,maban,manv,ngaylap,ngaythanhtoan,trangthai,tongtien) SELECT 1013,(SELECT maban FROM ban ORDER BY maban LIMIT 1 OFFSET 0),@nvmau,@ngaybatdau+INTERVAL 3 DAY+INTERVAL 8 HOUR+INTERVAL 0 MINUTE,@ngaybatdau+INTERVAL 3 DAY+INTERVAL 8 HOUR+INTERVAL 20 MINUTE,'Đã thanh toán',0 WHERE NOT EXISTS (SELECT 1 FROM hoadon WHERE mahd=1013);
SET @themhoadon=ROW_COUNT();
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1013,mamon,1,gia FROM monnuoc WHERE tenmon='Rau muống xào tỏi' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-1 WHERE tenmon='Rau muống xào tỏi' AND @themhoadon=1;
UPDATE hoadon SET tongtien=(SELECT SUM(soluong*dongia) FROM chitiethoadon WHERE mahd=1013) WHERE mahd=1013 AND @themhoadon=1;

-- Hóa đơn mẫu 014: 2 dòng chi tiết.
INSERT INTO hoadon (mahd,maban,manv,ngaylap,ngaythanhtoan,trangthai,tongtien) SELECT 1014,(SELECT maban FROM ban ORDER BY maban LIMIT 1 OFFSET 1),@nvmau,@ngaybatdau+INTERVAL 3 DAY+INTERVAL 8 HOUR+INTERVAL 10 MINUTE,@ngaybatdau+INTERVAL 3 DAY+INTERVAL 8 HOUR+INTERVAL 30 MINUTE,'Đã thanh toán',0 WHERE NOT EXISTS (SELECT 1 FROM hoadon WHERE mahd=1014);
SET @themhoadon=ROW_COUNT();
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1014,mamon,2,gia FROM monnuoc WHERE tenmon='Nước suối' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-2 WHERE tenmon='Nước suối' AND @themhoadon=1;
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1014,mamon,3,gia FROM monnuoc WHERE tenmon='Cơm chiên hải sản' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-3 WHERE tenmon='Cơm chiên hải sản' AND @themhoadon=1;
UPDATE hoadon SET tongtien=(SELECT SUM(soluong*dongia) FROM chitiethoadon WHERE mahd=1014) WHERE mahd=1014 AND @themhoadon=1;

-- Hóa đơn mẫu 015: 3 dòng chi tiết.
INSERT INTO hoadon (mahd,maban,manv,ngaylap,ngaythanhtoan,trangthai,tongtien) SELECT 1015,(SELECT maban FROM ban ORDER BY maban LIMIT 1 OFFSET 2),@nvmau,@ngaybatdau+INTERVAL 4 DAY+INTERVAL 8 HOUR+INTERVAL 20 MINUTE,@ngaybatdau+INTERVAL 4 DAY+INTERVAL 8 HOUR+INTERVAL 40 MINUTE,'Đã thanh toán',0 WHERE NOT EXISTS (SELECT 1 FROM hoadon WHERE mahd=1015);
SET @themhoadon=ROW_COUNT();
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1015,mamon,3,gia FROM monnuoc WHERE tenmon='Cà phê sữa' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-3 WHERE tenmon='Cà phê sữa' AND @themhoadon=1;
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1015,mamon,1,gia FROM monnuoc WHERE tenmon='Tôm rang muối' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-1 WHERE tenmon='Tôm rang muối' AND @themhoadon=1;
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1015,mamon,2,gia FROM monnuoc WHERE tenmon='Trà sữa' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-2 WHERE tenmon='Trà sữa' AND @themhoadon=1;
UPDATE hoadon SET tongtien=(SELECT SUM(soluong*dongia) FROM chitiethoadon WHERE mahd=1015) WHERE mahd=1015 AND @themhoadon=1;

-- Hóa đơn mẫu 016: 1 dòng chi tiết.
INSERT INTO hoadon (mahd,maban,manv,ngaylap,ngaythanhtoan,trangthai,tongtien) SELECT 1016,(SELECT maban FROM ban ORDER BY maban LIMIT 1 OFFSET 3),@nvmau,@ngaybatdau+INTERVAL 4 DAY+INTERVAL 8 HOUR+INTERVAL 0 MINUTE,@ngaybatdau+INTERVAL 4 DAY+INTERVAL 8 HOUR+INTERVAL 20 MINUTE,'Đã thanh toán',0 WHERE NOT EXISTS (SELECT 1 FROM hoadon WHERE mahd=1016);
SET @themhoadon=ROW_COUNT();
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1016,mamon,1,gia FROM monnuoc WHERE tenmon='Sườn nướng mật ong' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-1 WHERE tenmon='Sườn nướng mật ong' AND @themhoadon=1;
UPDATE hoadon SET tongtien=(SELECT SUM(soluong*dongia) FROM chitiethoadon WHERE mahd=1016) WHERE mahd=1016 AND @themhoadon=1;

-- Hóa đơn mẫu 017: 2 dòng chi tiết.
INSERT INTO hoadon (mahd,maban,manv,ngaylap,ngaythanhtoan,trangthai,tongtien) SELECT 1017,(SELECT maban FROM ban ORDER BY maban LIMIT 1 OFFSET 4),@nvmau,@ngaybatdau+INTERVAL 4 DAY+INTERVAL 8 HOUR+INTERVAL 10 MINUTE,@ngaybatdau+INTERVAL 4 DAY+INTERVAL 8 HOUR+INTERVAL 30 MINUTE,'Đã thanh toán',0 WHERE NOT EXISTS (SELECT 1 FROM hoadon WHERE mahd=1017);
SET @themhoadon=ROW_COUNT();
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1017,mamon,2,gia FROM monnuoc WHERE tenmon='Coca Cola' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-2 WHERE tenmon='Coca Cola' AND @themhoadon=1;
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1017,mamon,3,gia FROM monnuoc WHERE tenmon='Bò lúc lắc' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-3 WHERE tenmon='Bò lúc lắc' AND @themhoadon=1;
UPDATE hoadon SET tongtien=(SELECT SUM(soluong*dongia) FROM chitiethoadon WHERE mahd=1017) WHERE mahd=1017 AND @themhoadon=1;

-- Hóa đơn mẫu 018: 3 dòng chi tiết.
INSERT INTO hoadon (mahd,maban,manv,ngaylap,ngaythanhtoan,trangthai,tongtien) SELECT 1018,(SELECT maban FROM ban ORDER BY maban LIMIT 1 OFFSET 5),@nvmau,@ngaybatdau+INTERVAL 5 DAY+INTERVAL 8 HOUR+INTERVAL 20 MINUTE,@ngaybatdau+INTERVAL 5 DAY+INTERVAL 8 HOUR+INTERVAL 40 MINUTE,'Đã thanh toán',0 WHERE NOT EXISTS (SELECT 1 FROM hoadon WHERE mahd=1018);
SET @themhoadon=ROW_COUNT();
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1018,mamon,3,gia FROM monnuoc WHERE tenmon='7 Up' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-3 WHERE tenmon='7 Up' AND @themhoadon=1;
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1018,mamon,1,gia FROM monnuoc WHERE tenmon='Lẩu thái hải sản' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-1 WHERE tenmon='Lẩu thái hải sản' AND @themhoadon=1;
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1018,mamon,2,gia FROM monnuoc WHERE tenmon='Cà phê đen' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-2 WHERE tenmon='Cà phê đen' AND @themhoadon=1;
UPDATE hoadon SET tongtien=(SELECT SUM(soluong*dongia) FROM chitiethoadon WHERE mahd=1018) WHERE mahd=1018 AND @themhoadon=1;

-- Hóa đơn mẫu 019: 1 dòng chi tiết.
INSERT INTO hoadon (mahd,maban,manv,ngaylap,ngaythanhtoan,trangthai,tongtien) SELECT 1019,(SELECT maban FROM ban ORDER BY maban LIMIT 1 OFFSET 0),@nvmau,@ngaybatdau+INTERVAL 5 DAY+INTERVAL 8 HOUR+INTERVAL 0 MINUTE,@ngaybatdau+INTERVAL 5 DAY+INTERVAL 8 HOUR+INTERVAL 20 MINUTE,'Đã thanh toán',0 WHERE NOT EXISTS (SELECT 1 FROM hoadon WHERE mahd=1019);
SET @themhoadon=ROW_COUNT();
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1019,mamon,1,gia FROM monnuoc WHERE tenmon='Mì xào bò' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-1 WHERE tenmon='Mì xào bò' AND @themhoadon=1;
UPDATE hoadon SET tongtien=(SELECT SUM(soluong*dongia) FROM chitiethoadon WHERE mahd=1019) WHERE mahd=1019 AND @themhoadon=1;

-- Hóa đơn mẫu 020: 2 dòng chi tiết.
INSERT INTO hoadon (mahd,maban,manv,ngaylap,ngaythanhtoan,trangthai,tongtien) SELECT 1020,(SELECT maban FROM ban ORDER BY maban LIMIT 1 OFFSET 1),@nvmau,@ngaybatdau+INTERVAL 5 DAY+INTERVAL 8 HOUR+INTERVAL 10 MINUTE,@ngaybatdau+INTERVAL 5 DAY+INTERVAL 8 HOUR+INTERVAL 30 MINUTE,'Đã thanh toán',0 WHERE NOT EXISTS (SELECT 1 FROM hoadon WHERE mahd=1020);
SET @themhoadon=ROW_COUNT();
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1020,mamon,2,gia FROM monnuoc WHERE tenmon='Cam vắt' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-2 WHERE tenmon='Cam vắt' AND @themhoadon=1;
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1020,mamon,3,gia FROM monnuoc WHERE tenmon='Gà chiên nước mắm' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-3 WHERE tenmon='Gà chiên nước mắm' AND @themhoadon=1;
UPDATE hoadon SET tongtien=(SELECT SUM(soluong*dongia) FROM chitiethoadon WHERE mahd=1020) WHERE mahd=1020 AND @themhoadon=1;

-- Hóa đơn mẫu 021: 3 dòng chi tiết.
INSERT INTO hoadon (mahd,maban,manv,ngaylap,ngaythanhtoan,trangthai,tongtien) SELECT 1021,(SELECT maban FROM ban ORDER BY maban LIMIT 1 OFFSET 2),@nvmau,@ngaybatdau+INTERVAL 6 DAY+INTERVAL 8 HOUR+INTERVAL 20 MINUTE,@ngaybatdau+INTERVAL 6 DAY+INTERVAL 8 HOUR+INTERVAL 40 MINUTE,'Đã thanh toán',0 WHERE NOT EXISTS (SELECT 1 FROM hoadon WHERE mahd=1021);
SET @themhoadon=ROW_COUNT();
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1021,mamon,3,gia FROM monnuoc WHERE tenmon='Cơm chiên hải sản' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-3 WHERE tenmon='Cơm chiên hải sản' AND @themhoadon=1;
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1021,mamon,1,gia FROM monnuoc WHERE tenmon='Cá chiên xù' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-1 WHERE tenmon='Cá chiên xù' AND @themhoadon=1;
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1021,mamon,2,gia FROM monnuoc WHERE tenmon='Cà phê sữa' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-2 WHERE tenmon='Cà phê sữa' AND @themhoadon=1;
UPDATE hoadon SET tongtien=(SELECT SUM(soluong*dongia) FROM chitiethoadon WHERE mahd=1021) WHERE mahd=1021 AND @themhoadon=1;

-- Hóa đơn mẫu 022: 1 dòng chi tiết.
INSERT INTO hoadon (mahd,maban,manv,ngaylap,ngaythanhtoan,trangthai,tongtien) SELECT 1022,(SELECT maban FROM ban ORDER BY maban LIMIT 1 OFFSET 3),@nvmau,@ngaybatdau+INTERVAL 6 DAY+INTERVAL 8 HOUR+INTERVAL 0 MINUTE,@ngaybatdau+INTERVAL 6 DAY+INTERVAL 8 HOUR+INTERVAL 20 MINUTE,'Đã thanh toán',0 WHERE NOT EXISTS (SELECT 1 FROM hoadon WHERE mahd=1022);
SET @themhoadon=ROW_COUNT();
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1022,mamon,1,gia FROM monnuoc WHERE tenmon='Tôm rang muối' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-1 WHERE tenmon='Tôm rang muối' AND @themhoadon=1;
UPDATE hoadon SET tongtien=(SELECT SUM(soluong*dongia) FROM chitiethoadon WHERE mahd=1022) WHERE mahd=1022 AND @themhoadon=1;

-- Hóa đơn mẫu 023: 2 dòng chi tiết.
INSERT INTO hoadon (mahd,maban,manv,ngaylap,ngaythanhtoan,trangthai,tongtien) SELECT 1023,(SELECT maban FROM ban ORDER BY maban LIMIT 1 OFFSET 4),@nvmau,@ngaybatdau+INTERVAL 6 DAY+INTERVAL 8 HOUR+INTERVAL 10 MINUTE,@ngaybatdau+INTERVAL 6 DAY+INTERVAL 8 HOUR+INTERVAL 30 MINUTE,'Đã thanh toán',0 WHERE NOT EXISTS (SELECT 1 FROM hoadon WHERE mahd=1023);
SET @themhoadon=ROW_COUNT();
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1023,mamon,2,gia FROM monnuoc WHERE tenmon='Nước chanh' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-2 WHERE tenmon='Nước chanh' AND @themhoadon=1;
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1023,mamon,3,gia FROM monnuoc WHERE tenmon='Canh chua cá' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-3 WHERE tenmon='Canh chua cá' AND @themhoadon=1;
UPDATE hoadon SET tongtien=(SELECT SUM(soluong*dongia) FROM chitiethoadon WHERE mahd=1023) WHERE mahd=1023 AND @themhoadon=1;

-- Hóa đơn mẫu 024: 3 dòng chi tiết.
INSERT INTO hoadon (mahd,maban,manv,ngaylap,ngaythanhtoan,trangthai,tongtien) SELECT 1024,(SELECT maban FROM ban ORDER BY maban LIMIT 1 OFFSET 5),@nvmau,@ngaybatdau+INTERVAL 6 DAY+INTERVAL 8 HOUR+INTERVAL 20 MINUTE,@ngaybatdau+INTERVAL 6 DAY+INTERVAL 8 HOUR+INTERVAL 40 MINUTE,'Đã thanh toán',0 WHERE NOT EXISTS (SELECT 1 FROM hoadon WHERE mahd=1024);
SET @themhoadon=ROW_COUNT();
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1024,mamon,3,gia FROM monnuoc WHERE tenmon='Bò lúc lắc' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-3 WHERE tenmon='Bò lúc lắc' AND @themhoadon=1;
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1024,mamon,1,gia FROM monnuoc WHERE tenmon='Trà đào' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-1 WHERE tenmon='Trà đào' AND @themhoadon=1;
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1024,mamon,2,gia FROM monnuoc WHERE tenmon='7 Up' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-2 WHERE tenmon='7 Up' AND @themhoadon=1;
UPDATE hoadon SET tongtien=(SELECT SUM(soluong*dongia) FROM chitiethoadon WHERE mahd=1024) WHERE mahd=1024 AND @themhoadon=1;

-- Hóa đơn mẫu 025: 1 dòng chi tiết.
INSERT INTO hoadon (mahd,maban,manv,ngaylap,ngaythanhtoan,trangthai,tongtien) SELECT 1025,(SELECT maban FROM ban ORDER BY maban LIMIT 1 OFFSET 0),@nvmau,@ngaybatdau+INTERVAL 7 DAY+INTERVAL 8 HOUR+INTERVAL 0 MINUTE,@ngaybatdau+INTERVAL 7 DAY+INTERVAL 8 HOUR+INTERVAL 20 MINUTE,'Đã thanh toán',0 WHERE NOT EXISTS (SELECT 1 FROM hoadon WHERE mahd=1025);
SET @themhoadon=ROW_COUNT();
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1025,mamon,1,gia FROM monnuoc WHERE tenmon='Lẩu thái hải sản' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-1 WHERE tenmon='Lẩu thái hải sản' AND @themhoadon=1;
UPDATE hoadon SET tongtien=(SELECT SUM(soluong*dongia) FROM chitiethoadon WHERE mahd=1025) WHERE mahd=1025 AND @themhoadon=1;

-- Hóa đơn mẫu 026: 2 dòng chi tiết.
INSERT INTO hoadon (mahd,maban,manv,ngaylap,ngaythanhtoan,trangthai,tongtien) SELECT 1026,(SELECT maban FROM ban ORDER BY maban LIMIT 1 OFFSET 1),@nvmau,@ngaybatdau+INTERVAL 7 DAY+INTERVAL 8 HOUR+INTERVAL 10 MINUTE,@ngaybatdau+INTERVAL 7 DAY+INTERVAL 8 HOUR+INTERVAL 30 MINUTE,'Đã thanh toán',0 WHERE NOT EXISTS (SELECT 1 FROM hoadon WHERE mahd=1026);
SET @themhoadon=ROW_COUNT();
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1026,mamon,2,gia FROM monnuoc WHERE tenmon='Trà tắc' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-2 WHERE tenmon='Trà tắc' AND @themhoadon=1;
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1026,mamon,3,gia FROM monnuoc WHERE tenmon='Rau muống xào tỏi' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-3 WHERE tenmon='Rau muống xào tỏi' AND @themhoadon=1;
UPDATE hoadon SET tongtien=(SELECT SUM(soluong*dongia) FROM chitiethoadon WHERE mahd=1026) WHERE mahd=1026 AND @themhoadon=1;

-- Hóa đơn mẫu 027: 3 dòng chi tiết.
INSERT INTO hoadon (mahd,maban,manv,ngaylap,ngaythanhtoan,trangthai,tongtien) SELECT 1027,(SELECT maban FROM ban ORDER BY maban LIMIT 1 OFFSET 2),@nvmau,@ngaybatdau+INTERVAL 7 DAY+INTERVAL 8 HOUR+INTERVAL 20 MINUTE,@ngaybatdau+INTERVAL 7 DAY+INTERVAL 8 HOUR+INTERVAL 40 MINUTE,'Đã thanh toán',0 WHERE NOT EXISTS (SELECT 1 FROM hoadon WHERE mahd=1027);
SET @themhoadon=ROW_COUNT();
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1027,mamon,3,gia FROM monnuoc WHERE tenmon='Gà chiên nước mắm' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-3 WHERE tenmon='Gà chiên nước mắm' AND @themhoadon=1;
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1027,mamon,1,gia FROM monnuoc WHERE tenmon='Nước suối' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-1 WHERE tenmon='Nước suối' AND @themhoadon=1;
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1027,mamon,2,gia FROM monnuoc WHERE tenmon='Cơm chiên hải sản' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-2 WHERE tenmon='Cơm chiên hải sản' AND @themhoadon=1;
UPDATE hoadon SET tongtien=(SELECT SUM(soluong*dongia) FROM chitiethoadon WHERE mahd=1027) WHERE mahd=1027 AND @themhoadon=1;

-- Hóa đơn mẫu 028: 1 dòng chi tiết.
INSERT INTO hoadon (mahd,maban,manv,ngaylap,ngaythanhtoan,trangthai,tongtien) SELECT 1028,(SELECT maban FROM ban ORDER BY maban LIMIT 1 OFFSET 3),@nvmau,@ngaybatdau+INTERVAL 8 DAY+INTERVAL 8 HOUR+INTERVAL 0 MINUTE,@ngaybatdau+INTERVAL 8 DAY+INTERVAL 8 HOUR+INTERVAL 20 MINUTE,'Đã thanh toán',0 WHERE NOT EXISTS (SELECT 1 FROM hoadon WHERE mahd=1028);
SET @themhoadon=ROW_COUNT();
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1028,mamon,1,gia FROM monnuoc WHERE tenmon='Cá chiên xù' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-1 WHERE tenmon='Cá chiên xù' AND @themhoadon=1;
UPDATE hoadon SET tongtien=(SELECT SUM(soluong*dongia) FROM chitiethoadon WHERE mahd=1028) WHERE mahd=1028 AND @themhoadon=1;

-- Hóa đơn mẫu 029: 2 dòng chi tiết.
INSERT INTO hoadon (mahd,maban,manv,ngaylap,ngaythanhtoan,trangthai,tongtien) SELECT 1029,(SELECT maban FROM ban ORDER BY maban LIMIT 1 OFFSET 4),@nvmau,@ngaybatdau+INTERVAL 8 DAY+INTERVAL 8 HOUR+INTERVAL 10 MINUTE,@ngaybatdau+INTERVAL 8 DAY+INTERVAL 8 HOUR+INTERVAL 30 MINUTE,'Đã thanh toán',0 WHERE NOT EXISTS (SELECT 1 FROM hoadon WHERE mahd=1029);
SET @themhoadon=ROW_COUNT();
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1029,mamon,2,gia FROM monnuoc WHERE tenmon='Trà sữa' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-2 WHERE tenmon='Trà sữa' AND @themhoadon=1;
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1029,mamon,3,gia FROM monnuoc WHERE tenmon='Sườn nướng mật ong' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-3 WHERE tenmon='Sườn nướng mật ong' AND @themhoadon=1;
UPDATE hoadon SET tongtien=(SELECT SUM(soluong*dongia) FROM chitiethoadon WHERE mahd=1029) WHERE mahd=1029 AND @themhoadon=1;

-- Hóa đơn mẫu 030: 3 dòng chi tiết.
INSERT INTO hoadon (mahd,maban,manv,ngaylap,ngaythanhtoan,trangthai,tongtien) SELECT 1030,(SELECT maban FROM ban ORDER BY maban LIMIT 1 OFFSET 5),@nvmau,@ngaybatdau+INTERVAL 8 DAY+INTERVAL 8 HOUR+INTERVAL 20 MINUTE,@ngaybatdau+INTERVAL 8 DAY+INTERVAL 8 HOUR+INTERVAL 40 MINUTE,'Đã thanh toán',0 WHERE NOT EXISTS (SELECT 1 FROM hoadon WHERE mahd=1030);
SET @themhoadon=ROW_COUNT();
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1030,mamon,3,gia FROM monnuoc WHERE tenmon='Canh chua cá' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-3 WHERE tenmon='Canh chua cá' AND @themhoadon=1;
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1030,mamon,1,gia FROM monnuoc WHERE tenmon='Coca Cola' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-1 WHERE tenmon='Coca Cola' AND @themhoadon=1;
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1030,mamon,2,gia FROM monnuoc WHERE tenmon='Bò lúc lắc' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-2 WHERE tenmon='Bò lúc lắc' AND @themhoadon=1;
UPDATE hoadon SET tongtien=(SELECT SUM(soluong*dongia) FROM chitiethoadon WHERE mahd=1030) WHERE mahd=1030 AND @themhoadon=1;

-- Hóa đơn mẫu 031: 1 dòng chi tiết.
INSERT INTO hoadon (mahd,maban,manv,ngaylap,ngaythanhtoan,trangthai,tongtien) SELECT 1031,(SELECT maban FROM ban ORDER BY maban LIMIT 1 OFFSET 0),@nvmau,@ngaybatdau+INTERVAL 9 DAY+INTERVAL 8 HOUR+INTERVAL 0 MINUTE,@ngaybatdau+INTERVAL 9 DAY+INTERVAL 8 HOUR+INTERVAL 20 MINUTE,'Đã thanh toán',0 WHERE NOT EXISTS (SELECT 1 FROM hoadon WHERE mahd=1031);
SET @themhoadon=ROW_COUNT();
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1031,mamon,1,gia FROM monnuoc WHERE tenmon='Trà đào' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-1 WHERE tenmon='Trà đào' AND @themhoadon=1;
UPDATE hoadon SET tongtien=(SELECT SUM(soluong*dongia) FROM chitiethoadon WHERE mahd=1031) WHERE mahd=1031 AND @themhoadon=1;

-- Hóa đơn mẫu 032: 2 dòng chi tiết.
INSERT INTO hoadon (mahd,maban,manv,ngaylap,ngaythanhtoan,trangthai,tongtien) SELECT 1032,(SELECT maban FROM ban ORDER BY maban LIMIT 1 OFFSET 1),@nvmau,@ngaybatdau+INTERVAL 9 DAY+INTERVAL 8 HOUR+INTERVAL 10 MINUTE,@ngaybatdau+INTERVAL 9 DAY+INTERVAL 8 HOUR+INTERVAL 30 MINUTE,'Đã thanh toán',0 WHERE NOT EXISTS (SELECT 1 FROM hoadon WHERE mahd=1032);
SET @themhoadon=ROW_COUNT();
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1032,mamon,2,gia FROM monnuoc WHERE tenmon='Cà phê đen' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-2 WHERE tenmon='Cà phê đen' AND @themhoadon=1;
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1032,mamon,3,gia FROM monnuoc WHERE tenmon='Mì xào bò' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-3 WHERE tenmon='Mì xào bò' AND @themhoadon=1;
UPDATE hoadon SET tongtien=(SELECT SUM(soluong*dongia) FROM chitiethoadon WHERE mahd=1032) WHERE mahd=1032 AND @themhoadon=1;

-- Hóa đơn mẫu 033: 3 dòng chi tiết.
INSERT INTO hoadon (mahd,maban,manv,ngaylap,ngaythanhtoan,trangthai,tongtien) SELECT 1033,(SELECT maban FROM ban ORDER BY maban LIMIT 1 OFFSET 2),@nvmau,@ngaybatdau+INTERVAL 9 DAY+INTERVAL 8 HOUR+INTERVAL 20 MINUTE,@ngaybatdau+INTERVAL 9 DAY+INTERVAL 8 HOUR+INTERVAL 40 MINUTE,'Đã thanh toán',0 WHERE NOT EXISTS (SELECT 1 FROM hoadon WHERE mahd=1033);
SET @themhoadon=ROW_COUNT();
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1033,mamon,3,gia FROM monnuoc WHERE tenmon='Rau muống xào tỏi' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-3 WHERE tenmon='Rau muống xào tỏi' AND @themhoadon=1;
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1033,mamon,1,gia FROM monnuoc WHERE tenmon='Cam vắt' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-1 WHERE tenmon='Cam vắt' AND @themhoadon=1;
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1033,mamon,2,gia FROM monnuoc WHERE tenmon='Gà chiên nước mắm' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-2 WHERE tenmon='Gà chiên nước mắm' AND @themhoadon=1;
UPDATE hoadon SET tongtien=(SELECT SUM(soluong*dongia) FROM chitiethoadon WHERE mahd=1033) WHERE mahd=1033 AND @themhoadon=1;

-- Hóa đơn mẫu 034: 1 dòng chi tiết.
INSERT INTO hoadon (mahd,maban,manv,ngaylap,ngaythanhtoan,trangthai,tongtien) SELECT 1034,(SELECT maban FROM ban ORDER BY maban LIMIT 1 OFFSET 3),@nvmau,@ngaybatdau+INTERVAL 9 DAY+INTERVAL 8 HOUR+INTERVAL 0 MINUTE,@ngaybatdau+INTERVAL 9 DAY+INTERVAL 8 HOUR+INTERVAL 20 MINUTE,'Đã thanh toán',0 WHERE NOT EXISTS (SELECT 1 FROM hoadon WHERE mahd=1034);
SET @themhoadon=ROW_COUNT();
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1034,mamon,1,gia FROM monnuoc WHERE tenmon='Nước suối' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-1 WHERE tenmon='Nước suối' AND @themhoadon=1;
UPDATE hoadon SET tongtien=(SELECT SUM(soluong*dongia) FROM chitiethoadon WHERE mahd=1034) WHERE mahd=1034 AND @themhoadon=1;

-- Hóa đơn mẫu 035: 2 dòng chi tiết.
INSERT INTO hoadon (mahd,maban,manv,ngaylap,ngaythanhtoan,trangthai,tongtien) SELECT 1035,(SELECT maban FROM ban ORDER BY maban LIMIT 1 OFFSET 4),@nvmau,@ngaybatdau+INTERVAL 10 DAY+INTERVAL 8 HOUR+INTERVAL 10 MINUTE,@ngaybatdau+INTERVAL 10 DAY+INTERVAL 8 HOUR+INTERVAL 30 MINUTE,'Đã thanh toán',0 WHERE NOT EXISTS (SELECT 1 FROM hoadon WHERE mahd=1035);
SET @themhoadon=ROW_COUNT();
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1035,mamon,2,gia FROM monnuoc WHERE tenmon='Cà phê sữa' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-2 WHERE tenmon='Cà phê sữa' AND @themhoadon=1;
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1035,mamon,3,gia FROM monnuoc WHERE tenmon='Tôm rang muối' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-3 WHERE tenmon='Tôm rang muối' AND @themhoadon=1;
UPDATE hoadon SET tongtien=(SELECT SUM(soluong*dongia) FROM chitiethoadon WHERE mahd=1035) WHERE mahd=1035 AND @themhoadon=1;

-- Hóa đơn mẫu 036: 3 dòng chi tiết.
INSERT INTO hoadon (mahd,maban,manv,ngaylap,ngaythanhtoan,trangthai,tongtien) SELECT 1036,(SELECT maban FROM ban ORDER BY maban LIMIT 1 OFFSET 5),@nvmau,@ngaybatdau+INTERVAL 10 DAY+INTERVAL 8 HOUR+INTERVAL 20 MINUTE,@ngaybatdau+INTERVAL 10 DAY+INTERVAL 8 HOUR+INTERVAL 40 MINUTE,'Đã thanh toán',0 WHERE NOT EXISTS (SELECT 1 FROM hoadon WHERE mahd=1036);
SET @themhoadon=ROW_COUNT();
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1036,mamon,3,gia FROM monnuoc WHERE tenmon='Sườn nướng mật ong' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-3 WHERE tenmon='Sườn nướng mật ong' AND @themhoadon=1;
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1036,mamon,1,gia FROM monnuoc WHERE tenmon='Nước chanh' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-1 WHERE tenmon='Nước chanh' AND @themhoadon=1;
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1036,mamon,2,gia FROM monnuoc WHERE tenmon='Canh chua cá' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-2 WHERE tenmon='Canh chua cá' AND @themhoadon=1;
UPDATE hoadon SET tongtien=(SELECT SUM(soluong*dongia) FROM chitiethoadon WHERE mahd=1036) WHERE mahd=1036 AND @themhoadon=1;

-- Hóa đơn mẫu 037: 1 dòng chi tiết.
INSERT INTO hoadon (mahd,maban,manv,ngaylap,ngaythanhtoan,trangthai,tongtien) SELECT 1037,(SELECT maban FROM ban ORDER BY maban LIMIT 1 OFFSET 0),@nvmau,@ngaybatdau+INTERVAL 10 DAY+INTERVAL 8 HOUR+INTERVAL 0 MINUTE,@ngaybatdau+INTERVAL 10 DAY+INTERVAL 8 HOUR+INTERVAL 20 MINUTE,'Đã thanh toán',0 WHERE NOT EXISTS (SELECT 1 FROM hoadon WHERE mahd=1037);
SET @themhoadon=ROW_COUNT();
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1037,mamon,1,gia FROM monnuoc WHERE tenmon='Coca Cola' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-1 WHERE tenmon='Coca Cola' AND @themhoadon=1;
UPDATE hoadon SET tongtien=(SELECT SUM(soluong*dongia) FROM chitiethoadon WHERE mahd=1037) WHERE mahd=1037 AND @themhoadon=1;

-- Hóa đơn mẫu 038: 2 dòng chi tiết.
INSERT INTO hoadon (mahd,maban,manv,ngaylap,ngaythanhtoan,trangthai,tongtien) SELECT 1038,(SELECT maban FROM ban ORDER BY maban LIMIT 1 OFFSET 1),@nvmau,@ngaybatdau+INTERVAL 11 DAY+INTERVAL 8 HOUR+INTERVAL 10 MINUTE,@ngaybatdau+INTERVAL 11 DAY+INTERVAL 8 HOUR+INTERVAL 30 MINUTE,'Đã thanh toán',0 WHERE NOT EXISTS (SELECT 1 FROM hoadon WHERE mahd=1038);
SET @themhoadon=ROW_COUNT();
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1038,mamon,2,gia FROM monnuoc WHERE tenmon='7 Up' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-2 WHERE tenmon='7 Up' AND @themhoadon=1;
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1038,mamon,3,gia FROM monnuoc WHERE tenmon='Lẩu thái hải sản' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-3 WHERE tenmon='Lẩu thái hải sản' AND @themhoadon=1;
UPDATE hoadon SET tongtien=(SELECT SUM(soluong*dongia) FROM chitiethoadon WHERE mahd=1038) WHERE mahd=1038 AND @themhoadon=1;

-- Hóa đơn mẫu 039: 3 dòng chi tiết.
INSERT INTO hoadon (mahd,maban,manv,ngaylap,ngaythanhtoan,trangthai,tongtien) SELECT 1039,(SELECT maban FROM ban ORDER BY maban LIMIT 1 OFFSET 2),@nvmau,@ngaybatdau+INTERVAL 11 DAY+INTERVAL 8 HOUR+INTERVAL 20 MINUTE,@ngaybatdau+INTERVAL 11 DAY+INTERVAL 8 HOUR+INTERVAL 40 MINUTE,'Đã thanh toán',0 WHERE NOT EXISTS (SELECT 1 FROM hoadon WHERE mahd=1039);
SET @themhoadon=ROW_COUNT();
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1039,mamon,3,gia FROM monnuoc WHERE tenmon='Mì xào bò' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-3 WHERE tenmon='Mì xào bò' AND @themhoadon=1;
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1039,mamon,1,gia FROM monnuoc WHERE tenmon='Trà tắc' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-1 WHERE tenmon='Trà tắc' AND @themhoadon=1;
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1039,mamon,2,gia FROM monnuoc WHERE tenmon='Rau muống xào tỏi' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-2 WHERE tenmon='Rau muống xào tỏi' AND @themhoadon=1;
UPDATE hoadon SET tongtien=(SELECT SUM(soluong*dongia) FROM chitiethoadon WHERE mahd=1039) WHERE mahd=1039 AND @themhoadon=1;

-- Hóa đơn mẫu 040: 1 dòng chi tiết.
INSERT INTO hoadon (mahd,maban,manv,ngaylap,ngaythanhtoan,trangthai,tongtien) SELECT 1040,(SELECT maban FROM ban ORDER BY maban LIMIT 1 OFFSET 3),@nvmau,@ngaybatdau+INTERVAL 11 DAY+INTERVAL 8 HOUR+INTERVAL 0 MINUTE,@ngaybatdau+INTERVAL 11 DAY+INTERVAL 8 HOUR+INTERVAL 20 MINUTE,'Đã thanh toán',0 WHERE NOT EXISTS (SELECT 1 FROM hoadon WHERE mahd=1040);
SET @themhoadon=ROW_COUNT();
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1040,mamon,1,gia FROM monnuoc WHERE tenmon='Cam vắt' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-1 WHERE tenmon='Cam vắt' AND @themhoadon=1;
UPDATE hoadon SET tongtien=(SELECT SUM(soluong*dongia) FROM chitiethoadon WHERE mahd=1040) WHERE mahd=1040 AND @themhoadon=1;

-- Hóa đơn mẫu 041: 2 dòng chi tiết.
INSERT INTO hoadon (mahd,maban,manv,ngaylap,ngaythanhtoan,trangthai,tongtien) SELECT 1041,(SELECT maban FROM ban ORDER BY maban LIMIT 1 OFFSET 4),@nvmau,@ngaybatdau+INTERVAL 12 DAY+INTERVAL 8 HOUR+INTERVAL 10 MINUTE,@ngaybatdau+INTERVAL 12 DAY+INTERVAL 8 HOUR+INTERVAL 30 MINUTE,'Đã thanh toán',0 WHERE NOT EXISTS (SELECT 1 FROM hoadon WHERE mahd=1041);
SET @themhoadon=ROW_COUNT();
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1041,mamon,2,gia FROM monnuoc WHERE tenmon='Cơm chiên hải sản' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-2 WHERE tenmon='Cơm chiên hải sản' AND @themhoadon=1;
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1041,mamon,3,gia FROM monnuoc WHERE tenmon='Cá chiên xù' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-3 WHERE tenmon='Cá chiên xù' AND @themhoadon=1;
UPDATE hoadon SET tongtien=(SELECT SUM(soluong*dongia) FROM chitiethoadon WHERE mahd=1041) WHERE mahd=1041 AND @themhoadon=1;

-- Hóa đơn mẫu 042: 3 dòng chi tiết.
INSERT INTO hoadon (mahd,maban,manv,ngaylap,ngaythanhtoan,trangthai,tongtien) SELECT 1042,(SELECT maban FROM ban ORDER BY maban LIMIT 1 OFFSET 5),@nvmau,@ngaybatdau+INTERVAL 12 DAY+INTERVAL 8 HOUR+INTERVAL 20 MINUTE,@ngaybatdau+INTERVAL 12 DAY+INTERVAL 8 HOUR+INTERVAL 40 MINUTE,'Đã thanh toán',0 WHERE NOT EXISTS (SELECT 1 FROM hoadon WHERE mahd=1042);
SET @themhoadon=ROW_COUNT();
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1042,mamon,3,gia FROM monnuoc WHERE tenmon='Tôm rang muối' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-3 WHERE tenmon='Tôm rang muối' AND @themhoadon=1;
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1042,mamon,1,gia FROM monnuoc WHERE tenmon='Trà sữa' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-1 WHERE tenmon='Trà sữa' AND @themhoadon=1;
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1042,mamon,2,gia FROM monnuoc WHERE tenmon='Sườn nướng mật ong' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-2 WHERE tenmon='Sườn nướng mật ong' AND @themhoadon=1;
UPDATE hoadon SET tongtien=(SELECT SUM(soluong*dongia) FROM chitiethoadon WHERE mahd=1042) WHERE mahd=1042 AND @themhoadon=1;

-- Hóa đơn mẫu 043: 1 dòng chi tiết.
INSERT INTO hoadon (mahd,maban,manv,ngaylap,ngaythanhtoan,trangthai,tongtien) SELECT 1043,(SELECT maban FROM ban ORDER BY maban LIMIT 1 OFFSET 0),@nvmau,@ngaybatdau+INTERVAL 12 DAY+INTERVAL 8 HOUR+INTERVAL 0 MINUTE,@ngaybatdau+INTERVAL 12 DAY+INTERVAL 8 HOUR+INTERVAL 20 MINUTE,'Đã thanh toán',0 WHERE NOT EXISTS (SELECT 1 FROM hoadon WHERE mahd=1043);
SET @themhoadon=ROW_COUNT();
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1043,mamon,1,gia FROM monnuoc WHERE tenmon='Nước chanh' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-1 WHERE tenmon='Nước chanh' AND @themhoadon=1;
UPDATE hoadon SET tongtien=(SELECT SUM(soluong*dongia) FROM chitiethoadon WHERE mahd=1043) WHERE mahd=1043 AND @themhoadon=1;

-- Hóa đơn mẫu 044: 2 dòng chi tiết.
INSERT INTO hoadon (mahd,maban,manv,ngaylap,ngaythanhtoan,trangthai,tongtien) SELECT 1044,(SELECT maban FROM ban ORDER BY maban LIMIT 1 OFFSET 1),@nvmau,@ngaybatdau+INTERVAL 12 DAY+INTERVAL 8 HOUR+INTERVAL 10 MINUTE,@ngaybatdau+INTERVAL 12 DAY+INTERVAL 8 HOUR+INTERVAL 30 MINUTE,'Đã thanh toán',0 WHERE NOT EXISTS (SELECT 1 FROM hoadon WHERE mahd=1044);
SET @themhoadon=ROW_COUNT();
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1044,mamon,2,gia FROM monnuoc WHERE tenmon='Bò lúc lắc' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-2 WHERE tenmon='Bò lúc lắc' AND @themhoadon=1;
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1044,mamon,3,gia FROM monnuoc WHERE tenmon='Trà đào' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-3 WHERE tenmon='Trà đào' AND @themhoadon=1;
UPDATE hoadon SET tongtien=(SELECT SUM(soluong*dongia) FROM chitiethoadon WHERE mahd=1044) WHERE mahd=1044 AND @themhoadon=1;

-- Hóa đơn mẫu 045: 3 dòng chi tiết.
INSERT INTO hoadon (mahd,maban,manv,ngaylap,ngaythanhtoan,trangthai,tongtien) SELECT 1045,(SELECT maban FROM ban ORDER BY maban LIMIT 1 OFFSET 2),@nvmau,@ngaybatdau+INTERVAL 13 DAY+INTERVAL 8 HOUR+INTERVAL 20 MINUTE,@ngaybatdau+INTERVAL 13 DAY+INTERVAL 8 HOUR+INTERVAL 40 MINUTE,'Đã thanh toán',0 WHERE NOT EXISTS (SELECT 1 FROM hoadon WHERE mahd=1045);
SET @themhoadon=ROW_COUNT();
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1045,mamon,3,gia FROM monnuoc WHERE tenmon='Lẩu thái hải sản' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-3 WHERE tenmon='Lẩu thái hải sản' AND @themhoadon=1;
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1045,mamon,1,gia FROM monnuoc WHERE tenmon='Cà phê đen' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-1 WHERE tenmon='Cà phê đen' AND @themhoadon=1;
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1045,mamon,2,gia FROM monnuoc WHERE tenmon='Mì xào bò' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-2 WHERE tenmon='Mì xào bò' AND @themhoadon=1;
UPDATE hoadon SET tongtien=(SELECT SUM(soluong*dongia) FROM chitiethoadon WHERE mahd=1045) WHERE mahd=1045 AND @themhoadon=1;

-- Hóa đơn mẫu 046: 1 dòng chi tiết.
INSERT INTO hoadon (mahd,maban,manv,ngaylap,ngaythanhtoan,trangthai,tongtien) SELECT 1046,(SELECT maban FROM ban ORDER BY maban LIMIT 1 OFFSET 3),@nvmau,@ngaybatdau+INTERVAL 13 DAY+INTERVAL 8 HOUR+INTERVAL 0 MINUTE,@ngaybatdau+INTERVAL 13 DAY+INTERVAL 8 HOUR+INTERVAL 20 MINUTE,'Đã thanh toán',0 WHERE NOT EXISTS (SELECT 1 FROM hoadon WHERE mahd=1046);
SET @themhoadon=ROW_COUNT();
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1046,mamon,1,gia FROM monnuoc WHERE tenmon='Trà tắc' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-1 WHERE tenmon='Trà tắc' AND @themhoadon=1;
UPDATE hoadon SET tongtien=(SELECT SUM(soluong*dongia) FROM chitiethoadon WHERE mahd=1046) WHERE mahd=1046 AND @themhoadon=1;

-- Hóa đơn mẫu 047: 2 dòng chi tiết.
INSERT INTO hoadon (mahd,maban,manv,ngaylap,ngaythanhtoan,trangthai,tongtien) SELECT 1047,(SELECT maban FROM ban ORDER BY maban LIMIT 1 OFFSET 4),@nvmau,@ngaybatdau+INTERVAL 13 DAY+INTERVAL 8 HOUR+INTERVAL 10 MINUTE,@ngaybatdau+INTERVAL 13 DAY+INTERVAL 8 HOUR+INTERVAL 30 MINUTE,'Đã thanh toán',0 WHERE NOT EXISTS (SELECT 1 FROM hoadon WHERE mahd=1047);
SET @themhoadon=ROW_COUNT();
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1047,mamon,2,gia FROM monnuoc WHERE tenmon='Gà chiên nước mắm' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-2 WHERE tenmon='Gà chiên nước mắm' AND @themhoadon=1;
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1047,mamon,3,gia FROM monnuoc WHERE tenmon='Nước suối' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-3 WHERE tenmon='Nước suối' AND @themhoadon=1;
UPDATE hoadon SET tongtien=(SELECT SUM(soluong*dongia) FROM chitiethoadon WHERE mahd=1047) WHERE mahd=1047 AND @themhoadon=1;

-- Hóa đơn mẫu 048: 3 dòng chi tiết.
INSERT INTO hoadon (mahd,maban,manv,ngaylap,ngaythanhtoan,trangthai,tongtien) SELECT 1048,(SELECT maban FROM ban ORDER BY maban LIMIT 1 OFFSET 5),@nvmau,@ngaybatdau+INTERVAL 14 DAY+INTERVAL 8 HOUR+INTERVAL 20 MINUTE,@ngaybatdau+INTERVAL 14 DAY+INTERVAL 8 HOUR+INTERVAL 40 MINUTE,'Đã thanh toán',0 WHERE NOT EXISTS (SELECT 1 FROM hoadon WHERE mahd=1048);
SET @themhoadon=ROW_COUNT();
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1048,mamon,3,gia FROM monnuoc WHERE tenmon='Cá chiên xù' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-3 WHERE tenmon='Cá chiên xù' AND @themhoadon=1;
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1048,mamon,1,gia FROM monnuoc WHERE tenmon='Cà phê sữa' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-1 WHERE tenmon='Cà phê sữa' AND @themhoadon=1;
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1048,mamon,2,gia FROM monnuoc WHERE tenmon='Tôm rang muối' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-2 WHERE tenmon='Tôm rang muối' AND @themhoadon=1;
UPDATE hoadon SET tongtien=(SELECT SUM(soluong*dongia) FROM chitiethoadon WHERE mahd=1048) WHERE mahd=1048 AND @themhoadon=1;

-- Hóa đơn mẫu 049: 1 dòng chi tiết.
INSERT INTO hoadon (mahd,maban,manv,ngaylap,ngaythanhtoan,trangthai,tongtien) SELECT 1049,(SELECT maban FROM ban ORDER BY maban LIMIT 1 OFFSET 0),@nvmau,@ngaybatdau+INTERVAL 14 DAY+INTERVAL 8 HOUR+INTERVAL 0 MINUTE,@ngaybatdau+INTERVAL 14 DAY+INTERVAL 8 HOUR+INTERVAL 20 MINUTE,'Đã thanh toán',0 WHERE NOT EXISTS (SELECT 1 FROM hoadon WHERE mahd=1049);
SET @themhoadon=ROW_COUNT();
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1049,mamon,1,gia FROM monnuoc WHERE tenmon='Trà sữa' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-1 WHERE tenmon='Trà sữa' AND @themhoadon=1;
UPDATE hoadon SET tongtien=(SELECT SUM(soluong*dongia) FROM chitiethoadon WHERE mahd=1049) WHERE mahd=1049 AND @themhoadon=1;

-- Hóa đơn mẫu 050: 2 dòng chi tiết.
INSERT INTO hoadon (mahd,maban,manv,ngaylap,ngaythanhtoan,trangthai,tongtien) SELECT 1050,(SELECT maban FROM ban ORDER BY maban LIMIT 1 OFFSET 1),@nvmau,@ngaybatdau+INTERVAL 14 DAY+INTERVAL 8 HOUR+INTERVAL 10 MINUTE,@ngaybatdau+INTERVAL 14 DAY+INTERVAL 8 HOUR+INTERVAL 30 MINUTE,'Đã thanh toán',0 WHERE NOT EXISTS (SELECT 1 FROM hoadon WHERE mahd=1050);
SET @themhoadon=ROW_COUNT();
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1050,mamon,2,gia FROM monnuoc WHERE tenmon='Canh chua cá' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-2 WHERE tenmon='Canh chua cá' AND @themhoadon=1;
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1050,mamon,3,gia FROM monnuoc WHERE tenmon='Coca Cola' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-3 WHERE tenmon='Coca Cola' AND @themhoadon=1;
UPDATE hoadon SET tongtien=(SELECT SUM(soluong*dongia) FROM chitiethoadon WHERE mahd=1050) WHERE mahd=1050 AND @themhoadon=1;

-- Hóa đơn mẫu 051: 3 dòng chi tiết.
INSERT INTO hoadon (mahd,maban,manv,ngaylap,ngaythanhtoan,trangthai,tongtien) SELECT 1051,(SELECT maban FROM ban ORDER BY maban LIMIT 1 OFFSET 2),@nvmau,@ngaybatdau+INTERVAL 15 DAY+INTERVAL 8 HOUR+INTERVAL 20 MINUTE,@ngaybatdau+INTERVAL 15 DAY+INTERVAL 8 HOUR+INTERVAL 40 MINUTE,'Đã thanh toán',0 WHERE NOT EXISTS (SELECT 1 FROM hoadon WHERE mahd=1051);
SET @themhoadon=ROW_COUNT();
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1051,mamon,3,gia FROM monnuoc WHERE tenmon='Trà đào' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-3 WHERE tenmon='Trà đào' AND @themhoadon=1;
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1051,mamon,1,gia FROM monnuoc WHERE tenmon='7 Up' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-1 WHERE tenmon='7 Up' AND @themhoadon=1;
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1051,mamon,2,gia FROM monnuoc WHERE tenmon='Lẩu thái hải sản' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-2 WHERE tenmon='Lẩu thái hải sản' AND @themhoadon=1;
UPDATE hoadon SET tongtien=(SELECT SUM(soluong*dongia) FROM chitiethoadon WHERE mahd=1051) WHERE mahd=1051 AND @themhoadon=1;

-- Hóa đơn mẫu 052: 1 dòng chi tiết.
INSERT INTO hoadon (mahd,maban,manv,ngaylap,ngaythanhtoan,trangthai,tongtien) SELECT 1052,(SELECT maban FROM ban ORDER BY maban LIMIT 1 OFFSET 3),@nvmau,@ngaybatdau+INTERVAL 15 DAY+INTERVAL 8 HOUR+INTERVAL 0 MINUTE,@ngaybatdau+INTERVAL 15 DAY+INTERVAL 8 HOUR+INTERVAL 20 MINUTE,'Đã thanh toán',0 WHERE NOT EXISTS (SELECT 1 FROM hoadon WHERE mahd=1052);
SET @themhoadon=ROW_COUNT();
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1052,mamon,1,gia FROM monnuoc WHERE tenmon='Cà phê đen' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-1 WHERE tenmon='Cà phê đen' AND @themhoadon=1;
UPDATE hoadon SET tongtien=(SELECT SUM(soluong*dongia) FROM chitiethoadon WHERE mahd=1052) WHERE mahd=1052 AND @themhoadon=1;

-- Hóa đơn mẫu 053: 2 dòng chi tiết.
INSERT INTO hoadon (mahd,maban,manv,ngaylap,ngaythanhtoan,trangthai,tongtien) SELECT 1053,(SELECT maban FROM ban ORDER BY maban LIMIT 1 OFFSET 4),@nvmau,@ngaybatdau+INTERVAL 15 DAY+INTERVAL 8 HOUR+INTERVAL 10 MINUTE,@ngaybatdau+INTERVAL 15 DAY+INTERVAL 8 HOUR+INTERVAL 30 MINUTE,'Đã thanh toán',0 WHERE NOT EXISTS (SELECT 1 FROM hoadon WHERE mahd=1053);
SET @themhoadon=ROW_COUNT();
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1053,mamon,2,gia FROM monnuoc WHERE tenmon='Rau muống xào tỏi' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-2 WHERE tenmon='Rau muống xào tỏi' AND @themhoadon=1;
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1053,mamon,3,gia FROM monnuoc WHERE tenmon='Cam vắt' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-3 WHERE tenmon='Cam vắt' AND @themhoadon=1;
UPDATE hoadon SET tongtien=(SELECT SUM(soluong*dongia) FROM chitiethoadon WHERE mahd=1053) WHERE mahd=1053 AND @themhoadon=1;

-- Hóa đơn mẫu 054: 3 dòng chi tiết.
INSERT INTO hoadon (mahd,maban,manv,ngaylap,ngaythanhtoan,trangthai,tongtien) SELECT 1054,(SELECT maban FROM ban ORDER BY maban LIMIT 1 OFFSET 5),@nvmau,@ngaybatdau+INTERVAL 15 DAY+INTERVAL 8 HOUR+INTERVAL 20 MINUTE,@ngaybatdau+INTERVAL 15 DAY+INTERVAL 8 HOUR+INTERVAL 40 MINUTE,'Đã thanh toán',0 WHERE NOT EXISTS (SELECT 1 FROM hoadon WHERE mahd=1054);
SET @themhoadon=ROW_COUNT();
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1054,mamon,3,gia FROM monnuoc WHERE tenmon='Nước suối' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-3 WHERE tenmon='Nước suối' AND @themhoadon=1;
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1054,mamon,1,gia FROM monnuoc WHERE tenmon='Cơm chiên hải sản' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-1 WHERE tenmon='Cơm chiên hải sản' AND @themhoadon=1;
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1054,mamon,2,gia FROM monnuoc WHERE tenmon='Cá chiên xù' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-2 WHERE tenmon='Cá chiên xù' AND @themhoadon=1;
UPDATE hoadon SET tongtien=(SELECT SUM(soluong*dongia) FROM chitiethoadon WHERE mahd=1054) WHERE mahd=1054 AND @themhoadon=1;

-- Hóa đơn mẫu 055: 1 dòng chi tiết.
INSERT INTO hoadon (mahd,maban,manv,ngaylap,ngaythanhtoan,trangthai,tongtien) SELECT 1055,(SELECT maban FROM ban ORDER BY maban LIMIT 1 OFFSET 0),@nvmau,@ngaybatdau+INTERVAL 16 DAY+INTERVAL 8 HOUR+INTERVAL 0 MINUTE,@ngaybatdau+INTERVAL 16 DAY+INTERVAL 8 HOUR+INTERVAL 20 MINUTE,'Đã thanh toán',0 WHERE NOT EXISTS (SELECT 1 FROM hoadon WHERE mahd=1055);
SET @themhoadon=ROW_COUNT();
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1055,mamon,1,gia FROM monnuoc WHERE tenmon='Cà phê sữa' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-1 WHERE tenmon='Cà phê sữa' AND @themhoadon=1;
UPDATE hoadon SET tongtien=(SELECT SUM(soluong*dongia) FROM chitiethoadon WHERE mahd=1055) WHERE mahd=1055 AND @themhoadon=1;

-- Hóa đơn mẫu 056: 2 dòng chi tiết.
INSERT INTO hoadon (mahd,maban,manv,ngaylap,ngaythanhtoan,trangthai,tongtien) SELECT 1056,(SELECT maban FROM ban ORDER BY maban LIMIT 1 OFFSET 1),@nvmau,@ngaybatdau+INTERVAL 16 DAY+INTERVAL 8 HOUR+INTERVAL 10 MINUTE,@ngaybatdau+INTERVAL 16 DAY+INTERVAL 8 HOUR+INTERVAL 30 MINUTE,'Đã thanh toán',0 WHERE NOT EXISTS (SELECT 1 FROM hoadon WHERE mahd=1056);
SET @themhoadon=ROW_COUNT();
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1056,mamon,2,gia FROM monnuoc WHERE tenmon='Sườn nướng mật ong' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-2 WHERE tenmon='Sườn nướng mật ong' AND @themhoadon=1;
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1056,mamon,3,gia FROM monnuoc WHERE tenmon='Nước chanh' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-3 WHERE tenmon='Nước chanh' AND @themhoadon=1;
UPDATE hoadon SET tongtien=(SELECT SUM(soluong*dongia) FROM chitiethoadon WHERE mahd=1056) WHERE mahd=1056 AND @themhoadon=1;

-- Hóa đơn mẫu 057: 3 dòng chi tiết.
INSERT INTO hoadon (mahd,maban,manv,ngaylap,ngaythanhtoan,trangthai,tongtien) SELECT 1057,(SELECT maban FROM ban ORDER BY maban LIMIT 1 OFFSET 2),@nvmau,@ngaybatdau+INTERVAL 16 DAY+INTERVAL 8 HOUR+INTERVAL 20 MINUTE,@ngaybatdau+INTERVAL 16 DAY+INTERVAL 8 HOUR+INTERVAL 40 MINUTE,'Đã thanh toán',0 WHERE NOT EXISTS (SELECT 1 FROM hoadon WHERE mahd=1057);
SET @themhoadon=ROW_COUNT();
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1057,mamon,3,gia FROM monnuoc WHERE tenmon='Coca Cola' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-3 WHERE tenmon='Coca Cola' AND @themhoadon=1;
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1057,mamon,1,gia FROM monnuoc WHERE tenmon='Bò lúc lắc' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-1 WHERE tenmon='Bò lúc lắc' AND @themhoadon=1;
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1057,mamon,2,gia FROM monnuoc WHERE tenmon='Trà đào' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-2 WHERE tenmon='Trà đào' AND @themhoadon=1;
UPDATE hoadon SET tongtien=(SELECT SUM(soluong*dongia) FROM chitiethoadon WHERE mahd=1057) WHERE mahd=1057 AND @themhoadon=1;

-- Hóa đơn mẫu 058: 1 dòng chi tiết.
INSERT INTO hoadon (mahd,maban,manv,ngaylap,ngaythanhtoan,trangthai,tongtien) SELECT 1058,(SELECT maban FROM ban ORDER BY maban LIMIT 1 OFFSET 3),@nvmau,@ngaybatdau+INTERVAL 17 DAY+INTERVAL 8 HOUR+INTERVAL 0 MINUTE,@ngaybatdau+INTERVAL 17 DAY+INTERVAL 8 HOUR+INTERVAL 20 MINUTE,'Đã thanh toán',0 WHERE NOT EXISTS (SELECT 1 FROM hoadon WHERE mahd=1058);
SET @themhoadon=ROW_COUNT();
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1058,mamon,1,gia FROM monnuoc WHERE tenmon='7 Up' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-1 WHERE tenmon='7 Up' AND @themhoadon=1;
UPDATE hoadon SET tongtien=(SELECT SUM(soluong*dongia) FROM chitiethoadon WHERE mahd=1058) WHERE mahd=1058 AND @themhoadon=1;

-- Hóa đơn mẫu 059: 2 dòng chi tiết.
INSERT INTO hoadon (mahd,maban,manv,ngaylap,ngaythanhtoan,trangthai,tongtien) SELECT 1059,(SELECT maban FROM ban ORDER BY maban LIMIT 1 OFFSET 4),@nvmau,@ngaybatdau+INTERVAL 17 DAY+INTERVAL 8 HOUR+INTERVAL 10 MINUTE,@ngaybatdau+INTERVAL 17 DAY+INTERVAL 8 HOUR+INTERVAL 30 MINUTE,'Đã thanh toán',0 WHERE NOT EXISTS (SELECT 1 FROM hoadon WHERE mahd=1059);
SET @themhoadon=ROW_COUNT();
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1059,mamon,2,gia FROM monnuoc WHERE tenmon='Mì xào bò' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-2 WHERE tenmon='Mì xào bò' AND @themhoadon=1;
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1059,mamon,3,gia FROM monnuoc WHERE tenmon='Trà tắc' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-3 WHERE tenmon='Trà tắc' AND @themhoadon=1;
UPDATE hoadon SET tongtien=(SELECT SUM(soluong*dongia) FROM chitiethoadon WHERE mahd=1059) WHERE mahd=1059 AND @themhoadon=1;

-- Hóa đơn mẫu 060: 3 dòng chi tiết.
INSERT INTO hoadon (mahd,maban,manv,ngaylap,ngaythanhtoan,trangthai,tongtien) SELECT 1060,(SELECT maban FROM ban ORDER BY maban LIMIT 1 OFFSET 5),@nvmau,@ngaybatdau+INTERVAL 17 DAY+INTERVAL 8 HOUR+INTERVAL 20 MINUTE,@ngaybatdau+INTERVAL 17 DAY+INTERVAL 8 HOUR+INTERVAL 40 MINUTE,'Đã thanh toán',0 WHERE NOT EXISTS (SELECT 1 FROM hoadon WHERE mahd=1060);
SET @themhoadon=ROW_COUNT();
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1060,mamon,3,gia FROM monnuoc WHERE tenmon='Cam vắt' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-3 WHERE tenmon='Cam vắt' AND @themhoadon=1;
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1060,mamon,1,gia FROM monnuoc WHERE tenmon='Gà chiên nước mắm' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-1 WHERE tenmon='Gà chiên nước mắm' AND @themhoadon=1;
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1060,mamon,2,gia FROM monnuoc WHERE tenmon='Nước suối' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-2 WHERE tenmon='Nước suối' AND @themhoadon=1;
UPDATE hoadon SET tongtien=(SELECT SUM(soluong*dongia) FROM chitiethoadon WHERE mahd=1060) WHERE mahd=1060 AND @themhoadon=1;

-- Hóa đơn mẫu 061: 1 dòng chi tiết.
INSERT INTO hoadon (mahd,maban,manv,ngaylap,ngaythanhtoan,trangthai,tongtien) SELECT 1061,(SELECT maban FROM ban ORDER BY maban LIMIT 1 OFFSET 0),@nvmau,@ngaybatdau+INTERVAL 18 DAY+INTERVAL 8 HOUR+INTERVAL 0 MINUTE,@ngaybatdau+INTERVAL 18 DAY+INTERVAL 8 HOUR+INTERVAL 20 MINUTE,'Đã thanh toán',0 WHERE NOT EXISTS (SELECT 1 FROM hoadon WHERE mahd=1061);
SET @themhoadon=ROW_COUNT();
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1061,mamon,1,gia FROM monnuoc WHERE tenmon='Cơm chiên hải sản' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-1 WHERE tenmon='Cơm chiên hải sản' AND @themhoadon=1;
UPDATE hoadon SET tongtien=(SELECT SUM(soluong*dongia) FROM chitiethoadon WHERE mahd=1061) WHERE mahd=1061 AND @themhoadon=1;

-- Hóa đơn mẫu 062: 2 dòng chi tiết.
INSERT INTO hoadon (mahd,maban,manv,ngaylap,ngaythanhtoan,trangthai,tongtien) SELECT 1062,(SELECT maban FROM ban ORDER BY maban LIMIT 1 OFFSET 1),@nvmau,@ngaybatdau+INTERVAL 18 DAY+INTERVAL 8 HOUR+INTERVAL 10 MINUTE,@ngaybatdau+INTERVAL 18 DAY+INTERVAL 8 HOUR+INTERVAL 30 MINUTE,'Đã thanh toán',0 WHERE NOT EXISTS (SELECT 1 FROM hoadon WHERE mahd=1062);
SET @themhoadon=ROW_COUNT();
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1062,mamon,2,gia FROM monnuoc WHERE tenmon='Tôm rang muối' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-2 WHERE tenmon='Tôm rang muối' AND @themhoadon=1;
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1062,mamon,3,gia FROM monnuoc WHERE tenmon='Trà sữa' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-3 WHERE tenmon='Trà sữa' AND @themhoadon=1;
UPDATE hoadon SET tongtien=(SELECT SUM(soluong*dongia) FROM chitiethoadon WHERE mahd=1062) WHERE mahd=1062 AND @themhoadon=1;

-- Hóa đơn mẫu 063: 3 dòng chi tiết.
INSERT INTO hoadon (mahd,maban,manv,ngaylap,ngaythanhtoan,trangthai,tongtien) SELECT 1063,(SELECT maban FROM ban ORDER BY maban LIMIT 1 OFFSET 2),@nvmau,@ngaybatdau+INTERVAL 18 DAY+INTERVAL 8 HOUR+INTERVAL 20 MINUTE,@ngaybatdau+INTERVAL 18 DAY+INTERVAL 8 HOUR+INTERVAL 40 MINUTE,'Đã thanh toán',0 WHERE NOT EXISTS (SELECT 1 FROM hoadon WHERE mahd=1063);
SET @themhoadon=ROW_COUNT();
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1063,mamon,3,gia FROM monnuoc WHERE tenmon='Nước chanh' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-3 WHERE tenmon='Nước chanh' AND @themhoadon=1;
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1063,mamon,1,gia FROM monnuoc WHERE tenmon='Canh chua cá' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-1 WHERE tenmon='Canh chua cá' AND @themhoadon=1;
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1063,mamon,2,gia FROM monnuoc WHERE tenmon='Coca Cola' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-2 WHERE tenmon='Coca Cola' AND @themhoadon=1;
UPDATE hoadon SET tongtien=(SELECT SUM(soluong*dongia) FROM chitiethoadon WHERE mahd=1063) WHERE mahd=1063 AND @themhoadon=1;

-- Hóa đơn mẫu 064: 1 dòng chi tiết.
INSERT INTO hoadon (mahd,maban,manv,ngaylap,ngaythanhtoan,trangthai,tongtien) SELECT 1064,(SELECT maban FROM ban ORDER BY maban LIMIT 1 OFFSET 3),@nvmau,@ngaybatdau+INTERVAL 18 DAY+INTERVAL 8 HOUR+INTERVAL 0 MINUTE,@ngaybatdau+INTERVAL 18 DAY+INTERVAL 8 HOUR+INTERVAL 20 MINUTE,'Đã thanh toán',0 WHERE NOT EXISTS (SELECT 1 FROM hoadon WHERE mahd=1064);
SET @themhoadon=ROW_COUNT();
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1064,mamon,1,gia FROM monnuoc WHERE tenmon='Bò lúc lắc' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-1 WHERE tenmon='Bò lúc lắc' AND @themhoadon=1;
UPDATE hoadon SET tongtien=(SELECT SUM(soluong*dongia) FROM chitiethoadon WHERE mahd=1064) WHERE mahd=1064 AND @themhoadon=1;

-- Hóa đơn mẫu 065: 2 dòng chi tiết.
INSERT INTO hoadon (mahd,maban,manv,ngaylap,ngaythanhtoan,trangthai,tongtien) SELECT 1065,(SELECT maban FROM ban ORDER BY maban LIMIT 1 OFFSET 4),@nvmau,@ngaybatdau+INTERVAL 19 DAY+INTERVAL 8 HOUR+INTERVAL 10 MINUTE,@ngaybatdau+INTERVAL 19 DAY+INTERVAL 8 HOUR+INTERVAL 30 MINUTE,'Đã thanh toán',0 WHERE NOT EXISTS (SELECT 1 FROM hoadon WHERE mahd=1065);
SET @themhoadon=ROW_COUNT();
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1065,mamon,2,gia FROM monnuoc WHERE tenmon='Lẩu thái hải sản' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-2 WHERE tenmon='Lẩu thái hải sản' AND @themhoadon=1;
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1065,mamon,3,gia FROM monnuoc WHERE tenmon='Cà phê đen' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-3 WHERE tenmon='Cà phê đen' AND @themhoadon=1;
UPDATE hoadon SET tongtien=(SELECT SUM(soluong*dongia) FROM chitiethoadon WHERE mahd=1065) WHERE mahd=1065 AND @themhoadon=1;

-- Hóa đơn mẫu 066: 3 dòng chi tiết.
INSERT INTO hoadon (mahd,maban,manv,ngaylap,ngaythanhtoan,trangthai,tongtien) SELECT 1066,(SELECT maban FROM ban ORDER BY maban LIMIT 1 OFFSET 5),@nvmau,@ngaybatdau+INTERVAL 19 DAY+INTERVAL 8 HOUR+INTERVAL 20 MINUTE,@ngaybatdau+INTERVAL 19 DAY+INTERVAL 8 HOUR+INTERVAL 40 MINUTE,'Đã thanh toán',0 WHERE NOT EXISTS (SELECT 1 FROM hoadon WHERE mahd=1066);
SET @themhoadon=ROW_COUNT();
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1066,mamon,3,gia FROM monnuoc WHERE tenmon='Trà tắc' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-3 WHERE tenmon='Trà tắc' AND @themhoadon=1;
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1066,mamon,1,gia FROM monnuoc WHERE tenmon='Rau muống xào tỏi' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-1 WHERE tenmon='Rau muống xào tỏi' AND @themhoadon=1;
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1066,mamon,2,gia FROM monnuoc WHERE tenmon='Cam vắt' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-2 WHERE tenmon='Cam vắt' AND @themhoadon=1;
UPDATE hoadon SET tongtien=(SELECT SUM(soluong*dongia) FROM chitiethoadon WHERE mahd=1066) WHERE mahd=1066 AND @themhoadon=1;

-- Hóa đơn mẫu 067: 1 dòng chi tiết.
INSERT INTO hoadon (mahd,maban,manv,ngaylap,ngaythanhtoan,trangthai,tongtien) SELECT 1067,(SELECT maban FROM ban ORDER BY maban LIMIT 1 OFFSET 0),@nvmau,@ngaybatdau+INTERVAL 19 DAY+INTERVAL 8 HOUR+INTERVAL 0 MINUTE,@ngaybatdau+INTERVAL 19 DAY+INTERVAL 8 HOUR+INTERVAL 20 MINUTE,'Đã thanh toán',0 WHERE NOT EXISTS (SELECT 1 FROM hoadon WHERE mahd=1067);
SET @themhoadon=ROW_COUNT();
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1067,mamon,1,gia FROM monnuoc WHERE tenmon='Gà chiên nước mắm' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-1 WHERE tenmon='Gà chiên nước mắm' AND @themhoadon=1;
UPDATE hoadon SET tongtien=(SELECT SUM(soluong*dongia) FROM chitiethoadon WHERE mahd=1067) WHERE mahd=1067 AND @themhoadon=1;

-- Hóa đơn mẫu 068: 2 dòng chi tiết.
INSERT INTO hoadon (mahd,maban,manv,ngaylap,ngaythanhtoan,trangthai,tongtien) SELECT 1068,(SELECT maban FROM ban ORDER BY maban LIMIT 1 OFFSET 1),@nvmau,@ngaybatdau+INTERVAL 20 DAY+INTERVAL 8 HOUR+INTERVAL 10 MINUTE,@ngaybatdau+INTERVAL 20 DAY+INTERVAL 8 HOUR+INTERVAL 30 MINUTE,'Đã thanh toán',0 WHERE NOT EXISTS (SELECT 1 FROM hoadon WHERE mahd=1068);
SET @themhoadon=ROW_COUNT();
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1068,mamon,2,gia FROM monnuoc WHERE tenmon='Cá chiên xù' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-2 WHERE tenmon='Cá chiên xù' AND @themhoadon=1;
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1068,mamon,3,gia FROM monnuoc WHERE tenmon='Cà phê sữa' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-3 WHERE tenmon='Cà phê sữa' AND @themhoadon=1;
UPDATE hoadon SET tongtien=(SELECT SUM(soluong*dongia) FROM chitiethoadon WHERE mahd=1068) WHERE mahd=1068 AND @themhoadon=1;

-- Hóa đơn mẫu 069: 3 dòng chi tiết.
INSERT INTO hoadon (mahd,maban,manv,ngaylap,ngaythanhtoan,trangthai,tongtien) SELECT 1069,(SELECT maban FROM ban ORDER BY maban LIMIT 1 OFFSET 2),@nvmau,@ngaybatdau+INTERVAL 20 DAY+INTERVAL 8 HOUR+INTERVAL 20 MINUTE,@ngaybatdau+INTERVAL 20 DAY+INTERVAL 8 HOUR+INTERVAL 40 MINUTE,'Đã thanh toán',0 WHERE NOT EXISTS (SELECT 1 FROM hoadon WHERE mahd=1069);
SET @themhoadon=ROW_COUNT();
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1069,mamon,3,gia FROM monnuoc WHERE tenmon='Trà sữa' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-3 WHERE tenmon='Trà sữa' AND @themhoadon=1;
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1069,mamon,1,gia FROM monnuoc WHERE tenmon='Sườn nướng mật ong' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-1 WHERE tenmon='Sườn nướng mật ong' AND @themhoadon=1;
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1069,mamon,2,gia FROM monnuoc WHERE tenmon='Nước chanh' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-2 WHERE tenmon='Nước chanh' AND @themhoadon=1;
UPDATE hoadon SET tongtien=(SELECT SUM(soluong*dongia) FROM chitiethoadon WHERE mahd=1069) WHERE mahd=1069 AND @themhoadon=1;

-- Hóa đơn mẫu 070: 1 dòng chi tiết.
INSERT INTO hoadon (mahd,maban,manv,ngaylap,ngaythanhtoan,trangthai,tongtien) SELECT 1070,(SELECT maban FROM ban ORDER BY maban LIMIT 1 OFFSET 3),@nvmau,@ngaybatdau+INTERVAL 20 DAY+INTERVAL 8 HOUR+INTERVAL 0 MINUTE,@ngaybatdau+INTERVAL 20 DAY+INTERVAL 8 HOUR+INTERVAL 20 MINUTE,'Đã thanh toán',0 WHERE NOT EXISTS (SELECT 1 FROM hoadon WHERE mahd=1070);
SET @themhoadon=ROW_COUNT();
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1070,mamon,1,gia FROM monnuoc WHERE tenmon='Canh chua cá' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-1 WHERE tenmon='Canh chua cá' AND @themhoadon=1;
UPDATE hoadon SET tongtien=(SELECT SUM(soluong*dongia) FROM chitiethoadon WHERE mahd=1070) WHERE mahd=1070 AND @themhoadon=1;

-- Hóa đơn mẫu 071: 2 dòng chi tiết.
INSERT INTO hoadon (mahd,maban,manv,ngaylap,ngaythanhtoan,trangthai,tongtien) SELECT 1071,(SELECT maban FROM ban ORDER BY maban LIMIT 1 OFFSET 4),@nvmau,@ngaybatdau+INTERVAL 21 DAY+INTERVAL 8 HOUR+INTERVAL 10 MINUTE,@ngaybatdau+INTERVAL 21 DAY+INTERVAL 8 HOUR+INTERVAL 30 MINUTE,'Đã thanh toán',0 WHERE NOT EXISTS (SELECT 1 FROM hoadon WHERE mahd=1071);
SET @themhoadon=ROW_COUNT();
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1071,mamon,2,gia FROM monnuoc WHERE tenmon='Trà đào' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-2 WHERE tenmon='Trà đào' AND @themhoadon=1;
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1071,mamon,3,gia FROM monnuoc WHERE tenmon='7 Up' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-3 WHERE tenmon='7 Up' AND @themhoadon=1;
UPDATE hoadon SET tongtien=(SELECT SUM(soluong*dongia) FROM chitiethoadon WHERE mahd=1071) WHERE mahd=1071 AND @themhoadon=1;

-- Hóa đơn mẫu 072: 3 dòng chi tiết.
INSERT INTO hoadon (mahd,maban,manv,ngaylap,ngaythanhtoan,trangthai,tongtien) SELECT 1072,(SELECT maban FROM ban ORDER BY maban LIMIT 1 OFFSET 5),@nvmau,@ngaybatdau+INTERVAL 21 DAY+INTERVAL 8 HOUR+INTERVAL 20 MINUTE,@ngaybatdau+INTERVAL 21 DAY+INTERVAL 8 HOUR+INTERVAL 40 MINUTE,'Đã thanh toán',0 WHERE NOT EXISTS (SELECT 1 FROM hoadon WHERE mahd=1072);
SET @themhoadon=ROW_COUNT();
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1072,mamon,3,gia FROM monnuoc WHERE tenmon='Cà phê đen' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-3 WHERE tenmon='Cà phê đen' AND @themhoadon=1;
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1072,mamon,1,gia FROM monnuoc WHERE tenmon='Mì xào bò' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-1 WHERE tenmon='Mì xào bò' AND @themhoadon=1;
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1072,mamon,2,gia FROM monnuoc WHERE tenmon='Trà tắc' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-2 WHERE tenmon='Trà tắc' AND @themhoadon=1;
UPDATE hoadon SET tongtien=(SELECT SUM(soluong*dongia) FROM chitiethoadon WHERE mahd=1072) WHERE mahd=1072 AND @themhoadon=1;

-- Hóa đơn mẫu 073: 1 dòng chi tiết.
INSERT INTO hoadon (mahd,maban,manv,ngaylap,ngaythanhtoan,trangthai,tongtien) SELECT 1073,(SELECT maban FROM ban ORDER BY maban LIMIT 1 OFFSET 0),@nvmau,@ngaybatdau+INTERVAL 21 DAY+INTERVAL 8 HOUR+INTERVAL 0 MINUTE,@ngaybatdau+INTERVAL 21 DAY+INTERVAL 8 HOUR+INTERVAL 20 MINUTE,'Đã thanh toán',0 WHERE NOT EXISTS (SELECT 1 FROM hoadon WHERE mahd=1073);
SET @themhoadon=ROW_COUNT();
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1073,mamon,1,gia FROM monnuoc WHERE tenmon='Rau muống xào tỏi' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-1 WHERE tenmon='Rau muống xào tỏi' AND @themhoadon=1;
UPDATE hoadon SET tongtien=(SELECT SUM(soluong*dongia) FROM chitiethoadon WHERE mahd=1073) WHERE mahd=1073 AND @themhoadon=1;

-- Hóa đơn mẫu 074: 2 dòng chi tiết.
INSERT INTO hoadon (mahd,maban,manv,ngaylap,ngaythanhtoan,trangthai,tongtien) SELECT 1074,(SELECT maban FROM ban ORDER BY maban LIMIT 1 OFFSET 1),@nvmau,@ngaybatdau+INTERVAL 21 DAY+INTERVAL 8 HOUR+INTERVAL 10 MINUTE,@ngaybatdau+INTERVAL 21 DAY+INTERVAL 8 HOUR+INTERVAL 30 MINUTE,'Đã thanh toán',0 WHERE NOT EXISTS (SELECT 1 FROM hoadon WHERE mahd=1074);
SET @themhoadon=ROW_COUNT();
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1074,mamon,2,gia FROM monnuoc WHERE tenmon='Nước suối' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-2 WHERE tenmon='Nước suối' AND @themhoadon=1;
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1074,mamon,3,gia FROM monnuoc WHERE tenmon='Cơm chiên hải sản' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-3 WHERE tenmon='Cơm chiên hải sản' AND @themhoadon=1;
UPDATE hoadon SET tongtien=(SELECT SUM(soluong*dongia) FROM chitiethoadon WHERE mahd=1074) WHERE mahd=1074 AND @themhoadon=1;

-- Hóa đơn mẫu 075: 3 dòng chi tiết.
INSERT INTO hoadon (mahd,maban,manv,ngaylap,ngaythanhtoan,trangthai,tongtien) SELECT 1075,(SELECT maban FROM ban ORDER BY maban LIMIT 1 OFFSET 2),@nvmau,@ngaybatdau+INTERVAL 22 DAY+INTERVAL 8 HOUR+INTERVAL 20 MINUTE,@ngaybatdau+INTERVAL 22 DAY+INTERVAL 8 HOUR+INTERVAL 40 MINUTE,'Đã thanh toán',0 WHERE NOT EXISTS (SELECT 1 FROM hoadon WHERE mahd=1075);
SET @themhoadon=ROW_COUNT();
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1075,mamon,3,gia FROM monnuoc WHERE tenmon='Cà phê sữa' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-3 WHERE tenmon='Cà phê sữa' AND @themhoadon=1;
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1075,mamon,1,gia FROM monnuoc WHERE tenmon='Tôm rang muối' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-1 WHERE tenmon='Tôm rang muối' AND @themhoadon=1;
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1075,mamon,2,gia FROM monnuoc WHERE tenmon='Trà sữa' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-2 WHERE tenmon='Trà sữa' AND @themhoadon=1;
UPDATE hoadon SET tongtien=(SELECT SUM(soluong*dongia) FROM chitiethoadon WHERE mahd=1075) WHERE mahd=1075 AND @themhoadon=1;

-- Hóa đơn mẫu 076: 1 dòng chi tiết.
INSERT INTO hoadon (mahd,maban,manv,ngaylap,ngaythanhtoan,trangthai,tongtien) SELECT 1076,(SELECT maban FROM ban ORDER BY maban LIMIT 1 OFFSET 3),@nvmau,@ngaybatdau+INTERVAL 22 DAY+INTERVAL 8 HOUR+INTERVAL 0 MINUTE,@ngaybatdau+INTERVAL 22 DAY+INTERVAL 8 HOUR+INTERVAL 20 MINUTE,'Đã thanh toán',0 WHERE NOT EXISTS (SELECT 1 FROM hoadon WHERE mahd=1076);
SET @themhoadon=ROW_COUNT();
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1076,mamon,1,gia FROM monnuoc WHERE tenmon='Sườn nướng mật ong' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-1 WHERE tenmon='Sườn nướng mật ong' AND @themhoadon=1;
UPDATE hoadon SET tongtien=(SELECT SUM(soluong*dongia) FROM chitiethoadon WHERE mahd=1076) WHERE mahd=1076 AND @themhoadon=1;

-- Hóa đơn mẫu 077: 2 dòng chi tiết.
INSERT INTO hoadon (mahd,maban,manv,ngaylap,ngaythanhtoan,trangthai,tongtien) SELECT 1077,(SELECT maban FROM ban ORDER BY maban LIMIT 1 OFFSET 4),@nvmau,@ngaybatdau+INTERVAL 22 DAY+INTERVAL 8 HOUR+INTERVAL 10 MINUTE,@ngaybatdau+INTERVAL 22 DAY+INTERVAL 8 HOUR+INTERVAL 30 MINUTE,'Đã thanh toán',0 WHERE NOT EXISTS (SELECT 1 FROM hoadon WHERE mahd=1077);
SET @themhoadon=ROW_COUNT();
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1077,mamon,2,gia FROM monnuoc WHERE tenmon='Coca Cola' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-2 WHERE tenmon='Coca Cola' AND @themhoadon=1;
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1077,mamon,3,gia FROM monnuoc WHERE tenmon='Bò lúc lắc' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-3 WHERE tenmon='Bò lúc lắc' AND @themhoadon=1;
UPDATE hoadon SET tongtien=(SELECT SUM(soluong*dongia) FROM chitiethoadon WHERE mahd=1077) WHERE mahd=1077 AND @themhoadon=1;

-- Hóa đơn mẫu 078: 3 dòng chi tiết.
INSERT INTO hoadon (mahd,maban,manv,ngaylap,ngaythanhtoan,trangthai,tongtien) SELECT 1078,(SELECT maban FROM ban ORDER BY maban LIMIT 1 OFFSET 5),@nvmau,@ngaybatdau+INTERVAL 23 DAY+INTERVAL 8 HOUR+INTERVAL 20 MINUTE,@ngaybatdau+INTERVAL 23 DAY+INTERVAL 8 HOUR+INTERVAL 40 MINUTE,'Đã thanh toán',0 WHERE NOT EXISTS (SELECT 1 FROM hoadon WHERE mahd=1078);
SET @themhoadon=ROW_COUNT();
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1078,mamon,3,gia FROM monnuoc WHERE tenmon='7 Up' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-3 WHERE tenmon='7 Up' AND @themhoadon=1;
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1078,mamon,1,gia FROM monnuoc WHERE tenmon='Lẩu thái hải sản' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-1 WHERE tenmon='Lẩu thái hải sản' AND @themhoadon=1;
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1078,mamon,2,gia FROM monnuoc WHERE tenmon='Cà phê đen' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-2 WHERE tenmon='Cà phê đen' AND @themhoadon=1;
UPDATE hoadon SET tongtien=(SELECT SUM(soluong*dongia) FROM chitiethoadon WHERE mahd=1078) WHERE mahd=1078 AND @themhoadon=1;

-- Hóa đơn mẫu 079: 1 dòng chi tiết.
INSERT INTO hoadon (mahd,maban,manv,ngaylap,ngaythanhtoan,trangthai,tongtien) SELECT 1079,(SELECT maban FROM ban ORDER BY maban LIMIT 1 OFFSET 0),@nvmau,@ngaybatdau+INTERVAL 23 DAY+INTERVAL 8 HOUR+INTERVAL 0 MINUTE,@ngaybatdau+INTERVAL 23 DAY+INTERVAL 8 HOUR+INTERVAL 20 MINUTE,'Đã thanh toán',0 WHERE NOT EXISTS (SELECT 1 FROM hoadon WHERE mahd=1079);
SET @themhoadon=ROW_COUNT();
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1079,mamon,1,gia FROM monnuoc WHERE tenmon='Mì xào bò' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-1 WHERE tenmon='Mì xào bò' AND @themhoadon=1;
UPDATE hoadon SET tongtien=(SELECT SUM(soluong*dongia) FROM chitiethoadon WHERE mahd=1079) WHERE mahd=1079 AND @themhoadon=1;

-- Hóa đơn mẫu 080: 2 dòng chi tiết.
INSERT INTO hoadon (mahd,maban,manv,ngaylap,ngaythanhtoan,trangthai,tongtien) SELECT 1080,(SELECT maban FROM ban ORDER BY maban LIMIT 1 OFFSET 1),@nvmau,@ngaybatdau+INTERVAL 23 DAY+INTERVAL 8 HOUR+INTERVAL 10 MINUTE,@ngaybatdau+INTERVAL 23 DAY+INTERVAL 8 HOUR+INTERVAL 30 MINUTE,'Đã thanh toán',0 WHERE NOT EXISTS (SELECT 1 FROM hoadon WHERE mahd=1080);
SET @themhoadon=ROW_COUNT();
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1080,mamon,2,gia FROM monnuoc WHERE tenmon='Cam vắt' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-2 WHERE tenmon='Cam vắt' AND @themhoadon=1;
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1080,mamon,3,gia FROM monnuoc WHERE tenmon='Gà chiên nước mắm' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-3 WHERE tenmon='Gà chiên nước mắm' AND @themhoadon=1;
UPDATE hoadon SET tongtien=(SELECT SUM(soluong*dongia) FROM chitiethoadon WHERE mahd=1080) WHERE mahd=1080 AND @themhoadon=1;

-- Hóa đơn mẫu 081: 3 dòng chi tiết.
INSERT INTO hoadon (mahd,maban,manv,ngaylap,ngaythanhtoan,trangthai,tongtien) SELECT 1081,(SELECT maban FROM ban ORDER BY maban LIMIT 1 OFFSET 2),@nvmau,@ngaybatdau+INTERVAL 24 DAY+INTERVAL 8 HOUR+INTERVAL 20 MINUTE,@ngaybatdau+INTERVAL 24 DAY+INTERVAL 8 HOUR+INTERVAL 40 MINUTE,'Đã thanh toán',0 WHERE NOT EXISTS (SELECT 1 FROM hoadon WHERE mahd=1081);
SET @themhoadon=ROW_COUNT();
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1081,mamon,3,gia FROM monnuoc WHERE tenmon='Cơm chiên hải sản' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-3 WHERE tenmon='Cơm chiên hải sản' AND @themhoadon=1;
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1081,mamon,1,gia FROM monnuoc WHERE tenmon='Cá chiên xù' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-1 WHERE tenmon='Cá chiên xù' AND @themhoadon=1;
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1081,mamon,2,gia FROM monnuoc WHERE tenmon='Cà phê sữa' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-2 WHERE tenmon='Cà phê sữa' AND @themhoadon=1;
UPDATE hoadon SET tongtien=(SELECT SUM(soluong*dongia) FROM chitiethoadon WHERE mahd=1081) WHERE mahd=1081 AND @themhoadon=1;

-- Hóa đơn mẫu 082: 1 dòng chi tiết.
INSERT INTO hoadon (mahd,maban,manv,ngaylap,ngaythanhtoan,trangthai,tongtien) SELECT 1082,(SELECT maban FROM ban ORDER BY maban LIMIT 1 OFFSET 3),@nvmau,@ngaybatdau+INTERVAL 24 DAY+INTERVAL 8 HOUR+INTERVAL 0 MINUTE,@ngaybatdau+INTERVAL 24 DAY+INTERVAL 8 HOUR+INTERVAL 20 MINUTE,'Đã thanh toán',0 WHERE NOT EXISTS (SELECT 1 FROM hoadon WHERE mahd=1082);
SET @themhoadon=ROW_COUNT();
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1082,mamon,1,gia FROM monnuoc WHERE tenmon='Tôm rang muối' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-1 WHERE tenmon='Tôm rang muối' AND @themhoadon=1;
UPDATE hoadon SET tongtien=(SELECT SUM(soluong*dongia) FROM chitiethoadon WHERE mahd=1082) WHERE mahd=1082 AND @themhoadon=1;

-- Hóa đơn mẫu 083: 2 dòng chi tiết.
INSERT INTO hoadon (mahd,maban,manv,ngaylap,ngaythanhtoan,trangthai,tongtien) SELECT 1083,(SELECT maban FROM ban ORDER BY maban LIMIT 1 OFFSET 4),@nvmau,@ngaybatdau+INTERVAL 24 DAY+INTERVAL 8 HOUR+INTERVAL 10 MINUTE,@ngaybatdau+INTERVAL 24 DAY+INTERVAL 8 HOUR+INTERVAL 30 MINUTE,'Đã thanh toán',0 WHERE NOT EXISTS (SELECT 1 FROM hoadon WHERE mahd=1083);
SET @themhoadon=ROW_COUNT();
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1083,mamon,2,gia FROM monnuoc WHERE tenmon='Nước chanh' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-2 WHERE tenmon='Nước chanh' AND @themhoadon=1;
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1083,mamon,3,gia FROM monnuoc WHERE tenmon='Canh chua cá' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-3 WHERE tenmon='Canh chua cá' AND @themhoadon=1;
UPDATE hoadon SET tongtien=(SELECT SUM(soluong*dongia) FROM chitiethoadon WHERE mahd=1083) WHERE mahd=1083 AND @themhoadon=1;

-- Hóa đơn mẫu 084: 3 dòng chi tiết.
INSERT INTO hoadon (mahd,maban,manv,ngaylap,ngaythanhtoan,trangthai,tongtien) SELECT 1084,(SELECT maban FROM ban ORDER BY maban LIMIT 1 OFFSET 5),@nvmau,@ngaybatdau+INTERVAL 24 DAY+INTERVAL 8 HOUR+INTERVAL 20 MINUTE,@ngaybatdau+INTERVAL 24 DAY+INTERVAL 8 HOUR+INTERVAL 40 MINUTE,'Đã thanh toán',0 WHERE NOT EXISTS (SELECT 1 FROM hoadon WHERE mahd=1084);
SET @themhoadon=ROW_COUNT();
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1084,mamon,3,gia FROM monnuoc WHERE tenmon='Bò lúc lắc' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-3 WHERE tenmon='Bò lúc lắc' AND @themhoadon=1;
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1084,mamon,1,gia FROM monnuoc WHERE tenmon='Trà đào' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-1 WHERE tenmon='Trà đào' AND @themhoadon=1;
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1084,mamon,2,gia FROM monnuoc WHERE tenmon='7 Up' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-2 WHERE tenmon='7 Up' AND @themhoadon=1;
UPDATE hoadon SET tongtien=(SELECT SUM(soluong*dongia) FROM chitiethoadon WHERE mahd=1084) WHERE mahd=1084 AND @themhoadon=1;

-- Hóa đơn mẫu 085: 1 dòng chi tiết.
INSERT INTO hoadon (mahd,maban,manv,ngaylap,ngaythanhtoan,trangthai,tongtien) SELECT 1085,(SELECT maban FROM ban ORDER BY maban LIMIT 1 OFFSET 0),@nvmau,@ngaybatdau+INTERVAL 25 DAY+INTERVAL 8 HOUR+INTERVAL 0 MINUTE,@ngaybatdau+INTERVAL 25 DAY+INTERVAL 8 HOUR+INTERVAL 20 MINUTE,'Đã thanh toán',0 WHERE NOT EXISTS (SELECT 1 FROM hoadon WHERE mahd=1085);
SET @themhoadon=ROW_COUNT();
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1085,mamon,1,gia FROM monnuoc WHERE tenmon='Lẩu thái hải sản' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-1 WHERE tenmon='Lẩu thái hải sản' AND @themhoadon=1;
UPDATE hoadon SET tongtien=(SELECT SUM(soluong*dongia) FROM chitiethoadon WHERE mahd=1085) WHERE mahd=1085 AND @themhoadon=1;

-- Hóa đơn mẫu 086: 2 dòng chi tiết.
INSERT INTO hoadon (mahd,maban,manv,ngaylap,ngaythanhtoan,trangthai,tongtien) SELECT 1086,(SELECT maban FROM ban ORDER BY maban LIMIT 1 OFFSET 1),@nvmau,@ngaybatdau+INTERVAL 25 DAY+INTERVAL 8 HOUR+INTERVAL 10 MINUTE,@ngaybatdau+INTERVAL 25 DAY+INTERVAL 8 HOUR+INTERVAL 30 MINUTE,'Đã thanh toán',0 WHERE NOT EXISTS (SELECT 1 FROM hoadon WHERE mahd=1086);
SET @themhoadon=ROW_COUNT();
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1086,mamon,2,gia FROM monnuoc WHERE tenmon='Trà tắc' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-2 WHERE tenmon='Trà tắc' AND @themhoadon=1;
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1086,mamon,3,gia FROM monnuoc WHERE tenmon='Rau muống xào tỏi' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-3 WHERE tenmon='Rau muống xào tỏi' AND @themhoadon=1;
UPDATE hoadon SET tongtien=(SELECT SUM(soluong*dongia) FROM chitiethoadon WHERE mahd=1086) WHERE mahd=1086 AND @themhoadon=1;

-- Hóa đơn mẫu 087: 3 dòng chi tiết.
INSERT INTO hoadon (mahd,maban,manv,ngaylap,ngaythanhtoan,trangthai,tongtien) SELECT 1087,(SELECT maban FROM ban ORDER BY maban LIMIT 1 OFFSET 2),@nvmau,@ngaybatdau+INTERVAL 25 DAY+INTERVAL 8 HOUR+INTERVAL 20 MINUTE,@ngaybatdau+INTERVAL 25 DAY+INTERVAL 8 HOUR+INTERVAL 40 MINUTE,'Đã thanh toán',0 WHERE NOT EXISTS (SELECT 1 FROM hoadon WHERE mahd=1087);
SET @themhoadon=ROW_COUNT();
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1087,mamon,3,gia FROM monnuoc WHERE tenmon='Gà chiên nước mắm' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-3 WHERE tenmon='Gà chiên nước mắm' AND @themhoadon=1;
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1087,mamon,1,gia FROM monnuoc WHERE tenmon='Nước suối' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-1 WHERE tenmon='Nước suối' AND @themhoadon=1;
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1087,mamon,2,gia FROM monnuoc WHERE tenmon='Cơm chiên hải sản' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-2 WHERE tenmon='Cơm chiên hải sản' AND @themhoadon=1;
UPDATE hoadon SET tongtien=(SELECT SUM(soluong*dongia) FROM chitiethoadon WHERE mahd=1087) WHERE mahd=1087 AND @themhoadon=1;

-- Hóa đơn mẫu 088: 1 dòng chi tiết.
INSERT INTO hoadon (mahd,maban,manv,ngaylap,ngaythanhtoan,trangthai,tongtien) SELECT 1088,(SELECT maban FROM ban ORDER BY maban LIMIT 1 OFFSET 3),@nvmau,@ngaybatdau+INTERVAL 26 DAY+INTERVAL 8 HOUR+INTERVAL 0 MINUTE,@ngaybatdau+INTERVAL 26 DAY+INTERVAL 8 HOUR+INTERVAL 20 MINUTE,'Đã thanh toán',0 WHERE NOT EXISTS (SELECT 1 FROM hoadon WHERE mahd=1088);
SET @themhoadon=ROW_COUNT();
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1088,mamon,1,gia FROM monnuoc WHERE tenmon='Cá chiên xù' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-1 WHERE tenmon='Cá chiên xù' AND @themhoadon=1;
UPDATE hoadon SET tongtien=(SELECT SUM(soluong*dongia) FROM chitiethoadon WHERE mahd=1088) WHERE mahd=1088 AND @themhoadon=1;

-- Hóa đơn mẫu 089: 2 dòng chi tiết.
INSERT INTO hoadon (mahd,maban,manv,ngaylap,ngaythanhtoan,trangthai,tongtien) SELECT 1089,(SELECT maban FROM ban ORDER BY maban LIMIT 1 OFFSET 4),@nvmau,@ngaybatdau+INTERVAL 26 DAY+INTERVAL 8 HOUR+INTERVAL 10 MINUTE,@ngaybatdau+INTERVAL 26 DAY+INTERVAL 8 HOUR+INTERVAL 30 MINUTE,'Đã thanh toán',0 WHERE NOT EXISTS (SELECT 1 FROM hoadon WHERE mahd=1089);
SET @themhoadon=ROW_COUNT();
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1089,mamon,2,gia FROM monnuoc WHERE tenmon='Trà sữa' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-2 WHERE tenmon='Trà sữa' AND @themhoadon=1;
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1089,mamon,3,gia FROM monnuoc WHERE tenmon='Sườn nướng mật ong' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-3 WHERE tenmon='Sườn nướng mật ong' AND @themhoadon=1;
UPDATE hoadon SET tongtien=(SELECT SUM(soluong*dongia) FROM chitiethoadon WHERE mahd=1089) WHERE mahd=1089 AND @themhoadon=1;

-- Hóa đơn mẫu 090: 3 dòng chi tiết.
INSERT INTO hoadon (mahd,maban,manv,ngaylap,ngaythanhtoan,trangthai,tongtien) SELECT 1090,(SELECT maban FROM ban ORDER BY maban LIMIT 1 OFFSET 5),@nvmau,@ngaybatdau+INTERVAL 26 DAY+INTERVAL 8 HOUR+INTERVAL 20 MINUTE,@ngaybatdau+INTERVAL 26 DAY+INTERVAL 8 HOUR+INTERVAL 40 MINUTE,'Đã thanh toán',0 WHERE NOT EXISTS (SELECT 1 FROM hoadon WHERE mahd=1090);
SET @themhoadon=ROW_COUNT();
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1090,mamon,3,gia FROM monnuoc WHERE tenmon='Canh chua cá' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-3 WHERE tenmon='Canh chua cá' AND @themhoadon=1;
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1090,mamon,1,gia FROM monnuoc WHERE tenmon='Coca Cola' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-1 WHERE tenmon='Coca Cola' AND @themhoadon=1;
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1090,mamon,2,gia FROM monnuoc WHERE tenmon='Bò lúc lắc' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-2 WHERE tenmon='Bò lúc lắc' AND @themhoadon=1;
UPDATE hoadon SET tongtien=(SELECT SUM(soluong*dongia) FROM chitiethoadon WHERE mahd=1090) WHERE mahd=1090 AND @themhoadon=1;

-- Hóa đơn mẫu 091: 1 dòng chi tiết.
INSERT INTO hoadon (mahd,maban,manv,ngaylap,ngaythanhtoan,trangthai,tongtien) SELECT 1091,(SELECT maban FROM ban ORDER BY maban LIMIT 1 OFFSET 0),@nvmau,@ngaybatdau+INTERVAL 27 DAY+INTERVAL 8 HOUR+INTERVAL 0 MINUTE,@ngaybatdau+INTERVAL 27 DAY+INTERVAL 8 HOUR+INTERVAL 20 MINUTE,'Đã thanh toán',0 WHERE NOT EXISTS (SELECT 1 FROM hoadon WHERE mahd=1091);
SET @themhoadon=ROW_COUNT();
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1091,mamon,1,gia FROM monnuoc WHERE tenmon='Trà đào' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-1 WHERE tenmon='Trà đào' AND @themhoadon=1;
UPDATE hoadon SET tongtien=(SELECT SUM(soluong*dongia) FROM chitiethoadon WHERE mahd=1091) WHERE mahd=1091 AND @themhoadon=1;

-- Hóa đơn mẫu 092: 2 dòng chi tiết.
INSERT INTO hoadon (mahd,maban,manv,ngaylap,ngaythanhtoan,trangthai,tongtien) SELECT 1092,(SELECT maban FROM ban ORDER BY maban LIMIT 1 OFFSET 1),@nvmau,@ngaybatdau+INTERVAL 27 DAY+INTERVAL 8 HOUR+INTERVAL 10 MINUTE,@ngaybatdau+INTERVAL 27 DAY+INTERVAL 8 HOUR+INTERVAL 30 MINUTE,'Đã thanh toán',0 WHERE NOT EXISTS (SELECT 1 FROM hoadon WHERE mahd=1092);
SET @themhoadon=ROW_COUNT();
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1092,mamon,2,gia FROM monnuoc WHERE tenmon='Cà phê đen' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-2 WHERE tenmon='Cà phê đen' AND @themhoadon=1;
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1092,mamon,3,gia FROM monnuoc WHERE tenmon='Mì xào bò' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-3 WHERE tenmon='Mì xào bò' AND @themhoadon=1;
UPDATE hoadon SET tongtien=(SELECT SUM(soluong*dongia) FROM chitiethoadon WHERE mahd=1092) WHERE mahd=1092 AND @themhoadon=1;

-- Hóa đơn mẫu 093: 3 dòng chi tiết.
INSERT INTO hoadon (mahd,maban,manv,ngaylap,ngaythanhtoan,trangthai,tongtien) SELECT 1093,(SELECT maban FROM ban ORDER BY maban LIMIT 1 OFFSET 2),@nvmau,@ngaybatdau+INTERVAL 27 DAY+INTERVAL 8 HOUR+INTERVAL 20 MINUTE,@ngaybatdau+INTERVAL 27 DAY+INTERVAL 8 HOUR+INTERVAL 40 MINUTE,'Đã thanh toán',0 WHERE NOT EXISTS (SELECT 1 FROM hoadon WHERE mahd=1093);
SET @themhoadon=ROW_COUNT();
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1093,mamon,3,gia FROM monnuoc WHERE tenmon='Rau muống xào tỏi' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-3 WHERE tenmon='Rau muống xào tỏi' AND @themhoadon=1;
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1093,mamon,1,gia FROM monnuoc WHERE tenmon='Cam vắt' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-1 WHERE tenmon='Cam vắt' AND @themhoadon=1;
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1093,mamon,2,gia FROM monnuoc WHERE tenmon='Gà chiên nước mắm' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-2 WHERE tenmon='Gà chiên nước mắm' AND @themhoadon=1;
UPDATE hoadon SET tongtien=(SELECT SUM(soluong*dongia) FROM chitiethoadon WHERE mahd=1093) WHERE mahd=1093 AND @themhoadon=1;

-- Hóa đơn mẫu 094: 1 dòng chi tiết.
INSERT INTO hoadon (mahd,maban,manv,ngaylap,ngaythanhtoan,trangthai,tongtien) SELECT 1094,(SELECT maban FROM ban ORDER BY maban LIMIT 1 OFFSET 3),@nvmau,@ngaybatdau+INTERVAL 27 DAY+INTERVAL 8 HOUR+INTERVAL 0 MINUTE,@ngaybatdau+INTERVAL 27 DAY+INTERVAL 8 HOUR+INTERVAL 20 MINUTE,'Đã thanh toán',0 WHERE NOT EXISTS (SELECT 1 FROM hoadon WHERE mahd=1094);
SET @themhoadon=ROW_COUNT();
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1094,mamon,1,gia FROM monnuoc WHERE tenmon='Nước suối' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-1 WHERE tenmon='Nước suối' AND @themhoadon=1;
UPDATE hoadon SET tongtien=(SELECT SUM(soluong*dongia) FROM chitiethoadon WHERE mahd=1094) WHERE mahd=1094 AND @themhoadon=1;

-- Hóa đơn mẫu 095: 2 dòng chi tiết.
INSERT INTO hoadon (mahd,maban,manv,ngaylap,ngaythanhtoan,trangthai,tongtien) SELECT 1095,(SELECT maban FROM ban ORDER BY maban LIMIT 1 OFFSET 4),@nvmau,@ngaybatdau+INTERVAL 28 DAY+INTERVAL 8 HOUR+INTERVAL 10 MINUTE,@ngaybatdau+INTERVAL 28 DAY+INTERVAL 8 HOUR+INTERVAL 30 MINUTE,'Đã thanh toán',0 WHERE NOT EXISTS (SELECT 1 FROM hoadon WHERE mahd=1095);
SET @themhoadon=ROW_COUNT();
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1095,mamon,2,gia FROM monnuoc WHERE tenmon='Cà phê sữa' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-2 WHERE tenmon='Cà phê sữa' AND @themhoadon=1;
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1095,mamon,3,gia FROM monnuoc WHERE tenmon='Tôm rang muối' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-3 WHERE tenmon='Tôm rang muối' AND @themhoadon=1;
UPDATE hoadon SET tongtien=(SELECT SUM(soluong*dongia) FROM chitiethoadon WHERE mahd=1095) WHERE mahd=1095 AND @themhoadon=1;

-- Hóa đơn mẫu 096: 3 dòng chi tiết.
INSERT INTO hoadon (mahd,maban,manv,ngaylap,ngaythanhtoan,trangthai,tongtien) SELECT 1096,(SELECT maban FROM ban ORDER BY maban LIMIT 1 OFFSET 5),@nvmau,@ngaybatdau+INTERVAL 28 DAY+INTERVAL 8 HOUR+INTERVAL 20 MINUTE,@ngaybatdau+INTERVAL 28 DAY+INTERVAL 8 HOUR+INTERVAL 40 MINUTE,'Đã thanh toán',0 WHERE NOT EXISTS (SELECT 1 FROM hoadon WHERE mahd=1096);
SET @themhoadon=ROW_COUNT();
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1096,mamon,3,gia FROM monnuoc WHERE tenmon='Sườn nướng mật ong' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-3 WHERE tenmon='Sườn nướng mật ong' AND @themhoadon=1;
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1096,mamon,1,gia FROM monnuoc WHERE tenmon='Nước chanh' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-1 WHERE tenmon='Nước chanh' AND @themhoadon=1;
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1096,mamon,2,gia FROM monnuoc WHERE tenmon='Canh chua cá' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-2 WHERE tenmon='Canh chua cá' AND @themhoadon=1;
UPDATE hoadon SET tongtien=(SELECT SUM(soluong*dongia) FROM chitiethoadon WHERE mahd=1096) WHERE mahd=1096 AND @themhoadon=1;

-- Hóa đơn mẫu 097: 1 dòng chi tiết.
INSERT INTO hoadon (mahd,maban,manv,ngaylap,ngaythanhtoan,trangthai,tongtien) SELECT 1097,(SELECT maban FROM ban ORDER BY maban LIMIT 1 OFFSET 0),@nvmau,@ngaybatdau+INTERVAL 28 DAY+INTERVAL 8 HOUR+INTERVAL 0 MINUTE,@ngaybatdau+INTERVAL 28 DAY+INTERVAL 8 HOUR+INTERVAL 20 MINUTE,'Đã thanh toán',0 WHERE NOT EXISTS (SELECT 1 FROM hoadon WHERE mahd=1097);
SET @themhoadon=ROW_COUNT();
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1097,mamon,1,gia FROM monnuoc WHERE tenmon='Coca Cola' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-1 WHERE tenmon='Coca Cola' AND @themhoadon=1;
UPDATE hoadon SET tongtien=(SELECT SUM(soluong*dongia) FROM chitiethoadon WHERE mahd=1097) WHERE mahd=1097 AND @themhoadon=1;

-- Hóa đơn mẫu 098: 2 dòng chi tiết.
INSERT INTO hoadon (mahd,maban,manv,ngaylap,ngaythanhtoan,trangthai,tongtien) SELECT 1098,(SELECT maban FROM ban ORDER BY maban LIMIT 1 OFFSET 1),@nvmau,@ngaybatdau+INTERVAL 29 DAY+INTERVAL 8 HOUR+INTERVAL 10 MINUTE,@ngaybatdau+INTERVAL 29 DAY+INTERVAL 8 HOUR+INTERVAL 30 MINUTE,'Đã thanh toán',0 WHERE NOT EXISTS (SELECT 1 FROM hoadon WHERE mahd=1098);
SET @themhoadon=ROW_COUNT();
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1098,mamon,2,gia FROM monnuoc WHERE tenmon='7 Up' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-2 WHERE tenmon='7 Up' AND @themhoadon=1;
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1098,mamon,3,gia FROM monnuoc WHERE tenmon='Lẩu thái hải sản' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-3 WHERE tenmon='Lẩu thái hải sản' AND @themhoadon=1;
UPDATE hoadon SET tongtien=(SELECT SUM(soluong*dongia) FROM chitiethoadon WHERE mahd=1098) WHERE mahd=1098 AND @themhoadon=1;

-- Hóa đơn mẫu 099: 3 dòng chi tiết.
INSERT INTO hoadon (mahd,maban,manv,ngaylap,ngaythanhtoan,trangthai,tongtien) SELECT 1099,(SELECT maban FROM ban ORDER BY maban LIMIT 1 OFFSET 2),@nvmau,@ngaybatdau+INTERVAL 29 DAY+INTERVAL 8 HOUR+INTERVAL 20 MINUTE,@ngaybatdau+INTERVAL 29 DAY+INTERVAL 8 HOUR+INTERVAL 40 MINUTE,'Đã thanh toán',0 WHERE NOT EXISTS (SELECT 1 FROM hoadon WHERE mahd=1099);
SET @themhoadon=ROW_COUNT();
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1099,mamon,3,gia FROM monnuoc WHERE tenmon='Mì xào bò' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-3 WHERE tenmon='Mì xào bò' AND @themhoadon=1;
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1099,mamon,1,gia FROM monnuoc WHERE tenmon='Trà tắc' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-1 WHERE tenmon='Trà tắc' AND @themhoadon=1;
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1099,mamon,2,gia FROM monnuoc WHERE tenmon='Rau muống xào tỏi' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-2 WHERE tenmon='Rau muống xào tỏi' AND @themhoadon=1;
UPDATE hoadon SET tongtien=(SELECT SUM(soluong*dongia) FROM chitiethoadon WHERE mahd=1099) WHERE mahd=1099 AND @themhoadon=1;

-- Hóa đơn mẫu 100: 1 dòng chi tiết.
INSERT INTO hoadon (mahd,maban,manv,ngaylap,ngaythanhtoan,trangthai,tongtien) SELECT 1100,(SELECT maban FROM ban ORDER BY maban LIMIT 1 OFFSET 3),@nvmau,@ngaybatdau+INTERVAL 29 DAY+INTERVAL 8 HOUR+INTERVAL 0 MINUTE,@ngaybatdau+INTERVAL 29 DAY+INTERVAL 8 HOUR+INTERVAL 20 MINUTE,'Đã thanh toán',0 WHERE NOT EXISTS (SELECT 1 FROM hoadon WHERE mahd=1100);
SET @themhoadon=ROW_COUNT();
INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) SELECT 1100,mamon,1,gia FROM monnuoc WHERE tenmon='Cam vắt' AND @themhoadon=1;
UPDATE monnuoc SET soluongton=soluongton-1 WHERE tenmon='Cam vắt' AND @themhoadon=1;
UPDATE hoadon SET tongtien=(SELECT SUM(soluong*dongia) FROM chitiethoadon WHERE mahd=1100) WHERE mahd=1100 AND @themhoadon=1;
COMMIT;

-- Đối chiếu: 100 hóa đơn mẫu, 199 dòng chi tiết, 30 ngày và mỗi hóa đơn 1..3 dòng.
SELECT COUNT(*) AS so_hoa_don_mau,MIN(DATE(ngaythanhtoan)) AS tu_ngay,MAX(DATE(ngaythanhtoan)) AS den_ngay,COUNT(DISTINCT DATE(ngaythanhtoan)) AS so_ngay
FROM hoadon WHERE mahd BETWEEN 1001 AND 1100;
SELECT h.mahd,COUNT(c.mamon) AS so_dong,h.tongtien,SUM(c.soluong*c.dongia) AS tong_chi_tiet
FROM hoadon h JOIN chitiethoadon c ON h.mahd=c.mahd WHERE h.mahd BETWEEN 1001 AND 1100
GROUP BY h.mahd,h.tongtien HAVING so_dong NOT BETWEEN 1 AND 3 OR h.tongtien<>tong_chi_tiet;
