using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MinimalApi.Entities
{
    public class Veiculo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } = 0;

        [Required]
        [StringLength(50)]
        public string Nome { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string Marca { get; set; } = string.Empty;

        [Required]       
        public int Ano { get; set; } = 0;
    }
}