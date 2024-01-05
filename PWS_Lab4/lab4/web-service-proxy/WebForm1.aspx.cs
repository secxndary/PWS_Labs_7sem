﻿using System;
namespace web_service_proxy
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        private Simplex _client;

        protected void Page_Load(object sender, EventArgs e)
        {
            _client = new Simplex();
        }

        protected void SumClick(object sender, EventArgs e)
        {
            int first;
            int second;
            if(int.TryParse(x.Text.ToString(), out first) &&
               int.TryParse(y.Text.ToString(), out second))
            {
                result.Text = _client.Add(first, second).ToString();
            }
        }
    }
}