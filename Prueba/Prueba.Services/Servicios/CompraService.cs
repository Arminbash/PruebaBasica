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
    public class CompraService : IService<Compra>
    {
        public Compra obtenerXId(int id)
        {
            using (PruebaDbContext context = new PruebaDbContext())
            {
                try
                {
                    var _entity = context.Compra.FirstOrDefault(x => x.IdCompra == id);
                    if (_entity == null)
                    {
                        Console.WriteLine("No existe el Kardex");
                        return new Compra();
                    }
                    return _entity;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return new Compra();
                }
            }
        }

        public ICollection<Compra> obtenerTodos()
        {
            using (PruebaDbContext context = new PruebaDbContext())
            {
                try
                {
                    var _entitys = context.Compra.ToList();
                    if (_entitys == null)
                    {
                        Console.WriteLine("No existen Compra");
                        return new List<Compra>();
                    }
                    return _entitys;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return new List<Compra>();
                }
            }
        }

        public bool guardar(Compra entity)
        {
            using (PruebaDbContext context = new PruebaDbContext())
            {
                try
                {
                    context.Compra.Add(entity);
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

        public bool editar(Compra entity)
        {
            using (PruebaDbContext context = new PruebaDbContext())
            {
                try
                {
                    var _entity = context.Compra.FirstOrDefault(x => x.IdCompra == entity.IdCompra);
                    if (_entity == null)
                    {
                        Console.WriteLine("No existe el Compra");
                        return false;
                    }
                    var config = new MapperConfiguration(x => x.CreateMap<Compra, Compra>());

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
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var _entity = context.Compra.FirstOrDefault(x => x.IdCompra == id);
                        if (_entity == null)
                        {
                            Console.WriteLine("No existe el Compra");
                            return false;
                        }
                        context.Compra.Remove(_entity);

                        foreach (var item in context.Kardex.Where(x => x.IdDocumento == id && x.Documento == "Compra").ToList())
                        {
                            context.Kardex.Remove(item);
                        }

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
    }
}
