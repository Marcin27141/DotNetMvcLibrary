﻿using LibraryApp.Models.Database.Entities;
using LibraryApp.Models.Repositories.Accounts.AccountValidator;
using LibraryApp.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LibraryApp.Models.Repositories.Accounts.AccountVerifiers
{
    public class PasswordEqualityVerifier : IAccountVerifier
    {
        public AccountValidationResult VerifyAccount(RegisterViewModel user)
        {
            return user.Password.Equals(user.ConfirmPassword) ?
                AccountValidationResult.Success() :
                AccountValidationResult.Failure(new AccountValidationError(
                nameof(user.ConfirmPassword), "Passwords must be the same"));
        }
    }
}