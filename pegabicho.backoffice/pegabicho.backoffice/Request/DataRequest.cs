using Newtonsoft.Json;
using pegabicho.backoffice.Request.Base;
using pegabicho.shared.Notifications;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using static pegabicho.backoffice.Models.Enums;

namespace pegabicho.backoffice.Request {
    public class DataRequest<T> : BaseRequest {
        public DataRequest(string baseUri) : base(baseUri) {
        }


        private async Task HandleResponse(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var notifications = JsonConvert.DeserializeObject<List<Notification>>(content);
                if (response.StatusCode == HttpStatusCode.BadRequest)
                    throw new DomainNotifyer(notifications.FirstOrDefault()?.Value);
                else
                    throw new DomainNotifyer("Ocorreu um erro.");
            }
        }

        public async Task<T> Get(string endpoint, string token = "") {
            var response = await SendAsync(RequestMethod.Get, endpoint, null, token);
            var retorno = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(retorno);
        }

        public async Task<T> GetById(string endpoint, string id, string token = "") {
            var response = await SendAsync(RequestMethod.Get, $"{endpoint}?id={id}", null, token);
            var retorno = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(retorno);
        }

        public async Task<T> Put(string endpoint, object command, string token) {
            var response = await SendAsync(RequestMethod.Put, endpoint, command, token);
            var retorno = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(retorno);
        }

        public async Task<T> Post(string endpoint, object command, string token = "") {
            var response = await SendAsync(RequestMethod.Post, endpoint, command, token);
            var retorno = await response.Content.ReadAsStringAsync();

            await HandleResponse(response);

            return JsonConvert.DeserializeObject<T>(retorno);
        }

        public async Task PostAnonymous(string endpoint, object command, string token = "") {
            var response = await SendAsync(RequestMethod.Post, endpoint, command, token);
            var retorno = await response.Content.ReadAsStringAsync();
        }
    }
}
