using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProNotes.ViewModels
{
    public class CategoryUpdateVM
    {
        public int? CategoryId { get; set; }

        public string CategoryName { get; set; } = null!;

        public List<SelectListItem> Categories { get; set; } = null!;

        public int? ParentCategoryId { get; set; } = null!;

        public List<SelectListItem> ParentCategories { get; set; } = null!;
    }
}
