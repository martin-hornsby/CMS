using CMS.Users.Mediator.Types.Commands;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Users.Mediator.UnitTests.Commands.DeleteUserHandlerTests
{
    [TestFixture]
    public class When_User_Does_Not_Exist : DeleteUserHandlerTestBase
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
        public async Task Then_Successful_Response_Is_Returned()
        {
            var request = new DeleteUserRequest(Id);

            var response = await Handler.Handle(request, CancellationToken);

            Assert.IsTrue(response.Success);
        }

        [Test]
        public async Task Then_User_Not_Longer_Exists()
        {
            var request = new DeleteUserRequest(Id);

            await Handler.Handle(request, CancellationToken);

            var user = await DataContext.Users.SingleOrDefaultAsync(u => u.Id == Id);

            Assert.IsNull(user);
        }
    }
}
