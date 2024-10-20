using Domain.Contracts;
using Domain.Entities.OrderEntities;
using System.Linq.Expressions;

namespace Services.Specifications
{
    internal class OrderWithIncludeSpecifications : Specifications<Order>
    {
        public OrderWithIncludeSpecifications(Guid id)
            : base(order => order.Id == id)
        {
            AddIncludes(order => order.DeliveryMethods);
            AddIncludes(order => order.OrderItems);

        }

        public OrderWithIncludeSpecifications(string email)
           : base(order => order.UserEmail == email)
        {
            AddIncludes(order => order.DeliveryMethods);
            AddIncludes(order => order.OrderItems);

            SetOrderBy(order => order.OrderDate);
        }
    }
}
