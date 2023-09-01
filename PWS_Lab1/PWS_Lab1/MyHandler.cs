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
                    res.ContentType = "application/json";
                    res.Write(
                        "{\"result\": " + 
                            (_stack.Count > 0 ? 
                            (_result + _stack.Peek()) : 
                            _result) + 
                        "}");
                    break;

                case "POST":
                    int resultParameter;
                    int.TryParse(req.QueryString["result"], out resultParameter);
                    _result += resultParameter;
                    break;

                case "PUT":
                    int addParameter;
                    int.TryParse(req.QueryString["add"], out addParameter);
                    _stack.Push(addParameter);
                    break;

                case "DELETE":
                    if (_stack.Count > 0)
                        _stack.Pop();
                    break;

                default:
                    res.StatusCode = 405;
                    res.AddHeader("Content-Type", "text/html");
                    res.Write("<h1>[ERROR] 405: Method Not Allowed</h1>");
                    break;
            }
        }
    }
}
