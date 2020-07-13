using CMS.Users.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace CMS.Users.Mediator.UnitTests
{
    public static class DataContextHelper
    {
        public static UserDataContext GetInMemoryDataContext()
        {
            var dbContextOptions = new DbContextOptionsBuilder<UserDataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new UserDataContext(dbContextOptions);
        }
    }
}
