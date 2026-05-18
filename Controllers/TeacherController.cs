using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SE_1st_projects.Service.Interface;
using SE_1st_projects.Models;

public class TeacherController : Controller
{
    private readonly ITeacherService _teacherService;
    private readonly IDepartmentService _departmentService;

    public TeacherController(
        ITeacherService teacherService,
        IDepartmentService departmentService)
    {
        _teacherService = teacherService;
        _departmentService = departmentService;
    }

    // INDEX WITH PAGINATION
    public async Task<IActionResult> Index(
        int page = 1,
        int pageSize = 5)
    {
        var result =
            await _teacherService.GetPagedAsync(
                page,
                pageSize
            );

        ViewBag.CurrentPage = page;

        ViewBag.TotalPages =
            result.TotalPages;

        return View(result.Teachers);
    }

    // CREATE
    public async Task<IActionResult> Create()
    {
        var departments =
            await _departmentService.GetAllAsync();

        ViewBag.Departments =
            new SelectList(
                departments,
                "Id",
                "DepartmentName"
            );

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        Teacher teacher)
    {
        await _teacherService.CreateAsync(teacher);

        return RedirectToAction("Index");
    }

    // EDIT
    public async Task<IActionResult> Edit(int id)
    {
        var teacher =
            await _teacherService.GetByIdAsync(id);

        var departments =
            await _departmentService.GetAllAsync();

        ViewBag.Departments =
            new SelectList(
                departments,
                "Id",
                "DepartmentName"
            );

        return View(teacher);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(
        Teacher teacher)
    {
        await _teacherService.UpdateAsync(
            teacher.Id,
            teacher
        );

        return RedirectToAction("Index");
    }

    // DELETE
    public async Task<IActionResult> Delete(int id)
    {
        await _teacherService.DeleteAsync(id);

        return RedirectToAction("Index");
    }
}