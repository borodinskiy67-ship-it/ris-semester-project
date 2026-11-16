// Учебная база данных в памяти
let mockData = [
  { id: 1, number: 'REQ-2026-001', title: 'Не включается монитор', equipmentId: 3, description: 'Нет картинки', status: 'Новая' },
  { id: 2, number: 'REQ-2026-002', title: 'Сломан проектор', equipmentId: 5, description: 'Не горит лампа', status: 'В работе' }
]
let nextId = 3

// Флаг для имитации ошибки 500 (можешь включить его вручную для проверки)
const FORCE_500 = false 

export const mockGetTickets = async () => {
  // Имитация задержки сети (500 мс)
  await new Promise(resolve => setTimeout(resolve, 500))
  
  if (FORCE_500) {
    return { ok: false, status: 500, data: null }
  }

  return { ok: true, status: 200, data: [...mockData] }
}

export const mockCreateTicket = async (body) => {
  await new Promise(resolve => setTimeout(resolve, 500))

  // Имитация ошибки 400 (Bad Request)
  if (!body.title || body.title.trim().length < 5) {
    return { ok: false, status: 400, data: null }
  }

  // Имитация ошибки 500 (Internal Server Error)
  if (FORCE_500) {
    return { ok: false, status: 500, data: null }
  }

  // Успешное создание
  const newTicket = {
    id: nextId,
    number: `REQ-2026-00${nextId}`, // Номер приходит "с сервера"
    title: body.title,
    equipmentId: body.equipmentId,
    description: body.description,
    status: 'Новая'
  }
  
  mockData.push(newTicket)
  nextId++

  return { ok: true, status: 201, data: newTicket }
}