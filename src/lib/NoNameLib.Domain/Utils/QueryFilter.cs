namespace NoNameLib.Domain.Utils
{
    public abstract class QueryFilter
    {
        public int PageSize { get; set; } = 15;
        public int PageCount { get; set; } = 1;
        public int PageOffset { get; set; }
    }
}
