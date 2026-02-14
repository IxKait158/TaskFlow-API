# TaskFlow API

TaskFlow – це backend Web API для управління завданнями всередині проектів.
Проект реалізований як навчальний pet-project із фокусом на чисту архітектуру, REST API та роботу з базою даних.

## Можливості

- Реєстрація та автентифікація користувачів (JWT)
- Створення та управління проектами
- Робота із завданнями всередині проектів
- призначення виконавців завдань
- Зміна статусів завдань
- Контроль доступу та прав користувачів

## Архітектура

Проект побудований з використанням layered (clean-ish) архітектури та поділений на декілька проектів:

```text
WebAPITest.Api            — HTTP шар (Controllers, Middleware)
WebAPITest.Application    — бізнес-логіка, сервіси, DTO
WebAPITest.Domain         — доменні сутності та enum'и
WebAPITest.Infrastructure — EF Core, БД, JWT, репозиторії
```

## Стек технологій
```text
C#
ASP.NET Core Web API
Entity Framework Core
SQL Server
JWT Authentication
Swagger (OpenAPI)
```
