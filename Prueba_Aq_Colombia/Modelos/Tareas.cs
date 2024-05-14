using System.ComponentModel.DataAnnotations;

namespace Prueba_Aq_Colombia.Modelos
{
    public class Tareas
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Estado { get; set;}
        public string Categoria { get; set; }
    }
}
