#nullable disable

namespace ProNotes.AppData.Entities;
public class Category
{
    public int CategoryId { get; set; }
    public string CategoryText { get; set; }
    public int? ParentCategoryId { get; set; }
    public virtual Category ParentCategory { get; set; }
    public virtual ICollection<Category> ChildCategories { get; set; }
}

// https://www.mikesdotnetting.com/article/255/entity-framework-recipe-hierarchical-data-management