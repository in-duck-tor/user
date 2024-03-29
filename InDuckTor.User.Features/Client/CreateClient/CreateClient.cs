﻿using FluentResults;
using InDuckTor.Shared.Security.Context;
using InDuckTor.User.Domain;
using InDuckTor.User.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace InDuckTor.User.Features.Client.CreateClient
{
    public class CreateClient : ICreateClient
    {
        private readonly UsersDbContext _context;
        private readonly ISecurityContext _securityContext;

        public CreateClient(UsersDbContext context, ISecurityContext securityContext)
        {
            _context = context;
            _securityContext = securityContext;
        }

        public async Task<Result<CreateClientResult>> Execute(CreateClientRequest input, CancellationToken ct)
        {
            var user = await _context.Users.Where(u => u.Login == input.Login).FirstOrDefaultAsync(ct);
            if (user is not null) return new Errors.User.LoginExists(input.Login);

            var client = new Domain.Client
            {

                User = new Domain.User
                {
                    Login = input.Login,
                    AccountType = Domain.AccountType.Client,
                },
                Email = input.Email,
                FirstName = input.FirstName,
                LastName = input.LastName,
                MiddleName = input.MiddleName,
                BirthDate = input.BirthDate
            };

            _context.Add(client);
            await _context.SaveChangesAsync(ct);

            return new CreateClientResult(client.Id);
        }
    }
}
