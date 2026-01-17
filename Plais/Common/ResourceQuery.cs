namespace Plais.Common
{
    public class ResourceQuery
    {
        public string? SearchPhrase { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? SortBy { get; set; }
        public SortDirection SortDirection { get; set; }

    }
}
