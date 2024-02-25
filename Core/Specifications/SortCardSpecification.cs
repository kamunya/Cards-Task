using Cards.Core.Entities;

namespace Cards.Core.Specifications
{
    public class SortCardSpecification : BaseSpecification<Card>
    {
        public SortCardSpecification(int Id):
            base(x=>x.Id== Id)
        {
            AddInclude(o => o.Name);
            AddInclude(o => o.Color);
            AddInclude(d => d.Status);
            AddInclude(d => d.CreatedOn);
            AddOrderByDecending(od => od.Name);
        }
    }
}
