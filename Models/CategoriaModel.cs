using System;
using System.ComponentModel.DataAnnotations;

namespace CRUDintermediario.Models
{
    public class CategoriaModel
    {
        [Key]
        public int ID { get; set; }
        
        [Required, MaxLength(128)]
        public string Nome { get; set; }

        public string DataCadastro { get; set; }

        public string DataUltimaAlteracao { get; set; }

        public bool Ativo { get; set; }
    }
}