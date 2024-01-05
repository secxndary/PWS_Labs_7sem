using System;
using System.Web.Http;

namespace PWS_Lab3.Controllers
{
    [RoutePrefix("api/error")]
    public class ErrorController : ApiController
    {
        [Route("{code:int}/{id:int}")]
        [HttpGet]
        public IHttpActionResult Error(int code, int id)
        {
            switch (id)
            {
                default:
                case 1:
                    {
                        return Ok($"Ошибка {code}/{id}. Записи с заданным вами идентификатором не существует. Введите корректный идентификатор");
                    }
                case 2:
                    {
                        return Ok($"Ошибка {code}/{id}. Неподдерживаемый формат данных. Поддерживаемые форматы: JSON и XML.");
                    }
                case 3:
                    {
                        return Ok($"Ошибка {code}/{id}. Вы пытаетесь добавить запись, но введенные вами данные не прошли проверку на корректность. Убедитесь в корректности вводимых значений и попробуйте снова.");
                    }
                case 4:
                    {
                        return Ok($"Ошибка {code}/{id}. Поля limit, offset, minId, и maxId должны быть положительными числами.");
                    }
                case 5:
                    {
                        return Ok($"Ошибка {code}/{id}. minId должен быть меньше maxId.");
                    }
            }
        }
    }
}