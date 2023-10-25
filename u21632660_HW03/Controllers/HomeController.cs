using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Net;
using PagedList;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using u21632660_HW03.Models;
using u21632660_HW03.Controllers;


namespace u21632660_HW03.Controllers
{
    public class HomeController : Controller
    {
        public LibraryEntities db_Library = new LibraryEntities();
        public async Task<ActionResult> Index(string filterstring, string filterbooks, string sortOrder, string currentFilter, int? page, int? pageBooks, string sortOrderbooks, string currentFilterbooks)
        {
            int pageSize = 15;
            int pageNumber = (page ?? 1);

            int pageSizeBooks = 15;
            int pageNumberBooks = (pageBooks ?? 1);

            ViewBag.CurrentSort = sortOrder;
            ViewBag.SortOrderBooks = sortOrderbooks;
            //
            if (filterstring != null)
            {
                page = 1;
            }
            else
            {
                filterstring = currentFilter;
            }

            if (filterbooks != null)
            {
                pageBooks = 1;
            }
            else
            {
                filterbooks = currentFilterbooks;
            }

            var student = await db_Library.students.ToListAsync();
            var borrow = await db_Library.borrows.ToListAsync();
            var book = await db_Library.books.ToListAsync(); ;
            var author = await db_Library.authors.ToListAsync();
            var type = await db_Library.types.ToListAsync();

            var LibDetails = new LibraryVM();

            LibDetails.Students = student.ToList().OrderByDescending(i => i.studentId).ToPagedList(pageNumber,pageSize);
            LibDetails.Borrows = borrow;
            LibDetails.Books = book.ToList().OrderByDescending(i => i.bookId).ToPagedList(pageNumberBooks,pageSizeBooks);
            LibDetails.Authors = author;
            LibDetails.BookTypes = type;

            ViewBag.CurrentFilter = filterstring;
            ViewBag.CurrentFilterbooks = filterbooks;

            return View(LibDetails);
        }
        public ActionResult Maintain()
        {
            return View();
        }

        public ActionResult Report()
        {
            return View();
        }

        public async Task<ActionResult> CreatStudent(string name, string surname, string birthdate, string gender, string stuClass, int? point)
        {
            try
            {
                db_Library.students.Add(new student
                {
                    name = name,
                    surname = surname,
                    birthdate = Convert.ToDateTime(birthdate),
                    classes = stuClass,
                    gender = gender,
                    point = point
                });

                await db_Library.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
        }

        public async Task<ActionResult> CreatBook(string name, int? pagecount, int? author, int? bookType, int? points)
        {
            try
            {
                db_Library.books.Add(new book
                {
                    name = name,
                    authorId = author,
                    point = points,
                    pagecount = pagecount,
                    typeId = bookType,
                });

                await db_Library.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
        }
    }
}