using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using XYZ.Starter.Data;

namespace XYZ.Starter.Unit.Tests
{
    public class AppDbContextMocker
    {
        /// <summary>
        /// Get an In memory version of the app db context with some seeded data
        /// </summary>
        /// <param name="dbName"></param>
        /// <returns></returns>
        public static AppDbContext GetAppDbContext(string dbName)
        {
            //set up the options to use for this dbcontext
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(dbName)                
                .Options;
            var dbContext = new AppDbContext(options);
            dbContext.SeedAppDbContext();
            return dbContext;
        }
    }
}
