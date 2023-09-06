using System.Collections.Generic;
using System.Web;

namespace PWS_Lab1
{
    public class MyHandler : IHttpHandler
    {
        private int _result;
        private readonly Stack<int> _stack = new Stack<int>();

        public bool IsReusable => true;

        public void ProcessRequest(HttpContext context)
        {
            var req = context.Request;
            var res = context.Response;

            switch (req.HttpMethod)
            {
                case "GET":
                    var result = (_stack.Count > 0) ? (_result + _stack.Peek()) : _result;
                    res.ContentType = "application/json";
                    res.Write("{\"result\": " + result + "}");
                    break;

                case "POST":
                    if (!int.TryParse(req.QueryString["result"], out int resultParameter))
                    {
                        SendResponse(res, 400, "[ERROR] Enter integer parameter.");
                        break;
                    }
                    _result += resultParameter;
                    break;

                case "PUT":
                    if (!int.TryParse(req.QueryString["add"], out int addParameter))
                    {
                        SendResponse(res, 400, "[ERROR] Enter integer parameter.");
                        break;
                    }
                    _stack.Push(addParameter);
                    break;

                case "DELETE":
                    if (_stack.Count <= 0)
                    {
                        SendResponse(res, 400, "[ERROR] Stack is empty."); 
                        break;
                    }
                    _stack.Pop();
                    break;

                default:
                    SendResponse(res, 405, "[ERROR] Method Not Allowed.");
                    break;
            }
        }

        private void SendResponse(HttpResponse res, int code, string message)
        {
            res.StatusCode = code;
            res.Write(message);
        }
    }
}
