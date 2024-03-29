﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace pegabicho.shared.Notifications {
    public class Notifier {
        // # properties
        public List<Notification> Notifications { get; set; }

        // # ctor
        public Notifier() => Notifications = new List<Notification>();

        // # validator
        public bool HasAny => Notifications.Any();
    }

    public class Notification {
        // # for service
        public string Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public string Date { get; set; }

        // # for server
        public int StatusCode { get; set; }
        public string[] Exception { get; set; }
        public string[] Call { get; set; }

        public Notification() {
            Id = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
            Date = DateTime.Now.ToString("MM/dd/yyyy H:mm");
            Value = "Ops! Algo deu errado.";
        }

        public Notification(string msg) {
            new Notification() { Value = msg };
        }
    }
}
