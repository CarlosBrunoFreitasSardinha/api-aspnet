using System;
using System.Collections.Generic;
using System.Text;

namespace CB.BackDefault.Application.Aggregates.AuthAggregate.ViewModels
{
    internal class JwtViewModel
    {
        public string Secret { get; set; }
        public int ExpirationInHours { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}
