
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
        //-----------------------------------------------------------------------------------------
        public IActionResult Index()
        {
            IEnumerable<EmprestimosModel> emprestimos = _db.tb_Emprestimo;
            return View(emprestimos);
        }
        //-----------------------------------------------------------------------------------------
        [HttpGet] // Cadastrar -------------------------------------------------------------------
        public IActionResult Cadastrar()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Cadastrar(EmprestimosModel emprestimos)
        {
            if (ModelState.IsValid)
            {
                _db.tb_Emprestimo.Add(emprestimos);
                _db.SaveChanges();

                TempData["MensagemSucesso"] = "CADASTRO realizado com SUCESSO";

                return RedirectToAction("Index");
            }
            TempData["MensagemErro"] = "Cadastro NÃO realizado com SUCESSO";
            return View();
        }
        //-----------------------------------------------------------------------------------------
        [HttpGet] // Editar -------------------------------------------------------------------
        public IActionResult Editar(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            EmprestimosModel emprestimo = _db.tb_Emprestimo.FirstOrDefault(x => x.Id == id);

            if (emprestimo == null)
            {
                return NotFound();
            }

            return View(emprestimo);
        }
        [HttpPost]
        public IActionResult Editar(EmprestimosModel emprestimo)
        {
            if (ModelState.IsValid)
            {
                _db.tb_Emprestimo.Update(emprestimo);
                _db.SaveChanges();

                TempData["MensagemSucesso"] = "EDIÇÃO realizada com SUCESSO";

                return RedirectToAction("Index");
            }

            TempData["MensagemErro"] = "EDIÇÃO NÃO realizada com SUCESSO";
            return View(emprestimo);
        }
        //-----------------------------------------------------------------------------------------
        [HttpGet] // Excluir-------------------------------------------------------------------
        public IActionResult Excluir(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            EmprestimosModel emprestimo = _db.tb_Emprestimo.FirstOrDefault(x => x.Id == id);

            if (emprestimo == null)
            {
                return NotFound();
            }

            return View(emprestimo);
        }
        [HttpPost]
        public IActionResult Excluir(EmprestimosModel emprestimo)
        {
            if (emprestimo == null)
            {
                return NotFound();
            }

            _db.tb_Emprestimo.Remove(emprestimo);
            _db.SaveChanges();

            TempData["MensagemSucesso"] = "EXCLUSÃO realizada com SUCESSO";

            return RedirectToAction("Index");            
        }
    }
}
