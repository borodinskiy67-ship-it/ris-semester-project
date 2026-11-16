import { mockGetTickets, mockCreateTicket } from './mockTickets.js'

// Переключатель: true - используем mock, false - реальный сервер (fetch)
const USE_MOCK = true 
const baseUrl = import.meta.env.VITE_API_BASE_URL

export const getTickets = async () => {
  if (USE_MOCK) {
    const response = await mockGetTickets()
    if (!response.ok) throw new Error(`HTTP ${response.status}`)
    return response.data
  }

  // Реальный fetch (для будущих лаб)
  const response = await fetch(`${baseUrl}/api/tickets`)
  if (!response.ok) throw new Error(`HTTP ${response.status}`)
  return await response.json()
}

export const createTicket = async (body) => {
  if (USE_MOCK) {
    const response = await mockCreateTicket(body)
    if (!response.ok) {
      const error = new Error(`HTTP ${response.status}`)
      error.status = response.status
      throw error
    }
    return response.data
  }

  const response = await fetch(`${baseUrl}/api/tickets`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(body)
  })
  
  if (!response.ok) {
    const error = new Error(`HTTP ${response.status}`)
    error.status = response.status
    throw error
  }
  return await response.json()
}