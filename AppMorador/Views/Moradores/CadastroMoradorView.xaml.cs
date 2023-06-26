using AppMorador.ViewModels.Moradores;

namespace AppMorador.Views.Moradores;

public partial class CadastroMoradorView : ContentPage
{
	private CadastroMoradorViewModel moradorViewModel;
	public CadastroMoradorView()
	{
		InitializeComponent();

        moradorViewModel = new CadastroMoradorViewModel();
		BindingContext = moradorViewModel;
		Title = "Novo Morador";
	}
}