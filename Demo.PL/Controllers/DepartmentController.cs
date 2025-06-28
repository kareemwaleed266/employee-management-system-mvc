using Demo.BLL.Interfaces;
using Demo.DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Demo.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private IUnitOfWork _unitOfWork;
        private readonly ILogger<DepartmentController> _logger;

        public DepartmentController(IUnitOfWork unitOfWork,
            ILogger<DepartmentController>logger)
        {
            _unitOfWork = unitOfWork;
            _logger =logger;
        }
        public IActionResult Index()
        {
            var departments = _unitOfWork.DepartmentRepository.GetAll();
            return View(departments);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View(new Department());
        }
        [HttpPost]
        public IActionResult Create(Department department)
        {
            if(ModelState.IsValid)
            {
                _unitOfWork.DepartmentRepository.Add(department);
                _unitOfWork.Complete();
                TempData["MessageTemp"] = "Department Added Successfully";
                return Redirect(nameof(Index));
            }
            return View(department);
        }
        public IActionResult Details(int? id)
        {
            try
            {
                if(id is null)
                {
                    return BadRequest();
                }
                var department = _unitOfWork.DepartmentRepository.GetById(id);
                if (department == null)
                    return NotFound();
                return View(department);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return RedirectToAction("Error","Home");
            }
        }
        public IActionResult Update(int? id)
        {
            if(id is null)
            {
                return NotFound();
            }
            var department = _unitOfWork.DepartmentRepository.GetById(id);
            if (department == null)
                return NotFound();
            return View(department);
        }
        [HttpPost]

        public IActionResult Update(int id,Department department)
        {
            if (id != department.Id)
                return NotFound();
            
            try
            {
                if (ModelState.IsValid)
                {
                    _unitOfWork.DepartmentRepository.Update(department);
                    _unitOfWork.Complete();
                    TempData["MessageTempUpdated"] = "Department Updated Successfully";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
               throw new Exception(ex.Message);
            }
            return View(department);
        }

        public IActionResult Delete(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }
            var department = _unitOfWork.DepartmentRepository.GetById(id);
            if (department is null)
                return NotFound();

            _unitOfWork.DepartmentRepository.Delete(department);
            _unitOfWork.Complete();

            TempData["MessageTempDeleted"] = "Department deleted Successfully";

            return RedirectToAction("Index");
            //return View(department);
        }
    }
}
