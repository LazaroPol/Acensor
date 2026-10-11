// Columnas del tablero y cómo se ordenan sus tarjetas.
export const COLUMNS = [
  { status: 'idea', title: '💡 Ideas', hint: 'Bandeja de entrada: no se trabaja hasta pasarla a Listo' },
  { status: 'todo', title: 'Listo', hint: 'El orden es la prioridad: arriba = siguiente sesión' },
  { status: 'in_progress', title: 'En curso', hint: 'Máximo una tarea' },
  { status: 'review', title: 'Revisión', hint: 'Probar en Unity / validar' },
  { status: 'blocked', title: 'Bloqueado', hint: 'Necesita algo antes de seguir' },
  { status: 'done', title: 'Hecho', hint: 'Cerradas por las sesiones' },
];

export const STATUS_LABEL = Object.fromEntries(COLUMNS.map((c) => [c.status, c.title.replace('💡 ', '')]));

export function tasksFor(state, status) {
  const rank = new Map(state.board.priority.map((id, i) => [id, i]));
  const list = state.board.tasks.filter((t) => t.status === status && matches(state.filters, t));
  if (status === 'done') return list.sort((a, b) => (b.session || 0) - (a.session || 0));
  return list.sort((a, b) => (rank.get(a.id) ?? 1e9) - (rank.get(b.id) ?? 1e9));
}

function matches(f, t) {
  if (f.phase && t.phase !== f.phase) return false;
  if (f.label && !t.labels.includes(f.label) && t.type !== f.label) return false;
  if (!f.text) return true;
  const hay = `${t.id} ${t.title} ${t.description} ${t.labels.join(' ')}`.toLowerCase();
  return hay.includes(f.text.toLowerCase());
}

// Posición en la cola entre las tareas «Listo» (1 = la siguiente sesión la coge).
export function readyRank(state) {
  const ready = state.board.priority.filter((id) => state.byId.get(id)?.status === 'todo');
  return new Map(ready.map((id, i) => [id, i + 1]));
}
