using Bogus;
using Domain.Models;
using System.Collections;

namespace InfrastructureTests.Services
{
    public class RefreshTokenServiceTestsData : IEnumerable<object[]>
    {
        Faker<CustomUser> customUserFaker;
        readonly List<object[]> _data = new List<object[]>();
        public RefreshTokenServiceTestsData()
        {
            customUserFaker = new Faker<CustomUser>()
                .RuleFor(u => u.Id, Guid.NewGuid)
                .RuleFor(u => u.Email, f => f.Internet.Email())
                .RuleFor(u => u.UserName, f => f.Internet.UserName());

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
