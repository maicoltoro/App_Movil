using Prueba_Aq_Colombia.ViewModels;

namespace Prueba_Aq_Colombia.Views;

public partial class TareasPage : ContentPage
{
	public TareasPage(TareaViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}