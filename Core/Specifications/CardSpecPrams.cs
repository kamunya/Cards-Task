using System;
using System.Collections.Generic;
using System.Text;

namespace Cards.Core.Specifications
{
    public class CardSpecPrams
    {
        private const int MaxPageSize = 50;
        public int pageIndex { get; set; } = 1;
        private int _pageSize =6;
        public int pageSize {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
        public string Color { get; set; }
        public string Status { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string sort { get; set; }
        public string _search;
        public string Search
        {
            get => _search;
            set => _search = value.ToLower();
        }

    }
}