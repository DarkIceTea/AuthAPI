using Application.Commands.LoginUser;
using Bogus;
using System.Collections;

namespace APITests.Endpoints
{
    public class AuthEndpointsTestsLoginUserCommandData : IEnumerable<object[]>
    {
        Faker<LoginUserCommand> customUserFaker;
        readonly List<object[]> _data = new List<object[]>();
        public AuthEndpointsTestsLoginUserCommandData()
        {
            customUserFaker = new Faker<LoginUserCommand>()
                .RuleFor(u => u.Email, f => f.Internet.Email())
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
