using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Http;
using PWS_Lab3.Context;

namespace PWS_Lab3.Controllers
{
    public class PwsController : ApiController
    {
        private StudentContext _repository = new StudentContext();

        [HttpGet]
        async public Task<IHttpActionResult> Get()
        {
            var students = await _repository.Students.ToListAsync();
            return Ok(students);
        }

        [HttpPost]
        public IHttpActionResult Post()
        {
            return Ok();
        }

        [HttpPut]
        public IHttpActionResult Put()
        {
            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult Delete()
        {
            return Ok();
        }
    }
}