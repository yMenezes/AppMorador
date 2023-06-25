using AppMorador.ViewModels.Moradores;

namespace AppMorador.Views.Moradores;

public partial class ListagemView : ContentPage
{

	ListagemMoradorViewModel viewModel;

	public ListagemView()
	{
		InitializeComponent();

		viewModel = new ListagemMoradorViewModel();
		BindingContext = viewModel;
		Title = "Morador - App Morador";
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
		_ = viewModel.ObterMoradores();
    }
}