using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Authorize(Roles = "Admin")]
public class RoutineController : Controller
{
    private readonly MyDBContext _context;
    private readonly IWebHostEnvironment _environment;

    public RoutineController(
        MyDBContext context,
        IWebHostEnvironment environment)
    {
        _context = context;
        _environment = environment;
    }

    // =========================
    // ROUTINE LIST
    // =========================
    public async Task<IActionResult> Index()
    {
        var routines = await _context.Routines.ToListAsync();
        return View(routines);
    }

    // =========================
    // CREATE PAGE
    // =========================
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    // =========================
    // UPLOAD ROUTINE
    // =========================
    [HttpPost]
    public async Task<IActionResult> Create(
        Routine routine,
        IFormFile file)
    {
        if (file != null && file.Length > 0)
        {
            // folder path
            string folderPath = Path.Combine(
                _environment.WebRootPath,
                "uploads",
                "routines"
            );

            // 🔥 FIX: auto create folder if not exists
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            // unique file name
            string fileName =
                Guid.NewGuid().ToString()
                + Path.GetExtension(file.FileName);

            // full path
            string fullPath = Path.Combine(folderPath, fileName);

            // save file
            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // save in DB
            routine.FilePath = "/uploads/routines/" + fileName;
            routine.UploadedAt = DateTime.Now;

            _context.Routines.Add(routine);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        return View(routine);
    }

    // =========================
    // DELETE ROUTINE
    // =========================
    public async Task<IActionResult> Delete(int id)
    {
        var routine = await _context.Routines.FindAsync(id);

        if (routine == null)
            return NotFound();

        // physical file path
        string fullPath = Path.Combine(
            _environment.WebRootPath,
            routine.FilePath.TrimStart('/').Replace("/", "\\")
        );

        // delete file if exists
        if (System.IO.File.Exists(fullPath))
        {
            System.IO.File.Delete(fullPath);
        }

        // delete from DB
        _context.Routines.Remove(routine);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }
}