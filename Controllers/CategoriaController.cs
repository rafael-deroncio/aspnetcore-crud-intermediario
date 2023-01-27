using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRUDintermediario.Models;
using CRUDintermediario.Service.Sqlite;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRUDintermediario.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly CrudEstoqueContext _context;

        public CategoriaController(CrudEstoqueContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Subtitulo = "Listagem de Categorias";

            List<CategoriaModel> categorias = await _context.Categorias
                .OrderBy(x => x.ID)
                .AsNoTracking()
                .ToListAsync();

            return View(categorias);
        }

        [HttpGet]
        public async Task<IActionResult> Cadastrar(int? id)
        {
            if (id.HasValue)
            {
                ViewBag.Subtitulo = "Listagem de Categorias";
                CategoriaModel categoriaModel = await _context.Categorias.FindAsync(id);

                if (categoriaModel != null)
                {
                    return View(categoriaModel);
                }
                return NotFound();
            }
            return View(new CategoriaModel());
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(int? id, [FromForm] CategoriaModel categoria)
        {
            // Modelo Preenchido
            if (ModelState.IsValid)
            {
                // ID in route? Edit
                if (id.HasValue && id.Value != 0)
                {
                    var categoriaModel = _context.Categorias.Where(x => x.ID == id.Value).First();
                    if (categoriaModel != null)
                    {
                        categoriaModel.Nome = categoria.Nome;
                        categoriaModel.Ativo = categoria.Ativo;
                        categoriaModel.DataUltimaAlteracao = DateTime.Now.ToString();

                        _context.Update(categoriaModel);
                    }
                }

                // ID not in route? Create
                else
                {
                    categoria.DataCadastro = DateTime.Now.ToString();
                    categoria.DataUltimaAlteracao = DateTime.Now.ToString();
                    categoria.Ativo = true;
                    await _context.AddAsync(categoria);    
                }
                
                TempData["salvou"] = (await _context.SaveChangesAsync() > 0);
                return RedirectToAction("Index");
            }
            // Modelo Nao Preenchido
            else
            {
                return View(categoria);
            }

        }

        [HttpGet]
        public async Task<IActionResult> Excluir(int? id)
        {
            CategoriaModel categoriaModel = await _context.Categorias.FindAsync(id.Value);

            if (!id.HasValue | categoriaModel == null)
            {
                TempData["existe"] = false;
                return RedirectToAction("Index");
            }
            
            return View(categoriaModel);
        }

        [HttpPost]
        public async Task<IActionResult> Excluir(CategoriaModel categoria)
        {
            CategoriaModel categoriaModel = await _context.Categorias.FindAsync(categoria.ID);

            if (categoriaModel != null)
            {
                _context.Remove(categoriaModel);
                 
                TempData["excluiu"] = (await _context.SaveChangesAsync() > 0) ? true : false;
            }
            else
            {
                TempData["existe"] = false;
            }
            
            return RedirectToAction("Index");
        }
    }
}   