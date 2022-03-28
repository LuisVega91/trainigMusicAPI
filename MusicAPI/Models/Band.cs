using System.ComponentModel.DataAnnotations;

namespace MusicAPI.Models
{
    public class Band
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "El nombre es requerido")]
        [MaxLength(50, ErrorMessage = "El número máximo de caracteres permitidos es 50")]
        [Display(Name = "Nombre")]
        public string Name { get; set; }

        public List<Album> Albums { get; set; }
    }

}
