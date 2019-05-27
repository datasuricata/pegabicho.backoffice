using Microsoft.Extensions.Configuration;

namespace pegabicho.backoffice.Request.Base {
    public class WebService {
        private readonly IConfiguration config;

        private static string environment = string.Empty;

        public WebService(IConfiguration config) {
            this.config = config;
            environment = config["Endpoint"];
        }

        /// <summary>
        /// Hasta Endpoint Web API 2
        /// </summary>
        public string Endpoint {
            get {
                switch (environment) {
                    case "local": return "http://localhost:11455/";
                    case "dev": return "";
                    default: return "http://localhost:11455/";
                }
            }
        }

    }
}
