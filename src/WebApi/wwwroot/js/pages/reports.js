import { api, downloadFile } from "../api.js";
import { esc, fmtMoney, showToast } from "../ui.js";

let loai = 0;
let tu = new Date(Date.now() - 29 * 86400000).toISOString().slice(0, 10);
let den = new Date().toISOString().slice(0, 10);
let thang = false;
let nam = new Date().getFullYear();

export async function renderReportsPage(container) {
  await draw(container);
}

async function draw(container) {
  const qs = `loai=${loai}&tu=${tu}&den=${den}&thang=${thang}&nam=${nam}`;
  const data = await api(`/api/thongke/bao-cao?${qs}`);
  const isQuarter = loai === 1 || loai === 2;

  container.innerHTML = `
    <div class="bg-white border border-slate-200 rounded-xl p-4 space-y-4">
      <!-- Tabs -->
      <div class="flex flex-wrap gap-2 border-b border-slate-200 pb-3 text-xs">
        ${[
          [0, "Thu - Chi - Lợi nhuận"],
          [1, "Nhân viên theo Quý"],
          [2, "Sản phẩm theo Quý"],
          [3, "Món bán chạy"]
        ].map(([id, label]) => `
          <button data-loai="${id}" class="rep-tab px-3 py-2 rounded-lg font-semibold border cursor-pointer ${
            loai === id ? "border-teal-700 bg-teal-700 text-white" : "border-slate-300 bg-white hover:bg-slate-50"
          }">${label}</button>`).join("")}
      </div>

      <!-- Filters + Export -->
      <div class="flex flex-wrap items-end justify-between gap-3 text-xs">
        <div class="flex flex-wrap items-end gap-2">
          ${isQuarter ? `
            <div>
              <label class="block font-semibold text-slate-600 mb-1">Năm</label>
              <input id="rep-nam" type="number" min="2000" max="2100" value="${nam}" class="w-24 px-2.5 py-1.5 rounded-lg border border-slate-300" />
            </div>
          ` : `
            <div>
              <label class="block font-semibold text-slate-600 mb-1">Từ ngày</label>
              <input id="rep-tu" type="date" value="${tu}" class="px-2.5 py-1.5 rounded-lg border border-slate-300" />
            </div>
            <div>
              <label class="block font-semibold text-slate-600 mb-1">Đến ngày</label>
              <input id="rep-den" type="date" value="${den}" class="px-2.5 py-1.5 rounded-lg border border-slate-300" />
            </div>
            ${loai === 0 ? `
              <div>
                <label class="block font-semibold text-slate-600 mb-1">Nhóm theo</label>
                <select id="rep-thang" class="px-2.5 py-1.5 rounded-lg border border-slate-300 bg-white">
                  <option value="false" ${!thang ? "selected" : ""}>Theo ngày</option>
                  <option value="true" ${thang ? "selected" : ""}>Theo tháng</option>
                </select>
              </div>` : ""}
          `}
          <button id="rep-view" class="px-3.5 py-1.5 rounded-lg bg-slate-900 text-white font-semibold hover:bg-slate-800 cursor-pointer">Xem báo cáo</button>
        </div>

        <div class="flex gap-2">
          <button id="rep-xls" class="px-3 py-1.5 rounded-lg border border-slate-300 font-semibold hover:bg-slate-50 cursor-pointer">📊 Xuất Excel (.xls)</button>
          <button id="rep-pdf" class="px-3 py-1.5 rounded-lg bg-teal-700 text-white font-semibold hover:bg-teal-800 cursor-pointer">📄 Xuất PDF</button>
        </div>
      </div>

      <!-- Table -->
      <div class="overflow-x-auto max-h-[460px] overflow-y-auto border border-slate-200 rounded-xl">
        <table class="w-full text-xs text-left">
          <thead class="bg-slate-50 border-b border-slate-200 text-slate-600 sticky top-0">
            <tr>
              ${data.columns.map((c, idx) => `<th class="py-2.5 px-3 ${idx > 0 ? "text-right" : ""}">${esc(c)}</th>`).join("")}
            </tr>
          </thead>
          <tbody class="divide-y divide-slate-100">
            ${data.rows.length === 0
              ? `<tr><td colspan="${data.columns.length}" class="py-8 text-center text-slate-400">Không có dữ liệu trong kỳ.</td></tr>`
              : data.rows.map(r => {
                  const isTotal = r[data.columns[0]] === "Tổng";
                  return `
                    <tr class="${isTotal ? "bg-teal-50 font-bold" : "hover:bg-slate-50"}">
                      ${data.columns.map((c, idx) => {
                        const v = r[c];
                        const isMoneyCol = c.includes("(đ)") || c.includes("Quý");
                        return `<td class="py-2 px-3 ${idx > 0 ? "text-right tabular-nums" : ""}">${isMoneyCol ? fmtMoney(v) : esc(v)}</td>`;
                      }).join("")}
                    </tr>`;
                }).join("")}
          </tbody>
        </table>
      </div>
    </div>
  `;

  container.querySelectorAll(".rep-tab").forEach(btn => {
    btn.onclick = async () => {
      loai = Number(btn.dataset.loai);
      await draw(container);
    };
  });

  document.getElementById("rep-view").onclick = async () => {
    if (isQuarter) {
      nam = Number(document.getElementById("rep-nam").value);
    } else {
      tu = document.getElementById("rep-tu").value;
      den = document.getElementById("rep-den").value;
      const thEl = document.getElementById("rep-thang");
      if (thEl) thang = thEl.value === "true";
    }
    try {
      await draw(container);
    } catch (err) {
      showToast(err.message, "error");
    }
  };

  document.getElementById("rep-pdf").onclick = async () => {
    try { await downloadFile(`/api/thongke/xuat-pdf?${qs}`, "BaoCao.pdf"); }
    catch (err) { showToast(err.message, "error"); }
  };

  document.getElementById("rep-xls").onclick = async () => {
    try { await downloadFile(`/api/thongke/xuat-xls?${qs}`, "BaoCao.xls"); }
    catch (err) { showToast(err.message, "error"); }
  };
}
