using LibraryApp.Models.Accounts.AccountValidator;
using LibraryApp.Models.Accounts.AccountVerifiers;
using LibraryApp.Models.Accounts.Contracts;
using LibraryApp.Models.Renewals.Contracts;
using LibraryApp.Models.Renewals.RenewalValidators;
using LibraryApp.Models.Repositories.Accounts;
using LibraryApp.Models.Repositories.Readers;
using LibraryApp.Models.Repositories.Renewals.Contracts;
using LibraryApp.Models.Repositories.Renewals.RenewalRepositories;
using LibraryApp.Models.Repositories.Rentals;
using LibraryApp.Models.Specifications.Passwords;
using LibraryApp.Models.Specifications.RenewalSpecification;

namespace LibraryApp.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAccountValidation(this IServiceCollection services)
        {
            services.AddScoped<IAccountValidator, AccountValidator>();
            services.AddScoped<IAccountVerifier, UserAgeVerifier>();
            services.AddScoped<IAccountVerifier, ValidDateVerifier>();
            services.AddScoped<IAccountVerifier, NameFormatVerifier>();
            services.AddScoped<IAccountVerifier, EmailFormatVerifier>();
            services.AddScoped<IAccountVerifier, DocumentsAcceptedVerifier>();
            services.AddScoped<IAccountVerifier, PasswordEqualityVerifier>();
            services.AddScoped<IAccountVerifier, PasswordStrengthVerifier>();

            return services;
        }

        public static IServiceCollection AddSpecifications(this IServiceCollection services)
        {
            services.AddSingleton<IPasswordsSpecification, PasswordsSpecification>();
            services.AddSingleton<IRenewalSpecification, RenewalSpecification>();

            return services;
        }

        public static IServiceCollection AddLibraryRepositories(this IServiceCollection services)
        {
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IReaderRepository, ReaderRepository>();
            services.AddScoped<IRentalRepository, RentalRepository>();
            services.AddScoped<IRenewalRepository, RenewalRepository>();

            return services;
        }

        public static IServiceCollection AddRenewalValidators(this IServiceCollection services)
        {
            services.AddScoped<IRenewalValidator, UnpaidPenaltiesValidator>();
            services.AddScoped<IRenewalValidator, RenewalLimitValidator>();

            return services;
        }
    }
}
