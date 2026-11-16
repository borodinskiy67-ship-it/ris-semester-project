<template>
  <section class="content-card">
    <h2>Создание заявки</h2>
    <form @submit.prevent class="modern-form">
      <div class="form-group">
        <label for="title">Тема (5-80 символов)</label>
        <input id="title" name="title" placeholder="Например: Не включается монитор" autofocus />
      </div>
      <div class="form-group">
        <label for="equipmentId">ID Оборудования</label>
        <input id="equipmentId" name="equipmentId" type="number" placeholder="Например: 3" />
      </div>
      <div class="form-group">
        <label for="description">Описание (10-500 символов)</label>
        <textarea id="description" name="description" rows="4" placeholder="Опишите проблему подробнее..."></textarea>
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
.form-group input:focus, .form-group textarea:focus { outline: none; border-color: #42b883; background: white; }
.btn-primary { padding: 14px 24px; background: #42b883; color: white; border: none; border-radius: 10px; font-size: 16px; font-weight: 600; cursor: pointer; width: 100%; }
</style>