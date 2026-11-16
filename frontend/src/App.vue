
<script setup>
import { ref } from 'vue'

const screen = ref('list')

const tickets = ref([
  {
    id: 1,
    number: 'REQ-2026-001',
    title: 'Не включается монитор',
    equipmentId: 3,
    status: 'Новая'
  },
  {
    id: 2,
    number: 'REQ-2026-002',
    title: 'Сломан проектор',
    equipmentId: 5,
    status: 'В работе'
  }
])

const newTicket = ref({
  title: '',
  equipmentId: '',
  description: ''
})

const loginData = ref({
  login: '',
  password: ''
})

const message = ref('')

function createTicket() {
  const id = tickets.value.length + 1

  tickets.value.push({
    id,
    number: `REQ-${new Date().getFullYear()}-${String(id).padStart(3, '0')}`,
    title: newTicket.value.title,
    equipmentId: Number(newTicket.value.equipmentId),
    status: 'Новая'
  })

  newTicket.value = {
    title: '',
    equipmentId: '',
    description: ''
  }

  message.value = 'Заявка успешно создана!'
  screen.value = 'list'
}

function login() {
  message.value = 'Форма входа отправлена. Подключите авторизацию для проверки данных.'
}
</script>

<template>
  <main class="app-wrapper">
    <div class="app-container">

      <header class="header">
        <div class="logo">🔧</div>
        <h1>Система учёта заявок</h1>
        <p class="subtitle">Ремонт компьютерного оборудования</p>
      </header>

      <nav class="tabs">
        <button
          type="button"
          :class="{ active: screen === 'list' }"
          @click="screen = 'list'; message = ''"
        >
          📋 Список заявок
        </button>

        <button
          type="button"
          :class="{ active: screen === 'new' }"
          @click="screen = 'new'; message = ''"
        >
          ➕ Создать заявку
        </button>

        <button
          type="button"
          :class="{ active: screen === 'login' }"
          @click="screen = 'login'; message = ''"
        >
          👤 Вход
        </button>
      </nav>

      <div v-if="message" class="notification">
        {{ message }}
      </div>

      <!-- СПИСОК ЗАЯВОК -->
      <section v-if="screen === 'list'" class="content-card">
        <div class="section-heading">
          <div>
            <h2>Все заявки</h2>
            <p>Список обращений по ремонту оборудования</p>
          </div>
          <span class="count">{{ tickets.length }} заявок</span>
        </div>

        <div class="table-responsive">
          <table class="styled-table">
            <thead>
              <tr>
                <th>Номер</th>
                <th>Тема</th>
                <th>ID оборудования</th>
                <th>Статус</th>
              </tr>
            </thead>

            <tbody>
              <tr v-for="item in tickets" :key="item.id">
                <td class="nowrap">
                  <strong>{{ item.number }}</strong>
                </td>

                <td>{{ item.title }}</td>

                <td>
                  <span class="equipment-badge">
                    {{ item.equipmentId }}
                  </span>
                </td>

                <td class="nowrap">
                  <span
                    class="status"
                    :class="item.status === 'Новая'
                      ? 'status-new'
                      : 'status-progress'"
                  >
                    {{ item.status }}
                  </span>
                </td>
              </tr>

              <tr v-if="tickets.length === 0">
                <td colspan="4" class="empty">
                  Заявок пока нет
                </td>
              </tr>
            </tbody>
          </table>
        </div>
      </section>

      <!-- СОЗДАНИЕ ЗАЯВКИ -->
      <section v-else-if="screen === 'new'" class="content-card">
        <div class="section-heading">
          <div>
            <h2>Создание заявки</h2>
            <p>Заполните информацию о неисправности</p>
          </div>
        </div>

        <form @submit.prevent="createTicket" class="modern-form">

          <div class="form-group">
            <label for="title">Тема заявки</label>
            <input
              id="title"
              v-model.trim="newTicket.title"
              type="text"
              minlength="5"
              maxlength="80"
              placeholder="Например: Не включается монитор"
              required
            />
            <small>От 5 до 80 символов</small>
          </div>

          <div class="form-group">
            <label for="equipmentId">ID оборудования</label>
            <input
              id="equipmentId"
              v-model="newTicket.equipmentId"
              type="number"
              min="1"
              placeholder="Например: 3"
              required
            />
          </div>

          <div class="form-group">
            <label for="description">Описание проблемы</label>
            <textarea
              id="description"
              v-model.trim="newTicket.description"
              minlength="10"
              maxlength="500"
              rows="4"
              placeholder="Опишите проблему подробнее..."
              required
            ></textarea>
            <small>От 10 до 500 символов</small>
          </div>

          <button type="submit" class="btn-primary">
            Создать заявку
          </button>

        </form>
      </section>

      <!-- ВХОД -->
      <section v-else-if="screen === 'login'" class="content-card">
        <div class="section-heading">
          <div>
            <h2>Вход в систему</h2>
            <p>Введите свои данные для входа</p>
          </div>
        </div>

        <form @submit.prevent="login" class="modern-form">

          <div class="form-group">
            <label for="login">Логин</label>
            <input
              id="login"
              v-model.trim="loginData.login"
              type="text"
              placeholder="Введите логин"
              required
            />
          </div>

          <div class="form-group">
            <label for="password">Пароль</label>
            <input
              id="password"
              v-model="loginData.password"
              type="password"
              placeholder="Введите пароль"
              required
            />
          </div>

          <button type="submit" class="btn-primary">
            Войти
          </button>

        </form>
      </section>

      <footer>
        Система учёта заявок © {{ new Date().getFullYear() }}
      </footer>

    </div>
  </main>
