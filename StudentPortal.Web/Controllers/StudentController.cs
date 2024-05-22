using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentPortal.Web.Data;


namespace StudentPortal.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController(AppDbContext context) : ControllerBase
    {
        private readonly AppDbContext _context = context;

        // POST: api/student
        [HttpPost]
        public async Task<ActionResult<Student>> AddStudent(Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Students.Add(student);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetStudent), new { id = student.Id }, student);
            }
            return BadRequest(ModelState);
        }

        // GET: api/student
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            return await _context.Students.ToListAsync();
        }

        // GET: api/student/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return student;
        }

        // PUT: api/student/1
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, Student updatedStudent)
        {
            if (id != updatedStudent.Id)
            {
                return BadRequest();
            }

            _context.Entry(updatedStudent).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Students.Any(s => s.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent(); // Add this return statement
        }

        // DELETE: api/student/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            try
            {
                var student = await _context.Students.FindAsync(id);
                if (student == null)
                {
                    return NotFound();
                }

                _context.Students.Remove(student);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Error deleting student with ID {id}: {ex.Message}");
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

    }
}