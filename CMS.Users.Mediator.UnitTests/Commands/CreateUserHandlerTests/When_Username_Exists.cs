using CMS.Users.Data.Entities;
using CMS.Users.Mediator.Types.Commands;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace CMS.Users.Mediator.UnitTests.Commands.CreateUserHandlerTests
{
    [TestFixture]
    public class When_Username_Exsists : CreateUserHandlerTestBase
    {
        private string Username;

        [SetUp]
        public override void Setup()
        {
            base.Setup();

            Username = Guid.NewGuid().ToString();

            var user = new User { Username = Username };
            DataContext.Users.Add(user);
            DataContext.SaveChanges();
        }

        [Test]
        public async Task Then_Unsuccessful_Response_Is_Returned()
        {
            var request = new CreateUserRequest(Username);

            var response = await Handler.Handle(request, CancellationToken);

            Assert.IsFalse(response.Success);
        }
    }
}
