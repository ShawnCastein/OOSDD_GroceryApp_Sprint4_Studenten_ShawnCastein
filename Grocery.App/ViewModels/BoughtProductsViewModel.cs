using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;
using System.Collections.ObjectModel;


namespace Grocery.App.ViewModels
{
    public partial class BoughtProductsViewModel : BaseViewModel
    {
        private readonly IBoughtProductsService _boughtProductsService;

        [ObservableProperty]
        Product selectedProduct;
        public ObservableCollection<BoughtProducts> BoughtProductsList { get; set; } = [];
        public ObservableCollection<Product> Products { get; set; }

        public BoughtProductsViewModel(IBoughtProductsService boughtProductsService, IProductService productService)
        {
            _boughtProductsService = boughtProductsService;
            Products = new(productService.GetAll());
        }

        partial void OnSelectedProductChanged(Product? oldValue, Product newValue)
        {

            if (newValue != null)
            {
                // Get bought products for the selected product
                var boughtProducts = _boughtProductsService.Get(newValue.Id);

                // Add them to the observable collection
                foreach (var item in boughtProducts)
                {
                    BoughtProductsList.Add(item);
                }
            }
            else { BoughtProductsList.Clear(); }
        }

        [RelayCommand]
        public void NewSelectedProduct(Product product)
        {
            SelectedProduct = product;
        }
    }
}
