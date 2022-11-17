namespace Services;

public interface IFolderService
{
    public IEnumerable<ValidFolder> Get();

    public ValidFolder Post(Folder folder);

    public ValidFolder Delete(long id);

    public ValidFolder Put(Folder folder);

    public IEnumerable<ValidFolder> GetRoots();

    public IEnumerable<ValidFolder> GetNestedFolders(long id);
}