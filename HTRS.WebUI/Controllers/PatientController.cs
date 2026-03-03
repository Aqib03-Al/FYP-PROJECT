using HTRS.Application.Interfaces;
using HTRS.Domain.Entities;
using HTRS.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HTRS.WebUI.Controllers
{
    [Authorize(Roles = "Patient")]
    public class PatientController : Controller
    {
        private readonly IPatientService _patientService;
        private readonly ISymptomService _symptomService;
        private readonly IRecommendationService _recommendationService;
        private readonly UserManager<ApplicationUser> _userManager;

        public PatientController(
            IPatientService patientService,
            ISymptomService symptomService,
            IRecommendationService recommendationService,
            UserManager<ApplicationUser> userManager)
        {
            _patientService = patientService;
            _symptomService = symptomService;
            _recommendationService = recommendationService;
            _userManager = userManager;
        }

        // ✅ Patient Dashboard
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var patient = await _patientService.GetPatientByUserIdAsync(user.Id);

            if (patient == null)
                return RedirectToAction("CreateProfile");

            var sessions = await _recommendationService.GetPatientSessionsAsync(patient.Id);
            ViewBag.PatientName = patient.FullName;
            ViewBag.Sessions = sessions;
            return View();
        }

        // ✅ Create Profile GET
        public IActionResult CreateProfile()
        {
            return View();
        }

        // ✅ Create Profile POST
        [HttpPost]
        public async Task<IActionResult> CreateProfile(PatientProfile model)
        {
            var user = await _userManager.GetUserAsync(User);
            model.UserId = user.Id;
            await _patientService.CreatePatientProfileAsync(model);
            return RedirectToAction("Index");
        }

        // ✅ Symptoms Page GET
        public async Task<IActionResult> Symptoms()
        {
            var symptoms = await _symptomService.GetAllSymptomsAsync();
            return View(symptoms);
        }

        // ✅ Submit Symptoms POST
        [HttpPost]
        public async Task<IActionResult> SubmitSymptoms(
            List<int> selectedSymptomIds,
            IFormCollection form)
        {
            var user = await _userManager.GetUserAsync(User);
            var patient = await _patientService.GetPatientByUserIdAsync(user.Id);

            // Session banao
            var session = await _recommendationService.CreateSessionAsync(patient.Id);

            // Symptoms ka JSON banao
            var symptomsList = new List<object>();
            foreach (var id in selectedSymptomIds)
            {
                var duration = form[$"durations_{id}"].ToString();
                var intensity = form[$"intensities_{id}"].ToString();
                symptomsList.Add(new
                {
                    symptomId = id,
                    duration = duration,
                    intensity = intensity
                });
            }

            var symptomsJson = System.Text.Json.JsonSerializer.Serialize(symptomsList);

            // AI ko bhejo
            await _recommendationService.ProcessRecommendationsAsync(session.Id, symptomsJson);

            return RedirectToAction("SessionDetail", new { id = session.Id });
        }

        // ✅ Session Detail
        public async Task<IActionResult> SessionDetail(int id)
        {
            var recommendations = await _recommendationService.GetRecommendationsAsync(id);
            var session = await _recommendationService.GetSessionByIdAsync(id);
            ViewBag.Session = session;
            return View(recommendations);
        }
    }
}