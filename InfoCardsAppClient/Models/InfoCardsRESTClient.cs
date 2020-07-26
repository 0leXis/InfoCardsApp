using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json;

namespace InfoCardsAppClient
{
    class InfoCardsRESTClient
    {
        public string ControllerUrl { get; }

        private HttpClient _client = new HttpClient();
        public InfoCardsRESTClient(string ControllerUrl)
        {
            this.ControllerUrl = ControllerUrl;
            _client.DefaultRequestHeaders.ConnectionClose = true;
            //Disable certificate validation
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
        }

        public async Task AddInfoCard(InfoCard card)
        {
            using (var content = new StringContent(JsonConvert.SerializeObject(card), Encoding.UTF8, "application/json"))
            {
                var data = await _client.PutAsync(ControllerUrl, content);
                if (!data.IsSuccessStatusCode)
                    if (data.StatusCode == HttpStatusCode.NotFound)
                        throw new HttpRequestException("Запрашиваемые данные не были найдены на сервере");
                    else
                        throw new HttpRequestException("Неизвестная ошибка");
            }
        }

        public async Task ChangeInfoCard(InfoCard card)
        {
            using (var content = new StringContent(JsonConvert.SerializeObject(card), Encoding.UTF8, "application/json"))
            {
                var data = await _client.PostAsync(ControllerUrl, content);
                if (!data.IsSuccessStatusCode)
                    if (data.StatusCode == HttpStatusCode.NotFound)
                        throw new HttpRequestException("Запрашиваемые данные не были найдены на сервере");
                    else
                        throw new HttpRequestException("Неизвестная ошибка");
            }
        }

        public async Task RemoveInfoCard(int cardId)
        {
            var data = await _client.DeleteAsync(ControllerUrl + "/" + cardId);
            if (!data.IsSuccessStatusCode)
                if (data.StatusCode == HttpStatusCode.NotFound)
                    throw new HttpRequestException("Запрашиваемые данные не были найдены на сервере");
                else
                    throw new HttpRequestException("Неизвестная ошибка");
        }

        public async Task<List<InfoCard>> GetInfoCardsList()
        {
            var data = await _client.GetAsync(ControllerUrl);
            if (!data.IsSuccessStatusCode)
                if(data.StatusCode == HttpStatusCode.NotFound)
                    throw new HttpRequestException("Запрашиваемые данные не были найдены на сервере");
                else
                    throw new HttpRequestException("Неизвестная ошибка");
            var json = await data.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<InfoCard>>(json);
        }
    }
}
