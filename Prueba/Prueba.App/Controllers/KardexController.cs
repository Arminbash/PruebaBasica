using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Prueba.Domain.Model;
using Prueba.Infrastructure;
using Prueba.Services;

namespace Prueba.App.Controllers
{
    public class KardexController : Controller
    {
        private KardexService kardexService = new KardexService();
        private ProductoService ProductoService = new ProductoService();

        // GET: Kardex
        public ActionResult Index()
        {
            var kardex = kardexService.obtenerTodos();
            return View(kardex.ToList());
        }

        // GET: Kardex/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kardex kardex = kardexService.obtenerXId((int)id);
            if (kardex == null)
            {
                return HttpNotFound();
            }
            return View(kardex);
        }

        // GET: Kardex/Create
        public ActionResult Create()
        {
            ViewBag.IdProducto = new SelectList(ProductoService.obtenerTodos(), "IdProducto", "Codigo");
            return View();
        }

        // POST: Kardex/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdKardex,IdProducto,CantidadEntrada,CantidadSalida,Costo,IdDocumento,Documento")] Kardex kardex)
        {
            if (ModelState.IsValid)
            {
                kardexService.guardar(kardex);
                return RedirectToAction("Index");
            }

            ViewBag.IdProducto = new SelectList(ProductoService.obtenerTodos(), "IdProducto", "Codigo", kardex.IdProducto);
            return View(kardex);
        }

        // GET: Kardex/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kardex kardex = kardexService.obtenerXId((int)id);
            if (kardex == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdProducto = new SelectList(ProductoService.obtenerTodos(), "IdProducto", "Codigo", kardex.IdProducto);
            return View(kardex);
        }

        // POST: Kardex/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdKardex,IdProducto,CantidadEntrada,CantidadSalida,Costo,IdDocumento,Documento")] Kardex kardex)
        {
            if (ModelState.IsValid)
            {
                kardexService.editar(kardex);
                return RedirectToAction("Index");
            }
            ViewBag.IdProducto = new SelectList(ProductoService.obtenerTodos(), "IdProducto", "Codigo", kardex.IdProducto);
            return View(kardex);
        }

        // GET: Kardex/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kardex kardex = kardexService.obtenerXId((int)id);
            if (kardex == null)
            {
                return HttpNotFound();
            }
            return View(kardex);
        }

        // POST: Kardex/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            kardexService.eliminar(id);
            return RedirectToAction("Index");
        }

    }
}
