using CMS.Users.Data;
using CMS.Users.Mediator.Commands;
using NUnit.Framework;
using System.Threading;

namespace CMS.Users.Mediator.UnitTests.Commands.DeleteUserHandlerTests
{
    public class DeleteUserHandlerTestBase
    {
        protected UserDataContext DataContext;
        protected CancellationToken CancellationToken;
        protected DeleteUserHandler Handler;


        [SetUp]
        public virtual void Setup()
        {
            DataContext = DataContextHelper.GetInMemoryDataContext();
            CancellationToken = new CancellationToken();
            Handler = new DeleteUserHandler(DataContext);
        }
    }
}
