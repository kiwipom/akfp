using Funcy.Domain;

namespace Funcy.Services
{
    public class BetterCustomerService
    {
        private readonly ICacheService _cacheService;
        private readonly ICustomerRepository _customerRepository;

        public BetterCustomerService(ICacheService cacheService, ICustomerRepository customerRepository)
        {
            _cacheService = cacheService;
            _customerRepository = customerRepository;
        }

        /// <summary>
        /// Use <seealso cref="ICacheService.Fetch"/> to do all the caching work
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public Customer GetCustomerDetails(int customerId)
        {
            // Build the key, as per the simple CustomerService
            var cacheKey = string.Format(@"CustomerId{0}", customerId);

            //  Call the extension method; this encapsulates the "look in cache / go get data / add to cache" work
            return _cacheService.Fetch(
                () => _customerRepository.GetById(customerId), 
                cacheKey);
        }
    }

    /*
     But we still have caching stuff going on in our GetCustomerDetails method.
     Ideally, we would only have 'getting customer details' work, not caching work.
     * 
     Go look at FuncyCustomerService for the next level...
     */
}