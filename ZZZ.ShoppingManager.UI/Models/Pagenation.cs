using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZZZ.ShoppingManager.UI.Models
{
    public class Pagenation
    {
        private int _recondsCount = 0;
        private int _pageSize = 0;
        public Pagenation(int recondsCount,int pageSize)
        {
            this._recondsCount = recondsCount;
            this._pageSize = pageSize;
        }
        public int First { get { return 1; } }
        public int Last { get { return PageCount; } }
        public int PageNo { get; set; }
        public int Prev { get { return PageNo - 1 < 1 ? 1 : PageNo - 1; } }
        public int Next { get { return PageNo + 1 > PageCount ? PageCount : PageNo + 1; } }
        public int PageSize { get { return this._pageSize; } }
        public int PageCount { get { return (int)Math.Ceiling(_recondsCount * 1.0 / _pageSize); } }
    }
}