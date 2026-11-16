<script setup>
defineProps({ items: { type: Array, required: true } })
</script>

<template>
  <div class="table-responsive">
    <table class="styled-table">
      <thead>
        <tr>
          <th>Номер</th><th>Тема</th><th>ID Оборудования</th><th>Статус</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="item in items" :key="item.id">
          <td class="nowrap"><strong>{{ item.number }}</strong></td>
          <td>{{ item.title }}</td>
          <td><span class="badge">{{ item.equipmentId }}</span></td>
          <td class="nowrap">
            <span class="status" :class="item.status === 'Новая' ? 'status-new' : 'status-progress'">
              {{ item.status }}
            </span>
          </td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<style scoped>
.table-responsive { width: 100%; overflow-x: auto; }
.styled-table { width: 100%; border-collapse: collapse; }
.styled-table th { text-align: left; padding: 15px; background-color: #f7fafc; color: #4a5568; font-weight: 600; font-size: 14px; text-transform: uppercase; letter-spacing: 0.5px; border-bottom: 2px solid #edf2f7; }
.styled-table td { padding: 18px 15px; border-bottom: 1px solid #edf2f7; vertical-align: middle; font-size: 15px; }
.styled-table tr:last-child td { border-bottom: none; }
.styled-table tr:hover { background-color: #f8fafc; }
.nowrap { white-space: nowrap; }
.badge { background: #edf2f7; padding: 4px 10px; border-radius: 6px; font-weight: 600; font-size: 14px; color: #4a5568; }
.status { display: inline-block; padding: 6px 14px; border-radius: 20px; font-size: 13px; font-weight: 600; white-space: nowrap; }
.status-new { background-color: #e6fffa; color: #319795; }
.status-progress { background-color: #fefcbf; color: #b7791f; }
</style>