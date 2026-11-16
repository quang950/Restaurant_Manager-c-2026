import { api } from "../api.js";
import { esc, fmtMoney, showToast, openModal, closeModal, confirmDialog } from "../ui.js";

const CONFIGS = {
  monnuoc: {
    endpoint: "/api/monnuoc",
    idField: "mamon",
    title: "Món ăn / Nước uống",
    columns: [
      { key: "mamon", label: "Mã" },
      { key: "tenmon", label: "Tên món / nước" },
      { key: "loai", label: "Loại" },
      { key: "donvi", label: "Đơn vị" },
      { key: "gia", label: "Giá bán", money: true },
      { key: "soluongton", label: "Tồn kho", right: true }
    ],
    fields: [
      { name: "tenMon", source: "tenmon", label: "Tên món / nước", type: "text", required: true },
      { name: "loai", source: "loai", label: "Loại", type: "select", options: ["Món ăn", "Nước uống"] },
      { name: "donVi", source: "donvi", label: "Đơn vị tính", type: "text", required: true },
      { name: "gia", source: "gia", label: "Giá bán (đ)", type: "number", min: 1, required: true }
    ]
  },
  ban: {
    endpoint: "/api/ban",
    idField: "maban",
    title: "Bàn ăn",
    columns: [
      { key: "maban", label: "Mã" },
      { key: "tenban", label: "Tên bàn" },
      { key: "socho", label: "Số chỗ", right: true },
      { key: "trangthai", label: "Trạng thái" }
    ],
    fields: [
      { name: "tenBan", source: "tenban", label: "Tên bàn", type: "text", required: true },
      { name: "soCho", source: "socho", label: "Số chỗ ngồi", type: "number", min: 1, max: 100, required: true }
    ]
  },
  nhacungcap: {
    endpoint: "/api/nhacungcap",
    idField: "mancc",
    title: "Nhà cung cấp",
    columns: [
      { key: "mancc", label: "Mã" },
      { key: "tenncc", label: "Tên nhà cung cấp" },
      { key: "sdt", label: "Số điện thoại" },
      { key: "diachi", label: "Địa chỉ" }
    ],
    fields: [
      { name: "tenNcc", source: "tenncc", label: "Tên nhà cung cấp", type: "text", required: true },
      { name: "sdt", source: "sdt", label: "Số điện thoại (9-15 số)", type: "text", required: true },
      { name: "diaChi", source: "diachi", label: "Địa chỉ", type: "text", required: true }
    ]
  },
  nhanvien: {
    endpoint: "/api/nhanvien",
    idField: "manv",
    title: "Nhân viên",
    columns: [
      { key: "manv", label: "Mã" },
      { key: "hoten", label: "Họ và tên" },
      { key: "sdt", label: "Số điện thoại" },
      { key: "tendangnhap", label: "Tên đăng nhập" },
      { key: "vaitro", label: "Vai trò" }
    ],
    fields: [
      { name: "hoTen", source: "hoten", label: "Họ và tên", type: "text", required: true },
      { name: "sdt", source: "sdt", label: "Số điện thoại", type: "text", required: true },
      { name: "tenDangNhap", source: "tendangnhap", label: "Tên đăng nhập", type: "text", required: true },
      { name: "vaiTro", source: "vaitro", label: "Vai trò", type: "select", options: ["Nhân viên", "Quản lý"] },
      { name: "matKhau", source: "", label: "Mật khẩu (chỉ khi thêm mới)", type: "password", createOnly: true }
    ]
  }
};

let search = "";

export async function renderCatalogPage(container, type) {
  search = "";
  await drawCatalog(container, type);
}

