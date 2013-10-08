using System;
using Funcy.Domain;

namespace Funcy.Services
{
    public class FuncyCustomerService
    {
        private readonly ICacheService _cacheService;
        private readonly ICustomerRepository _customerRepository;


        /*
         * Create GetCustomerDetails, not as a method on this class, but as a public field
         * the field is a Func - the kind of method that takes in an int (customerId) and returns a Customer
         */
        public Func<int, Customer> GetCustomerDetails;

        public FuncyCustomerService(ICacheService cacheService, ICustomerRepository customerRepository)
        {
            _cacheService = cacheService;
            _customerRepository = customerRepository;

            // Get the Func from the Encachify extension method. It's just a Func, it won't be called yet!
            GetCustomerDetails = _cacheService.Encachify(
                (int customerId) => string.Format(@"CustomerId{0}", customerId),    //  Pass in a way to make the cache key
                GetCustomerDetailsCore);                                            //  and a way to get the data, if not in the cache
        }

        //  Now we have a method that is *only* concerned with getting data, not caching...
        private Customer GetCustomerDetailsCore(int customerId)
        {
            return _customerRepository.GetById(customerId);
        }
    }
}