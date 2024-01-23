﻿using LibraryApp.Models.Accounts;
using LibraryApp.Models.Database.Entities;
using LibraryApp.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.IdentityModel.Tokens;

namespace LibraryApp.Models.Accounts.AccountVerifiers
{
    public class NameFormatVerifier : IAccountVerifier
    {
        private readonly List<AccountValidationError> _errors = [];
        public AccountValidationResult VerifyAccount(RegisterViewModel user)
        {
            AddErrorsIfInvalid(user.FirstName, nameof(user.FirstName));
            AddErrorsIfInvalid(user.LastName, nameof(user.LastName));

            return _errors.Count > 0 ?
                AccountValidationResult.Failure(_errors) :
                AccountValidationResult.Success();
        }

        private void AddErrorsIfInvalid(string subject, string property)
        {
            if (!IsProperlyCapitalized(subject) || !ContainsValidCharacters(subject))
                _errors.Add(new AccountValidationError(property, "This is not a valid format"));
        }

        private bool ContainsValidCharacters(string text) => text.All(char.IsLetter);

        private bool IsProperlyCapitalized(string text)
            => IsFirstLetterUpper(text) && AreFollowingLettersLower(text);

        private bool IsFirstLetterUpper(string text) => text.IsNullOrEmpty() || char.IsUpper(text.First());
        private bool AreFollowingLettersLower(string text) => text.IsNullOrEmpty() || text.Skip(1).All(char.IsLower);
    }
}