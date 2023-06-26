using AppMorador.Models;
using AppMorador.Services.Moradores;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppMorador.ViewModels.Moradores
{
    [QueryProperty("MoradorSelecionadoId", "mId")]
    public class CadastroMoradorViewModel : BaseViewModel
    {
        private MoradorService mService;


        public ICommand SalvarCommand { get; }
        public ICommand CancelarCommand { get; set; }

        public CadastroMoradorViewModel()
        {
            string token = Preferences.Get("UsuarioToken", string.Empty);
            mService = new MoradorService(token);

            SalvarCommand = new Command(async () => { await SalvarMorador(); });
            CancelarCommand = new Command(async () => CancelarCadastro());
        }

        private async void CancelarCadastro()
        {
            await Shell.Current.GoToAsync("..");
        }

        private int id;
        private string nome;
        private string cpf;
        private string telefone;
        private int idApartamento;

        public int Id
        {
            get => id;
            set
            {
                id = value;
                OnPropertyChanged();
            }
        }

        public string Nome
        {
            get => nome;
            set
            {
                nome = value;
                OnPropertyChanged();
            }
        }

        public string Cpf
        {
            get => cpf;
            set
            {
                cpf = value;
                OnPropertyChanged();
            }
        }

        public string Telefone
        {
            get => telefone;
            set
            {
                telefone = value;
                OnPropertyChanged();
            }
        }

        public int IdApartamento
        {
            get => idApartamento;
            set
            {
                idApartamento = value;
                OnPropertyChanged();
            }
        }

        public async Task SalvarMorador()
        {
            try
            {
                Morador model = new Morador()
                {
                    Id = this.id,
                    Nome = this.nome,
                    Cpf = this.cpf,
                    Telefone = this.telefone,
                    IdApartamento = this.idApartamento
                };
                if (model.Id == 0)
                    await mService.PostMoradorAsync(model);
                else
                    await mService.PutMoradorAsync(model);

                await Application.Current.MainPage.DisplayAlert("Mensagem", "Dados salvos com sucesso!", "Ok");

                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Ops", ex.Message + " Detalhes: " + ex.InnerException, "Ok");
            }
        }


        public async void CarregarMorador()
        {
            try
            {
                Morador m = await mService.GetMoradorAsync(int.Parse(moradorSelecionadoId));

                this.Id = m.Id;
                this.Nome = m.Nome;
                this.Cpf = m.Cpf;
                this.Telefone = m.Telefone;
                this.IdApartamento = m.IdApartamento;
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Ops", ex.Message + " Detalhes: " + ex.InnerException, "Ok");
            }
        }

        private string moradorSelecionadoId;
        public string MoradorSelecionadoId
        { 
            set
            {
                if (value != null)
                {
                    moradorSelecionadoId = Uri.UnescapeDataString(value);
                    CarregarMorador();
                }
            }
        }
    }
}
