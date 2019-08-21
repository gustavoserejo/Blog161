using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Models;
using WebApplication3.ViewModel;

namespace WebApplication3.Controllers
{
    public class MensagemController : Controller
    {
        private readonly WebApplication3Context _context;

        public MensagemController(WebApplication3Context context)
        {
            _context = context;
        }

        // GET: Mensagem
        public async Task<IActionResult> Index()
        {
            var blogContext = _context.Mensagem.Include(m => m.Categorias);
            var blogContext1 = _context.Comentario.Include(c => c.Mensagem);

            var vm = new VmMensagemComentario
            {
                Mensagems = await blogContext.ToListAsync(),
                Comentarios = await blogContext1.ToListAsync()
            };

            return View(vm);
        }

        // GET: Mensagem/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mensagem = await _context.Mensagem
                .Include(m => m.Categorias)
                .Include(m => m.Comentarios)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mensagem == null)
            {
                return NotFound();
            }

            return View(mensagem);
        }

        // GET: Mensagem/Create
        public IActionResult Create()
        {
            ViewData["CategoriaId"] = new SelectList(_context.Categoria, "CategoriaId", "Descriacao");
            ViewData["ComentarioId"] = new SelectList(_context.Comentario, "ComentarioId", "Descricao");
            return View();
        }

        // POST: Mensagem/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titulo,Descricao,Data,CategoriaId,ComentarioId")] Mensagem mensagem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mensagem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categoria, "CategoriaId", "Descriacao", mensagem.CategoriaId);
            ViewData["ComentarioId"] = new SelectList(_context.Comentario, "ComentarioId", "Descricao", mensagem.ComentarioId);
            return View(mensagem);
        }

        // GET: Mensagem/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mensagem = await _context.Mensagem.FindAsync(id);
            if (mensagem == null)
            {
                return NotFound();
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categoria, "CategoriaId", "Descriacao", mensagem.CategoriaId);
            ViewData["ComentarioId"] = new SelectList(_context.Comentario, "ComentarioId", "Descricao", mensagem.ComentarioId);
            return View(mensagem);
        }

        // POST: Mensagem/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,Descricao,Data,CategoriaId,ComentarioId")] Mensagem mensagem)
        {
            if (id != mensagem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mensagem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MensagemExists(mensagem.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categoria, "CategoriaId", "Descriacao", mensagem.CategoriaId);
            ViewData["ComentarioId"] = new SelectList(_context.Comentario, "ComentarioId", "Descricao", mensagem.ComentarioId);
            return View(mensagem);
        }

        // GET: Mensagem/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mensagem = await _context.Mensagem
                .Include(m => m.Categorias)
                .Include(m => m.Comentarios)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mensagem == null)
            {
                return NotFound();
            }

            return View(mensagem);
        }

        // POST: Mensagem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mensagem = await _context.Mensagem.FindAsync(id);
            _context.Mensagem.Remove(mensagem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MensagemExists(int id)
        {
            return _context.Mensagem.Any(e => e.Id == id);
        }
    }
}
