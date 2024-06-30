using Microsoft.EntityFrameworkCore;
using TPAlumno.Models;
using TPAlumnotest.Models;
namespace TPAlumnotest.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) :base(options) {

        }
        public DbSet<Usuario> Usuarios{ get; set; }
        public DbSet<Persona> Personas { get; set; }
        public DbSet<Facultad> Facultads { get; set; }
        public DbSet<Carrera> Carreras { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Usuario>(tb =>
            {
                tb.HasKey(col => col.IDUsuario);
                tb.Property(col => col.IDUsuario)
                .UseIdentityColumn()
                .ValueGeneratedOnAdd();

                tb.Property(col => col.Username).HasMaxLength(100);
                tb.Property(col => col.Clave).HasMaxLength(500);
            });
            modelBuilder.Entity<Carrera>(tb =>
            {
                tb.HasKey(col => col.idCarrera);
                tb.Property(col => col.idCarrera).UseIdentityColumn().ValueGeneratedOnAdd();
                tb.Property(col => col.nombre);
                
            });
            modelBuilder.Entity<Facultad>(tb =>
            {
                tb.HasKey(col => col.idFacultad);
            });
            modelBuilder.Entity<Persona>(tb =>
            {
                tb.HasKey(col => col.idPersona);
            });


            modelBuilder.Entity<Usuario>().ToTable("Usuario");
        }
    }
}
