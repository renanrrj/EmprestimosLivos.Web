using EmprestimosLivos.Data;
using EmprestimosLivos.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmprestimosLivos.Controllers
{
    public class EmprestimosController : Controller
    {
        readonly private Contexxt _db;

        public EmprestimosController(Contexxt db) 
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<EmprestimosModel> emprestimos = _db.tb_Emprestimo;
            return View(emprestimos);
        }
    }
}
