using Prueba_Aq_Colombia.ViewModels;

namespace Prueba_Aq_Colombia
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }

}
