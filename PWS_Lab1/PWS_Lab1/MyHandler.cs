using System.Collections.Generic;
using System.Web;
using System.Web.SessionState;

namespace PWS_Lab1
{
    public class MyHandler : IHttpHandler
    {
        private int _result;

        public bool IsReusable => true;

        public void ProcessRequest(HttpContext context)
        {
            var req = context.Request;
            var res = context.Response;

            switch (req.HttpMethod)
            {
                case "GET":
                    res.ContentType = "application/json";
                    res.Write("{\"result\": " + _result + "}");
                    break;

                case "POST":
                    int resultParameter;
                    int.TryParse(req.QueryString["result"], out resultParameter);
                    _result += resultParameter;
                    break;

                case "PUT":
                    int addParameter;
                    int.TryParse(req.QueryString["add"], out addParameter);
                    var stack = GetSessionStack(context.Session);
                    stack.Push(addParameter);
                    break;

                case "DELETE":
                    stack = GetSessionStack(context.Session);
                    if (stack.Count > 0)
                        stack.Pop();
                    break;

                default:
                    res.StatusCode = 405;
                    res.AddHeader("Content-Type", "text/html");
                    res.Write("<h1>[ERROR] 405: Method Not Allowed</h1>");
                    break;
            }
        }

        private Stack<int> GetSessionStack(HttpSessionState session)
        {
            if (session["Stack"] == null)
                session["Stack"] = new Stack<int>();

            return (Stack<int>)session["Stack"];
        }
    }
}
