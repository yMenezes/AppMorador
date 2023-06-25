using AppMorador.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppMorador.Services.Moradores
{
    public class MoradorService : Request
    {
        private readonly Request _request;
        private const string apiUrlBase = "http://VitorC-RafaelM.somee.com/ApiMorador/Moradores";

        private string _token;

        public MoradorService(string token)
        {
            _request = new Request();
            _token = token;
        }

        public async Task<ObservableCollection<Morador>> GetMoradoresAsync()
        {
            string urlComplementar = string.Format("{0}", "/GetAll");
            ObservableCollection<Models.Morador> moradores = await _request.GetAsync<ObservableCollection<Models.Morador>>(apiUrlBase + urlComplementar, _token);
            return moradores;

        }

        public async Task<Morador> GetMoradorAsync(int moradorId)
        {
            string urlComplementar = string.Format("/{0}", moradorId);
            var morador = await _request.GetAsync<Models.Morador>(apiUrlBase + urlComplementar, _token);
            return morador;
        }

        public async Task<int> PostMoradorAsync(Morador m)
        {
            return await _request.PostReturnIntTokenAsync(apiUrlBase, m, _token);
        }

        public async Task<int> PutMoradorAsync(Morador m)
        {
            var result = await _request.PutAsync(apiUrlBase, m, _token);
            return result;
        }

        public async Task<int> DeleteMoradorAsync(int moradorId)
        {
            string urlComplementar = string.Format("/{0}", moradorId);
            var result = await _request.DeleteAsync(apiUrlBase + urlComplementar, _token);
            return result;
        }
    }

    
}
