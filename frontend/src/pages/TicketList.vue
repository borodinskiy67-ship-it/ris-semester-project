<script setup>
import { items } from '../ticketsStore.js'
import TicketTable from '../components/TicketTable.vue'
</script>

<template>
  <section class="content-card">
    <h2>Все заявки</h2>
    <TicketTable :items="items" />
  </section>
</template>

<style scoped>
.content-card { background: white; padding: 35px; border-radius: 16px; box-shadow: 0 10px 25px rgba(0,0,0,0.05); border: 1px solid #edf2f7; }
.content-card h2 { margin-top: 0; margin-bottom: 25px; font-size: 24px; color: #2d3748; text-align: center; }
</style>