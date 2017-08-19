namespace WorkforceManagement.DAL.Concrete
{
    public class DbInitalizer
    {
        public static void Initalize(EFDbContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
