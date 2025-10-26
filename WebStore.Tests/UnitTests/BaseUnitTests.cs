using WebStore.DAL;

namespace WebStore.Tests.UnitTests
{
    public abstract class BaseUnitTests
    {
        protected readonly WebStoreDbContext DbContext;
        
        public BaseUnitTests(WebStoreDbContext dbContext)
        {
            DbContext = dbContext;
        }
    }
}
