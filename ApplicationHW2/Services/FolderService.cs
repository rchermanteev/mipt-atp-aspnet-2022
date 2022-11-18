using BD.Entities;
using Exceptions;

namespace Services;

internal class FolderService : IFolderService
{    
    // private readonly List<ValidFolder> _folders = new();
    private readonly AspNetContext _dbContext;

    public FolderService(AspNetContext dBContext)
    {
        _dbContext = dBContext;
    }

    public IEnumerable<ValidFolder> Get()
    {
        List<ValidFolder> validFolders = new List<ValidFolder>();
        
        foreach (var folder in _dbContext.Folder.AsQueryable().ToList())
        {
            validFolders.Add(new ValidFolder(folder.Id, folder.Title, folder.ParentId));
        }
        return validFolders;
    }

    public ValidFolder Post(Folder folder)
    {
        var validFolder = new ValidFolder(folder.Id, folder.Title, folder.ParentId);

        var foldersOnOneLevel = _dbContext.Folder.AsQueryable().Where(item => item.ParentId == validFolder.ParentId).ToList();
        if (foldersOnOneLevel.Any(item => item.Title == folder.Title))
        {
            throw new UserFriendlyException($"Папка с названием {folder.Title} уже существует на этом уровне");
        }

        _dbContext.Folder.Add(new Folder(validFolder.Id, validFolder.Title, validFolder.ParentId));
        _dbContext.SaveChanges();
        
        return validFolder;
    }

    public ValidFolder Delete(long id)
    {
        var rawRemoveFolder = _dbContext.Folder.AsQueryable().First(folder => folder.Id == id);
        var removeFolder = new ValidFolder(rawRemoveFolder.Id, rawRemoveFolder.Title, rawRemoveFolder.ParentId);
        _dbContext.Folder.Remove(rawRemoveFolder);
        _dbContext.SaveChanges();
        
        return removeFolder;
    }

    public ValidFolder Put(Folder folder)
    {
        var newFolder = new ValidFolder(folder.Id, folder.Title, folder.ParentId);
        var oldFolder = _dbContext.Folder.AsQueryable().First(item => item.Id == folder.Id);

        _dbContext.Folder.Remove(oldFolder);
        _dbContext.Folder.Add(new Folder(newFolder.Id, newFolder.Title, newFolder.ParentId));
        _dbContext.SaveChanges();
        
        return newFolder;
    }
    
    public IEnumerable<ValidFolder> GetRoots()
    {
        List<ValidFolder> validFolders = new List<ValidFolder>();
        foreach (var folder in _dbContext.Folder.AsQueryable().Where(folder => folder.ParentId.Equals(null)).ToList())
        {
            validFolders.Add(new ValidFolder(folder.Id, folder.Title, folder.ParentId));
        }
        
        return validFolders;
    }
    
    private void AddNestedFolderInQueue(Queue<ValidFolder> queue, long id)
    {
        foreach (var folder in _dbContext.Folder.AsQueryable().Where(folder => folder.ParentId == id))
        {
            queue.Enqueue(new ValidFolder(folder.Id, folder.Title, folder.ParentId));
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