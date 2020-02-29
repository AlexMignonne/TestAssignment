using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using Accounts.Domain.Events;
using Accounts.Domain.Exceptions;
using Accounts.Domain.Extensions;
using Accounts.Domain.SeedWork;
using Accounts.Domain.Types;

namespace Accounts.Domain.AggregatesModel.Account
{
    [SuppressMessage(
        "ReSharper",
        "AutoPropertyCanBeMadeGetOnly.Local")]
    public sealed class AccountDomain
        : Entity,
            IAggregateRoot
    {
        public AccountDomain(
            string correlationId,
            bool? agree,
            string email,
            string password,
            int provinceId)
        {
            Id = default;

            if (agree == null ||
                !agree.Value)
                throw new DomainException(
                    "You must accept the terms of the license agreement");

            AccountStatus = AccountStatusType.Active;

            Email = !email.IsEmail()
                ? throw new DomainException(
                    $"Email incorrect: {email}.")
                : email;

            if (string.IsNullOrWhiteSpace(password))
                throw new DomainException(
                    "Password cannot be empty.");

            var regex = new Regex(
                @"[a-zA-Z]+[0-9]+");

            HashPassword = regex
                .IsMatch(password)
                ? password
                    .Sha256()
                : throw new DomainException(
                    "Password must contain min 1 digit and min 1 letter.");

            ProvinceId = provinceId;

            AddDomainEvent(
                new RegisteredUserDomainEvent(
                    correlationId,
                    this));
        }

        // ReSharper disable once UnusedMember.Local
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        private AccountDomain(
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
            int provinceId)
        {
            ProvinceId = provinceId;
        }

        public new int Id { get; private set; }
        public AccountStatusType AccountStatus { get; private set; }

        public string Email { get; private set; }
        public string HashPassword { get; private set; }
        public int ProvinceId { get; private set; }

        public void ChangeStatus(
            AccountStatusType status)
        {
            AccountStatus = status;
        }
    }
}
