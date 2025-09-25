using Microsoft.AspNetCore.Mvc;

public class HomeController : Controller
{
    private readonly VideoCatalogueService _catalogueService;

    public HomeController(VideoCatalogueService catalogueService)
    {
        _catalogueService = catalogueService;
    }

    public IActionResult Index()
    {
        var catalogue = _catalogueService.GetCatalogue();
        return View(catalogue);
    }
}
