import { api, downloadFile } from "../api.js";
import { esc, fmtMoney, fmtDate, showToast, confirmDialog, openModal, closeModal } from "../ui.js";

let state = {
  tables: [],
  menu: [],
  openInvoices: {},
  selectedTableId: 0,
  selectedBill: null,
  billItems: [],
  filterType: "Tất cả",
  search: ""
};

// Parse "Bàn 01 · 4 chỗ · Đang phục vụ" -> { maban, tenban, socho, trangthai }
function parseTableRow(row) {
  const parts = String(row.hienthi || "").split(" · ");
  return {
    maban: row.maban,
    tenban: parts[0] || `Bàn #${row.maban}`,
    socho: parts[1] || "",
    trangthai: parts[2] || "Trống"
  };
}

// Parse "Cơm chiên hải sản · 65000 đ / Đĩa · tồn: 18"
function parseMenuRow(row) {
  const raw = String(row.hienthi || "");
  const parts = raw.split(" · ");
  const tenmon = parts[0] || "";
  const giaPart = parts[1] || "";
  const tonPart = parts[2] || "";
  const giaMatch = giaPart.match(/(\d+)/);
  const unitMatch = giaPart.split("/")[1]?.trim() || "";
  const tonMatch = tonPart.match(/(-?\d+)/);
  return {
    mamon: row.mamon,
    tenmon,
    gia: giaMatch ? Number(giaMatch[1]) : 0,
    donvi: unitMatch,
    ton: tonMatch ? Number(tonMatch[1]) : 0
  };
}

export async function renderPosPage(container, ctx) {
  if (ctx?.selectedTableId) {
    state.selectedTableId = ctx.selectedTableId;
  }

  container.innerHTML = `<div class="text-xs text-slate-500 p-4">Đang tải dữ liệu POS...</div>`;
  await loadPosData();
  drawPos(container);
  ctx?.onStatsChanged?.();
}

async function loadPosData() {
  const data = await api("/api/hoadon/pos-data");
  state.tables = (data.ban || []).map(parseTableRow);
  state.menu = (data.mon || []).map(parseMenuRow);

  state.openInvoices = {};
  for (const hd of data.hoaDon || []) {
    if (hd.trangthai === "Chưa thanh toán") {
      state.openInvoices[hd.maban] = hd;
    }
  }

  if (!state.selectedTableId || !state.tables.some(t => t.maban === state.selectedTableId)) {
    const busy = state.tables.find(t => t.trangthai === "Đang phục vụ");
    state.selectedTableId = busy ? busy.maban : (state.tables[0]?.maban || 0);
  }

  const activeInv = state.openInvoices[state.selectedTableId];
  if (activeInv) {
    const detail = await api(`/api/hoadon/${activeInv.mahd}`);
    state.selectedBill = detail.thongTin;
    state.billItems = detail.chiTiet || [];
  } else {
    state.selectedBill = null;
    state.billItems = [];
  }
}

