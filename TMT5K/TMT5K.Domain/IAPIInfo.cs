
using System.Collections.Generic;

namespace TMT5K.Domain
{
    public interface IAPIInfo
    {
        string Endpoint { get; set; }
        string APIKey { get; set; }
        Dictionary<string, string> Arguments {get; set;}
    }
}
