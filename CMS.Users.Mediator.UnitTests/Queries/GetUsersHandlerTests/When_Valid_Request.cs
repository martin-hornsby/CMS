using CMS.Users.Mediator.Types.Queries;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Users.Mediator.UnitTests.Queries.GetUsersHandlerTests
{
    [TestFixture]
    public class When_Valid_Request : GetUsersHandlerTestBase
    {
        [Test]
        public async Task Then_Successful_Response_Is_Returned()
        {
            var request = new GetUsersRequest();

            var response = await Handler.Handle(request, CancellationToken);

            Assert.IsTrue(response.Success);
        }

        [Test]
        public async Task Then_A_List_Of_Users_Is_Returned()
        {
            var request = new GetUsersRequest();

            var response = await Handler.Handle(request, CancellationToken);

            CollectionAssert.IsNotEmpty(response.Value);
        }

        [Test]
        public async Task Then_User_Are_Ordered_By_Ascending_Id()
        {
            var request = new GetUsersRequest();

            var response = await Handler.Handle(request, CancellationToken);

            var idList = response.Value.Select(u => u.Id);

            CollectionAssert.IsOrdered(idList);
        }
    }
}
