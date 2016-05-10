using DAL.Operations.BaseClasses;
using Models.Base;
using Models.Users;
using UnitOfWork.Implementations.Repository.BaseRepository;

namespace DAL.Operations
{
    public class UserOperations:BaseOperations
    {
        public UserOperations(string connectionString, bool isTest = false) : base(connectionString, isTest)
        {
        }

        public bool SearchByEmail(string email)
        {
            var cacheKey = $"WhereEmailEqualsTo=>{email}";
            
            if (IsTest)
            {
               var repo   = (RepositoryTest<User>) RepoSelector<BaseEntity>(MappedRepositories.UserRepository, cacheKey);
                return repo.Any(c => c.Email == email, cacheKey);
            }
            else
            {
                var repo   = (RepositoryProduction<User>) RepoSelector<BaseEntity>(MappedRepositories.UserRepository, cacheKey);
                return repo.Any(c => c.Email == email, cacheKey);
            }
        }
    }
}