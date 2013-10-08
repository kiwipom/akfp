using Funcy.Domain;

namespace Funcy.Services
{
    public class CustomerService
    {
        private readonly ICacheService _cacheService;
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICacheService cacheService, ICustomerRepository customerRepository)
        {
            _cacheService = cacheService;
            _customerRepository = customerRepository;
        }

        /// <summary>
        /// Naive implementation
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public Customer GetCustomerDetails(int customerId)
        {
            // Create a cache key
            var cacheKey = string.Format(@"CustomerId{0}", customerId);

            //  Attempt to get data from the cache
            var result = _cacheService.Get<Customer>(cacheKey);

            //  Did we get data?
            if (result == null)
            {
                //  Nope - so go get it...
                result = _customerRepository.GetById(customerId);

                //  ...and then cache it
                _cacheService.Add(cacheKey, result);
            }

            return result;
        }


        /*
         Now imagine we had a suite of methods in this service that *all* followed this pattern:
         *  Build a cache key
         *  Go to the cache, try find the data
         *  If none found, then go get the data (and cache it)
         *  Return the result
         That's a lot of repetition... there must be a better way!
         * 
         Now go look at BetterCustomerService
         */
    }
}