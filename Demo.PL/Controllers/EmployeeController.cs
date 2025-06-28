using AutoMapper;
using Demo.BLL.Interfaces;
using Demo.DAL.Entities;
using Demo.PL.Helper;
using Demo.PL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Demo.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(IUnitOfWork unitOfWork,
                ILogger<EmployeeController> logger,
                IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        public IActionResult Index(string SearchValue = "")
        {

            IEnumerable<EmployeeViewModel> employeeViewModels;
            IEnumerable<Employee> employees;

            if (string.IsNullOrEmpty(SearchValue))
            {
                employees = _unitOfWork.EmployeeRepository.GetAll();
                employeeViewModels = _mapper.Map<IEnumerable<EmployeeViewModel>>(employees);
            }

            else
            {
                employees = _unitOfWork.EmployeeRepository.Search(SearchValue);
                employeeViewModels = _mapper.Map<IEnumerable<EmployeeViewModel>>(employees);
            }

            return View(employeeViewModels);
        }

        public IActionResult Create()
        {
            ViewBag.Departments = _unitOfWork.DepartmentRepository.GetAll();
            return View(new EmployeeViewModel());
        }
        [HttpPost]

        public IActionResult Create(EmployeeViewModel employeeViewModel)
        {

            ModelState["Department"].ValidationState = ModelValidationState.Valid;
            if (ModelState.IsValid)
            {
                var employee = _mapper.Map<Employee>(employeeViewModel);
                employee.ImageUrl = DocumentHelper.UploadFile(employeeViewModel.Image, "Images");
                _unitOfWork.EmployeeRepository.Add(employee);
                _unitOfWork.Complete();
                TempData["MessageTemp"] = "Employee Added Successfully";
                return RedirectToAction("Index");
            }
            ViewBag.Departments = _unitOfWork.DepartmentRepository.GetAll();
            return View(employeeViewModel);
        }


        public IActionResult Details(int? id)
        {
            try
            {
                if (id is null)
                {
                    return BadRequest();
                }
                var employee = _unitOfWork.EmployeeRepository.GetById(id);
                if (employee == null)
                    return NotFound();
                var employeeViewModel = _mapper.Map<EmployeeViewModel>(employee);
                return View(employeeViewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return RedirectToAction("Error", "Home");
            }
        }
        public IActionResult Update(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var employee = _unitOfWork.EmployeeRepository.GetById(id);
            if (employee == null)
            {
                return NotFound();
            }

            var employeeViewModel = _mapper.Map<EmployeeViewModel>(employee);


            ViewBag.Departments = _unitOfWork.DepartmentRepository.GetAll();
            return View(employeeViewModel);
        }
        [HttpPost]

        public IActionResult Update(int id, EmployeeViewModel employeeViewModel)
        {

            var employee = _mapper.Map<Employee>(employeeViewModel);
            if (id != employee.Id)
                return NotFound();

            try
            {
                ModelState["Department"].ValidationState = ModelValidationState.Valid;
                if (ModelState.IsValid)
                {
                    employee.ImageUrl = DocumentHelper.UploadFile(employeeViewModel.Image, "Images");
                    _unitOfWork.EmployeeRepository.Update(employee);
                    _unitOfWork.Complete();
                    TempData["MessageTempUpdated"] = "Employee Updated Successfully";
                    ViewBag.Departments = _unitOfWork.DepartmentRepository.GetAll();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return View(employeeViewModel);
        }
        public IActionResult Delete(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }
            var employee = _unitOfWork.EmployeeRepository.GetById(id);
            if (employee is null)
                return NotFound();

            _unitOfWork.EmployeeRepository.Delete(employee);
            _unitOfWork.Complete();

            TempData["MessageTempDeleted"] = "Employee deleted Successfully";

            return RedirectToAction("Index");
        }
    }
}
