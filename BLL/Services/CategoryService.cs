using DAL;
using DAL.Entities;
using BLL.Interfaces;
using Common.Logging;
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
            Log.Info("CategoryService created");
        }

        public IEnumerable<Category> GetAll()
        {
            Log.Info("GetAll categories");
            return _context.Categories.ToList();
        }
    }
}
