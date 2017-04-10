using PagedList;
using System.Collections.Generic;

namespace Service.Services
{
    public interface IRepository<T> where T: IEntity
    {
        IPagedList<T> IndexList(string sortOrder, string searchString, int pagenumber, int pageSize);
        void Add(T enity);
        List<T> List { get; }
        T FindById(int? Id);
        void Edit(T entity);
        void Delete(int? id);
    }
}
