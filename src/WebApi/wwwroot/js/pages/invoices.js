import { api, downloadFile } from "../api.js";
import { esc, fmtMoney, fmtDate, showToast } from "../ui.js";

let search = "";
let selectedId = 0;

export async function renderInvoicesPage(container) {
  container.innerHTML = `<div class="text-xs text-slate-500 p-4">Đang tải danh sách hóa đơn...</div>`;
  await draw(container);
}

async function draw(container) {
  const list = await api(`/api/hoadon?tim=${encodeURIComponent(search)}`);
  let detail = null;
  if (selectedId > 0) {
    try { detail = await api(`/api/hoadon/${selectedId}`); } catch { selectedId = 0; }
  }

  container.innerHTML = `
    <div class="grid grid-cols-1 lg:grid-cols-12 gap-4 items-start">
      <div class="lg:col-span-7 min-w-0 bg-white border border-slate-200 rounded-xl p-4 space-y-3">
        <div class="flex flex-wrap items-center justify-between gap-2">
          <input id="inv-search" type="text" value="${esc(search)}" placeholder="Tìm theo mã HĐ, tên bàn, nhân viên, trạng thái..."
            class="flex-1 min-w-[200px] px-3 py-2 text-xs rounded-lg border border-slate-300 focus:outline-none focus:ring-2 focus:ring-teal-600" />
          <span class="text-xs text-slate-500">${list.length} hóa đơn</span>
        </div>
        <div class="overflow-x-auto max-h-[500px] overflow-y-auto">
          <table class="w-full text-xs text-left">
            <thead class="border-b border-slate-200 text-slate-500 bg-slate-50 sticky top-0">
              <tr>
                <th class="py-2 px-2.5">Mã</th>
                <th class="py-2 px-2.5">Bàn</th>
                <th class="py-2 px-2.5">Nhân viên</th>
                <th class="py-2 px-2.5">Trạng thái</th>
                <th class="py-2 px-2.5 text-right">Tổng tiền</th>
              </tr>
            </thead>
            <tbody class="divide-y divide-slate-100">
              ${list.map(h => `
                <tr data-id="${h.mahd}" class="inv-row cursor-pointer hover:bg-slate-50 ${h.mahd === selectedId ? "bg-teal-50/70" : ""}">
                  <td class="py-2.5 px-2.5 font-bold">#${h.mahd}</td>
                  <td class="py-2.5 px-2.5">${esc(h.tenban)}</td>
                  <td class="py-2.5 px-2.5 text-slate-600">${esc(h.hoten)}</td>
                  <td class="py-2.5 px-2.5">
                    <span class="px-2 py-0.5 rounded-full text-xs font-medium ${h.trangthai === "Đã thanh toán" ? "bg-emerald-50 text-emerald-700" : "bg-amber-50 text-amber-700"}">${esc(h.trangthai)}</span>
                  </td>
                  <td class="py-2.5 px-2.5 text-right font-semibold tabular-nums">${fmtMoney(h.tongtien)}</td>
                </tr>`).join("")}
            </tbody>
          </table>
        </div>
      </div>

      <div class="lg:col-span-5 min-w-0 bg-white border border-slate-200 rounded-xl p-4 space-y-3">
        ${!detail
          ? `<div class="text-xs text-slate-400 py-16 text-center">Chọn một hóa đơn bên trái để xem chi tiết và xuất PDF.</div>`
          : `
            <div class="flex items-start justify-between gap-2 pb-3 border-b border-slate-200">
              <div>
                <h3 class="text-sm font-bold text-slate-900">HĐ #${detail.thongTin.mahd} — ${esc(detail.thongTin.tenban)}</h3>
                <div class="text-xs text-slate-500 mt-0.5">NV: ${esc(detail.thongTin.hoten)} • Lập: ${fmtDate(detail.thongTin.ngaylap)}</div>
                ${detail.thongTin.ngaythanhtoan ? `<div class="text-xs text-slate-500">Thanh toán: ${fmtDate(detail.thongTin.ngaythanhtoan)}</div>` : ""}
              </div>
              <button id="inv-pdf" class="px-3 py-1.5 rounded-lg bg-teal-700 text-white text-xs font-semibold hover:bg-teal-800 shrink-0 cursor-pointer">📄 Xuất PDF</button>
            </div>
            <div class="space-y-1.5 max-h-[340px] overflow-y-auto">
              ${detail.chiTiet.map(c => `
                <div class="flex items-center justify-between text-xs py-1.5 border-b border-slate-100">
                  <div>
                    <div class="font-semibold text-slate-800">${esc(c.tenmon)}</div>
                    <div class="text-slate-500">${fmtMoney(c.dongia)} × ${c.soluong}</div>
                  </div>
                  <div class="font-bold tabular-nums">${fmtMoney(c.thanhtien)}</div>
                </div>`).join("")}
            </div>
            <div class="pt-2 flex items-center justify-between text-sm font-bold">
              <span>Tổng cộng</span>
              <span class="text-teal-800 tabular-nums">${fmtMoney(detail.thongTin.tongtien)}</span>
            </div>
          `}
      </div>
    </div>
  `;

  const sEl = document.getElementById("inv-search");
  sEl.oninput = async (e) => {
    search = e.target.value;
    await draw(container);
    const el = document.getElementById("inv-search");
    el.focus();
    el.setSelectionRange(el.value.length, el.value.length);
  };

  container.querySelectorAll(".inv-row").forEach(tr => {
    tr.onclick = async () => {
      selectedId = Number(tr.dataset.id);
      await draw(container);
    };
  });

  const pdfBtn = document.getElementById("inv-pdf");
  if (pdfBtn) {
    pdfBtn.onclick = async () => {
      try {
        await downloadFile(`/api/hoadon/${selectedId}/pdf`, `HoaDon_${selectedId}.pdf`);
      } catch (err) {
        showToast(err.message, "error");
      }
    };
  }
}
