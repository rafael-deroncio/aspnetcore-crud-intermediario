using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRUDintermediario.Models
{
    public class ProdutoModel
    {
        [Key]
        public int ID { get; set; }

        [Required, MaxLength(128)]
        public string Nome { get; set; }
        
        public double Valor { get; set; }
        
        public int Quantidade { get; set; }
        
        public string DataCadastro { get; set; }
        
        public string DataUltimaAlteracao { get; set; }
        
        [ForeignKey("Categoria")]
        public int IDCategoria { get; set; }
        
        public CategoriaModel Categoria { get; set; }
    }
}