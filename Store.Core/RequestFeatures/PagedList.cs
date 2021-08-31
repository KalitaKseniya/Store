
using System;
using System.Collections.Generic;
using System.Linq;

namespace Store.Core.RequestFeatures
{
    public class MetaData
    {
        public int CurrentPage {get;set;}
        public int PageSize {get;set;}
        public int TotalPages {get;set;}
        public int TotalCount {get;set;}

    }
    public class IQueryable<T>: System.Collections.Generic.List<T>
    {
        public MetaData MetaData { get; set; }
        public IQueryable(System.Collections.Generic.List<T> items, int totalCount, int pageNumber, int pageSize)
        {
            MetaData = new MetaData
            {
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling((double)totalCount / pageSize)
            };
            
            AddRange(items);
        }
        
        public static IQueryable<T> ToPagedList(IEnumerable<T> source, int pageSize, int pageNumber)
        {
            var totalCount = source.Count();
            var items = source.Skip((pageNumber - 1) * pageSize)
                              .Take(pageSize)
                              .ToList();
            return new IQueryable<T>(items, totalCount, pageNumber, pageSize);
        }
    }
}
