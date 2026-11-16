<script setup>
import { ref } from 'vue'
import { useRouter } from 'vue-router'
import { createTicket } from '../api/tickets.js'

const router = useRouter()

const form = ref({
  title: '',
  equipmentId: null,
  description: ''
})

const errors = ref({ title: '', equipmentId: '', description: '' })
const isSubmitting = ref(false)
const serverError = ref('')

async function onCreate() {
  if (isSubmitting.value) return // Защита от двойного клика

  errors.value = { title: '', equipmentId: '', description: '' }
  serverError.value = ''
  let isValid = true

  const cleanTitle = form.value.title.trim()
  if (cleanTitle.length < 5 || cleanTitle.length > 80) {
    errors.value.title = 'Тема: от 5 до 80 символов'
    isValid = false
  }

  if (!Number.isInteger(form.value.equipmentId) || form.value.equipmentId <= 0) {
    errors.value.equipmentId = 'ID оборудования: целое число больше 0'
    isValid = false
  }

  const cleanDesc = form.value.description.trim()
  if (cleanDesc.length < 10 || cleanDesc.length > 500) {
    errors.value.description = 'Описание: от 10 до 500 символов'
    isValid = false
  }

  if (!isValid) return

  // Отправляем на сервер (или mock)
  isSubmitting.value = true
  try {
    await createTicket({
      title: cleanTitle,
      equipmentId: form.value.equipmentId,
      description: cleanDesc
    })
    
    // Если успех — очищаем и идем на главную
    form.value = { title: '', equipmentId: null, description: '' }
    router.push('/')
    
  } catch (error) {
    // Обработка разных ошибок
    if (error.status === 400) {
      serverError.value = 'Проверьте правильность заполнения полей.'
    } else if (error.status === 500) {
      serverError.value = 'Сервер не смог обработать запрос. Попробуйте позже.'
    } else {
      serverError.value = 'Нет связи с сервером. Проверьте подключение.'
    }
  } finally {
    isSubmitting.value = false
  }
}
</script>

<template>
  <section class="content-card">
    <h2>Создание заявки</h2>
    
    <div v-if="serverError" class="server-error">
      {{ serverError }}
    </div>

    <form @submit.prevent="onCreate" class="modern-form">
      <div class="form-group">
        <label for="title">Тема (5-80 символов)</label>
        <input id="title" v-model="form.title" placeholder="Например: Не включается монитор" autofocus />
        <span v-if="errors.title" class="error-msg">{{ errors.title }}</span>
      </div>
      
      <div class="form-group">
        <label for="equipmentId">ID Оборудования</label>
        <input id="equipmentId" v-model.number="form.equipmentId" type="number" placeholder="Например: 3" />
        <span v-if="errors.equipmentId" class="error-msg">{{ errors.equipmentId }}</span>
      </div>

      <div class="form-group">
        <label for="description">Описание (10-500 символов)</label>
        <textarea id="description" v-model="form.description" rows="4" placeholder="Опишите проблему подробнее..."></textarea>
        <span v-if="errors.description" class="error-msg">{{ errors.description }}</span>
      </div>

      <button type="submit" class="btn-primary" :disabled="isSubmitting">
        {{ isSubmitting ? 'Создаём...' : 'Создать заявку' }}
      </button>
    </form>
  </section>
</template>

<style scoped>
.content-card { background: white; padding: 35px; border-radius: 16px; box-shadow: 0 10px 25px rgba(0,0,0,0.05); border: 1px solid #edf2f7; }
.content-card h2 { margin-top: 0; margin-bottom: 25px; font-size: 24px; color: #2d3748; text-align: center; }
.modern-form { max-width: 500px; margin: 0 auto; }
.form-group { margin-bottom: 20px; display: flex; flex-direction: column; }
.form-group label { margin-bottom: 8px; font-weight: 600; font-size: 14px; color: #4a5568; }
.form-group input, .form-group textarea { padding: 14px; border: 1px solid #cbd5e0; border-radius: 10px; font-size: 15px; font-family: inherit; background: #f8fafc; }
.form-group input:focus, .form-group textarea:focus { outline: none; border-color: #42b883; background: white; box-shadow: 0 0 0 4px rgba(66, 184, 131, 0.15); }

.error-msg { color: #e53e3e; font-size: 13px; margin-top: 6px; font-weight: 500; }
.server-error { background: #fed7d7; color: #c53030; padding: 15px; border-radius: 8px; margin-bottom: 20px; text-align: center; font-weight: 600; }

.btn-primary { padding: 14px 24px; background: #42b883; color: white; border: none; border-radius: 10px; font-size: 16px; font-weight: 600; cursor: pointer; width: 100%; margin-top: 10px; transition: all 0.2s; }
.btn-primary:hover:not(:disabled) { background: #3aa876; transform: translateY(-2px); box-shadow: 0 6px 15px rgba(66, 184, 131, 0.3); }
.btn-primary:disabled { background: #a0aec0; cursor: not-allowed; }
</style>