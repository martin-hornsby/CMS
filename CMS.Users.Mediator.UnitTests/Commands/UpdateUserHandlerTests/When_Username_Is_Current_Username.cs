using CMS.Users.Mediator.Types.Commands;
using NUnit.Framework;
using System.Threading.Tasks;

namespace CMS.Users.Mediator.UnitTests.Commands.UpdateUserHandlerTests
{
    [TestFixture]
    public class When_Username_Is_Current_Username : UpdateUserHandlerTestBase
    {
        [Test]
        public async Task Then_Unsuccessful_Response_Is_Returned()
        {
            var request = new UpdateUserRequest(ExistingUser.Id, ExistingUser.Username);

            var response = await Handler.Handle(request, CancellationToken);

            Assert.IsFalse(response.Success);
        }
    }
}
