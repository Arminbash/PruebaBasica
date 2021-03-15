using AutoMapper;
using Prueba.Domain.Model;
using Prueba.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Services
{
    public class DetalleCompraService : IService<CompraDetalle>
    {
        public CompraDetalle obtenerXId(int id)
        {
            using (PruebaDbContext context = new PruebaDbContext())
            {
                try
                {
                    var _entity = context.CompraDetalle.Include("Compra").Include("producto").FirstOrDefault(x => x.IdCompraDetalle == id);
                    if (_entity == null)
                    {
                        Console.WriteLine("No existe el detalle");
                        return new CompraDetalle();
                    }
                    return _entity;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return new CompraDetalle();
                }
            }
        }

        public ICollection<CompraDetalle> obtenerTodos()
        {
            using (PruebaDbContext context = new PruebaDbContext())
            {
                try
                {
                    var _entitys = context.CompraDetalle.Include("Compra").Include("producto").ToList();
                    if (_entitys == null)
                    {
                        Console.WriteLine("No existen detalles");
                        return new List<CompraDetalle>();
                    }
                    return _entitys;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return new List<CompraDetalle>();
                }
            }
        }

        public bool guardar(CompraDetalle entity)
        {
            using (PruebaDbContext context = new PruebaDbContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        context.CompraDetalle.Add(entity);
                        Kardex kardex = new Kardex();
                        kardex.IdDocumento = entity.IdCompra;
                        kardex.IdProducto = entity.IdProducto;
                        kardex.Documento = "Compra";
                        kardex.CantidadEntrada = entity.Cantidad;
                        kardex.Costo = entity.Precio;
                        kardex.CantidadSalida = 0;
                        context.Kardex.Add(kardex);
                        if (context.SaveChanges() > 0)
                        {
                            transaction.Commit();
                            return true;
                        }
                        transaction.Rollback();
                        return false;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                        transaction.Rollback();
                        return false;
                    }
                }                 
            }
        }

        public bool editar(CompraDetalle entity)
        {
            using (PruebaDbContext context = new PruebaDbContext())
            {
                try
                {
                    var _entity = context.CompraDetalle.FirstOrDefault(x => x.IdCompraDetalle == entity.IdCompraDetalle);
                    if (_entity == null)
                    {
                        Console.WriteLine("No existe el detalle");
                        return false;
                    }
                    var config = new MapperConfiguration(x => x.CreateMap<CompraDetalle, CompraDetalle>());

                    IMapper mapper = config.CreateMapper();
                    mapper.Map(entity, _entity);

                    if (context.SaveChanges() > 0)
                        return true;
                    return false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return false;
                }
            }
        }

        public bool eliminar(int id)
        {
            using (PruebaDbContext context = new PruebaDbContext())
            {
                try
                {
                    var _entity = context.CompraDetalle.FirstOrDefault(x => x.IdCompraDetalle == id);
                    if (_entity == null)
                    {
                        Console.WriteLine("No existe el detalle");
                        return false;
                    }
                    context.CompraDetalle.Remove(_entity);
                    if (context.SaveChanges() > 0)
                        return true;
                    return false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return false;
                }
            }
        }
    }
}
