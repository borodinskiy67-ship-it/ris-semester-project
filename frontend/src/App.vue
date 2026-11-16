
<script setup>
import AppHeader from './components/AppHeader.vue'
</script>

<template>
  <div class="app-wrapper">
    <main class="app-container">
      <AppHeader />
      <router-view />
    </main>
  </div>
</template>

<style>

* {
  box-sizing: border-box;
}

html,
body,
#app {
  width: 100% !important;
  min-height: 100vh;
  margin: 0 !important;
  padding: 0 !important;
}

body {
  background: #f4f7fb;
  font-family: 'Segoe UI', Roboto, sans-serif;
  color: #2c3e50;
}

.app-wrapper {
  width: 100% !important;
  min-height: 100vh;
  display: flex !important;
  justify-content: center !important;
  align-items: flex-start;
  padding: 40px 20px;
}

.app-container {
  width: 100% !important;
  max-width: 526px !important;
  margin: 0 auto !important;
  padding: 0 !important;
}

#app {
  max-width: none !important;
  width: 100% !important;
  margin: 0 !important;
  padding: 0 !important;
  text-align: left;
}

.app-wrapper {
  width: 100vw !important;
  display: flex !important;
  justify-content: center !important;
}

.app-container {
  width: 100% !important;
  max-width: 526px !important;
  margin-left: auto !important;
  margin-right: auto !important;
}

</style>
