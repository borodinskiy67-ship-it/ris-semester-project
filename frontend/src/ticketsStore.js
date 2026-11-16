import { ref } from 'vue'

// Начальные тестовые данные (добавил поле description для полноты)
export const items = ref([
  { id: 1, number: 'REQ-2026-001', title: 'Не включается монитор', equipmentId: 3, description: 'Нет изображения', status: 'Новая' },
  { id: 2, number: 'REQ-2026-002', title: 'Сломан проектор', equipmentId: 5, description: 'Не работает лампа', status: 'В работе' }
])

// Счетчик для новых ID
let nextId = 3

// Функция добавления новой заявки
export function addItem(title, equipmentId, description) {
  const newItem = {
    id: nextId,
    number: `TMP-${nextId}`, // Временный номер (по методичке)
    title: title,
    equipmentId: equipmentId,
    description: description,
    status: 'Новая' // Всегда 'Новая' (New)
  }
  
  items.value.push(newItem)
  nextId++
}