using CMS.Users.Data;
using CMS.Users.Data.Entities;
using CMS.Users.Mediator.Commands;
using NUnit.Framework;
using System;
using System.Threading;

namespace CMS.Users.Mediator.UnitTests.Commands.UpdateUserHandlerTests
{
    public class UpdateUserHandlerTestBase
    {
        protected UserDataContext DataContext;
        protected CancellationToken CancellationToken;
        protected UpdateUserHandler Handler;

        protected User ExistingUser;
        protected User AnotherExistingUser;

        [SetUp]
        public virtual void Setup()
        {
            DataContext = DataContextHelper.GetInMemoryDataContext();
            CancellationToken = new CancellationToken();
            Handler = new UpdateUserHandler(DataContext);

            var user1 = new User { Username = Guid.NewGuid().ToString() };
            var user2 = new User { Username = Guid.NewGuid().ToString() };

            DataContext.Users.AddRange(user1, user2);
            DataContext.SaveChanges();

            ExistingUser = user1;
            AnotherExistingUser = user2;
        }
    }
}
