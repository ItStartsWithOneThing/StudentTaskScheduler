using StudentTaskScheduler.BL.Authorization;
using StudentTaskScheduler.BL.HashService;
using StudentTaskScheduler.DAL.Entities;
using StudentTaskScheduler.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTaskScheduler.BL.Services.AuthorizationService
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IHashService _hashService;    
        private readonly IDbGenericRepository<Student> _genericStudentRepository;   
        private readonly ITokenGenerator _tokenGenerator;

        public AuthorizationService(
            IHashService hashService)
        {
            _hashService = hashService;
        }

        public async Task<string> SignIn(string login, string password)
        {
            var hashedPassword = _hashService.HashString(password);

            var user = await _genericStudentRepository.GetSingleByPredicateReadOnly(
                x => x.Login == login && x.Password == hashedPassword);

            if (user != null)
            {
                return _tokenGenerator.GenerateToken(user.Login, user.Role);
            }

            throw new UnauthorizedAccessException("Wrong Email or Password");
        }
    }
}
