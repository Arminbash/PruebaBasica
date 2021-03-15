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
    public class KardexService : IService<Kardex>
    {
        public Kardex obtenerXId(int id)
        {
            using (PruebaDbContext context = new PruebaDbContext())
            {
                try
                {
                    var _entity = context.Kardex.Include("Producto").FirstOrDefault(x => x.IdKardex == id);
                    if (_entity == null)
                    {
                        Console.WriteLine("No existe el Kardex");
                        return new Kardex();
                    }
                    return _entity;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return new Kardex();
                }
            }
        }

        public ICollection<Kardex> obtenerTodos()
        {
            using (PruebaDbContext context = new PruebaDbContext())
            {
                try
                {
                    var _entitys = context.Kardex.Include("Producto").ToList();
                    if (_entitys == null)
                    {
                        Console.WriteLine("No existen Kardex");
                        return new List<Kardex>();
                    }
                    return _entitys;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return new List<Kardex>();
                }
            }
        }

        public bool guardar(Kardex entity)
        {
            using (PruebaDbContext context = new PruebaDbContext())
            {
                try
                {
                    context.Kardex.Add(entity);
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

        public bool editar(Kardex entity)
        {
            using (PruebaDbContext context = new PruebaDbContext())
            {
                try
                {
                    var _entity = context.Kardex.FirstOrDefault(x => x.IdKardex == entity.IdKardex);
                    if (_entity == null)
                    {
                        Console.WriteLine("No existe el Kardex");
                        return false;
                    }
                    var config = new MapperConfiguration(x => x.CreateMap<Kardex, Kardex>());

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
                    var _entity = context.Kardex.FirstOrDefault(x => x.IdKardex == id);
                    if (_entity == null)
                    {
                        Console.WriteLine("No existe el Kardex");
                        return false;
                    }
                    context.Kardex.Remove(_entity);
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
