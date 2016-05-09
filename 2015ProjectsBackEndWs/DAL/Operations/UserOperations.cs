using DAL.Operations.BaseClasses;

namespace DAL.Operations
{
    public class UserOperations:BaseOperations
    {
        public UserOperations(string connectionString, bool isTest = false) : base(connectionString, isTest)
        {
        }
    }
}