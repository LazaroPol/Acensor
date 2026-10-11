// Punto de entrada: carga el estado y conecta las piezas.
import { api, toast } from './api.js';
import { openEditor } from './editor.js';
import { renderBoard, renderProgress } from './render.js';
import { fillSelects, renderGit, setupToolbar } from './toolbar.js';

const state = {
  board: null,
  byId: new Map(),
  phaseById: new Map(),
  filters: { text: '', phase: '', label: '', showDone: false },
};

function rerender() {
  renderProgress(state);
  renderBoard(state, {
    onOpen: (task) => openEditor(state, task, reload),
    onMove: move,
  });
}

async function reload() {
  try {
    const first = !state.board;
    state.board = await api.board();
    state.byId = new Map(state.board.tasks.map((t) => [t.id, t]));
    state.phaseById = new Map(state.board.phases.map((p) => [p.id, p]));
    if (first) fillSelects(state);
    rerender();
    renderGit(reload);
  } catch (e) { toast(e.message, true); }
}

async function move(id, status, before) {
  const task = state.byId.get(id);
  if (!task || (task.status === status && before === id)) return;
  try {
    await api.move(id, status, before);
  } catch (e) { toast(e.message, true); }
  reload();
}

setupToolbar(state, { reload, rerender });
window.addEventListener('focus', () => { if (!document.getElementById('editor').open) reload(); });
reload();
