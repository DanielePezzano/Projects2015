using System.Collections.Generic;
using AuthModel;
using AutUow.Interfaces;

namespace AutUow.Implementations.Context
{
    public class TestContext : IContext
    {
        public TestContext()
        {
            IsTest = true;
        }

        public List<UserProfile> UserProfiles { get; set; }
        public List<Roles> UserRoleses { get; set; }
        public bool IsTest { get; set; }
    }
}