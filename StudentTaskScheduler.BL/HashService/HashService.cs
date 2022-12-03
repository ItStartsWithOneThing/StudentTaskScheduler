using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Options;
using StudentTaskScheduler.BL.Options;
using System;

namespace StudentTaskScheduler.BL.HashService
{
    public class HashService : IHashService
    {
        private readonly AuthorizationOptions _authOptions;

        public HashService(IOptions<AuthorizationOptions> options)
        {
            _authOptions = options.Value;
        }

        public string HashString(string source)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                 password: source,
                 salt: Convert.FromBase64String(_authOptions.Salt),
                 prf: KeyDerivationPrf.HMACSHA256,
                 iterationCount: 100000,
                 numBytesRequested: 32));
        }
    }
}
