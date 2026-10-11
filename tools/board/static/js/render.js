// Pinta la cabecera de progreso y las columnas con sus tarjetas.
import { el } from './api.js';
import { COLUMNS, readyRank, tasksFor } from './columns.js';
import { attachDrop, makeDraggable } from './dnd.js';

export function chip(name, color, extra = '') {
  return el('span', { class: `chip ${extra}`, style: color ? `--c:${color}` : '' }, name);
}

export function renderProgress(state) {
  const s = state.board.summary;
  const next = s.next_task ? state.byId.get(s.next_task) : null;
  document.getElementById('progress').replaceChildren(
    el('div', { class: 'bar' }, el('i', { style: `width:${s.pct}%` })),
    el('span', {}, el('b', {}, `${s.pct}%`), ` (${s.done}/${s.total})`),
    el('span', {}, 'Siguiente: ', el('b', {}, next ? `${next.id} ${next.title}` : '—')),
  );
}

function card(state, task, rank, onOpen) {
  const cat = state.board.catalog;
  const typeColor = cat.tipos[task.type];
  const phase = state.phaseById.get(task.phase);
  const node = el('article', { class: `card${task.origin === 'tablero' ? ' new' : ''}`,
    style: typeColor ? `--type:${typeColor}` : '', 'data-id': task.id, title: phase?.name || '' },
  el('div', { class: 'top' },
    el('span', { class: 'id' }, task.id),
    rank ? el('span', { class: 'rank', title: 'Posición en la cola' }, `#${rank}`) : null),
  el('div', { class: 'title' }, task.title),
  el('div', { class: 'meta' },
    chip(task.type, typeColor),
    task.labels.map((l) => chip(l, cat.etiquetas[l])),
    chip(task.phase, null, 'phase')),
  task.pr ? el('div', { class: 'foot' }, `PR ${task.pr} · S${String(task.session).padStart(2, '0')}`) : null);
  node.addEventListener('click', () => onOpen(task));
  makeDraggable(node);
  return node;
}

export function renderBoard(state, { onOpen, onMove }) {
  const ranks = readyRank(state);
  const cols = COLUMNS.map((col) => {
    const tasks = tasksFor(state, col.status);
    const hidden = col.status === 'done' && !state.filters.showDone;
    const list = el('div', { class: 'cards', 'data-status': col.status },
      hidden ? [] : tasks.map((t) => card(state, t, ranks.get(t.id), onOpen)));
    const column = el('section', { class: `column${hidden ? ' collapsed' : ''}` },
      el('header', {},
        el('h2', {}, col.title, el('span', { class: 'count' }, tasks.length)),
        el('div', { class: 'hint' }, hidden ? 'Marca «Ver hechas» para mostrarlas' : col.hint)),
      list);
    attachDrop(column, list, onMove);
    return column;
  });
  document.getElementById('board').replaceChildren(...cols);
}
