using System;

namespace Diversos.Core.Models
{
    [Serializable]
    public partial class RequestFilter
    {
        public int PageSize { get; set; } = 10;
        public int CurrentPage { get; set; } = 1;
        public string Filter { get; set; } = "";

        public RequestFilter() { }
    }
}
