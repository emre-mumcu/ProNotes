#nullable disable

namespace ProNotes.AppData.Entities;
public class Note
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public string Caption { get; set; }
    public string Abstract { get; set; }
    public string Content { get; set; }
    public string Tags { get; set; }
}
