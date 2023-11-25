using Microsoft.AspNetCore.Mvc.Rendering;
using ProNotes.AppData.Entities;

namespace ProNotes.ViewModels
{
    public class NoteViewModel: Note
    {     
        public List<SelectListItem>? Categories { get; set; } = null!;
        public int SubcategoryId { get; set; }
        public List<SelectListItem>? Subcategories { get; set; } = null!;
    }
}
