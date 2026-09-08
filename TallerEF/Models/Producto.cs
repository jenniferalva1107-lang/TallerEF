using System.ComponentModel.DataAnnotations;

namespace TallerEF.Models
{
    public class Producto
    {
        [Key]
        public int IdProducto { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [StringLength(200)]
        public string? Descripcion { get; set; }

        [Range(0.01, 999999, ErrorMessage = "Ingrese un precio válido (mayor a 0)")]
        public decimal Precio { get; set; }

        [Range(0, 100000, ErrorMessage = "El stock no puede ser negativo")]
        public int Stock { get; set; }

        [Required(ErrorMessage = "La categoría es obligatoria")]
        public string Categoria { get; set; } = string.Empty;
    }
}