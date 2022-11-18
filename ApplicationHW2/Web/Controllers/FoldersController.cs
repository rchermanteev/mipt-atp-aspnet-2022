using System.Reflection.Emit;
using BD.Entities;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace WebApplication1.Controllers;

[ApiController]
[Route("[controller]")]
public class FoldersController : ControllerBase
{
    private readonly ILogger<FoldersController> _logger;
    private readonly IFolderService _folderService;

    public FoldersController(ILogger<FoldersController> logger, IFolderService folderService)
    {
        _logger = logger;
        _folderService = folderService;
    }

    [HttpGet]
    public IEnumerable<ValidFolder> Get()
    {
        return _folderService.Get();
    }

    [HttpPost]
    public ValidFolder Post([FromBody] Folder folder)
    {
        return _folderService.Post(folder);
    }

    [HttpDelete]
    public ValidFolder Delete([FromBody] long id)
    {
        return _folderService.Delete(id);
    }

    [HttpPut]
    public ValidFolder Put(Folder folder)
    {
        return _folderService.Put(folder);
    }

    [HttpGet]
    [Route("/roots")]
    public IEnumerable<ValidFolder> GetRoots()
    {
        return _folderService.GetRoots();
    }

    [HttpGet]
    [Route("/nested")]
    public IEnumerable<ValidFolder> GetNestedFolders(long id)
    {
        return _folderService.GetNestedFolders(id);
    }
}