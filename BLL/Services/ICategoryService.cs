using DAL.Entities;
using System.Collections.Generic;

namespace BLL.Interfaces
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetAll();
    }
}
