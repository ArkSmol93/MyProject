# 📦 Projekt: System Zarządzania Zasobami

Projekt to aplikacja WPF + ASP.NET Core API z synchronizacją danych w czasie rzeczywistym przy użyciu SignalR i RabbitMQ.

---

## 🔧 Wymagania

- Visual Studio 2022 lub nowsze  
- .NET 6/7 SDK  
- SQL Server / LocalDB  
- RabbitMQ (tylko jeśli planujesz synchronizację na wielu maszynach)

---

## 🚀 Uruchomienie API

1. Otwórz projekt **API** w Visual Studio  
2. Skonfiguruj `appsettings.json` z połączeniem do bazy danych:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=MyProjectDb;Trusted_Connection=True;"
}
Zbuduj projekt i uruchom go (F5 lub Ctrl+F5)

Sprawdź działanie API poprzez Swagger:
https://localhost:5001/swagger

Endpointy dostępne: /api/users/register, /api/users/login, /api/resources

🖥 Uruchomienie UI (WPF)
Otwórz projekt UI w Visual Studio

Upewnij się, że API działa

Uruchom UI (F5)

Okna aplikacji
LoginWindow – logowanie użytkownika

RegisterWindow – rejestracja nowego użytkownika

MainWindow – zarządzanie zasobami (dodawanie, edycja, usuwanie)

Lista zasobów odświeża się automatycznie dzięki SignalR

📝 Testy
Projekt testów: Tests

Testy jednostkowe kontrolerów API – xUnit

Testy integracyjne modułu synchronizacji danych – SignalR + xUnit

Testy UI (opcjonalnie) – Selenium lub Playwright

Aby uruchomić testy:

Otwórz projekt Tests w Visual Studio

Uruchom wszystkie testy (Test Explorer)

⚙️ Synchronizacja danych
SignalR – powiadamianie klientów o zmianach w czasie rzeczywistym

RabbitMQ – kolejki wiadomości między serwerami przy wielu instancjach

Wykrywanie konfliktów – kolumna LastUpdated w tabelach zasobów, HTTP 409 Conflict w przypadku kolizji

📂 Struktura repozytorium
/API
/UI
/Tests
/docs
   API.md
   UI.md
   Synchronization.md
   README.md
🔑 Wskazówki
Token JWT generowany po logowaniu jest wymagany do korzystania z endpointów API dla zasobów

SignalR i RabbitMQ umożliwiają automatyczną synchronizację zmian między różnymi instancjami aplikacji

Każdy nowy zasób lub zmiana istniejącego jest od razu widoczna w UI wszystkich zalogowanych użytkowników
