// Alta rápida, filtros y barra de sincronización con GitHub.
import { api, el, toast } from './api.js';

const $ = (id) => document.getElementById(id);

export function setupToolbar(state, { reload, rerender }) {
  $('quick').addEventListener('submit', async (e) => {
    e.preventDefault();
    const title = $('q-title').value.trim();
    if (!title) return;
    try {
      const task = await api.create({
        title, phase: $('q-phase').value, type: $('q-type').value,
        status: $('q-status').value, position: $('q-pos').value,
      });
      $('q-title').value = '';
      toast(`${task.id} añadida`);
      reload();
    } catch (err) { toast(err.message, true); }
  });

  const bind = (id, key, prop = 'value') => $(id).addEventListener('input', () => {
    state.filters[key] = $(id)[prop];
    rerender();
  });
  bind('f-text', 'text');
  bind('f-phase', 'phase');
  bind('f-label', 'label');
  bind('f-done', 'showDone', 'checked');
}

// Rellena los desplegables una sola vez, cuando llega el primer estado.
export function fillSelects(state) {
  const { phases, catalog, summary } = state.board;
  const phaseOpts = (sel) => phases.map((p) =>
    el('option', { value: p.id, selected: p.id === sel }, `${p.id} · ${p.name}`));
  $('q-phase').replaceChildren(...phaseOpts(summary.current_phase));
  $('f-phase').append(...phaseOpts(null));
  $('q-type').replaceChildren(...Object.keys(catalog.tipos).map((t) => el('option', { value: t }, t)));
  const names = [...Object.keys(catalog.tipos), ...Object.keys(catalog.etiquetas)];
  $('f-label').append(...[...new Set(names)].map((n) => el('option', { value: n }, n)));
}

export async function renderGit(reload) {
  const box = $('git');
  let st;
  try { st = await api.gitStatus(); } catch (e) { box.replaceChildren(el('span', { class: 'warn' }, e.message)); return; }
  if (st.disabled) { box.replaceChildren(el('span', {}, 'Sincronización desactivada')); return; }

  const run = (fn, btn) => async () => {
    btn.disabled = true;
    try { toast((await fn()).message); await reload(); } catch (e) { toast(e.message, true); }
    btn.disabled = false;
  };
  const pull = el('button', { title: 'git pull --rebase' }, '⟳ Traer');
  const push = el('button', { class: 'primary', title: 'Commit de docs/backlog + push' }, '⇪ Guardar y subir');
  pull.onclick = run(() => api.pull(), pull);
  push.onclick = run(() => api.publish(''), push);
  box.replaceChildren(
    el('span', { class: st.main ? '' : 'warn', title: 'Rama actual' }, `⎇ ${st.branch}`),
    el('span', {}, st.changes ? `${st.changes} cambios sin subir` : 'Al día'),
    pull, push);
}
