using FW.Common.Enum;

namespace FW.Common.Pagination.Interfaces
{
    public interface IPaginationInfo
    {
        int ItemsPerPage { get; set; }

        int TotalItems { get; set; }

        int CurrentPage { get; set; }

        int ItemsToSkip { get; set; }

        int TotalPages { get; }

        EPaginationType PaginationType { get; set; }
    }
}
