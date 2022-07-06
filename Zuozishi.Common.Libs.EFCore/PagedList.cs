using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace Zuozishi.Common.Libs.EFCore
{
    /// <summary>
    /// 分页类
    /// </summary>
    public class PagedList<T>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
        public List<T> Items { get; set; }

        public PagedList() { }

        public PagedList(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalCount = count;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            Items = new List<T>(items);
        }

        [JsonIgnore]
        public bool HasPreviousPage
        {
            get
            {
                return (PageIndex > 1);
            }
        }

        [JsonIgnore]
        public bool HasNextPage
        {
            get
            {
                return (PageIndex < TotalPages);
            }
        }

        public static PagedList<T> Create(IQueryable<T> source, int pageIndex, int pageSize)
        {
            if (pageIndex <= 0) pageIndex = 1;
            if (pageSize <= 0) pageSize = 20;
            var count = source.Count();
            var totalPages = (int)Math.Ceiling(count / (double)pageSize);
            if (totalPages < pageIndex) return new PagedList<T>(new List<T>(), count, pageIndex, pageSize);
            var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return new PagedList<T>(items, count, pageIndex, pageSize);
        }

        public static async Task<PagedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PagedList<T>(items, count, pageIndex, pageSize);
        }
    }
}
