using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AppGestionEMS.Models;

namespace AppGestionEMS.Controllers
{
    [Authorize(Roles = "admin")]
    public class AsignaciónDocentesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AsignaciónDocentes
        public ActionResult Index()
        {
            var asignaciónDocentes = db.AsignaciónDocentes.Include(a => a.Curso).Include(a => a.Grupo).Include(a => a.User);
            return View(asignaciónDocentes.ToList());
        }


        // GET: AsignaciónDocentes/Create
        public ActionResult Create()
        {
            var profesores = from user in db.Users
                             from u_r in user.Roles
                             join rol in db.Roles on u_r.RoleId equals rol.Id
                             where rol.Name == "profesor"
                             select user.Nombre;

            ViewBag.CursoId = new SelectList(db.Cursos, "Id", "actual");
            ViewBag.GrupoId = new SelectList(db.Grupos, "Id", "nombre");
            ViewBag.UserId = new SelectList(db.Users.Where(u => profesores.Contains(u.Nombre)), "Id", "Nombre");
            return View();
        }

        // POST: AsignaciónDocentes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,CursoId,GrupoId,fecha")] AsignaciónDocentes asignaciónDocentes)
        {
            if (ModelState.IsValid)
            {
                db.AsignaciónDocentes.Add(asignaciónDocentes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CursoId = new SelectList(db.Cursos, "Id", "actual", asignaciónDocentes.CursoId);
            ViewBag.GrupoId = new SelectList(db.Grupos, "Id", "nombre", asignaciónDocentes.GrupoId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Nombre", asignaciónDocentes.UserId);
            return View(asignaciónDocentes);
        }

        
        // GET: AsignaciónDocentes/Delete/5
        public ActionResult Delete(int? curso, int? grupo, string user)
        {
            if (curso == null || grupo == null || user == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AsignaciónDocentes asignaciónDocentes = db.AsignaciónDocentes.Find(user, curso, grupo);
            if (asignaciónDocentes == null)
            {
                return HttpNotFound();
            }
            return View(asignaciónDocentes);
        }

        // POST: AsignaciónDocentes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int curso, int grupo, string user)
        {
            AsignaciónDocentes asignaciónDocentes = db.AsignaciónDocentes.Find(user, curso, grupo);
            db.AsignaciónDocentes.Remove(asignaciónDocentes);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