</template>

<style>
/* Сброс отступов и правильный расчёт размеров */
* {
  box-sizing: border-box;
}

html {
  width: 100%;
  min-height: 100%;
  overflow-x: hidden;
}

body {
  margin: 0;
  width: 100%;
  min-width: 320px;
  min-height: 100vh;
  overflow-x: hidden;
  font-family: 'Segoe UI', Roboto, Arial, sans-serif;
  color: #2d3748;
  background: #f4f7fb;
}

button,
input,
textarea {
  font: inherit;
}

#app {
  width: 100%;
  min-height: 100vh;
  overflow-x: hidden;
}

/* Главная обёртка */
.app-wrapper {
  width: 100%;
  min-height: 100vh;
  padding: 40px 20px;
  display: flex;
  justify-content: center;
  align-items: flex-start;
}

/* Контейнер строго по центру */
.app-container {
  width: 100%;
  max-width: 850px;
  min-width: 0;
  margin: 0 auto;
}

/* Заголовок */
.header {
  text-align: center;
  margin-bottom: 30px;
}

.logo {
  font-size: 38px;
  margin-bottom: 10px;
}

.header h1 {
  font-size: clamp(23px, 4vw, 32px);
  margin: 0 0 10px;
  color: #1a202c;
  font-weight: 700;
}

.subtitle {
  color: #718096;
  font-size: 15px;
  margin: 0;
}

/* Вкладки */
.tabs {
  display: grid;
  grid-template-columns: repeat(3, minmax(0, 1fr));
  gap: 12px;
  margin-bottom: 25px;
}

.tabs button {
  min-width: 0;
  padding: 14px 8px;
  border: 1px solid #e2e8f0;
  background: white;
  border-radius: 10px;
  color: #4a5568;
  font-size: 14px;
  font-weight: 600;
  cursor: pointer;
  transition: 0.2s;
}

.tabs button:hover {
  border-color: #42b883;
  background: #f0fff8;
}

.tabs button.active {
  background: #42b883;
  color: white;
  border-color: #42b883;
  box-shadow: 0 4px 12px rgba(66, 184, 131, 0.2);
}

/* Уведомление */
.notification {
  background: #e6fffa;
  color: #267d69;
  padding: 14px 18px;
  border-radius: 10px;
  margin-bottom: 20px;
  text-align: center;
  font-weight: 600;
}

/* Карточка */
.content-card {
  width: 100%;
  min-width: 0;
  padding: 30px;
  background: white;
  border: 1px solid #edf2f7;
  border-radius: 16px;
  box-shadow: 0 8px 25px rgba(0, 0, 0, 0.04);
}

.section-heading {
  display: flex;
  justify-content: space-between;
  align-items: center;
  gap: 12px;
  margin-bottom: 25px;
}

.section-heading h2 {
  font-size: 23px;
  margin: 0 0 7px;
  color: #2d3748;
}

