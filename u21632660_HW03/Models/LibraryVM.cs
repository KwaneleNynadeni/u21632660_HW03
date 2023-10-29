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
        public PagedList.IPagedList<type> BookTypes { get; set; }
        public PagedList.IPagedList<author> Authors { get; set; }
        public PagedList.IPagedList<borrow> Borrows { get; set; }
        public List<type> BookTypesList { get; set; }
        public List<author> AuthorsList { get; set; }
        public List<borrow> BorrowsList { get; set; }
        public List<student> StudentsList { get; set; }
        public List<book> BooksList { get; set; }
        public string[] ArchiveFiles { get; set; }
    }
}