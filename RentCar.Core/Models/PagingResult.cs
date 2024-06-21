using System;

namespace Diversos.Core.Models
{
    [Serializable]
    public class PagingResult : RequestFilter, IDisposable
    {
        public int TotalRecords { get; set; }
        public int TotalPage
        {
            get
            {
                if (TotalRecords <= PageSize)
                    return 0;

                return (int)Math.Ceiling((double)TotalRecords / (double)PageSize);
            }
        }

        public int? PreviousPage
        {
            get
            {
                if (CurrentPage == 1)
                    return null;

                return CurrentPage - 1;
            }
        }

        public int? NextPage
        {
            get
            {
                if (CurrentPage == TotalPage)
                    return null;

                return CurrentPage + 1;
            }
        }

        public string Descripcion
        {
            get
            {
                if (TotalRecords <= 0)
                    return "0 registros";

                
                return string.Empty;
            }
        }

        public PagingResult() { }

        public PagingResult(int totalRecords) 
        {
            TotalRecords = totalRecords;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
