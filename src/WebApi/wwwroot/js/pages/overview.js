import { api } from "../api.js";
import { esc, fmtMoney, fmtDate } from "../ui.js";

export async function renderOverviewPage(container, onGoPosTable) {
  container.innerHTML = `<div class="text-xs text-slate-500 p-4">Đang tải tổng quan...</div>`;

  const [tk, hdList] = await Promise.all([
    api("/api/thongke/tong-quan"),
    api("/api/hoadon")
  ]);

  const stats = tk.tongQuan || { bantrong: 0, dangphucvu: 0, doanhthu: 0 };
  const tonThap = tk.tonThap || [];
  const activeBills = (hdList || []).filter(h => h.trangthai === "Chưa thanh toán");

  container.innerHTML = `
    <div class="space-y-4">
      <div class="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-4 gap-3">
        <div class="bg-white border border-slate-200 rounded-xl p-4">
          <div class="text-xs text-slate-500 font-medium">Bàn trống</div>
          <div class="text-2xl font-bold text-slate-900 mt-1 tabular-nums">${stats.bantrong}</div>
          <div class="text-xs text-emerald-700 mt-1">Sẵn sàng đón khách</div>
        </div>
        <div class="bg-white border border-slate-200 rounded-xl p-4">
          <div class="text-xs text-slate-500 font-medium">Đang phục vụ</div>
          <div class="text-2xl font-bold text-slate-900 mt-1 tabular-nums">${stats.dangphucvu}</div>
          <div class="text-xs text-amber-600 mt-1">Hóa đơn chưa thanh toán</div>
        </div>
        <div class="bg-white border border-slate-200 rounded-xl p-4">
          <div class="text-xs text-slate-500 font-medium">Doanh thu hôm nay</div>
          <div class="text-2xl font-bold text-teal-800 mt-1 tabular-nums">${fmtMoney(stats.doanhthu)}</div>
          <div class="text-xs text-slate-400 mt-1">Tính theo ngày thanh toán</div>
        </div>
        <div class="bg-white border border-slate-200 rounded-xl p-4">
          <div class="text-xs text-slate-500 font-medium">Món tồn thấp (&lt; 10)</div>
          <div class="text-2xl font-bold text-rose-600 mt-1 tabular-nums">${tonThap.length}</div>
          <div class="text-xs text-slate-400 mt-1">Cần kiểm tra nhập hàng</div>
        </div>
      </div>

      <div class="grid grid-cols-1 lg:grid-cols-2 gap-4">
        <div class="bg-white border border-slate-200 rounded-xl p-4 space-y-3">
          <div class="flex items-center justify-between">
            <h3 class="text-sm font-bold text-slate-900">Bàn đang hoạt động</h3>
            <a href="#/pos" class="text-xs font-semibold text-teal-700 hover:underline">Mở POS →</a>
          </div>
          <div class="space-y-2">
            ${activeBills.length === 0
              ? `<div class="text-xs text-slate-400 py-8 text-center">Hiện chưa có bàn nào đang phục vụ.</div>`
              : activeBills.map(b => `
                <div class="flex items-center justify-between gap-2 p-3 rounded-xl border border-slate-200 bg-slate-50">
                  <div class="min-w-0">
                    <div class="text-xs font-bold text-slate-900">${esc(b.tenban)} <span class="font-normal text-slate-500">• HĐ #${b.mahd}</span></div>
                    <div class="text-xs text-slate-500 truncate">NV: ${esc(b.hoten)} • ${fmtDate(b.ngaylap)}</div>
                  </div>
                  <div class="flex items-center gap-2 shrink-0">
                    <span class="text-xs font-bold text-amber-700 tabular-nums">${fmtMoney(b.tongtien)}</span>
                    <button data-maban="${b.maban}" class="ov-go-pos px-2.5 py-1.5 rounded-lg bg-teal-700 hover:bg-teal-800 text-white text-xs font-semibold cursor-pointer">Vào bàn</button>
                  </div>
                </div>`).join("")}
          </div>
        </div>

        <div class="bg-white border border-slate-200 rounded-xl p-4 space-y-3">
          <div class="flex items-center justify-between">
            <h3 class="text-sm font-bold text-slate-900">Cảnh báo tồn kho thấp</h3>
            <a href="#/import" class="text-xs px-2.5 py-1 rounded-lg border border-slate-300 font-semibold hover:bg-slate-50">+ Nhập hàng</a>
          </div>
          ${tonThap.length === 0
            ? `<div class="text-xs text-slate-400 py-8 text-center">Tất cả món đều đủ tồn kho.</div>`
            : `<table class="w-full text-xs text-left">
                <thead class="border-b border-slate-200 text-slate-500">
                  <tr><th class="py-2">Tên món</th><th class="py-2 text-right">Giá bán</th><th class="py-2 text-right">Tồn kho</th></tr>
                </thead>
                <tbody class="divide-y divide-slate-100">
                  ${tonThap.map(m => `
                    <tr>
                      <td class="py-2 font-medium text-slate-800">${esc(m.tenmon)}</td>
                      <td class="py-2 text-right tabular-nums">${fmtMoney(m.gia)}</td>
                      <td class="py-2 text-right tabular-nums font-bold ${m.soluongton <= 3 ? "text-rose-600" : "text-amber-600"}">${m.soluongton} ${esc(m.donvi)}</td>
                    </tr>`).join("")}
                </tbody>
              </table>`}
        </div>
      </div>
    </div>
  `;

  container.querySelectorAll(".ov-go-pos").forEach(btn => {
    btn.onclick = () => onGoPosTable(Number(btn.dataset.maban));
  });
}
