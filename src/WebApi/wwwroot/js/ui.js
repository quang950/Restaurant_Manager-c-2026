export function esc(str) {
  if (str === null || str === undefined) return "";
  return String(str)
    .replace(/&/g, "&amp;")
    .replace(/</g, "&lt;")
    .replace(/>/g, "&gt;")
    .replace(/"/g, "&quot;")
    .replace(/'/g, "&#39;");
}

export function fmtMoney(val) {
  return Number(val || 0).toLocaleString("vi-VN") + " đ";
}

export function fmtDate(val) {
  if (!val) return "—";
  const d = new Date(val);
  if (isNaN(d.getTime())) return String(val);
  return d.toLocaleString("vi-VN", {
    day: "2-digit", month: "2-digit", year: "numeric",
    hour: "2-digit", minute: "2-digit"
  });
}

export function showToast(message, type = "info") {
  const root = document.getElementById("toast-root");
  const colors = {
    info: "bg-slate-900 text-white",
    success: "bg-emerald-700 text-white",
    error: "bg-rose-600 text-white",
    warning: "bg-amber-600 text-white"
  };
  const el = document.createElement("div");
  el.className = `px-4 py-2.5 rounded-xl shadow-lg text-xs font-semibold transition-opacity ${colors[type] || colors.info}`;
  el.textContent = message;
  root.appendChild(el);
  setTimeout(() => {
    el.style.opacity = "0";
    setTimeout(() => el.remove(), 250);
  }, 2800);
}

export function openModal(html, maxWidth = "max-w-md") {
  const root = document.getElementById("modal-root");
  root.innerHTML = `
    <div class="bg-white border border-slate-200 rounded-2xl shadow-xl w-full ${maxWidth} overflow-hidden">
      ${html}
    </div>`;
  root.classList.remove("hidden");
  root.classList.add("flex");
}

export function closeModal() {
  const root = document.getElementById("modal-root");
  root.classList.add("hidden");
  root.classList.remove("flex");
  root.innerHTML = "";
}

export function confirmDialog(title, message, onConfirm, confirmText = "Xác nhận", danger = false) {
  openModal(`
    <div class="p-5 space-y-4">
      <div>
        <h3 class="text-base font-bold text-slate-900">${esc(title)}</h3>
        <p class="text-xs text-slate-600 mt-1 leading-relaxed">${esc(message)}</p>
      </div>
      <div class="flex justify-end gap-2 pt-2">
        <button id="mdl-cancel" class="px-3.5 py-2 rounded-lg border border-slate-300 text-xs font-semibold text-slate-700 hover:bg-slate-50">Hủy</button>
        <button id="mdl-ok" class="px-3.5 py-2 rounded-lg text-xs font-semibold text-white ${danger ? "bg-rose-600 hover:bg-rose-700" : "bg-teal-700 hover:bg-teal-800"}">${esc(confirmText)}</button>
      </div>
    </div>
  `);
  document.getElementById("mdl-cancel").onclick = closeModal;
  document.getElementById("mdl-ok").onclick = async () => {
    closeModal();
    await onConfirm();
  };
}
