using Core.Entities;
using Core.Model;
using MediatR;

namespace Core.Requests
{
    public class GetEmployeesPage : IRequest<PaginableContentModel<Employee>>
    {
        public GetEmployeesPage(int pageIndex, int pageSize, string orderBy, OrderDirection? orderDirection, string searchText, string searchDate)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            OrderBy = orderBy;
            OrderDirection = orderDirection;
            SearchText = searchText;
            SearchDate = searchDate;
        }

        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        
        public string OrderBy { get; set; }
        public string SearchText { get; set; }
        
        public string SearchDate { get; set; }
        public OrderDirection? OrderDirection { get; set; }
    }
}