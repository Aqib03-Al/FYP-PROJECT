using HTRS.Application.Interfaces;
using HTRS.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HTRS.WebUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ISymptomService _symptomService;
        private readonly IGenericRepository<MedicalTest> _medicalTestRepository;

        public AdminController(
            ISymptomService symptomService,
            IGenericRepository<MedicalTest> medicalTestRepository)
        {
            _symptomService = symptomService;
            _medicalTestRepository = medicalTestRepository;
        }

        // ✅ Admin Dashboard
        public async Task<IActionResult> Index()
        {
            var symptoms = await _symptomService.GetAllSymptomsAsync();
            var tests = await _medicalTestRepository.GetAllAsync();
            ViewBag.TotalSymptoms = symptoms.Count();
            ViewBag.TotalTests = tests.Count();
            return View();
        }

        // ✅ Symptoms List
        public async Task<IActionResult> Symptoms()
        {
            var symptoms = await _symptomService.GetAllSymptomsAsync();
            return View(symptoms);
        }

        // ✅ Add Symptom GET
        public IActionResult AddSymptom()
        {
            return View();
        }

        // ✅ Add Symptom POST
        [HttpPost]
        public async Task<IActionResult> AddSymptom(Symptom model)
        {
            await _symptomService.AddSymptomAsync(model);
            return RedirectToAction("Symptoms");
        }

        // ✅ Delete Symptom
        public async Task<IActionResult> DeleteSymptom(int id)
        {
            await _symptomService.DeleteSymptomAsync(id);
            return RedirectToAction("Symptoms");
        }

        // ✅ Medical Tests List
        public async Task<IActionResult> MedicalTests()
        {
            var tests = await _medicalTestRepository.GetAllAsync();
            return View(tests);
        }

        // ✅ Add Medical Test GET
        public IActionResult AddMedicalTest()
        {
            return View();
        }

        // ✅ Add Medical Test POST
        [HttpPost]
        public async Task<IActionResult> AddMedicalTest(MedicalTest model)
        {
            await _medicalTestRepository.AddAsync(model);
            return RedirectToAction("MedicalTests");
        }

        // ✅ Delete Medical Test
        public async Task<IActionResult> DeleteMedicalTest(int id)
        {
            await _medicalTestRepository.DeleteAsync(id);
            return RedirectToAction("MedicalTests");
        }
    }
}