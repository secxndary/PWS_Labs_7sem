using System.Data.Entity;
using System.Threading.Tasks;
using System.Linq;
using System.Web.Http;
using PWS_Lab3.Context;
using PWS_Lab3.Models;
using System;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;

namespace PWS_Lab3.Controllers
{
    public class PwsController : ApiController
    {
        private readonly StudentContext _repository = new StudentContext();


        [HttpGet]
        public async Task<HttpResponseMessage> Get([FromUri] string contentType)
        {
            var students = await _repository.Students.ToListAsync();
            var response = Request.CreateResponse(HttpStatusCode.OK, students);

            SetContentTypeHeader(response.Content.Headers, contentType);

            return response;
        }

        [HttpPost]
        public async Task<HttpResponseMessage> Post([FromBody] Student student, [FromUri] string contentType)
        {
            try
            {
                var createdStudent = _repository.Students.Add(student);
                await _repository.SaveChangesAsync();

                var response = Request.CreateResponse(HttpStatusCode.OK, createdStudent);
                SetContentTypeHeader(response.Content.Headers, contentType);

                return response;
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpPut]
        public async Task<HttpResponseMessage> Put([FromBody] Student student, [FromUri] string contentType)
        {
            try
            {
                var studentToUpdate = await _repository.Students.Where(s => s.Id == student.Id).SingleOrDefaultAsync();
            
                studentToUpdate.Name = student.Name;
                studentToUpdate.Phone = student.Phone;
            
                await _repository.SaveChangesAsync();

                var response = Request.CreateResponse(HttpStatusCode.OK, studentToUpdate);
                SetContentTypeHeader(response.Content.Headers, contentType);

                return response;
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpDelete]
        public async Task <HttpResponseMessage> Delete([FromUri] int id, [FromUri] string contentType)
        {
            try
            {
                var studentToDelete = await _repository.Students.Where(s => s.Id == id).SingleOrDefaultAsync();
                var deletedStudent = _repository.Students.Remove(studentToDelete);
                await _repository.SaveChangesAsync();

                var response = Request.CreateResponse(HttpStatusCode.OK, deletedStudent);
                SetContentTypeHeader(response.Content.Headers, contentType);

                return response;
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }


        private void SetContentTypeHeader(HttpContentHeaders contentHeaders, string contentType)
        {
            contentHeaders.ContentType = new MediaTypeHeaderValue(
                (contentType == "xml") ? "text/xml" : "application/json");
        }
    }
}