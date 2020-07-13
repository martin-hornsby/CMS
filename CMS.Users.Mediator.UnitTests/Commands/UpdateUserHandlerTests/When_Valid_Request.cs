using CMS.Users.Mediator.Types.Commands;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace CMS.Users.Mediator.UnitTests.Commands.UpdateUserHandlerTests
{
    [TestFixture]
    public class When_Valid_Request : UpdateUserHandlerTestBase
    {
        private string Username;

        [SetUp]
        public override void Setup()
        {
            base.Setup();

            Username = Guid.NewGuid().ToString();
        }

        [Test]
        public async Task Then_Successful_Response_Is_Returned()
        {
            var request = new UpdateUserRequest(ExistingUser.Id, Username);

            var response = await Handler.Handle(request, CancellationToken);

            Assert.IsTrue(response.Success);
        }

        [Test]
        public async Task Then_True_Is_Returned()
        {
            var request = new UpdateUserRequest(ExistingUser.Id, Username);

            var response = await Handler.Handle(request, CancellationToken);

            Assert.IsNotNull(response.Value);
        }

        [Test]
        public async Task Then_User_Has_Requested_Username()
        {
            var request = new UpdateUserRequest(ExistingUser.Id, Username);

            await Handler.Handle(request, CancellationToken);

            var user = await DataContext.Users.SingleOrDefaultAsync(u => u.Id == ExistingUser.Id);

            Assert.AreEqual(Username, user.Username);
        }
    }
}
