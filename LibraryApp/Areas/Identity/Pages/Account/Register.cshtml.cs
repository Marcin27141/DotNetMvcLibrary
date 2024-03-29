﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryApp.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        public IActionResult OnGet(string returnUrl = null)
        {
            return RedirectToAccountController();
        }

        public IActionResult OnPost(string returnUrl = null)
        {
            return RedirectToAccountController();
        }

        private RedirectToActionResult RedirectToAccountController() => RedirectToAction("RegisterReader", "Account", new { area = "" });
    }
}
