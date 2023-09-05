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
using Newtonsoft.Json;
using System.Xml;
using System.Collections;
using System.Web;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using System.Text;
using System.ComponentModel;

namespace PWS_Lab3.Controllers
{
    public class PwsController : ApiController
    {
        private readonly StudentContext _repository = new StudentContext();


        [HttpGet]
        public async Task<HttpResponseMessage> Get([FromUri] [DefaultValue(null)] string contentType = null)
        {
            try
            {
                var students = await _repository.Students.ToListAsync();
                var response = GetStudentsResponse(students, contentType);
                return response;
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        //[HttpPost]
        //public async Task<IHttpActionResult> Post([FromBody] Student student, [FromUri] string contentType)
        //{
        //    try
        //    {
        //        var createdStudent = _repository.Students.Add(student);
        //        await _repository.SaveChangesAsync();

        //        var response = Request.CreateResponse(HttpStatusCode.OK, createdStudent);
        //        SetResponseContentType(response.Content.Headers, contentType);

        //        return response;
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
        //    }
        //}

        //[HttpPut]
        //public async Task<IHttpActionResult> Put([FromBody] Student student, [FromUri] string contentType)
        //{
        //    try
        //    {
        //        var studentToUpdate = await _repository.Students.Where(s => s.Id == student.Id).SingleOrDefaultAsync();
            
        //        studentToUpdate.Name = student.Name;
        //        studentToUpdate.Phone = student.Phone;
            
        //        await _repository.SaveChangesAsync();

        //        var response = Request.CreateResponse(HttpStatusCode.OK, studentToUpdate);
        //        SetResponseContentType(response.Content.Headers, contentType);

        //        return response;
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
        //    }
        //}

        //[HttpDelete]
        //public async Task <IHttpActionResult> Delete([FromUri] int id, [FromUri] string contentType)
        //{
        //    try
        //    {
        //        var studentToDelete = await _repository.Students.Where(s => s.Id == id).SingleOrDefaultAsync();
        //        var deletedStudent = _repository.Students.Remove(studentToDelete);
        //        await _repository.SaveChangesAsync();

        //        var response = Request.CreateResponse(HttpStatusCode.OK, deletedStudent);
        //        SetResponseContentType(response.Content.Headers, contentType);

        //        return response;
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
        //    }
        //}


        private HttpResponseMessage GetStudentsResponse(IEnumerable<Student> students, string contentType)
        {
            var response = new HttpResponseMessage();

            response.Content = (contentType == "xml") ?
                 new StringContent(ConvertStudentsToXml(students), Encoding.UTF8, "text/xml") :
                 new StringContent(JsonConvert.SerializeObject(students), Encoding.UTF8, "application/json");

            return response;
        }

        private string ConvertStudentsToXml(IEnumerable<Student> students)
        {
            var serializer = new XmlSerializer(typeof(List<Student>));
            using (var writer = new StringWriter())
            {
                serializer.Serialize(writer, students);
                return writer.ToString();
            }
        }
    }
}