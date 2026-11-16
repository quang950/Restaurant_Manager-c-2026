let onUnauthorized = () => {};

export function setUnauthorizedHandler(fn) {
  onUnauthorized = fn;
}

export async function api(url, options = {}) {
  const isFormData = options.body instanceof FormData;
  const headers = isFormData ? {} : { "Content-Type": "application/json" };
  const res = await fetch(url, {
    credentials: "same-origin",
    ...options,
    headers: { ...headers, ...(options.headers || {}) }
  });

  if (res.status === 401 && !url.includes("/api/auth/login")) {
    onUnauthorized();
    throw new Error("Phiên đăng nhập đã hết hạn. Vui lòng đăng nhập lại.");
  }

  const contentType = res.headers.get("content-type") || "";
  if (contentType.includes("application/json")) {
    const data = await res.json();
    if (!res.ok) throw new Error(data.message || "Có lỗi xảy ra.");
    return data;
  }

  if (!res.ok) {
    const text = await res.text();
    throw new Error(text || `Lỗi HTTP ${res.status}`);
  }
  return res;
}

export async function downloadFile(url, defaultFilename) {
  const res = await fetch(url, { credentials: "same-origin" });
  if (res.status === 401) {
    onUnauthorized();
    throw new Error("Bạn cần đăng nhập trước.");
  }
  if (!res.ok) {
    const ct = res.headers.get("content-type") || "";
    if (ct.includes("application/json")) {
      const err = await res.json();
      throw new Error(err.message || "Không tải được file.");
    }
    throw new Error("Không tải được file.");
  }
  const blob = await res.blob();
  const cd = res.headers.get("content-disposition") || "";
  const match = cd.match(/filename\*?=(?:UTF-8'')?["']?([^"';\n]+)/i);
  const filename = match ? decodeURIComponent(match[1]) : defaultFilename;

  const a = document.createElement("a");
  a.href = URL.createObjectURL(blob);
  a.download = filename;
  document.body.appendChild(a);
  a.click();
  a.remove();
  URL.revokeObjectURL(a.href);
}
