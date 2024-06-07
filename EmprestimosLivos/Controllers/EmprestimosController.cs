
using ClosedXML.Excel;
using EmprestimosLivos.Data;
using EmprestimosLivos.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;

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
                emprestimos.DataEmprestimo = DateTime.Now;

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
                var emprestimosdb = _db.tb_Emprestimo.Find(emprestimo.Id);

                emprestimosdb.Recebedor = emprestimo.Recebedor;
                emprestimosdb.Fornecedor = emprestimo.Fornecedor;
                emprestimosdb.LivroEmprestado = emprestimo.LivroEmprestado;


                _db.tb_Emprestimo.Update(emprestimosdb);
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
        //-----------------------------------------------------------------------------------------
        [HttpGet] // Exportar-------------------------------------------------------------------
        public IActionResult Exportar()
        {
            var dados = GetDados();
            using (XLWorkbook workbook = new XLWorkbook())
            {
                workbook.AddWorksheet(dados, "Dados empréstimos");
                using (MemoryStream ms = new MemoryStream())
                {
                    workbook.SaveAs(ms);
                    return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spredsheetml.sheet", "Relatório Emprestimos.xls");
                }
            }            
        }
        private DataTable GetDados() // instalar pacote do nugget ClosedXML
        {
            DataTable datatable = new DataTable();

            datatable.TableName = "Dados Empréstimos";
            datatable.Columns.Add("Recebedor", typeof(string));
            datatable.Columns.Add("Fornecedor", typeof(string));
            datatable.Columns.Add("Livro Emprestado", typeof(string));
            datatable.Columns.Add("Data do Empréstimo", typeof(DateTime));

            var dados = _db.tb_Emprestimo.ToList();

            if (dados.Count > 0)
            {
                dados.ForEach(emprestimo =>
                {
                    datatable.Rows.Add(emprestimo.Recebedor, emprestimo.Fornecedor, emprestimo.LivroEmprestado, emprestimo.DataEmprestimo);
                });
            }
            return datatable;
        }

        //-----------------------------------------------------------------------------------------
    }
}
