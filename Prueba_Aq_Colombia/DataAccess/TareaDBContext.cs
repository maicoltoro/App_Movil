using Prueba_Aq_Colombia.Modelos;
using Prueba_Aq_Colombia.Utilidades;
using Microsoft.EntityFrameworkCore;

namespace Prueba_Aq_Colombia.DataAccess
{
    public class TareaDBContext : DbContext
    {
        public DbSet<Tareas> Tarea { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string conexionDB = $"Filename={ConexionDB.DevolderRuta("Tareas.db")}";
            optionsBuilder.UseSqlite(conexionDB);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tareas>(entity =>
            {
                entity.HasKey(col => col.Id);
                entity.Property(col => col.Id).IsRequired().ValueGeneratedOnAdd();
            });
        }
    }
}
