using System.Data.Entity;
using System.Threading.Tasks;
using System.Linq;
using System.Web.Http;
using PWS_Lab3.Context;
using PWS_Lab3.Models;

namespace PWS_Lab3.Controllers
{
    public class PwsController : ApiController
    {
        private StudentContext _repository = new StudentContext();

        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            var students = await _repository.Students.ToListAsync();
            return Ok(students);
        }

        [HttpPost]
        public async Task<IHttpActionResult> Post([FromBody] Student student)
        {
            var createdStudent = _repository.Students.Add(student);
            await _repository.SaveChangesAsync();
            return Ok(createdStudent);
        }

        [HttpPut]
        public async Task<IHttpActionResult> Put([FromBody] Student student)
        {
            var studentToUpdate = await _repository.Students.Where(s => s.Id == student.Id).SingleOrDefaultAsync();
            
            studentToUpdate.Name = student.Name;
            studentToUpdate.Phone = student.Phone;
            
            await _repository.SaveChangesAsync();
            return Ok(studentToUpdate);
        }

        [HttpDelete]
        public async Task <IHttpActionResult> Delete([FromUri] int id)
        {
            var studentToDelete = await _repository.Students.Where(s => s.Id == id).SingleOrDefaultAsync();
            
            var deletedStudent = _repository.Students.Remove(studentToDelete);
            await _repository.SaveChangesAsync();
            
            return Ok(deletedStudent);
        }
    }
}