function drawPos(container) {
  const activeTable = state.tables.find(t => t.maban === state.selectedTableId);
  const filteredMenu = state.menu.filter(m =>
    m.tenmon.toLowerCase().includes(state.search.trim().toLowerCase())
  );

  container.innerHTML = `
    <div class="grid grid-cols-1 lg:grid-cols-12 gap-3 items-start">
      <!-- Cột 1: Sơ đồ bàn -->
      <div class="lg:col-span-3 min-w-0 bg-white border border-slate-200 rounded-xl p-3 space-y-2.5 overflow-hidden">
        <div class="flex items-center justify-between gap-2">
          <span class="text-xs font-bold uppercase tracking-wider text-slate-500">Sơ đồ bàn (${state.tables.length})</span>
          <button id="pos-refresh" class="text-xs text-teal-700 hover:underline font-semibold cursor-pointer">↻ Tải lại</button>
        </div>
        <div class="grid grid-cols-2 gap-2 max-h-[520px] overflow-y-auto pr-0.5">
          ${state.tables.map(t => {
            const busy = t.trangthai === "Đang phục vụ";
            const inv = state.openInvoices[t.maban];
            const isSel = t.maban === state.selectedTableId;
            return `
              <button data-table="${t.maban}" class="pos-table-btn text-left p-2.5 rounded-xl border transition cursor-pointer min-w-0 ${
                isSel ? "border-teal-700 ring-2 ring-teal-600/20 bg-teal-50/40" : "border-slate-200 bg-white hover:border-slate-300"
              }">
                <div class="flex items-center justify-between gap-1">
                  <span class="text-xs font-bold text-slate-900 truncate">${esc(t.tenban)}</span>
                  <span class="w-2.5 h-2.5 rounded-full shrink-0 ${busy ? "bg-amber-500" : "bg-emerald-500"}"></span>
                </div>
                <div class="text-xs text-slate-500 mt-0.5 truncate">${esc(t.socho)}</div>
                <div class="text-xs font-bold mt-1.5 tabular-nums truncate ${busy ? "text-amber-700" : "text-slate-400"}">
                  ${busy && inv ? fmtMoney(inv.tongtien) : "Trống"}
                </div>
              </button>`;
          }).join("")}
        </div>
      </div>

      <!-- Cột 2: Thực đơn (đặt ô tìm kiếm tách hàng rõ ràng, chống đè nút) -->
      <div class="lg:col-span-5 min-w-0 bg-white border border-slate-200 rounded-xl p-3 space-y-3 overflow-hidden">
        <div class="space-y-2">
          <input id="pos-search" type="text" value="${esc(state.search)}" placeholder="Tìm tên món ăn / nước uống..."
            class="w-full min-w-0 px-3 py-2 text-xs rounded-lg border border-slate-300 focus:outline-none focus:ring-2 focus:ring-teal-600" />
          <div class="flex flex-wrap items-center justify-between gap-2 text-xs">
            <span class="text-slate-500">Chạm vào món để thêm (+1) vào bàn đang chọn</span>
            <span class="font-semibold text-slate-700">${filteredMenu.length} món</span>
          </div>
        </div>

        <div id="pos-menu-grid" class="grid grid-cols-1 sm:grid-cols-2 gap-2 max-h-[470px] overflow-y-auto pr-0.5">
          ${filteredMenu.length === 0
            ? `<div class="col-span-2 text-xs text-slate-400 text-center py-8">Không tìm thấy món phù hợp.</div>`
            : filteredMenu.map(m => {
                const out = m.ton <= 0;
                return `
                  <button data-menu="${m.mamon}" ${out ? "disabled" : ""}
                    class="pos-menu-btn text-left p-2.5 rounded-xl border transition flex flex-col justify-between min-w-0 ${
                      out ? "border-slate-200 bg-slate-50 opacity-60 cursor-not-allowed" : "border-slate-200 bg-slate-50/70 hover:border-teal-600 hover:bg-white cursor-pointer"
                    }">
                    <div>
                      <div class="text-xs font-bold text-slate-900 leading-snug">${esc(m.tenmon)}</div>
                      <div class="text-xs text-slate-500 mt-0.5">${esc(m.donvi)}</div>
                    </div>
                    <div class="flex items-center justify-between gap-2 mt-2 pt-1.5 border-t border-slate-200">
                      <span class="text-xs font-bold text-teal-800 tabular-nums">${fmtMoney(m.gia)}</span>
                      <span class="text-xs tabular-nums ${m.ton < 5 ? "text-rose-600 font-bold" : "text-slate-500"}">Tồn: ${m.ton}</span>
                    </div>
                  </button>`;
              }).join("")}
        </div>
      </div>

      <!-- Cột 3: Chi tiết hóa đơn -->
      <div class="lg:col-span-4 min-w-0 bg-white border border-slate-200 rounded-xl p-3 flex flex-col justify-between overflow-hidden min-h-[540px]">
        <div class="space-y-2.5 min-w-0">
          <div class="flex flex-wrap items-start justify-between gap-2 pb-2.5 border-b border-slate-200">
            <div class="min-w-0">
              <div class="text-sm font-bold text-slate-900 truncate">
                ${activeTable ? esc(activeTable.tenban) : "Chưa chọn bàn"}
                ${state.selectedBill ? `<span class="text-xs font-normal text-slate-500">• HĐ #${state.selectedBill.mahd}</span>` : ""}
              </div>
              <div class="text-xs text-slate-500 mt-0.5">
                ${state.selectedBill ? `NV: ${esc(state.selectedBill.hoten)} • ${fmtDate(state.selectedBill.ngaylap)}` : "Bàn trống — bấm Mở bàn hoặc chạm món"}
              </div>
            </div>
            ${state.selectedBill ? `
              <div class="flex items-center gap-1.5 shrink-0">
                <button id="pos-change-table" class="text-xs px-2.5 py-1 rounded-lg border border-slate-300 hover:bg-slate-50 font-medium whitespace-nowrap cursor-pointer">Đổi bàn</button>
                <button id="pos-cancel-bill" class="text-xs px-2.5 py-1 rounded-lg border border-rose-200 text-rose-600 hover:bg-rose-50 font-medium whitespace-nowrap cursor-pointer">Hủy HĐ</button>
              </div>` : `
              <button id="pos-open-table" class="text-xs px-3 py-1.5 rounded-lg bg-teal-700 text-white font-semibold hover:bg-teal-800 whitespace-nowrap cursor-pointer">Mở bàn</button>
            `}
          </div>

          <div class="space-y-2 max-h-[340px] overflow-y-auto pr-0.5">
            ${!state.selectedBill
              ? `<div class="text-xs text-slate-400 py-12 text-center">Bàn đang trống. Chọn món ở cột giữa để tự động mở bàn &amp; thêm món.</div>`
              : state.billItems.length === 0
                ? `<div class="text-xs text-slate-400 py-12 text-center">Hóa đơn chưa có món. Chạm vào món ở giữa để thêm.</div>`
                : state.billItems.map(it => `
                    <div class="flex items-center justify-between gap-2 p-2.5 rounded-xl border border-slate-200 bg-slate-50/70">
                      <div class="min-w-0">
                        <div class="text-xs font-bold text-slate-900 truncate">${esc(it.tenmon)}</div>
                        <div class="text-xs text-slate-500 tabular-nums">${fmtMoney(it.dongia)} × ${it.soluong} = <strong class="text-slate-800">${fmtMoney(it.thanhtien)}</strong></div>
                      </div>
                      <div class="flex items-center gap-1 shrink-0">
                        <button data-dec="${it.mamon}" data-qty="${it.soluong}" class="pos-dec w-7 h-7 rounded-lg border border-slate-300 bg-white hover:bg-slate-100 text-xs font-bold cursor-pointer">-</button>
                        <span class="w-6 text-center text-xs font-bold tabular-nums">${it.soluong}</span>
                        <button data-inc="${it.mamon}" class="pos-inc w-7 h-7 rounded-lg border border-slate-300 bg-white hover:bg-slate-100 text-xs font-bold cursor-pointer">+</button>
                      </div>
                    </div>`).join("")}
          </div>
        </div>

        <div class="pt-3 mt-3 border-t border-slate-200 space-y-2.5">
          <div class="flex items-center justify-between">
            <span class="text-xs font-bold uppercase text-slate-500">Tổng tiền</span>
            <span class="text-lg font-bold text-teal-800 tabular-nums">${fmtMoney(state.selectedBill?.tongtien || 0)}</span>
          </div>
          <div class="grid grid-cols-2 gap-2">
            <button id="pos-pdf" ${!state.selectedBill ? "disabled" : ""}
              class="py-2.5 px-3 rounded-xl border border-slate-300 text-xs font-semibold hover:bg-slate-50 disabled:opacity-40 cursor-pointer">
              📄 Xuất PDF
            </button>
            <button id="pos-pay" ${!state.selectedBill || state.billItems.length === 0 ? "disabled" : ""}
              class="py-2.5 px-3 rounded-xl bg-teal-700 hover:bg-teal-800 text-white text-xs font-semibold disabled:opacity-40 cursor-pointer">
              ✓ Thanh toán
            </button>
          </div>
        </div>
      </div>
    </div>
  `;

  bindPosEvents(container);
}

function bindPosEvents(container) {
  document.getElementById("pos-refresh").onclick = async () => {
    await loadPosData();
    drawPos(container);
  };

  const searchInput = document.getElementById("pos-search");
  searchInput.oninput = (e) => {
    state.search = e.target.value;
    drawPos(container);
    const el = document.getElementById("pos-search");
    el.focus();
    el.setSelectionRange(el.value.length, el.value.length);
  };

  container.querySelectorAll(".pos-table-btn").forEach(btn => {
    btn.onclick = async () => {
      state.selectedTableId = Number(btn.dataset.table);
      await loadPosData();
      drawPos(container);
    };
  });

  const openBtn = document.getElementById("pos-open-table");
  if (openBtn) {
    openBtn.onclick = async () => {
      try {
        await api("/api/hoadon/mo-ban", {
          method: "POST",
          body: JSON.stringify({ maBan: state.selectedTableId })
        });
        showToast("Đã mở bàn thành công.", "success");
        await loadPosData();
        drawPos(container);
        window.dispatchEvent(new CustomEvent("pos-updated"));
      } catch (err) {
        showToast(err.message, "error");
      }
    };
  }

  container.querySelectorAll(".pos-menu-btn").forEach(btn => {
    btn.onclick = async () => {
      const maMon = Number(btn.dataset.menu);
      try {
        if (!state.selectedBill) {
          const res = await api("/api/hoadon/mo-ban", {
            method: "POST",
            body: JSON.stringify({ maBan: state.selectedTableId })
          });
          state.selectedBill = { mahd: res.mahd };
        }
        await api(`/api/hoadon/${state.selectedBill.mahd}/chi-tiet`, {
          method: "POST",
          body: JSON.stringify({ maMon, soLuong: 1, thaoTac: "Them" })
        });
        await loadPosData();
        drawPos(container);
        window.dispatchEvent(new CustomEvent("pos-updated"));
      } catch (err) {
        showToast(err.message, "error");
      }
    };
  });

  container.querySelectorAll(".pos-inc").forEach(btn => {
    btn.onclick = async () => {
      const maMon = Number(btn.dataset.inc);
      try {
        await api(`/api/hoadon/${state.selectedBill.mahd}/chi-tiet`, {
          method: "POST",
          body: JSON.stringify({ maMon, soLuong: 1, thaoTac: "Them" })
        });
        await loadPosData();
        drawPos(container);
      } catch (err) {
        showToast(err.message, "error");
      }
    };
  });

  container.querySelectorAll(".pos-dec").forEach(btn => {
    btn.onclick = async () => {
      const maMon = Number(btn.dataset.dec);
      const qty = Number(btn.dataset.qty);
      try {
        if (qty <= 1) {
          await api(`/api/hoadon/${state.selectedBill.mahd}/chi-tiet`, {
            method: "POST",
            body: JSON.stringify({ maMon, soLuong: 0, thaoTac: "Xoa" })
          });
        } else {
          await api(`/api/hoadon/${state.selectedBill.mahd}/chi-tiet`, {
            method: "POST",
            body: JSON.stringify({ maMon, soLuong: qty - 1, thaoTac: "Sua" })
          });
        }
        await loadPosData();
        drawPos(container);
      } catch (err) {
        showToast(err.message, "error");
      }
    };
  });

  const changeBtn = document.getElementById("pos-change-table");
  if (changeBtn) {
    changeBtn.onclick = () => {
      const emptyTables = state.tables.filter(t => t.trangthai === "Trống");
      if (emptyTables.length === 0) {
        showToast("Không còn bàn trống để chuyển.", "warning");
        return;
      }
      openModal(`
        <div class="p-5 space-y-4">
          <h3 class="text-base font-bold text-slate-900">Đổi bàn cho HĐ #${state.selectedBill.mahd}</h3>
          <select id="mdl-new-table" class="w-full px-3 py-2 text-xs rounded-lg border border-slate-300">
            ${emptyTables.map(t => `<option value="${t.maban}">${esc(t.tenban)} (${esc(t.socho)})</option>`).join("")}
          </select>
          <div class="flex justify-end gap-2">
            <button id="mdl-c" class="px-3 py-2 rounded-lg border border-slate-300 text-xs font-semibold">Hủy</button>
            <button id="mdl-s" class="px-4 py-2 rounded-lg bg-teal-700 text-white text-xs font-semibold">Chuyển bàn</button>
          </div>
        </div>
      `);
      document.getElementById("mdl-c").onclick = closeModal;
      document.getElementById("mdl-s").onclick = async () => {
        const maBanMoi = Number(document.getElementById("mdl-new-table").value);
        try {
          await api(`/api/hoadon/${state.selectedBill.mahd}/ban`, {
            method: "PUT",
            body: JSON.stringify({ maBan: maBanMoi })
          });
          closeModal();
          state.selectedTableId = maBanMoi;
          showToast("Đã chuyển bàn thành công.", "success");
          await loadPosData();
          drawPos(container);
          window.dispatchEvent(new CustomEvent("pos-updated"));
        } catch (err) {
          showToast(err.message, "error");
        }
      };
    };
  }

  const cancelBtn = document.getElementById("pos-cancel-bill");
  if (cancelBtn) {
    cancelBtn.onclick = () => {
      confirmDialog(
        "Hủy hóa đơn chưa thanh toán?",
        `Xóa HĐ #${state.selectedBill.mahd} và hoàn lại toàn bộ số lượng tồn kho?`,
        async () => {
          try {
            await api(`/api/hoadon/${state.selectedBill.mahd}`, { method: "DELETE" });
            showToast("Đã hủy hóa đơn & hoàn tồn kho.", "success");
            await loadPosData();
            drawPos(container);
            window.dispatchEvent(new CustomEvent("pos-updated"));
          } catch (err) {
            showToast(err.message, "error");
          }
        },
        "Xóa hóa đơn",
        true
      );
    };
  }

  const pdfBtn = document.getElementById("pos-pdf");
  if (pdfBtn && state.selectedBill) {
    pdfBtn.onclick = async () => {
      try {
        await downloadFile(`/api/hoadon/${state.selectedBill.mahd}/pdf`, `HoaDon_${state.selectedBill.mahd}.pdf`);
      } catch (err) {
        showToast(err.message, "error");
      }
    };
  }

  const payBtn = document.getElementById("pos-pay");
  if (payBtn && state.selectedBill) {
    payBtn.onclick = () => {
      confirmDialog(
        "Xác nhận thanh toán",
        `Thanh toán toàn bộ HĐ #${state.selectedBill.mahd} (${fmtMoney(state.selectedBill.tongtien)})? Sau khi thanh toán không thể sửa hoặc xóa.`,
        async () => {
          try {
            await api(`/api/hoadon/${state.selectedBill.mahd}/thanh-toan`, { method: "POST" });
            showToast("Đã thanh toán! Bàn đã trở về trạng thái trống.", "success");
            await loadPosData();
            drawPos(container);
            window.dispatchEvent(new CustomEvent("pos-updated"));
          } catch (err) {
            showToast(err.message, "error");
          }
        },
        "Thanh toán ngay",
        false
      );
    };
  }
}
