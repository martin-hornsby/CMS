using CMS.Users.Mediator.Types.Commands;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Users.Mediator.UnitTests.Commands.CreateUserHandlerTests
{
    [TestFixture]
    public class When_Valid_Request : CreateUserHandlerTestBase
    {
        private string Username;

        [SetUp]
        public override void Setup()
        {
            base.Setup();

            Username = Guid.NewGuid().ToString();

            var existingUsers = DataContext.Users.Where(u => u.Username.Equals(Username, StringComparison.InvariantCultureIgnoreCase));

            DataContext.Users.RemoveRange(existingUsers);
            DataContext.SaveChanges();
        }

        [Test]
        public async Task Then_Successful_Response_Is_Returned()
        {
            var request = new CreateUserRequest(Username);

            var response = await Handler.Handle(request, CancellationToken);

            Assert.IsTrue(response.Success);
        }

        [Test]
        public async Task Then_User_Is_Returned()
        {
            var request = new CreateUserRequest(Username);

            var response = await Handler.Handle(request, CancellationToken);

            Assert.IsNotNull(response.Value);
        }

        [Test]
        public async Task Then_User_Has_Requested_Username()
        {
            var request = new CreateUserRequest(Username);

            var response = await Handler.Handle(request, CancellationToken);

            Assert.AreEqual(Username, response.Value.Username);
        }
    }
}
