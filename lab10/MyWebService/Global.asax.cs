﻿using System;
using System.Web;

namespace MyWebService
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            MyData.Info();
        }
        protected void Session_Start(object sender, EventArgs e)
        {
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");
            if (HttpContext.Current.Request.HttpMethod == "OPTIONS")
            {
                HttpContext.Current.Response.AddHeader(
                "Access-Control-Allow-Methods",
                "POST, PUT, DELETE");

                HttpContext.Current.Response.AddHeader(
                "Access-Control-Allow-Headers",
                "Content-Type, Accept");

                HttpContext.Current.Response.AddHeader(
                "Access-Control-Max-Age", "7200");
                HttpContext.Current.Response.End();
            }
        }
        protected void Application_AuthenticateRequest(object sender, EventArgs e) { }
        protected void Application_Error(object sender, EventArgs e) { }
        protected void Session_End(object sender, EventArgs e) { }
        protected void Application_End(object sender, EventArgs e) { }

    }
}