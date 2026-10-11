// Cliente de la API local del tablero.
async function call(method, url, body) {
  const res = await fetch(url, {
    method,
    headers: body ? { 'Content-Type': 'application/json' } : {},
    body: body ? JSON.stringify(body) : undefined,
  });
  const data = await res.json().catch(() => ({}));
  if (!res.ok) throw new Error(data.error || `Error ${res.status}`);
  return data;
}

export const api = {
  board: () => call('GET', '/api/board'),
  create: (task) => call('POST', '/api/tasks', task),
  update: (id, fields) => call('PUT', `/api/tasks/${id}`, fields),
  move: (id, status, before) => call('POST', `/api/tasks/${id}/move`, { status, before }),
  remove: (id) => call('DELETE', `/api/tasks/${id}`),
  gitStatus: () => call('GET', '/api/git'),
  pull: () => call('POST', '/api/git/pull'),
  publish: (message) => call('POST', '/api/git/publish', { message }),
};

let toastTimer;
export function toast(message, error = false) {
  const el = document.getElementById('toast');
  el.textContent = message;
  el.className = `toast show${error ? ' error' : ''}`;
  clearTimeout(toastTimer);
  toastTimer = setTimeout(() => { el.className = 'toast'; }, error ? 6000 : 2500);
}

export function el(tag, attrs = {}, ...children) {
  const node = document.createElement(tag);
  for (const [k, v] of Object.entries(attrs)) {
    if (k === 'class') node.className = v;
    else if (k === 'style') node.style.cssText = v;
    else if (k.startsWith('on')) node.addEventListener(k.slice(2), v);
    else if (v !== false && v != null) node.setAttribute(k, v === true ? '' : v);
  }
  for (const c of children.flat()) {
    if (c != null && c !== false) node.append(c instanceof Node ? c : document.createTextNode(c));
  }
  return node;
}
