﻿using Microsoft.AspNetCore.Mvc;

namespace WebServices.API.Controllers
{
    public class AuthenticationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
