<script setup>
</script>

<template>
  <header class="app-header">
    <h1>Система учёта заявок</h1>
    <nav class="tabs">
      <router-link to="/" class="tab-btn">Список заявок</router-link>
      <router-link to="/new" class="tab-btn">Создать заявку</router-link>
      <router-link to="/login" class="tab-btn">Вход</router-link>
    </nav>
  </header>
</template>

<style scoped>
.app-header { text-align: center; margin-bottom: 30px; }
h1 { font-size: 32px; color: #1a202c; margin-bottom: 20px; }
.tabs { display: flex; justify-content: center; gap: 15px; }
.tab-btn {
  padding: 12px 24px; border: 1px solid #e2e8f0; background: white;
  border-radius: 10px; font-size: 15px; font-weight: 600; color: #4a5568;
  text-decoration: none; transition: all 0.2s ease; box-shadow: 0 2px 4px rgba(0,0,0,0.02);
}
.tab-btn:hover { background: #f8fafc; transform: translateY(-2px); box-shadow: 0 4px 8px rgba(0,0,0,0.08); }
.router-link-active { background: #42b883; color: white; border-color: #42b883; box-shadow: 0 4px 12px rgba(66, 184, 131, 0.3); }
</style>