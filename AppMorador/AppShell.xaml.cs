using AppMorador.Views.Moradores;

namespace AppMorador;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute("_moradorViewModel", typeof(CadastroMoradorView));
	}
}
