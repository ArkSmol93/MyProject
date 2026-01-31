using Microsoft.EntityFrameworkCore;

namespace API.Models
{
    // Klasa odpowiedzialna za dostęp do bazy danych
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // Tabela użytkowników
        public DbSet<User> Users { get; set; }

        // Tabela zasobów
        public DbSet<Resource> Resources { get; set; }
    }

    // Przykładowa klasa użytkownika
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; } // nigdy nie trzymamy hasła wprost
    }

    // Przykładowa klasa zasobu
    public class Resource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
    }
}
