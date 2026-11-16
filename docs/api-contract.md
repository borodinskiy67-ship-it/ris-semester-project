# Контракт API

| Метод и путь | Тело запроса | Успех | Возможная ошибка |
|--------------|--------------|-------|------------------|
| GET `/api/tickets` | — | 200, массив или `[]` | — |
| GET `/api/tickets/{id}` | — | 200, объект | 404 |
| POST `/api/tickets` | `title`, `equipmentId`, `description` | 201, `id`, `number`, `status: New` | 400 |
| PATCH `/api/tickets/{id}/assignee` | `assigneeUserId` | 200 | 404 |
| PATCH `/api/tickets/{id}/status` | `status` | 200 | 404, 409 |

Клиент не присылает при создании `id`, `number`, `status` и исполнителя. Их определяет сервер.

## Примеры JSON

**Создание заявки (POST /api/tickets)**
```json
{
  "title": "Не включается монитор",
  "equipmentId": 3,
  "description": "При включении нет изображения"
}
{
  "id": 10,
  "number": "REQ-2026-001",
  "status": "New",
  "title": "Не включается монитор",
  "equipmentId": 3
}
{ "assigneeUserId": 5 }
{ "status": "InProgress" }