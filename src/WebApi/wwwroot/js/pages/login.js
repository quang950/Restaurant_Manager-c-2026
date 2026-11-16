import { api } from "../api.js";
import { esc, showToast, openModal, closeModal } from "../ui.js";

export function renderLoginPage(onSuccess) {
  const root = document.getElementById("app");
  root.innerHTML = `
    <div class="min-h-screen flex items-center justify-center bg-slate-100 p-4">
      <div class="w-full max-w-md bg-white border border-slate-200 rounded-2xl shadow-sm p-6 sm:p-8 space-y-6">
        <div class="text-center space-y-1">
          <div class="inline-flex items-center justify-center w-12 h-12 rounded-2xl bg-teal-50 text-teal-700 text-2xl mb-2">🍽️</div>
          <h1 class="text-xl font-bold text-slate-900">Quản Lý Nhà Hàng</h1>
          <p class="text-xs text-slate-500">Đăng nhập để vào hệ thống POS &amp; Quản trị</p>
        </div>

        <form id="login-form" class="space-y-4">
          <div class="space-y-1.5">
            <label class="block text-xs font-semibold text-slate-700">Tên đăng nhập</label>
            <input id="lg-user" type="text" required autocomplete="username" placeholder="Nhập tài khoản (vd: admin, nv01)"
              class="w-full px-3.5 py-2.5 text-sm rounded-xl border border-slate-300 focus:outline-none focus:ring-2 focus:ring-teal-600 focus:border-teal-600" />
          </div>
          <div class="space-y-1.5">
            <label class="block text-xs font-semibold text-slate-700">Mật khẩu</label>
            <input id="lg-pass" type="password" required autocomplete="current-password" placeholder="Nhập mật khẩu"
              class="w-full px-3.5 py-2.5 text-sm rounded-xl border border-slate-300 focus:outline-none focus:ring-2 focus:ring-teal-600 focus:border-teal-600" />
          </div>
          <div id="lg-err" class="hidden text-xs text-rose-600 bg-rose-50 border border-rose-200 rounded-lg px-3 py-2"></div>
          <button id="lg-btn" type="submit"
            class="w-full py-2.5 px-4 rounded-xl bg-teal-700 hover:bg-teal-800 text-white text-sm font-semibold transition cursor-pointer">
            Đăng nhập
          </button>
        </form>
      </div>
    </div>
  `;

  document.getElementById("lg-user").focus();
  document.getElementById("login-form").onsubmit = async (e) => {
    e.preventDefault();
    const errEl = document.getElementById("lg-err");
    const btn = document.getElementById("lg-btn");
    errEl.classList.add("hidden");
    btn.disabled = true;
    btn.textContent = "Đang đăng nhập...";
    try {
      const user = await api("/api/auth/login", {
        method: "POST",
        body: JSON.stringify({
          taiKhoan: document.getElementById("lg-user").value,
          matKhau: document.getElementById("lg-pass").value
        })
      });
      showToast(`Xin chào ${user.tenNhanVien}!`, "success");
      onSuccess(user);
    } catch (err) {
      errEl.textContent = esc(err.message);
      errEl.classList.remove("hidden");
    } finally {
      btn.disabled = false;
      btn.textContent = "Đăng nhập";
    }
  };
}

export function openChangePasswordModal() {
  openModal(`
    <div class="p-5 space-y-4">
      <div class="flex items-center justify-between border-b border-slate-200 pb-3">
        <h3 class="text-base font-bold text-slate-900">Đổi mật khẩu</h3>
        <button id="cp-close" class="text-slate-400 hover:text-slate-600 text-lg leading-none cursor-pointer">&times;</button>
      </div>
      <form id="cp-form" class="space-y-3 text-xs">
        <div>
          <label class="block font-semibold text-slate-700 mb-1">Mật khẩu hiện tại</label>
          <input id="cp-old" type="password" required class="w-full px-3 py-2 rounded-lg border border-slate-300 focus:outline-none focus:ring-2 focus:ring-teal-600" />
        </div>
        <div>
          <label class="block font-semibold text-slate-700 mb-1">Mật khẩu mới</label>
          <input id="cp-new" type="password" required class="w-full px-3 py-2 rounded-lg border border-slate-300 focus:outline-none focus:ring-2 focus:ring-teal-600" />
        </div>
        <div>
          <label class="block font-semibold text-slate-700 mb-1">Nhập lại mật khẩu mới</label>
          <input id="cp-confirm" type="password" required class="w-full px-3 py-2 rounded-lg border border-slate-300 focus:outline-none focus:ring-2 focus:ring-teal-600" />
        </div>
        <div id="cp-err" class="hidden text-rose-600 bg-rose-50 border border-rose-200 rounded-lg px-3 py-2"></div>
        <div class="flex justify-end gap-2 pt-2">
          <button type="button" id="cp-cancel" class="px-3.5 py-2 rounded-lg border border-slate-300 font-semibold text-slate-700">Hủy</button>
          <button type="submit" class="px-4 py-2 rounded-lg bg-teal-700 hover:bg-teal-800 text-white font-semibold">Lưu mật khẩu</button>
        </div>
      </form>
    </div>
  `);

  document.getElementById("cp-close").onclick = closeModal;
  document.getElementById("cp-cancel").onclick = closeModal;
  document.getElementById("cp-form").onsubmit = async (e) => {
    e.preventDefault();
    const errEl = document.getElementById("cp-err");
    errEl.classList.add("hidden");
    try {
      await api("/api/auth/change-password", {
        method: "POST",
        body: JSON.stringify({
          cu: document.getElementById("cp-old").value,
          moi: document.getElementById("cp-new").value,
          xacNhan: document.getElementById("cp-confirm").value
        })
      });
      closeModal();
      showToast("Đã đổi mật khẩu thành công.", "success");
    } catch (err) {
      errEl.textContent = err.message;
      errEl.classList.remove("hidden");
    }
  };
}
