
# API

Projekt API udostępnia backend dla aplikacji WPF/Blazor i obsługuje CRUD zasobów oraz logowanie użytkowników.

## Endpointy

### GET /api/resources
Zwraca listę wszystkich zasobów w bazie danych.

### POST /api/resources
Dodaje nowy zasób.

**Body JSON:**
```json
{
  "Name": "Przykładowy zasób",
  "Description": "Opis zasobu"
}
PUT /api/resources/{id}

Aktualizuje istniejący zasób o podanym id.

Body JSON:

{
  "Name": "Nowa nazwa",
  "Description": "Nowy opis",
  "LastUpdated": "2026-01-31T12:00:00Z"
}

DELETE /api/resources/{id}

Usuwa zasób o podanym id.

POST /api/users/register

Rejestracja nowego użytkownika.

Body JSON:

{
  "Username": "login",
  "Password": "hasło"
}

POST /api/users/login

Logowanie użytkownika i zwrócenie tokena JWT.

Body JSON:

{
  "Username": "login",
  "Password": "hasło"
}
