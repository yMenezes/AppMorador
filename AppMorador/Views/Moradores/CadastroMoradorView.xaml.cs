using AppMorador.ViewModels.Moradores;

namespace AppMorador.Views.Moradores;

public partial class CadastroMoradorView : ContentPage
{
	private CadastroMoradorViewModel _moradorViewModel;
	public CadastroMoradorView()
	{
		InitializeComponent();

		_moradorViewModel = new CadastroMoradorViewModel();
		BindingContext = _moradorViewModel;
		Title = "Novo Morador";
	}
}