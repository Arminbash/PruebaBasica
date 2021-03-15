using Prueba.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prueba.Infrastructure;
using AutoMapper;

namespace Prueba.Services
{
    public class ProductoService : IService<Producto>
    {
        public Producto obtenerXId(int id)
        {
            using (PruebaDbContext context = new PruebaDbContext())
            {
                try
                {
                    var _entity = context.Producto.FirstOrDefault(x => x.IdProducto == id);
                    if (_entity == null)
                    {
                        Console.WriteLine("No existe el producto");
                        return new Producto();
                    }
                    return _entity;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return new Producto();
                }
            }
        }

        public ICollection<Producto> obtenerTodos()
        {
            using (PruebaDbContext context = new PruebaDbContext())
            {
                try
                {
                    var _entitys = context.Producto.Where(x => x.Estado == true).ToList();
                    if (_entitys == null)
                    {
                        Console.WriteLine("No existen productos");
                        return new List<Producto>();
                    }
                    return _entitys;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return new List<Producto>();
                }
            }
        }

        public bool guardar(Producto entity)
        {
            using (PruebaDbContext context = new PruebaDbContext())
            {
                try
                {
                    context.Producto.Add(entity);
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

        public bool editar(Producto entity)
        {
            using (PruebaDbContext context = new PruebaDbContext())
            {
                try
                {
                    var _entity = context.Producto.FirstOrDefault(x => x.IdProducto == entity.IdProducto);
                    if (_entity == null)
                    {
                        Console.WriteLine("No existe el producto");
                        return false;
                    }
                    var config = new MapperConfiguration(x => x.CreateMap<Producto, Producto>());

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
                    var _entity = context.Producto.FirstOrDefault(x => x.IdProducto == id);
                    if (_entity == null)
                    {
                        Console.WriteLine("No existe el producto");
                        return false;
                    }
                    _entity.Estado = false;
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
