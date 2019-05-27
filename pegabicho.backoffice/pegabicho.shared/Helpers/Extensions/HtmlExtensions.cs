using System;

namespace pegabicho.shared.Helpers.Extensions {
    public static class HtmlExtensions {
        public static string AjustHtml(this string value) {
            return value.Replace(Environment.NewLine, "").Replace("\"", "'");
        }
    }
}
