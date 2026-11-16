import { api } from "../api.js";
import { esc, fmtMoney, fmtDate, showToast, confirmDialog } from "../ui.js";

let draftItems = [];
let maXacNhan = "";
let nccList = [];
let monList = [];
let receipts = [];
let viewingId = 0;

function newConfirmCode() {
  return (crypto.randomUUID ? crypto.randomUUID().replace(/-/g, "") : Math.random().toString(16).slice(2).padEnd(32, "0")).slice(0, 32);
}

export async function renderImportPage(container) {
  if (!maXacNhan) maXacNhan = newConfirmCode();
  const [dm, ds] = await Promise.all([
    api("/api/nhaphang/danh-muc"),
    api("/api/nhaphang")
  ]);
  nccList = dm.ncc || [];
  monList = dm.mon || [];
  receipts = ds || [];
  draw(container);
}

async function draw(container) {
  let viewDetail = null;
  if (viewingId > 0) {
    viewDetail = await api(`/api/nhaphang/${viewingId}`);
  }
  const totalDraft = draftItems.reduce((s, d) => s + d.soLuong * d.donGiaNhap, 0);

  container.innerHTML = `
    <div class="grid grid-cols-1 lg:grid-cols-12 gap-4 items-start">
      <!-- Lịch sử phiếu nhập -->
      <div class="lg:col-span-5 min-w-0 bg-white border border-slate-200 rounded-xl p-4 space-y-3">
        <div class="flex items-center justify-between">
          <h3 class="text-sm font-bold text-slate-900">Phiếu nhập đã ghi nhận</h3>
          <button id="imp-new" class="px-3 py-1.5 rounded-lg bg-teal-700 text-white text-xs font-semibold hover:bg-teal-800 cursor-pointer">+ Phiếu mới</button>
        </div>
        <div class="overflow-x-auto max-h-[480px] overflow-y-auto">
          <table class="w-full text-xs text-left">
            <thead class="border-b border-slate-200 text-slate-500 bg-slate-50 sticky top-0">
              <tr><th class="py-2 px-2">Mã</th><th class="py-2 px-2">Nhà cung cấp</th><th class="py-2 px-2">Ngày nhập</th><th class="py-2 px-2 text-right">Tổng tiền</th></tr>
            </thead>
            <tbody class="divide-y divide-slate-100">
              ${receipts.map(p => `
                <tr data-pn="${p.mapn}" class="imp-row cursor-pointer hover:bg-slate-50 ${viewingId === p.mapn ? "bg-teal-50" : ""}">
                  <td class="py-2 px-2 font-bold">#${p.mapn}</td>
                  <td class="py-2 px-2">${esc(p.tenncc)}</td>
                  <td class="py-2 px-2 text-slate-500">${fmtDate(p.ngaynhap)}</td>
                  <td class="py-2 px-2 text-right font-semibold tabular-nums">${fmtMoney(p.tongtien)}</td>
                </tr>`).join("")}
            </tbody>
          </table>
        </div>
      </div>

      <!-- Tạo phiếu mới hoặc xem phiếu cũ -->
      <div class="lg:col-span-7 min-w-0 bg-white border border-slate-200 rounded-xl p-4 space-y-4">
        ${viewDetail ? `
          <div class="flex items-center justify-between border-b border-slate-200 pb-3">
            <div>
              <h3 class="text-sm font-bold text-slate-900">Phiếu nhập #${viewDetail.thongTin.mapn} (Đã ghi nhận)</h3>
              <div class="text-xs text-slate-500">NV: ${esc(viewDetail.thongTin.hoten)} • Ngày: ${fmtDate(viewDetail.thongTin.ngaynhap)}</div>
            </div>
            <button id="imp-back-new" class="px-3 py-1.5 rounded-lg border border-slate-300 text-xs font-semibold hover:bg-slate-50 cursor-pointer">Tạo phiếu mới</button>
          </div>
          <table class="w-full text-xs text-left">
            <thead class="border-b border-slate-200 text-slate-500">
              <tr><th class="py-2">Món / nước</th><th class="py-2 text-right">SL</th><th class="py-2 text-right">Đơn giá nhập</th><th class="py-2 text-right">Thành tiền</th></tr>
            </thead>
            <tbody class="divide-y divide-slate-100">
              ${viewDetail.chiTiet.map(c => `
                <tr>
                  <td class="py-2 font-medium">${esc(c.tenmon)}</td>
                  <td class="py-2 text-right tabular-nums">${c.soluong}</td>
                  <td class="py-2 text-right tabular-nums">${fmtMoney(c.dongianhap)}</td>
                  <td class="py-2 text-right font-bold tabular-nums">${fmtMoney(c.thanhtien)}</td>
                </tr>`).join("")}
            </tbody>
          </table>
          <div class="text-right text-sm font-bold text-teal-800 pt-2 border-t border-slate-200">
            Tổng nhập: ${fmtMoney(viewDetail.thongTin.tongtien)}
          </div>
        ` : `
          <div class="border-b border-slate-200 pb-3">
            <h3 class="text-sm font-bold text-slate-900">Tạo phiếu nhập hàng mới</h3>
            <p class="text-xs text-slate-500">Tồn kho chỉ tăng sau khi bấm Ghi nhận phiếu.</p>
          </div>
          <div class="grid grid-cols-1 sm:grid-cols-2 gap-3 text-xs">
            <div>
              <label class="block font-semibold text-slate-700 mb-1">Nhà cung cấp</label>
              <select id="imp-ncc" class="w-full px-3 py-2 rounded-lg border border-slate-300 bg-white">
                ${nccList.map(n => `<option value="${n.mancc}">${esc(n.tenncc)}</option>`).join("")}
              </select>
            </div>
            <div>
              <label class="block font-semibold text-slate-700 mb-1">Ngày nhập</label>
              <input id="imp-date" type="datetime-local" value="${new Date(Date.now() - new Date().getTimezoneOffset() * 60000).toISOString().slice(0, 16)}"
                class="w-full px-3 py-2 rounded-lg border border-slate-300" />
            </div>
          </div>

          <div class="p-3 rounded-xl bg-slate-50 border border-slate-200 grid grid-cols-1 sm:grid-cols-12 gap-2 items-end text-xs">
            <div class="sm:col-span-5">
              <label class="block font-semibold text-slate-700 mb-1">Món / nước</label>
              <select id="imp-mon" class="w-full px-2.5 py-2 rounded-lg border border-slate-300 bg-white">
                ${monList.map(m => `<option value="${m.mamon}" data-ten="${esc(m.tenmon)}">${esc(m.hienthi)}</option>`).join("")}
              </select>
            </div>
            <div class="sm:col-span-2">
              <label class="block font-semibold text-slate-700 mb-1">Số lượng</label>
              <input id="imp-sl" type="number" min="1" value="10" class="w-full px-2.5 py-2 rounded-lg border border-slate-300 bg-white" />
            </div>
            <div class="sm:col-span-3">
              <label class="block font-semibold text-slate-700 mb-1">Đơn giá nhập</label>
              <input id="imp-gia" type="number" min="1" value="20000" class="w-full px-2.5 py-2 rounded-lg border border-slate-300 bg-white" />
            </div>
            <div class="sm:col-span-2">
              <button id="imp-add-line" class="w-full py-2 px-3 rounded-lg bg-slate-900 hover:bg-slate-800 text-white font-semibold cursor-pointer">+ Thêm</button>
            </div>
          </div>

          <div class="overflow-x-auto">
            <table class="w-full text-xs text-left">
              <thead class="border-b border-slate-200 text-slate-500">
                <tr><th class="py-2">Món</th><th class="py-2 text-right">SL</th><th class="py-2 text-right">Đơn giá</th><th class="py-2 text-right">Thành tiền</th><th class="py-2"></th></tr>
              </thead>
              <tbody class="divide-y divide-slate-100">
                ${draftItems.length === 0
                  ? `<tr><td colspan="5" class="py-6 text-center text-slate-400">Chưa có dòng hàng nào.</td></tr>`
                  : draftItems.map((d, idx) => `
                    <tr>
                      <td class="py-2 font-medium">${esc(d.tenMon)}</td>
                      <td class="py-2 text-right tabular-nums">${d.soLuong}</td>
                      <td class="py-2 text-right tabular-nums">${fmtMoney(d.donGiaNhap)}</td>
                      <td class="py-2 text-right font-bold tabular-nums">${fmtMoney(d.soLuong * d.donGiaNhap)}</td>
                      <td class="py-2 text-right"><button data-rm="${idx}" class="imp-rm text-rose-600 hover:underline cursor-pointer">Bỏ</button></td>
                    </tr>`).join("")}
              </tbody>
            </table>
          </div>

          <div class="flex flex-wrap items-center justify-between gap-3 pt-3 border-t border-slate-200">
            <div class="text-sm font-bold text-slate-900">Tổng nhập: <span class="text-teal-800 tabular-nums">${fmtMoney(totalDraft)}</span></div>
            <button id="imp-save" ${draftItems.length === 0 ? "disabled" : ""} class="px-4 py-2 rounded-xl bg-teal-700 hover:bg-teal-800 text-white text-xs font-semibold disabled:opacity-40 cursor-pointer">✓ Ghi nhận phiếu nhập</button>
          </div>
        `}
      </div>
    </div>
  `;

  document.querySelectorAll(".imp-row").forEach(tr => {
    tr.onclick = async () => {
      viewingId = Number(tr.dataset.pn);
      await draw(container);
    };
  });

  const newBtn = document.getElementById("imp-new");
  if (newBtn) newBtn.onclick = async () => { viewingId = 0; await draw(container); };

  const backBtn = document.getElementById("imp-back-new");
  if (backBtn) backBtn.onclick = async () => { viewingId = 0; await draw(container); };

  const addLine = document.getElementById("imp-add-line");
  if (addLine) {
    addLine.onclick = async () => {
      const sel = document.getElementById("imp-mon");
      const maMon = Number(sel.value);
      const tenMon = sel.options[sel.selectedIndex]?.dataset.ten || "";
      const soLuong = Number(document.getElementById("imp-sl").value);
      const donGiaNhap = Number(document.getElementById("imp-gia").value);
      if (soLuong < 1 || donGiaNhap < 1) { showToast("Số lượng và đơn giá phải > 0.", "error"); return; }
      if (draftItems.some(d => d.maMon === maMon)) { showToast("Món đã có trong phiếu.", "warning"); return; }
      draftItems.push({ maMon, tenMon, soLuong, donGiaNhap });
      await draw(container);
    };
  }

  container.querySelectorAll(".imp-rm").forEach(btn => {
    btn.onclick = async () => {
      draftItems.splice(Number(btn.dataset.rm), 1);
      await draw(container);
    };
  });

  const saveBtn = document.getElementById("imp-save");
  if (saveBtn) {
    saveBtn.onclick = () => {
      confirmDialog("Ghi nhận phiếu nhập?", "Tồn kho sẽ được cộng ngay sau khi ghi nhận và không thể sửa/xóa phiếu.", async () => {
        try {
          const res = await api("/api/nhaphang", {
            method: "POST",
            body: JSON.stringify({
              maNcc: Number(document.getElementById("imp-ncc").value),
              ngayNhap: document.getElementById("imp-date").value,
              maXacNhan,
              chiTiet: draftItems
            })
          });
          showToast(res.message, "success");
          draftItems = [];
          maXacNhan = newConfirmCode();
          viewingId = res.mapn;
          await renderImportPage(container);
        } catch (err) {
          showToast(err.message, "error");
        }
      });
    };
  }
}
