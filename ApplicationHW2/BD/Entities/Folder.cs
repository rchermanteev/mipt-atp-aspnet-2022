using System.ComponentModel.DataAnnotations;

namespace BD.Entities;

public class Folder
{
    public Folder(long id, string title, long? parentId)
    {
        Id = id;
        Title = title;
        ParentId = parentId;
    }

    [Required]
    public long Id { get; set; }
    [Required]
    public string Title { get; set; }
    public long? ParentId { get; set; }

}
