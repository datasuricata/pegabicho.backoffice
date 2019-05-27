using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using pegabicho.backoffice.Request;
using pegabicho.backoffice.Request.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static pegabicho.backoffice.Models.Enums;

namespace pegabicho.backoffice.Controllers {
    public class CoreController : Controller
    {
        #region [ attributes ]
        private readonly WebService WebService;

        private string Token { get; set; }

        #endregion

        #region [ ctor ]
        public CoreController(WebService WebService) {
            this.WebService = WebService;
        }

        #endregion

        #region [ components ]
        protected List<SelectListItem> ToDropDown<T>(List<T> list, string text, string value) {
            List<SelectListItem> dropdown = new List<SelectListItem>();
            foreach (var item in list) {
                var sItem = new SelectListItem();
                sItem.Text = item.GetType().GetProperty(text).GetValue(item, null) as string;
                sItem.Value = item.GetType().GetProperty(value).GetValue(item, null) as string;
                dropdown.Add(sItem);
            }

            return dropdown.OrderBy(x => x.Text).ToList();
        }
        protected List<SelectListItem> ToEnumDropDown<T>() {
            List<SelectListItem> dropdown = new List<SelectListItem>();
            foreach (var item in Enum.GetValues(typeof(T))) {
                var sItem = new SelectListItem();
                sItem.Text = Enum.GetName(typeof(T), item);
                sItem.Value = ((int)item).ToString();

                dropdown.Add(sItem);
            }
            return dropdown.OrderBy(x => x.Text).ToList();
        }

        #endregion

        #region [ async api ]
        protected async Task<T> Get<T>(string endpoint) {
            GetToken();
            var request = new DataRequest<T>(WebService.Endpoint);
            var result = await request.Get(endpoint, Token);

            return result;
        }
        protected async Task<T> GetById<T>(string endpoint, string id) {
            GetToken();
            var request = new DataRequest<T>(WebService.Endpoint);
            var result = await request.GetById(endpoint, id, Token);

            return result;
        }
        protected async Task<T> Put<T>(string endpoint, object command) {
            GetToken();
            var request = new DataRequest<T>(WebService.Endpoint);
            var result = await request.Put(endpoint, command, Token);

            return result;
        }
        protected async Task<T> Post<T>(string endpoint, object command) {
            GetToken();
            var request = new DataRequest<T>(WebService.Endpoint);
            var result = await request.Post(endpoint, command, Token);

            return result;
        }
        protected async Task PostAnonymous<T>(string endpoint, object command) {
            GetToken();
            var request = new DataRequest<T>(WebService.Endpoint);
            await request.PostAnonymous(endpoint, command, Token);

        }

        #endregion

        #region [ session ]
        private void GetToken() {
            Token = HttpContext.Session.GetString("Token");
        }

        #endregion

        #region [ notification ]

        /// <summary>
        /// set tempdata messages
        /// </summary>
        /// <param name="msg">Write your message</param>
        /// <param name="msgType">Define your message type</param>
        protected void SetMessage(string msg, MsgType msgType)
        {
            switch (msgType)
            {
                case MsgType.Success:
                    TempData["Message"] = msg;
                    break;
                case MsgType.Error:
                    TempData["Error"] = msg;
                    break;
                case MsgType.Info:
                    TempData["Info"] = msg;
                    break;
            }
        }

        #endregion
    }
}
