using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public interface IRepository<T> where T: IEntity
    {
        List<T> IndexList(string sortOrder, string searchString);
        void Add(T enity);
        List<T> List { get; }
        T FindById(int? Id);
        void Edit(T entity);
        void Delete(int? id);
    }
}
