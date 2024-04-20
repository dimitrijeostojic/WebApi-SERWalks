﻿using Microsoft.AspNetCore.Identity;

namespace SERWalks.API.Repositories
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