.section-heading p {
  margin: 0;
  color: #718096;
  font-size: 13px;
}

.count {
  white-space: nowrap;
  background: #e6fffa;
  color: #319795;
  padding: 8px 12px;
  border-radius: 8px;
  font-size: 13px;
  font-weight: 600;
}

/* Таблица */
.table-responsive {
  width: 100%;
  max-width: 100%;
  overflow-x: auto;
  -webkit-overflow-scrolling: touch;
}

.styled-table {
  width: 100%;
  border-collapse: collapse;
  text-align: left;
}

.styled-table th {
  padding: 14px 12px;
  background: #f7fafc;
  color: #718096;
  font-size: 12px;
  text-transform: uppercase;
  letter-spacing: 0.3px;
  border-bottom: 2px solid #edf2f7;
}

.styled-table td {
  padding: 17px 12px;
  font-size: 14px;
  border-bottom: 1px solid #edf2f7;
  overflow-wrap: anywhere;
}

.styled-table tbody tr:last-child td {
  border-bottom: none;
}

.styled-table tbody tr:hover {
  background: #f8fafc;
}

.nowrap {
  white-space: nowrap;
}

.equipment-badge {
  display: inline-block;
  padding: 5px 11px;
  background: #edf2f7;
  color: #4a5568;
  border-radius: 7px;
  font-weight: 600;
}

.status {
  display: inline-block;
  padding: 6px 12px;
  border-radius: 20px;
  font-size: 12px;
  font-weight: 600;
  white-space: nowrap;
}

.status-new {
  background: #e6fffa;
  color: #319795;
}

.status-progress {
  background: #fefcbf;
  color: #b7791f;
}

.empty {
  text-align: center;
  color: #a0aec0;
  padding: 30px !important;
}

/* Формы */
.modern-form {
  width: 100%;
  max-width: 500px;
  margin: 0 auto;
}

.form-group {
  display: flex;
  flex-direction: column;
  width: 100%;
  margin-bottom: 20px;
}

.form-group label {
  margin-bottom: 8px;
  color: #4a5568;
  font-size: 14px;
  font-weight: 600;
}

.form-group input,
.form-group textarea {
  display: block;
  width: 100%;
  max-width: 100%;
  min-width: 0;
  padding: 14px;
  border: 1px solid #cbd5e0;
  border-radius: 10px;
  outline: none;
  background: #f8fafc;
  color: #2d3748;
  font-size: 15px;
  transition: 0.2s;
}

.form-group textarea {
  resize: vertical;
}

.form-group input:focus,
.form-group textarea:focus {
  border-color: #42b883;
  background: white;
  box-shadow: 0 0 0 3px rgba(66, 184, 131, 0.12);
}

.form-group small {
  margin-top: 6px;
  color: #a0aec0;
  font-size: 12px;
}

/* Кнопка */
.btn-primary {
  width: 100%;
  padding: 15px;
  margin-top: 5px;
  border: none;
  border-radius: 10px;
  background: #42b883;
  color: white;
  font-size: 15px;
  font-weight: 600;
  cursor: pointer;
  transition: 0.2s;
}

.btn-primary:hover {
  background: #35a875;
  transform: translateY(-1px);
  box-shadow: 0 5px 14px rgba(66, 184, 131, 0.25);
}

footer {
  margin-top: 25px;
  text-align: center;
  color: #a0aec0;
  font-size: 12px;
}

/* Адаптация для телефона */
@media (max-width: 600px) {
  .app-wrapper {
    padding: 25px 12px;
  }

  .header {
    margin-bottom: 22px;
  }

  .tabs {
    gap: 6px;
  }

  .tabs button {
    padding: 12px 4px;
    font-size: 12px;
  }

  .content-card {
    padding: 20px 14px;
    border-radius: 12px;
  }

  .section-heading {
    align-items: flex-start;
    flex-direction: column;
  }

  .styled-table {
    min-width: 570px;
  }

  .modern-form {
    max-width: 100%;
  }
}

/* Очень маленькие экраны */
@media (max-width: 360px) {
  .app-wrapper {
    padding-left: 8px;
    padding-right: 8px;
  }

  .tabs button {
    font-size: 11px;
  }

  .content-card {
    padding: 16px 10px;
  }
}
</style>
