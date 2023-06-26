using AppMorador.Views.Moradores;

namespace AppMorador;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute("moradorViewModel", typeof(CadastroMoradorView));
	}
}
