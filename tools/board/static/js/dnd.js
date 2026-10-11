// Arrastrar y soltar: cambiar de columna y reordenar la prioridad.
let draggedId = null;
const placeholder = Object.assign(document.createElement('div'), { className: 'placeholder' });

export function makeDraggable(card) {
  card.draggable = true;
  card.addEventListener('dragstart', (e) => {
    draggedId = card.dataset.id;
    e.dataTransfer.effectAllowed = 'move';
    e.dataTransfer.setData('text/plain', draggedId);
    requestAnimationFrame(() => card.classList.add('dragging'));
  });
  card.addEventListener('dragend', () => {
    card.classList.remove('dragging');
    placeholder.remove();
    draggedId = null;
  });
}

// Tarjeta delante de la cual caería el puntero (null = al final).
function cardAfter(list, y) {
  const cards = [...list.querySelectorAll('.card:not(.dragging)')];
  return cards.find((c) => {
    const box = c.getBoundingClientRect();
    return y < box.top + box.height / 2;
  }) || null;
}

export function attachDrop(column, list, onMove) {
  column.addEventListener('dragover', (e) => {
    if (!draggedId) return;
    e.preventDefault();
    column.classList.add('over');
    const after = cardAfter(list, e.clientY);
    if (after) list.insertBefore(placeholder, after);
    else list.append(placeholder);
  });
  column.addEventListener('dragleave', (e) => {
    if (!column.contains(e.relatedTarget)) {
      column.classList.remove('over');
      placeholder.remove();
    }
  });
  column.addEventListener('drop', (e) => {
    e.preventDefault();
    column.classList.remove('over');
    const id = draggedId;
    if (!id) return;
    let next = placeholder.nextElementSibling;
    while (next && next.dataset.id === id) next = next.nextElementSibling;
    placeholder.remove();
    onMove(id, list.dataset.status, next ? next.dataset.id : null);
  });
}
