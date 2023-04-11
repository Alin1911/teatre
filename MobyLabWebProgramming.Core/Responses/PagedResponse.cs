namespace MobyLabWebProgramming.Core.Responses;

/// <summary>
/// This class encapsulated the response for a pagination request. 
/// </summary>
public class PagedResponse<T>
{
    /// <summary>
    /// Page is the number of the page.
    /// </summary>
    public uint Page { get; set; }
    /// <summary>
    /// PageSize is the maximum number of entries on each page.
    /// </summary>
    public uint PageSize { get; set; }
    /// <summary>
    /// TotalCount is the maximum number of entries that can be retrieved form the backend.
    /// It should be used by the client to determine the total number of pages for a given page size. 
    /// </summary>
    public uint TotalCount { get; set; }
    /// <summary>
    /// This is the collection of entries corresponding to the page requested.
    /// </summary>
    public List<T> Data { get; set; }

    public PagedResponse(uint page, uint pageSize, uint totalCount, List<T> data)
    {
        Page = page;
        PageSize = pageSize;
        TotalCount = totalCount;
        Data = data;
    }
}