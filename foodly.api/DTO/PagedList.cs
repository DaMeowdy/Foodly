using Microsoft.EntityFrameworkCore;

namespace foodly.api.DTO;

public class PagedList<T> where T : class
{
    private PagedList(List<T> _items, int _page, int _pageSize, int _count)
    {
        Items = _items;
        Page = _page;
        PageSize = _pageSize;
        ItemCount = _count;
    }
    public List<T> Items { get; }
    public int Page { get; }
    public int PageSize { get; }
    public int ItemCount { get; }
    public bool HasNextPage => Page * PageSize < ItemCount;
    public bool HasPreviousPage => Page > 1;
    public static async Task<PagedList<T>> CreateAsync(IQueryable<T> query, int page, int pageSize)
    {
        var count = await query.CountAsync();
        var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        return new(items, page, pageSize, count);
    }
}