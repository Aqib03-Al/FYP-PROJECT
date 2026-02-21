using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize(Roles = "Patient")]
public class PatientController : Controller
{
    public IActionResult Index()
    {
        // Patient Dashboard Logic
        return View();
    }
}