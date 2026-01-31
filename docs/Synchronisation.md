# Synchronizacja danych

## Mechanizm

- **SignalR** – komunikacja w czasie rzeczywistym między UI i API
- **RabbitMQ** – kolejki wiadomości między serwerami w przypadku wielu instancji
- **Wykrywanie konfliktów** – kolumna `LastUpdated` w tabelach, jeśli inna instancja zmieniła dane wcześniej, API zwraca `HTTP 409 Conflict`

## Przykład użycia

1. Klient wysyła zmianę do API (POST/PUT/DELETE)
2. API zapisuje zmiany w bazie danych
3. Hub SignalR (`/syncHub`) powiadamia wszystkie podłączone instancje
4. RabbitMQ przesyła wiadomość do wszystkich serwerów w klastrze
5. Każdy klient automatycznie aktualizuje UI

