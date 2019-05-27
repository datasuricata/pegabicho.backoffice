﻿using Newtonsoft.Json;
using pegabicho.backoffice.Request.Base;
using System.Threading.Tasks;
using static pegabicho.backoffice.Models.Enums;

namespace pegabicho.backoffice.Request {
    public class DataRequest<T> : BaseRequest {
        public DataRequest(string baseUri) : base(baseUri) {
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

            return JsonConvert.DeserializeObject<T>(retorno);
        }

        public async Task PostAnonymous(string endpoint, object command, string token = "") {
            var response = await SendAsync(RequestMethod.Post, endpoint, command, token);
            var retorno = await response.Content.ReadAsStringAsync();
        }
    }
}
