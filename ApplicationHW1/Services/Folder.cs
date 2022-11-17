namespace Services;

public class Folder
{
    public Folder(long id, string title, long? parentId)
    {
        Id = id;
        Title = title;
        ParentId = parentId;
    }

    public long Id { get; set; }
    public string Title { get; set; }
    public long? ParentId { get; set; }

}
