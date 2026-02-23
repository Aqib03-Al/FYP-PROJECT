using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AIHealthTestSystem.Controllers
{
    [Authorize(Roles = "Patient")] // Sirf Patient access kar sakay
    public class PatientController : Controller
    {
        public IActionResult Index()
        {
            // Patient Dashboard
            return View();
        }

        public IActionResult History()
        {
            // Past Assessments
            return View();
        }

        public IActionResult Profile()
        {
            // Patient Profile settings
            return View();
        }
    }
}