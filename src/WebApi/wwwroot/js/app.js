import { api, setUnauthorizedHandler } from "./api.js";
import { esc, fmtMoney, showToast } from "./ui.js";
import { renderLoginPage, openChangePasswordModal } from "./pages/login.js";
import { renderPosPage } from "./pages/pos.js";
import { renderInvoicesPage } from "./pages/invoices.js";
import { renderOverviewPage } from "./pages/overview.js";
import { renderCatalogPage } from "./pages/catalog.js";
import { renderImportPage } from "./pages/import.js";
import { renderReportsPage } from "./pages/reports.js";
import { renderXlsPage } from "./pages/xls.js";

let currentUser = null;
let sidebarCollapsed = false;

const NAV_GROUPS = [
  {
    label: "Vận hành",
    mgrOnly: false,
    items: [
      { id: "pos", icon: "🍽️", text: "POS Gọi món & Bàn", title: "POS Gọi Món & Sơ Đồ Bàn" },
      { id: "invoices", icon: "🧾", text: "Lịch sử Hóa đơn", title: "Danh Sách Hóa Đơn" }
    ]
  },
  {
    label: "Quản lý",
    mgrOnly: true,
    items: [
      { id: "overview", icon: "📊", text: "Tổng quan", title: "Tổng Quan Hoạt Động" },
      { id: "monnuoc", icon: "🍜", text: "Món ăn & Tồn kho", title: "Quản Lý Món / Nước" },
      { id: "ban", icon: "🪑", text: "Quản lý Bàn", title: "Quản Lý Bàn Ăn" },
      { id: "import", icon: "📦", text: "Nhập hàng", title: "Phiếu Nhập Hàng" },
      { id: "nhacungcap", icon: "🚚", text: "Nhà cung cấp", title: "Quản Lý Nhà Cung Cấp" },
      { id: "nhanvien", icon: "👥", text: "Nhân viên", title: "Quản Lý Nhân Viên" }
    ]
  },
  {
    label: "Báo cáo & Tệp",
    mgrOnly: true,
    items: [
      { id: "reports", icon: "📈", text: "Thống kê & PDF", title: "Thống Kê & Báo Cáo" },
      { id: "xls", icon: "📑", text: "Nhập / Xuất Excel", title: "Nhập / Xuất File .xls" }
    ]
  }
];

