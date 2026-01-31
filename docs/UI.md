
---

## **2️⃣ docs/UI.md**

```markdown
# Interfejs użytkownika (UI)

Projekt UI jest oparty na WPF i składa się z następujących elementów:

- **LoginWindow.xaml** – logowanie
- **RegisterWindow.xaml** – rejestracja
- **MainWindow.xaml** – zarządzanie zasobami (CRUD)

## LoginWindow

### Pola
- `UsernameTextBox` – wpisanie loginu
- `PasswordBox` – wpisanie hasła
- `MessageTextBlock` – komunikaty błędów/sukcesów

### Przyciski
- **Zaloguj** – loguje użytkownika poprzez `ApiService.Login` i zapisuje token JWT

---

## RegisterWindow

### Pola
- `UsernameTextBox` – login
- `PasswordBox` – hasło
- `ConfirmPasswordBox` – potwierdzenie hasła
- `MessageTextBlock` – komunikaty błędów

### Przyciski
- **Zarejestruj** – rejestracja użytkownika poprzez `ApiService.RegisterUser`

---

## MainWindow – zarządzanie zasobami

### Elementy
- `ListBox` – lista zasobów pobranych z API
- CRUD: Dodaj, Edytuj, Usuń

### Działanie
- Każda zmiana zasobu (dodanie/edycja/usunięcie) wysyła żądanie do API
- `SyncService` nasłuchuje aktualizacji z SignalR i odświeża listę w czasie rzeczywistym
