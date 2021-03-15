using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Services
{
    public interface IService<T> where T : class
    {
        T obtenerXId(int id);
        ICollection<T> obtenerTodos();
        bool guardar(T entity);
        bool editar(T entity);
        bool eliminar(int id);

    }
}