function getRoute() {
  const hash = location.hash.replace(/^#\/?/, "");
  return hash || "pos";
}

function isManager() {
  return currentUser?.vaiTro === "Quản lý";
}

async function refreshHeaderStats() {
  try {
    const res = await api("/api/thongke/tong-quan");
    const q = res.tongQuan;
    const e1 = document.getElementById("hdr-empty");
    const e2 = document.getElementById("hdr-busy");
    const e3 = document.getElementById("hdr-rev");
    if (e1) e1.textContent = q.bantrong;
    if (e2) e2.textContent = q.dangphucvu;
    if (e3) e3.textContent = fmtMoney(q.doanhthu);
  } catch { /* ignore */ }
}

function renderShell() {
  const route = getRoute();
  const root = document.getElementById("app");

  root.innerHTML = `
    <div class="min-h-screen flex flex-col md:flex-row">
      <aside id="sidebar" class="${sidebarCollapsed ? "md:w-16" : "md:w-60"} w-full shrink-0 bg-white border-b md:border-b-0 md:border-r border-slate-200 flex flex-col justify-between transition-all">
        <div class="p-3 space-y-3">
          <div class="flex items-center justify-between px-2 py-1.5">
            <div class="${sidebarCollapsed ? "hidden" : ""}">
              <div class="text-sm font-bold text-slate-900">🍽️ Nhà Hàng Pro</div>
              <div class="text-xs text-slate-400">Web Portal</div>
            </div>
            <button id="btn-collapse" title="Thu gọn menu" class="hidden md:inline-flex items-center justify-center w-7 h-7 rounded-lg border border-slate-200 hover:bg-slate-100 text-xs cursor-pointer">⇄</button>
          </div>
          <nav class="space-y-1 text-xs">
            ${NAV_GROUPS.filter(g => isManager() || !g.mgrOnly).map(g => `
              <div class="space-y-1">
                ${sidebarCollapsed ? `<div class="border-t border-slate-200 my-1.5"></div>` : `<div class="px-2.5 pt-2.5 pb-1 text-slate-400 font-semibold uppercase tracking-wider">${g.label}</div>`}
                ${g.items.map(it => {
                  const active = route === it.id;
                  return `
                    <a href="#/${it.id}" title="${it.text}" class="flex items-center gap-2.5 px-2.5 py-2 rounded-lg font-medium transition ${
                      active ? "bg-teal-700 text-white font-semibold" : "text-slate-700 hover:bg-slate-100"
                    } ${sidebarCollapsed ? "justify-center" : ""}">
                      <span>${it.icon}</span>
                      ${sidebarCollapsed ? "" : `<span class="truncate">${it.text}</span>`}
                    </a>`;
                }).join("")}
              </div>`).join("")}
          </nav>
        </div>

        <div class="p-3 border-t border-slate-200 flex items-center justify-between gap-2">
          <div class="min-w-0 ${sidebarCollapsed ? "hidden" : ""}">
            <div class="text-xs font-bold text-slate-900 truncate">${esc(currentUser.tenNhanVien)}</div>
            <div class="text-xs text-slate-500">${esc(currentUser.vaiTro)}</div>
          </div>
          <div class="flex items-center gap-1 shrink-0">
            <button id="btn-chpass" title="Đổi mật khẩu" class="px-2 py-1.5 rounded-lg border border-slate-200 hover:bg-slate-100 text-xs cursor-pointer">🔑</button>
            <button id="btn-logout" title="Đăng xuất" class="px-2 py-1.5 rounded-lg border border-rose-200 text-rose-600 hover:bg-rose-50 text-xs font-semibold cursor-pointer">Thoát</button>
          </div>
        </div>
      </aside>

      <div class="flex-1 flex flex-col min-w-0">
        <header class="px-4 py-3 bg-white border-b border-slate-200 flex flex-wrap items-center justify-between gap-3">
          <h2 id="shell-title" class="text-sm font-bold text-slate-900"></h2>
          <div class="flex flex-wrap items-center gap-2 text-xs">
            <span class="px-2.5 py-1 rounded-full border border-slate-200 bg-slate-50">🟢 Trống: <strong id="hdr-empty" class="tabular-nums">-</strong></span>
            <span class="px-2.5 py-1 rounded-full border border-slate-200 bg-slate-50">🟠 Đang phục vụ: <strong id="hdr-busy" class="tabular-nums">-</strong></span>
            ${isManager() ? `<span class="px-2.5 py-1 rounded-full border border-slate-200 bg-slate-50">💰 Thu hôm nay: <strong id="hdr-rev" class="tabular-nums">-</strong></span>` : ""}
          </div>
        </header>

        <main id="page-body" class="flex-1 p-3 md:p-4"></main>
      </div>
    </div>
  `;

  document.getElementById("btn-collapse").onclick = () => {
    sidebarCollapsed = !sidebarCollapsed;
    renderCurrentRoute();
  };
  document.getElementById("btn-chpass").onclick = openChangePasswordModal;
  document.getElementById("btn-logout").onclick = async () => {
    await api("/api/auth/logout", { method: "POST" });
    currentUser = null;
    renderLoginPage(onLoggedIn);
  };
}

async function renderCurrentRoute(extraCtx = {}) {
  if (!currentUser) return;
  let route = getRoute();
  const allItems = NAV_GROUPS.flatMap(g => g.items);
  const meta = allItems.find(x => x.id === route);
  const group = NAV_GROUPS.find(g => g.items.some(x => x.id === route));

  if (!meta || (group?.mgrOnly && !isManager())) {
    route = "pos";
    location.hash = "#/pos";
  }

  renderShell();
  document.getElementById("shell-title").textContent = allItems.find(x => x.id === route)?.title || "";
  refreshHeaderStats();

  const body = document.getElementById("page-body");
  try {
    if (route === "pos") {
      await renderPosPage(body, { ...extraCtx, onStatsChanged: refreshHeaderStats });
    } else if (route === "invoices") {
      await renderInvoicesPage(body);
    } else if (route === "overview") {
      await renderOverviewPage(body, (maBan) => {
        location.hash = "#/pos";
        setTimeout(() => renderCurrentRoute({ selectedTableId: maBan }), 0);
      });
    } else if (["monnuoc", "ban", "nhacungcap", "nhanvien"].includes(route)) {
      await renderCatalogPage(body, route);
    } else if (route === "import") {
      await renderImportPage(body);
    } else if (route === "reports") {
      await renderReportsPage(body);
    } else if (route === "xls") {
      await renderXlsPage(body);
    }
  } catch (err) {
    body.innerHTML = `<div class="p-4 rounded-xl bg-rose-50 border border-rose-200 text-xs text-rose-700">${esc(err.message)}</div>`;
    showToast(err.message, "error");
  }
}

function onLoggedIn(user) {
  currentUser = user;
  if (!location.hash) location.hash = "#/pos";
  renderCurrentRoute();
}

setUnauthorizedHandler(() => {
  currentUser = null;
  renderLoginPage(onLoggedIn);
});

window.addEventListener("hashchange", () => renderCurrentRoute());
window.addEventListener("pos-updated", () => refreshHeaderStats());

(async function init() {
  try {
    currentUser = await api("/api/auth/me");
    renderCurrentRoute();
  } catch {
    renderLoginPage(onLoggedIn);
  }
})();
