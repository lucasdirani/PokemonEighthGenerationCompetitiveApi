namespace EighthGenerationCompetitive.Api.Parameters
{
    /// <summary>
    /// Specifies which items of a query will be returned
    /// </summary>
    public abstract class PaginationParameters
    {
        private const int MaxPageSize = 20;

        private int _pageSize = MaxPageSize;

        /// <summary>
        /// Determines the starting point of search for results.
        /// </summary>
        public int PageNumber { get; set; } = 1;

        /// <summary>
        /// Determines how many items will be returned per page.
        /// </summary>
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
            }
        }
    }
}