using Models.Base;

namespace Models.Tech
{
    public class TechRequisiteNode : BaseEntity
    {
        public virtual Technology Technology { get; set; }
        public virtual Technology Requisite { get; set; }
    }
}
