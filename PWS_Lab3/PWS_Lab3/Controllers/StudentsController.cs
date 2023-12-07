using System.Data.Entity;
using System.Threading.Tasks;
using System.Linq;
using System.Web.Http;
using System;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using System.Text;
using PWS_Lab3.Context;
using PWS_Lab3.Entities.Models;
using PWS_Lab3.Entities.Exceptions.NotFound;
using PWS_Lab3.Service;
using PWS_Lab3.Shared.RequestFeatures.UserParameters;

namespace PWS_Lab3.Controllers
{
    // Route: localhost:PORT/api/students
    public class StudentsController : ApiController
    {
        private readonly StudentService _service = new StudentService();

        [HttpGet]
        public async Task<HttpResponseMessage> GetAllStudents([FromUri] StudentParameters studentParameters)
        {
            try
            {
                return await _service.GetStudents(studentParameters);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpPost]
        public async Task<HttpResponseMessage> CreateStudent([FromBody] Student student, [FromUri] StudentParameters studentParameters)
        {
            try
            {
                return await _service.CreateStudent(student, studentParameters);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpPut]
        public async Task<HttpResponseMessage> UpdateStudent([FromBody] Student student, [FromUri] StudentParameters studentParameters)
        {
            try
            {
                return await _service.UpdateStudent(student, studentParameters);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpDelete]
        public async Task<HttpResponseMessage> DeleteStudent([FromUri] int id, [FromUri] StudentParameters studentParameters)
        {
            try
            {
                return await _service.DeleteStudent(id, studentParameters);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}