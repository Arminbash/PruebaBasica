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
    public class CompraDetallesController : Controller
    {
        private DetalleCompraService detalleCompraService = new DetalleCompraService();
        private CompraService compraService = new CompraService();
        private ProductoService productoService = new ProductoService();
        // GET: CompraDetalles
        public ActionResult Index()
        {
            var compraDetalle = detalleCompraService.obtenerTodos();
            return View(compraDetalle.ToList());
        }

        // GET: CompraDetalles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompraDetalle compraDetalle = detalleCompraService.obtenerXId((int)id);
            if (compraDetalle == null)
            {
                return HttpNotFound();
            }
            return View(compraDetalle);
        }

        // GET: CompraDetalles/Create
        public ActionResult Create()
        {
            ViewBag.IdCompra = new SelectList(compraService.obtenerTodos(), "IdCompra", "Factura");
            ViewBag.IdProducto = new SelectList(productoService.obtenerTodos(), "IdProducto", "Codigo");
            return View();
        }

        // POST: CompraDetalles/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdCompraDetalle,IdCompra,IdProducto,Cantidad,Precio,Estado")] CompraDetalle compraDetalle)
        {
            if (ModelState.IsValid)
            {
                detalleCompraService.guardar(compraDetalle);
                return RedirectToAction("Index");
            }

            ViewBag.IdCompra = new SelectList(compraService.obtenerTodos(), "IdCompra", "Factura", compraDetalle.IdCompra);
            ViewBag.IdProducto = new SelectList(productoService.obtenerTodos(), "IdProducto", "Codigo", compraDetalle.IdProducto);
            return View(compraDetalle);
        }

        // GET: CompraDetalles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompraDetalle compraDetalle = detalleCompraService.obtenerXId((int)id);
            if (compraDetalle == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdCompra = new SelectList(compraService.obtenerTodos(), "IdCompra", "Factura", compraDetalle.IdCompra);
            ViewBag.IdProducto = new SelectList(productoService.obtenerTodos(), "IdProducto", "Codigo", compraDetalle.IdProducto);
            return View(compraDetalle);
        }

        // POST: CompraDetalles/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdCompraDetalle,IdCompra,IdProducto,Cantidad,Precio,Estado")] CompraDetalle compraDetalle)
        {
            if (ModelState.IsValid)
            {
                detalleCompraService.editar(compraDetalle);
                return RedirectToAction("Index");
            }
            ViewBag.IdCompra = new SelectList(compraService.obtenerTodos(), "IdCompra", "Factura", compraDetalle.IdCompra);
            ViewBag.IdProducto = new SelectList(productoService.obtenerTodos(), "IdProducto", "Codigo", compraDetalle.IdProducto);
            return View(compraDetalle);
        }

        // GET: CompraDetalles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompraDetalle compraDetalle = detalleCompraService.obtenerXId((int)id);
            if (compraDetalle == null)
            {
                return HttpNotFound();
            }
            return View(compraDetalle);
        }

        // POST: CompraDetalles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            detalleCompraService.eliminar(id);
            return RedirectToAction("Index");
        }

    }
}
