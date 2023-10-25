using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace u21632660_HW03.Models
{
    public class LibraryVM
    {
        public PagedList.IPagedList<student> Students { get; set; }
        public PagedList.IPagedList<book> Books { get; set; }
        public List<type> BookTypes { get; set; }
        public List<author> Authors { get; set; }
        public List<borrow> Borrows { get; set; }
    }
}