using pegabicho.shared.Endpoints.Base;

namespace pegabicho.shared.Endpoints
{
    public static class eLogin
    {
        public static string backoffice = $"{EndpointBase.login}backoffice";
        public static string customer = $"{EndpointBase.login}app/customer";
    }
}
