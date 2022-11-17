namespace Services;

internal class FolderService : IFolderService
{    
    private readonly List<ValidFolder> _folders = new();

    public IEnumerable<ValidFolder> Get()
    {
        return _folders;
    }

    public ValidFolder Post(Folder folder)
    {
        var validFolder = new ValidFolder(folder.Id, folder.Title, folder.ParentId);

        var foldersOnOneLevel = _folders.Where(folder => folder.ParentId == validFolder.ParentId).ToList();
        if (foldersOnOneLevel.Any(item => item.Title == folder.Title))
        {
            throw new UserFriendlyException($"Папка с названием {folder.Title} уже существует на этом уровне");
        }
        
        _folders.Add(validFolder);
        
        return validFolder;
    }

    public ValidFolder Delete(long id)
    {
        var removeFolder = _folders.First(folder => folder.Id == id);
        _folders.Remove(removeFolder);
        
        return removeFolder;
    }

    public ValidFolder Put(Folder folder)
    {
        var oldFolder = _folders.First(item => item.Id == folder.Id);

        var newFolder = new ValidFolder(folder.Id, folder.Title, folder.ParentId);
        
        _folders.Remove(oldFolder);
        _folders.Add(newFolder);

        return newFolder;
    }
    
    public IEnumerable<ValidFolder> GetRoots()
    {
        return _folders.Where(folder => folder.ParentId is null).ToList();
    }
    
    private void AddNestedFolderInQueue(Queue<ValidFolder> queue, long id)
    {
        foreach (var folder in _folders.Where(folder => folder.ParentId == id))
        {
            queue.Enqueue(folder);
        }
    }
    
    public IEnumerable<ValidFolder> GetNestedFolders(long id)
    {
        var nestedFolders = new List<ValidFolder>();
        var foldersQueue = new Queue<ValidFolder>();
        AddNestedFolderInQueue(foldersQueue, id);
        
        while (foldersQueue.Count > 0)
        {
            var folder = foldersQueue.Dequeue();
            nestedFolders.Add(folder);
            AddNestedFolderInQueue(foldersQueue, folder.Id);
        }
        
        return nestedFolders;
    }
}