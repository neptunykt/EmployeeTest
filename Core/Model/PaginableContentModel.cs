using System.Collections.Generic;

namespace Core.Model
{
    public class PaginableContentModel<T>
    {
        public PaginableContentModel()
        {
        }

        public PaginableContentModel(IEnumerable<T> pageItems, int totalItems, int currentPageIndex)
        {
            PageItems = pageItems;
            TotalItems = totalItems;
            CurrentPageIndex = currentPageIndex;
        }

        public IEnumerable<T> PageItems { get; set; }

        public int TotalItems { get; set; }

        public int CurrentPageIndex { get; set; }
    }
}