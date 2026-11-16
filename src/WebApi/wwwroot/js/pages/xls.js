import { api, downloadFile } from "../api.js";
import { esc, showToast, confirmDialog } from "../ui.js";

let loai = 0;
let previewData = null;
let selectedFile = null;

export async function renderXlsPage(container) {
  draw(container);
}

function draw(container) {
  const canImport = loai < 3;
  container.innerHTML = `
    <div class="bg-white border border-slate-200 rounded-xl p-5 space-y-4">
      <div class="flex flex-wrap items-end justify-between gap-3 border-b border-slate-200 pb-4">
        <div class="space-y-1">
          <label class="block text-xs font-semibold text-slate-700">Chọn danh mục</label>
          <select id="xls-loai" class="px-3 py-2 text-xs rounded-lg border border-slate-300 bg-white">
            <option value="0" ${loai === 0 ? "selected" : ""}>0. Món ăn / Nước uống</option>
            <option value="1" ${loai === 1 ? "selected" : ""}>1. Bàn ăn</option>
            <option value="2" ${loai === 2 ? "selected" : ""}>2. Nhà cung cấp</option>
            <option value="3" ${loai === 3 ? "selected" : ""}>3. Nhân viên (chỉ xuất)</option>
          </select>
        </div>

        <div class="flex flex-wrap gap-2 text-xs">
          ${canImport ? `<button id="xls-tpl" class="px-3 py-2 rounded-lg border border-slate-300 font-semibold hover:bg-slate-50 cursor-pointer">📥 Tải file mẫu (.xls)</button>` : ""}
          <button id="xls-exp" class="px-3 py-2 rounded-lg bg-slate-900 text-white font-semibold hover:bg-slate-800 cursor-pointer">📤 Xuất danh mục (.xls)</button>
        </div>
      </div>

      ${canImport ? `
        <div class="p-4 rounded-xl bg-slate-50 border border-slate-200 flex flex-wrap items-center justify-between gap-3 text-xs">
          <div class="flex items-center gap-3">
            <input id="xls-file" type="file" accept=".xls" class="text-xs" />
            <button id="xls-read" class="px-3 py-2 rounded-lg border border-slate-300 bg-white font-semibold hover:bg-slate-100 cursor-pointer">👁️ Đọc &amp; Xem trước</button>
          </div>
          <button id="xls-imp" ${!previewData ? "disabled" : ""} class="px-4 py-2 rounded-lg bg-teal-700 hover:bg-teal-800 text-white font-semibold disabled:opacity-40 cursor-pointer">✓ Nhập vào CSDL</button>
        </div>
      ` : `<div class="text-xs text-amber-700 bg-amber-50 border border-amber-200 rounded-lg p-3">Danh mục Nhân viên chỉ cho phép xuất thông tin (không kèm mật khẩu) và không hỗ trợ import.</div>`}

      ${previewData ? `
        <div class="space-y-2">
          <div class="text-xs font-semibold text-teal-800">Xem trước ${previewData.rows.length} dòng từ file:</div>
          <div class="overflow-x-auto max-h-[380px] border border-slate-200 rounded-xl">
            <table class="w-full text-xs text-left">
              <thead class="bg-slate-50 border-b border-slate-200">
                <tr>${previewData.columns.map(c => `<th class="py-2 px-3">${esc(c)}</th>`).join("")}</tr>
              </thead>
              <tbody class="divide-y divide-slate-100">
                ${previewData.rows.map(r => `<tr>${previewData.columns.map(c => `<td class="py-2 px-3">${esc(r[c])}</td>`).join("")}</tr>`).join("")}
              </tbody>
            </table>
          </div>
        </div>
      ` : ""}
    </div>
  `;

  document.getElementById("xls-loai").onchange = (e) => {
    loai = Number(e.target.value);
    previewData = null;
    selectedFile = null;
    draw(container);
  };

  const tplBtn = document.getElementById("xls-tpl");
  if (tplBtn) tplBtn.onclick = () => downloadFile(`/api/xls/mau/${loai}`, `MauNhap_${loai}.xls`).catch(e => showToast(e.message, "error"));

  document.getElementById("xls-exp").onclick = () => downloadFile(`/api/xls/xuat/${loai}`, `DanhMuc_${loai}.xls`).catch(e => showToast(e.message, "error"));

  const readBtn = document.getElementById("xls-read");
  if (readBtn) {
    readBtn.onclick = async () => {
      const fileInput = document.getElementById("xls-file");
      if (!fileInput.files.length) { showToast("Chọn file .xls trước.", "warning"); return; }
      selectedFile = fileInput.files[0];
      const fd = new FormData();
      fd.append("file", selectedFile);
      try {
        previewData = await api(`/api/xls/doc/${loai}`, { method: "POST", body: fd });
        showToast(`Đã đọc ${previewData.rows.length} dòng.`, "success");
        draw(container);
      } catch (err) {
        showToast(err.message, "error");
      }
    };
  }

  const impBtn = document.getElementById("xls-imp");
  if (impBtn && previewData && selectedFile) {
    impBtn.onclick = () => {
      confirmDialog("Xác nhận Import Excel", `Thêm ${previewData.rows.length} bản ghi vào danh mục? Nếu có 1 dòng lỗi, toàn bộ giao dịch sẽ hủy.`, async () => {
        const fd = new FormData();
        fd.append("file", selectedFile);
        try {
          const res = await api(`/api/xls/nhap/${loai}`, { method: "POST", body: fd });
          showToast(res.message, "success");
          previewData = null;
          selectedFile = null;
          draw(container);
        } catch (err) {
          showToast(err.message, "error");
        }
      });
    };
  }
}
