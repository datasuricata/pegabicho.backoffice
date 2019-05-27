using System;

namespace pegabicho.shared.Notifications {
    public class DomainNotifyer : Exception {

        public DomainNotifyer(string message) : base(message) {

        }

        public static void When(bool hasError, string error) {
            if (hasError) {
                throw new DomainNotifyer(error);
            }
        }
    }
}
