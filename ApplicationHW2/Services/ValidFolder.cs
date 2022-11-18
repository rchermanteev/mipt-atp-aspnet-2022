using Exceptions;

namespace Services;

public class ValidFolder
{
    public ValidFolder( long id, string title, long? parentId)
    {
        if (parentId == id)
        {
            throw new UserFriendlyException("Папка не может быть сама себе родительской");
        }

        ParentId = parentId;
        Title = title;
        Id = id;
    }

    public long Id { get; }
    public string Title { get; }
    public long? ParentId { get; }
        
}