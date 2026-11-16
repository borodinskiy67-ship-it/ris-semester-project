<script setup>
import { ref, onMounted } from 'vue'
import { getTickets } from '../api/tickets.js'
import TicketTable from '../components/TicketTable.vue'

// Состояния: 'loading', 'empty', 'data', 'error'
const screenState = ref('loading')
const tickets = ref([])
const errorMessage = ref('')

const loadTickets = async () => {
  screenState.value = 'loading'
  try {
    const data = await getTickets()
    tickets.value = data
    if (data.length === 0) {
      screenState.value = 'empty'
    } else {
      screenState.value = 'data'
    }
  } catch (error) {
    errorMessage.value = 'Не удалось загрузить список заявок. Попробуйте позже.'
    screenState.value = 'error'
  }
}

// Вызываем загрузку при открытии страницы
onMounted(() => {
  loadTickets()
})
</script>

<template>
  <section class="content-card">
    <h2>Все заявки</h2>
    
    <!-- Загрузка -->
    <div v-if="screenState === 'loading'" class="state-msg">
      Загрузка...
    </div>

    <!-- Пустой список -->
    <div v-else-if="screenState === 'empty'" class="state-msg">
      Записей пока нет
    </div>

    <!-- Ошибка -->
    <div v-else-if="screenState === 'error'" class="state-msg error-text">
      {{ errorMessage }}
      <button @click="loadTickets" class="retry-btn">Повторить</button>
    </div>

    <!-- Данные (Таблица) -->
    <div v-else-if="screenState === 'data'">
      <TicketTable :items="tickets" />
    </div>
  </section>
</template>

<style scoped>
.content-card { background: white; padding: 35px; border-radius: 16px; box-shadow: 0 10px 25px rgba(0,0,0,0.05); border: 1px solid #edf2f7; }
.content-card h2 { margin-top: 0; margin-bottom: 25px; font-size: 24px; color: #2d3748; text-align: center; }
.state-msg { text-align: center; padding: 40px; font-size: 18px; color: #4a5568; }
.error-text { color: #e53e3e; }
.retry-btn { margin-top: 15px; padding: 8px 16px; background: #e2e8f0; border: none; border-radius: 8px; cursor: pointer; }
.retry-btn:hover { background: #cbd5e0; }
</style>