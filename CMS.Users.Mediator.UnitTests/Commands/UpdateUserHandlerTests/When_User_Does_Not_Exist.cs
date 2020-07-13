using CMS.Users.Mediator.Types.Commands;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Users.Mediator.UnitTests.Commands.UpdateUserHandlerTests
{
    [TestFixture]
    public class When_User_Does_Not_Exist : UpdateUserHandlerTestBase
    {
        private long Id;

        [SetUp]
        public override void Setup()
        {
            base.Setup();

            var userWithHighestId = DataContext.Users.OrderByDescending(u => u.Id).FirstOrDefault();
            var currentMaxId = userWithHighestId?.Id ?? 0;

            Id = currentMaxId + 1;
        }

        [Test]
        public async Task Then_Unsuccessful_Response_Is_Returned()
        {
            var request = new UpdateUserRequest(Id, Guid.NewGuid().ToString());

            var response = await Handler.Handle(request, CancellationToken);

            Assert.IsFalse(response.Success);
        }
    }
}
