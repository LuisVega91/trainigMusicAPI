using System.ComponentModel.DataAnnotations;

namespace MusicAPI.Models
{
    public class Album
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "El nombre es requerido")]
        [MaxLength(50, ErrorMessage = "El número máximo de caracteres permitidos es 50")]
        [Display(Name = "Nombre")]
        public string Name { get; set; }

        [Required(ErrorMessage = "El año es requerido")]
        [Display(Name = "Año")]
        [Range(1000,9999)]
        public int Year { get; set; }

        public int BandId { get; set; }
    }
}
