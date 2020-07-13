using CMS.Users.Data.Entities;
using CMS.Users.Mediator.Types.Commands;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace CMS.Users.Mediator.UnitTests.Commands.DeleteUserHandlerTests
{
    [TestFixture]
    public class When_User_Exists : DeleteUserHandlerTestBase
    {
        private long Id;

        [SetUp]
        public override void Setup()
        {
            base.Setup();

            var username = Guid.NewGuid().ToString();

            var user = new User { Username = username };
            DataContext.Users.Add(user);
            DataContext.SaveChanges();

            Id = user.Id;
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
