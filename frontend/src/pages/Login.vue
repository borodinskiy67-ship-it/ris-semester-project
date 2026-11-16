<template>
  <section class="content-card">
    <h2>Вход в систему</h2>
    <form @submit.prevent class="modern-form">
      <div class="form-group">
        <label for="login">Логин</label>
        <input id="login" name="login" placeholder="Введите логин" />
      </div>
      <div class="form-group">
        <label for="password">Пароль</label>
        <input id="password" name="password" type="password" placeholder="Введите пароль" />
      </div>
      <button type="submit" class="btn-primary">Войти</button>
    </form>
  </section>
</template>

<style scoped>
/* Копируем стили */
.content-card { background: white; padding: 35px; border-radius: 16px; box-shadow: 0 10px 25px rgba(0,0,0,0.05); border: 1px solid #edf2f7; }
.content-card h2 { margin-top: 0; margin-bottom: 25px; font-size: 24px; color: #2d3748; text-align: center; }
.modern-form { max-width: 500px; margin: 0 auto; }
.form-group { margin-bottom: 20px; display: flex; flex-direction: column; }
.form-group label { margin-bottom: 8px; font-weight: 600; font-size: 14px; color: #4a5568; }
.form-group input { padding: 14px; border: 1px solid #cbd5e0; border-radius: 10px; font-size: 15px; font-family: inherit; background: #f8fafc; }
.form-group input:focus { outline: none; border-color: #42b883; background: white; }
.btn-primary { padding: 14px 24px; background: #42b883; color: white; border: none; border-radius: 10px; font-size: 16px; font-weight: 600; cursor: pointer; width: 100%; }
</style>