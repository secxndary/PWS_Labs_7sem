﻿using System.Web.Script.Services;
using System.Web.Services;

namespace lab4
{
    /// <summary>
    /// Сводное описание для Simplex
    /// </summary>
    [WebService(Namespace = "http://vad/", Description = "<h1>Lab4 by Secxndary</h1>")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Чтобы разрешить вызывать веб-службу из скрипта с помощью ASP.NET AJAX, раскомментируйте следующую строку. 
    [ScriptService]
    public class Simplex : WebService
    {
        [WebMethod(MessageName = "Add", Description = "Sum of two integer numbers")]
        public int Add(int x, int y)
        {
            return x + y;
        }

        [WebMethod(MessageName = "Concat", Description = "Concatenation of string and double values")]
        public string Concat(string s, double d)
        {
            return $"{s}{d}";
        }

        [WebMethod(MessageName = "Sum", Description = "Sum of two objects A")]
        public A Sum(A a1, A a2)
        {
            var context = Context.Request.InputStream;
            context.Position = 0;
            var reader = new System.IO.StreamReader(context);
            var json = reader.ReadToEnd();
            var result = new A(a1.s + a2.s, a1.k + a2.k, a1.f + a2.f);
            return result;
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod()]
        public string AddS(string x, string y)
        {
            return $"{x} + {y}";
        }
    }

    public class A
    {
        public string s;
        public int k;
        public float f;

        public A()
        {
        }

        public A(string v1, int v2, float v3)
        {
            s = v1;
            k = v2;
            f = v3;
        }
    }
}
