<script setup>
import { ref } from 'vue'
import { useRouter } from 'vue-router'
import { addItem } from '../ticketsStore.js'

// Хук для перехода на другие страницы
const router = useRouter()

// Объект формы
const form = ref({
  title: '',
  equipmentId: null,
  description: ''
})

// Объект для хранения текстов ошибок
const errors = ref({
  title: '',
  equipmentId: '',
  description: ''
})

// Функция, которая вызывается при отправке формы
function onCreate() {
  // 1. Сбрасываем старые ошибки
  errors.value = { title: '', equipmentId: '', description: '' }
  let isValid = true

  // 2. Проверяем тему (5-80 символов)
  const cleanTitle = form.value.title.trim()
  if (cleanTitle.length < 5 || cleanTitle.length > 80) {
    errors.value.title = 'Тема: от 5 до 80 символов'
    isValid = false
  }

  // 3. Проверяем ID оборудования (целое число > 0)
  if (!Number.isInteger(form.value.equipmentId) || form.value.equipmentId <= 0) {
    errors.value.equipmentId = 'ID оборудования: должно быть целое число больше 0'
    isValid = false
  }

  // 4. Проверяем описание (10-500 символов)
  const cleanDesc = form.value.description.trim()
  if (cleanDesc.length < 10 || cleanDesc.length > 500) {
    errors.value.description = 'Описание: от 10 до 500 символов'
    isValid = false
  }

  // Если есть ошибки — останавливаемся, ничего не добавляем
  if (!isValid) return

  // 5. Добавляем в общий массив
  addItem(cleanTitle, form.value.equipmentId, cleanDesc)

  // 6. Очищаем форму
  form.value = { title: '', equipmentId: null, description: '' }

  // 7. Переходим на страницу списка
  router.push('/')
}
</script>

<template>
  <section class="content-card">
    <h2>Создание заявки</h2>
    <!-- @submit.prevent не дает странице перезагружаться -->
    <form @submit.prevent="onCreate" class="modern-form">
      
      <!-- Поле: Тема -->
      <div class="form-group">
        <label for="title">Тема (5-80 символов)</label>
        <input 
          id="title" 
          v-model="form.title" 
          placeholder="Например: Не включается монитор" 
          autofocus 
        />
        <!-- Вывод ошибки, если она есть -->
        <span v-if="errors.title" class="error-msg">{{ errors.title }}</span>
      </div>
      
      <!-- Поле: ID оборудования -->
      <div class="form-group">
        <label for="equipmentId">ID Оборудования</label>
        <!-- v-model.number автоматически превращает введенный текст в число -->
        <input 
          id="equipmentId" 
          v-model.number="form.equipmentId" 
          type="number" 
          placeholder="Например: 3" 
        />
        <span v-if="errors.equipmentId" class="error-msg">{{ errors.equipmentId }}</span>
      </div>

      <!-- Поле: Описание -->
      <div class="form-group">
        <label for="description">Описание (10-500 символов)</label>
        <textarea 
          id="description" 
          v-model="form.description" 
          rows="4" 
          placeholder="Опишите проблему подробнее..."
        ></textarea>
        <span v-if="errors.description" class="error-msg">{{ errors.description }}</span>
      </div>

      <button type="submit" class="btn-primary">Создать заявку</button>
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

/* Стили для красного текста ошибки */
.error-msg { 
  color: #e53e3e; 
  font-size: 13px; 
  margin-top: 6px; 
  font-weight: 500;
}

.btn-primary { padding: 14px 24px; background: #42b883; color: white; border: none; border-radius: 10px; font-size: 16px; font-weight: 600; cursor: pointer; width: 100%; margin-top: 10px; transition: all 0.2s; }
.btn-primary:hover { background: #3aa876; transform: translateY(-2px); box-shadow: 0 6px 15px rgba(66, 184, 131, 0.3); }
</style>