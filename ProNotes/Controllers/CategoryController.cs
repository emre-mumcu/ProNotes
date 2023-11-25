using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ProNotes.AppData.EFCore.Context;
using ProNotes.AppData.Entities;
using ProNotes.AppLib.MVC.Attributes;
using ProNotes.ViewModels;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ProNotes.Controllers
{
    [CustomAuthorize(AllowedRoles = "Administrator")]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public CategoryController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [MenuItem(_SubmenuDisplayName: "Category List")]
        public IActionResult Index()
        {
            var categories = _appDbContext.Categories.ToList();

            return View(categories);
        }

        [MenuItem(_SubmenuDisplayName: "Update Categories")]
        [HttpGet]
        public IActionResult Update([ModelBinder(Name = "CategoryId")]int catId = 0)
        {
            CategoryUpdateVM model = new CategoryUpdateVM();

            model.ParentCategories = _appDbContext.Categories.Select(c => new SelectListItem { Text = c.CategoryText, Value = c.CategoryId.ToString() }).ToList();
            model.Categories = _appDbContext.Categories.Select(c => new SelectListItem { Text = c.CategoryText, Value = c.CategoryId.ToString() }).ToList();
            
            model.ParentCategories.Insert(0, new SelectListItem { Text = "[--ROOT--]", Value = "0" });

            Category? cat;
            if (catId != 0 && (cat = _appDbContext.Categories.FirstOrDefault(c => c.CategoryId == catId)) != null)
            {   
                model.CategoryName = cat.CategoryText;
                model.ParentCategoryId = cat.ParentCategoryId ?? 0;
                model.CategoryId = cat.CategoryId;
            }
            else
            {
                
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult Update(CategoryUpdateVM model, string Command)
        {
            Category? parent = _appDbContext.Categories.FirstOrDefault(c => c.CategoryId == Convert.ToInt32(model.ParentCategoryId));

            Category? category = _appDbContext.Categories.FirstOrDefault(c => c.CategoryId == Convert.ToInt32(model.CategoryId)) ?? new Category();

            category.ParentCategoryId = parent?.CategoryId ?? null;
            category.CategoryText = model.CategoryName;

            
            if(category.ParentCategoryId == category.CategoryId) 
            {
                category.ParentCategoryId = null;
            }

            var q = _appDbContext.Categories.Update(category);

            
            /*
            var states = new[] { EntityState.Added, EntityState.Modified, EntityState.Deleted };
            var entries = _appDbContext.ChangeTracker.Entries().Where(c => states.Contains(c.State))
                .Select(entry =>
                    (
                        string.Join(",", entry.Metadata.FindPrimaryKey()
                            .Properties.Select(p => p.PropertyInfo.GetValue(entry.Entity))),
                        entry.Metadata.ClrType.Name,
                        entry.State,
                        entry.Properties
                            .Where(p => p.IsModified == (p.EntityEntry.State == EntityState.Modified))
                            .Select(prop =>
                            (
                                prop.Metadata.PropertyInfo.Name,
                                prop.OriginalValue,
                                prop.CurrentValue
                            )
                    )));

            var c1 = (_appDbContext.ChangeTracker.DebugView.LongView);
            _appDbContext.ChangeTracker.DetectChanges();
            var c2 = (_appDbContext.ChangeTracker.DebugView.LongView);
            */

            _appDbContext.SaveChanges();

            model.CategoryId = category.CategoryId;

            model.ParentCategories = _appDbContext.Categories.Select(c => new SelectListItem { Text = c.CategoryText, Value = c.CategoryId.ToString() }).ToList();
            model.ParentCategories.Insert(0, new SelectListItem { Text = "[--ROOT--]", Value = "0" });

            return View(model);
        }
    }
}
