using EmprestimosLivos.Models;
using Microsoft.EntityFrameworkCore;
// 2º passo da conexão com o banco: String de conexão  ------------------------------------------------------------
namespace EmprestimosLivos.Data
{  
    public class Contexxt : DbContext
    {
        public Contexxt(DbContextOptions<Contexxt> option) : base(option) 
        {
        }  
        
        public DbSet<EmprestimosModel> tb_Emprestimo {  get; set; } // criãção da tabela no contexto
    }
}
