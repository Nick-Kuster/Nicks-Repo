using System;
using System.Collections.Generic;
using System.Text;

namespace TMT5K.Domain
{
    public class APIInfo : IAPIInfo
    {
        public string Endpoint { get; set; }
        public string APIKey { get; set; }
        public Dictionary<string, string> Arguments { get; set; }
    }
}
