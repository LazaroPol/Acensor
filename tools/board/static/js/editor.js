// Ventana de edición de una tarjeta.
import { api, el, toast } from './api.js';
import { COLUMNS } from './columns.js';
import { chip } from './render.js';

const dialog = () => document.getElementById('editor');

function options(values, selected, labelOf = (v) => v) {
  return values.map((v) => el('option', { value: v, selected: v === selected }, labelOf(v)));
}

export function openEditor(state, task, reload) {
  const cat = state.board.catalog;
  const labels = new Set(task.labels);
  const phase = state.phaseById.get(task.phase);
  const title = el('input', { name: 'title', value: task.title });
  const type = el('select', {}, options(Object.keys(cat.tipos), task.type));
  const status = el('select', {}, COLUMNS.map((c) =>
    el('option', { value: c.status, selected: c.status === task.status }, c.title)));
  const description = el('textarea', { placeholder: 'Qué hay que hacer y por qué' }, task.description || '');
  const acceptance = el('textarea', { placeholder: 'Un criterio por línea (se convertirán en tests)' },
    (task.acceptance || []).join('\n'));
  const labelChips = Object.entries(cat.etiquetas).map(([name, color]) => {
    const c = chip(name, color, labels.has(name) ? 'on' : '');
    c.addEventListener('click', () => {
      labels.has(name) ? labels.delete(name) : labels.add(name);
      c.classList.toggle('on');
    });
    return c;
  });

  async function save() {
    const fields = {
      title: title.value.trim(), type: type.value, labels: [...labels],
      description: description.value.trim(),
      acceptance: acceptance.value.split('\n').map((s) => s.trim()).filter(Boolean),
    };
    try {
      await api.update(task.id, fields);
      if (status.value !== task.status) await api.move(task.id, status.value, null);
      dialog().close();
      toast(`${task.id} guardada`);
      reload();
    } catch (e) { toast(e.message, true); }
  }

  async function remove() {
    if (!window.confirm(`¿Borrar ${task.id}?`)) return;
    try {
      await api.remove(task.id);
      dialog().close();
      toast(`${task.id} borrada`);
      reload();
    } catch (e) { toast(e.message, true); }
  }

  const deletable = task.origin === 'tablero' && task.status !== 'done';
  const form = el('form', { class: 'editor', method: 'dialog' },
    el('div', { class: 'head' },
      el('span', {}, `${task.id} · ${phase ? `${phase.id} ${phase.name}` : task.phase}`),
      el('span', {}, task.origin === 'tablero' ? 'Añadida en el tablero' : 'Backlog original',
        task.pr ? ` · PR ${task.pr} · sesión ${task.session}` : '')),
    title,
    el('div', { class: 'row' },
      el('label', {}, 'Columna', status),
      el('label', {}, 'Tipo', type)),
    el('label', {}, 'Etiquetas', el('div', { class: 'labels' }, labelChips)),
    el('label', {}, 'Descripción', description),
    el('label', {}, 'Criterios de aceptación', acceptance),
    el('div', { class: 'actions' },
      deletable ? el('button', { type: 'button', class: 'danger', onclick: remove }, 'Borrar') : el('span'),
      el('div', {},
        el('button', { type: 'button', onclick: () => dialog().close() }, 'Cancelar'),
        el('button', { type: 'submit', class: 'primary' }, 'Guardar'))));
  form.addEventListener('submit', (e) => { e.preventDefault(); save(); });
  dialog().replaceChildren(form);
  dialog().showModal();
  title.focus();
}
