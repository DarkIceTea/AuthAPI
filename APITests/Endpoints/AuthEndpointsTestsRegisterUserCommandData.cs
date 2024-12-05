using Application.Commands.RegisterUser;
using Bogus;
using System.Collections;

namespace APITests.Endpoints
{
    public class AuthEndpointsTestsRegisterUserCommandData : IEnumerable<object[]>
    {
        Faker<RegisterUserCommand> customUserFaker;
        readonly List<object[]> _data = new List<object[]>();
        public AuthEndpointsTestsRegisterUserCommandData()
        {
            customUserFaker = new Faker<RegisterUserCommand>()
                .RuleFor(u => u.UserRole, f => f.PickRandom(new[] { "patient", "doctor" }))
                .RuleFor(u => u.Email, f => f.Internet.Email())
                .RuleFor(u => u.UserName, f => f.Internet.UserName())
                .RuleFor(u => u.Password, f => f.Internet.Password());

            var randUsr = customUserFaker.Generate(10);
            foreach (var item in randUsr)
            {
                var obj = new object[] { item };
                _data.Add(obj);
            }
        }
        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
