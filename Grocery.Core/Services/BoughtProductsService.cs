
using Grocery.Core.Interfaces.Repositories;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;

namespace Grocery.Core.Services
{
    public class BoughtProductsService : IBoughtProductsService
    {
        private readonly IGroceryListItemsRepository _groceryListItemsRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IProductRepository _productRepository;
        private readonly IGroceryListRepository _groceryListRepository;
        public BoughtProductsService(IGroceryListItemsRepository groceryListItemsRepository, IGroceryListRepository groceryListRepository, IClientRepository clientRepository, IProductRepository productRepository)
        {
            _groceryListItemsRepository=groceryListItemsRepository;
            _groceryListRepository=groceryListRepository;
            _clientRepository=clientRepository;
            _productRepository=productRepository;
        }
        public List<BoughtProducts> Get(int? productId)
        {
            var result = new List<BoughtProducts>();

            // Get all grocery list items
            var groceryListItems = _groceryListItemsRepository.GetAll();

            // Filter items by productId if provided
            if (productId.HasValue)
            {
                groceryListItems = groceryListItems
                    .Where(item => item.ProductId == productId.Value)
                    .ToList();
            }

            // Get all grocery lists, products, and clients for lookup
            var groceryLists = _groceryListRepository.GetAll();
            var products = _productRepository.GetAll();
            var clients = _clientRepository.GetAll();

            foreach (var item in groceryListItems)
            {
                var groceryList = groceryLists.FirstOrDefault(gl => gl.Id == item.GroceryListId);
                var product = products.FirstOrDefault(p => p.Id == item.ProductId);
                var client = groceryList != null ? clients.FirstOrDefault(c => c.Id == groceryList.ClientId) : null;

                if (groceryList != null && product != null && client != null)
                {
                    result.Add(new BoughtProducts(client, groceryList, product));
                }
            }

            return result;
        }
    }
}
