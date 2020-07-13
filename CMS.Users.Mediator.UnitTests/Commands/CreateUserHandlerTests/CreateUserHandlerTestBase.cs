using CMS.Users.Data;
using CMS.Users.Mediator.Commands;
using NUnit.Framework;
using System.Threading;

namespace CMS.Users.Mediator.UnitTests.Commands.CreateUserHandlerTests
{
    public class CreateUserHandlerTestBase
    {
        protected UserDataContext DataContext;
        protected CancellationToken CancellationToken;
        protected CreateUserHandler Handler;


        [SetUp]
        public virtual void Setup()
        {
            DataContext = DataContextHelper.GetInMemoryDataContext();
            CancellationToken = new CancellationToken();
            Handler = new CreateUserHandler(DataContext);
        }
    }
}
