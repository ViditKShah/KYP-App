using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace KYP.API.Helpers
{
    public class PagedList<T> : List<T>
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }

        public PagedList(List<T> items, int totalItemsCount, int pageNumber, 
            int pageSize)
        {
            CurrentPage = pageNumber;
            PageSize = pageSize;
            TotalItems = totalItemsCount;
            TotalPages = (int)Math.Ceiling(totalItemsCount/ (double)pageSize);
            this.AddRange(items);
        }

        public static async Task<PagedList<T>> CreateAsync(IQueryable<T> source, 
            int pageNumber, int pageSize) 
        {
            var totalItemsCount = await source.CountAsync();
            
            var items = await source.Skip((pageNumber - 1)* pageSize)
                                    .Take(pageSize).ToListAsync();

            return new PagedList<T>(items, totalItemsCount, pageNumber, pageSize);
        }
    }
}