async function drawCatalog(container, type) {
  const cfg = CONFIGS[type];
  const rows = await api(`${cfg.endpoint}?tim=${encodeURIComponent(search)}`);

  container.innerHTML = `
    <div class="bg-white border border-slate-200 rounded-xl p-4 space-y-3">
      <div class="flex flex-wrap items-center justify-between gap-2">
        <div class="flex items-center gap-2 flex-1 min-w-[220px]">
          <input id="cat-search" type="text" value="${esc(search)}" placeholder="Tìm kiếm ${cfg.title.toLowerCase()}..."
            class="w-full max-w-sm px-3 py-2 text-xs rounded-lg border border-slate-300 focus:outline-none focus:ring-2 focus:ring-teal-600" />
          <span class="text-xs text-slate-500 whitespace-nowrap">${rows.length} bản ghi</span>
        </div>
        <button id="cat-add" class="px-3.5 py-2 rounded-lg bg-teal-700 hover:bg-teal-800 text-white text-xs font-semibold cursor-pointer">+ Thêm ${cfg.title}</button>
      </div>

      <div class="overflow-x-auto max-h-[520px] overflow-y-auto">
        <table class="w-full text-xs text-left">
          <thead class="border-b border-slate-200 text-slate-500 bg-slate-50 sticky top-0">
            <tr>
              ${cfg.columns.map(c => `<th class="py-2.5 px-3 ${c.money || c.right ? "text-right" : ""}">${c.label}</th>`).join("")}
              <th class="py-2.5 px-3 text-right">Thao tác</th>
            </tr>
          </thead>
          <tbody class="divide-y divide-slate-100">
            ${rows.map(r => `
              <tr class="hover:bg-slate-50">
                ${cfg.columns.map(c => {
                  const val = r[c.key];
                  return `<td class="py-2.5 px-3 ${c.money || c.right ? "text-right tabular-nums" : ""} ${c.key === cfg.idField ? "font-bold text-slate-500" : ""}">
                    ${c.money ? fmtMoney(val) : esc(val)}
                  </td>`;
                }).join("")}
                <td class="py-2.5 px-3 text-right whitespace-nowrap space-x-1.5">
                  <button data-edit='${r[cfg.idField]}' class="cat-edit px-2.5 py-1 rounded border border-slate-300 hover:bg-slate-100 font-medium cursor-pointer">Sửa</button>
                  <button data-del='${r[cfg.idField]}' class="cat-del px-2.5 py-1 rounded border border-rose-200 text-rose-600 hover:bg-rose-50 font-medium cursor-pointer">Xóa</button>
                </td>
              </tr>`).join("")}
          </tbody>
        </table>
      </div>
    </div>
  `;

  const sEl = document.getElementById("cat-search");
  sEl.oninput = async (e) => {
    search = e.target.value;
    await drawCatalog(container, type);
    const el = document.getElementById("cat-search");
    el.focus();
    el.setSelectionRange(el.value.length, el.value.length);
  };

  document.getElementById("cat-add").onclick = () => openFormModal(container, type, null);

  container.querySelectorAll(".cat-edit").forEach(btn => {
    btn.onclick = () => {
      const id = Number(btn.dataset.edit);
      const row = rows.find(x => Number(x[cfg.idField]) === id);
      openFormModal(container, type, row);
    };
  });

  container.querySelectorAll(".cat-del").forEach(btn => {
    btn.onclick = () => {
      const id = Number(btn.dataset.del);
      confirmDialog("Xác nhận xóa", `Bạn có chắc muốn xóa bản ghi #${id}?`, async () => {
        try {
          await api(`${cfg.endpoint}/${id}`, { method: "DELETE" });
          showToast("Đã xóa thành công.", "success");
          await drawCatalog(container, type);
        } catch (err) {
          showToast(err.message, "error");
        }
      }, "Xóa", true);
    };
  });
}

function openFormModal(container, type, row) {
  const cfg = CONFIGS[type];
  const isEdit = !!row;

  openModal(`
    <div class="p-5 space-y-4">
      <div class="flex items-center justify-between border-b border-slate-200 pb-3">
        <h3 class="text-base font-bold text-slate-900">${isEdit ? "Sửa" : "Thêm"} ${cfg.title}</h3>
        <button id="cf-close" class="text-slate-400 hover:text-slate-600 text-lg leading-none cursor-pointer">&times;</button>
      </div>
      <form id="cf-form" class="space-y-3 text-xs">
        ${cfg.fields.filter(f => !(isEdit && f.createOnly)).map(f => {
          const val = isEdit ? (row[f.source] ?? "") : (f.type === "number" ? 1 : "");
          if (f.type === "select") {
            return `
              <div>
                <label class="block font-semibold text-slate-700 mb-1">${f.label}</label>
                <select name="${f.name}" class="w-full px-3 py-2 rounded-lg border border-slate-300 bg-white">
                  ${f.options.map(opt => `<option value="${esc(opt)}" ${val === opt ? "selected" : ""}>${esc(opt)}</option>`).join("")}
                </select>
              </div>`;
          }
          return `
            <div>
              <label class="block font-semibold text-slate-700 mb-1">${f.label}</label>
              <input name="${f.name}" type="${f.type}" value="${esc(val)}" ${f.min ? `min="${f.min}"` : ""} ${f.max ? `max="${f.max}"` : ""} ${f.required || (!isEdit && f.createOnly) ? "required" : ""}
                class="w-full px-3 py-2 rounded-lg border border-slate-300 focus:outline-none focus:ring-2 focus:ring-teal-600" />
            </div>`;
        }).join("")}
        <div id="cf-err" class="hidden text-rose-600 bg-rose-50 border border-rose-200 rounded-lg px-3 py-2"></div>
        <div class="flex justify-end gap-2 pt-2">
          <button type="button" id="cf-cancel" class="px-3.5 py-2 rounded-lg border border-slate-300 font-semibold text-slate-700">Hủy</button>
          <button type="submit" class="px-4 py-2 rounded-lg bg-teal-700 hover:bg-teal-800 text-white font-semibold">${isEdit ? "Lưu thay đổi" : "Thêm mới"}</button>
        </div>
      </form>
    </div>
  `);

  document.getElementById("cf-close").onclick = closeModal;
  document.getElementById("cf-cancel").onclick = closeModal;
  document.getElementById("cf-form").onsubmit = async (e) => {
    e.preventDefault();
    const errEl = document.getElementById("cf-err");
    errEl.classList.add("hidden");
    const fd = new FormData(e.target);
    const payload = {};
    for (const f of cfg.fields) {
      if (isEdit && f.createOnly) continue;
      const v = fd.get(f.name);
      payload[f.name] = f.type === "number" ? Number(v) : String(v || "");
    }
    try {
      if (isEdit) {
        await api(`${cfg.endpoint}/${row[cfg.idField]}`, { method: "PUT", body: JSON.stringify(payload) });
        showToast("Đã lưu thay đổi.", "success");
      } else {
        await api(cfg.endpoint, { method: "POST", body: JSON.stringify(payload) });
        showToast("Đã thêm thành công.", "success");
      }
      closeModal();
      await drawCatalog(container, type);
    } catch (err) {
      errEl.textContent = err.message;
      errEl.classList.remove("hidden");
    }
  };
}
