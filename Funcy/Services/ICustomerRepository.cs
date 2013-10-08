using Funcy.Domain;

namespace Funcy.Services
{
    /// <summary>
    /// Interface to describe the Customers Repository - no implementation required for this demo...
    /// </summary>
    public interface ICustomerRepository
    {
        Customer GetById(int customerId);
    }
}