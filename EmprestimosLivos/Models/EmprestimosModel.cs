using System.ComponentModel.DataAnnotations;

namespace EmprestimosLivos.Models
{
    public class EmprestimosModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Digite o nome do RECEBEDOR")]
        public string Recebedor { get; set; }

        [Required(ErrorMessage = "Digite o nome do FORNECEDOR")]
        public string Fornecedor { get; set; }

        [Required(ErrorMessage = "Digite o nome do LIVRO EMPRESTADO")]
        public string LivroEmprestado { get; set; }
        public DateTime DataEmprestimo { get; set; }
    }




}
