﻿using System;
using System.Web;
using System.Web.Http;

namespace TaskMaster_Backend
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {            
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}