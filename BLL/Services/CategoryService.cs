using DAL;
using DAL.Entities;
using BLL.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;

        public CategoryService(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Category> GetAll()
        {
            return _context.Categories.ToList();
        }
    }
}
