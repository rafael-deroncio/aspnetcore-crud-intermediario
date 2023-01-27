using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRUDintermediario.Models;
using CRUDintermediario.Service.Sqlite;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CRUDintermediario.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly CrudEstoqueContext _context;

        public ProdutoController(CrudEstoqueContext context)
        {
            this._context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Subtitulo = "Listagem de Produtos";

            List<ProdutoModel> produtos = await _context.Produtos
                .OrderBy(x => x.ID)
                .AsNoTracking()
                .ToListAsync();

            produtos.ForEach(produto => 
            {
                produto.Categoria = _context.Categorias.Find(produto.IDCategoria);
            });

            return View(produtos);
        }

        [HttpGet]
        public async Task<IActionResult> Cadastrar(int? id)
        {
            ViewBag.Categorias = new SelectList(
                await _context.Categorias.OrderBy(x => x.ID).AsNoTracking().ToListAsync(),
                 nameof(CategoriaModel.ID), nameof(CategoriaModel.Nome));

            if (id.HasValue)
            {
                ViewBag.Subtitulo = "Listagem de Produtos";
                ProdutoModel produtoModel = await _context.Produtos.FindAsync(id);

                if (produtoModel != null)
                {
                    return View(produtoModel);
                }
                return NotFound();
            }
            return View(new ProdutoModel());
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(int? id, [FromForm] ProdutoModel produto)
        {
            // Modelo Preenchido
            if (ModelState.IsValid)
            {
                // ID in route? Edit
                if (id.HasValue && id.Value != 0)
                {
                    ProdutoModel produtoModel = await _context.Produtos.FindAsync(id);

                    if (produtoModel != null)
                    {
                        produtoModel.Nome = produto.Nome;
                        produtoModel.DataUltimaAlteracao = DateTime.Now.ToString();

                        _context.Update(produtoModel);
                    }
                }

                // ID not in route? Create
                else
                {
                    ProdutoModel produtoModel = new();
                    produtoModel.Nome = produto.Nome;
                    produtoModel.Valor = produto.Valor;
                    produtoModel.Quantidade = produto.Quantidade;
                    produtoModel.IDCategoria = produto.IDCategoria;
                    produtoModel.Categoria = await _context.Categorias.FindAsync(produto.IDCategoria);

                    produtoModel.DataCadastro = DateTime.Now.ToString();
                    produtoModel.DataUltimaAlteracao = DateTime.Now.ToString();
                    await _context.AddAsync(produtoModel);
                }
                
                TempData["salvou"] = (await _context.SaveChangesAsync() > 0);
                return RedirectToAction("Index");
            }
            // Modelo Nao Preenchido
            else
            {
                return View(produto);
            }

        }
    
        [HttpGet]
        public async Task<IActionResult> Excluir(int? id)
        {
            ProdutoModel produtoModel = await _context.Produtos.FindAsync(id.Value);

            if (!id.HasValue | produtoModel == null)
            {
                TempData["existe"] = false;
                return RedirectToAction("Index");
            }
            
            return View(produtoModel);
        }

        [HttpPost]
        public async Task<IActionResult> Excluir(ProdutoModel produto)
        {
            ProdutoModel produtoModel = await _context.Produtos.FindAsync(produto.ID);

            if (produtoModel != null)
            {
                _context.Remove(produtoModel);
                 
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