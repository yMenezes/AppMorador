using AppMorador.Models;
using AppMorador.Services.Moradores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppMorador.ViewModels.Moradores
{
    public class ListagemMoradorViewModel : BaseViewModel
    {
        private MoradorService moradorService;

        public ObservableCollection<Morador> Moradores { get; set; }

        public ListagemMoradorViewModel() 
        {
            string token = Preferences.Get("UsuarioToken", string.Empty);
            moradorService = new MoradorService(token);
            Moradores = new ObservableCollection<Morador>();

            _ = ObterMoradores();

            NovoMorador = new Command(async () => { await ExibirCadastroMorador(); });
            RemoverMoradorCommand = new Command<Morador>(async (Morador m) => { await RemoverMorador(m); });
        }

        public ICommand NovoMorador { get; }
        public ICommand RemoverMoradorCommand { get; }

        public async Task ObterMoradores()
        {
            try
            {
                Moradores = await moradorService.GetMoradoresAsync();
                OnPropertyChanged(nameof(Moradores));
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Ops", ex.Message + "Detalhes: " + ex.InnerException, "Ok");
            }
        }

        public async Task ExibirCadastroMorador()
        {
            try
            {
                await Shell.Current.GoToAsync("_moradorViewModel");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Ops" ,  ex.Message + " Detalhes: " + ex.InnerException, "Ok");
            }
        }

        private Morador moradorSelecionado;

        public Morador MoradorSelecionado
        {
            get { return moradorSelecionado; }
            set
            {
                if (value != null)
                {
                    moradorSelecionado = value;
                    Shell.Current.GoToAsync($"_moradorViewModel?mId={moradorSelecionado.Id}");
                }
            }
        }

        public async Task RemoverMorador(Morador m)
        {
            try
            {
                if(await Application.Current.MainPage.DisplayAlert("Confirmação", $"Confirma a remoção de {m.Nome}?", "Sim", "Não"))
                {
                    await moradorService.DeleteMoradorAsync(m.Id);

                    await Application.Current.MainPage.DisplayAlert("Mensagem", "Personagem removido com sucesso!", "Ok");

                    _ = ObterMoradores();
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Ops", ex.Message + " Detalhes: " + ex.InnerException, "Ok");
            }
        }
    }
}
