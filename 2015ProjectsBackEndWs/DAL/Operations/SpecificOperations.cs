using DAL.Operations.BaseClasses;

namespace DAL.Operations
{
    public class SpecificOperations:BaseOperations
    {
        public SpecificOperations(string connectionString, bool isTest = false) : base(connectionString, isTest)
        {
        }
    }
}