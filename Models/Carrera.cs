namespace TPAlumno.Models
{
    public class Carrera
    {
        public int idCarrera { get; set; }
        public string nombre { get; set; }
        public int idFacultad { get; set; }
        public string tipo { get; set; }
        public int cupoMax { get; set; }
        public string estado { get; set; }
        public int cantMaterias { get; set; }
        public int cantAlumnos { get; set; }
        public int cantCiclos { get; set; }
    }
}
