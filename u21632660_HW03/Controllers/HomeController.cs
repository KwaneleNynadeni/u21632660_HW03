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
        public async Task<ActionResult> Index()
        {
            int pageSize = 15;
            int pageSizeBooks = 10;
            int pageNumber = (Request.QueryString["page1"] != null) ? int.Parse(Request.QueryString["page1"]) : 1;
            int pageNumberBooks = (Request.QueryString["page2"] != null) ? int.Parse(Request.QueryString["page2"]) : 1;


            var student = await db_Library.students.ToListAsync();
            var borrow = await db_Library.borrows.ToListAsync();
            var book = await db_Library.books.ToListAsync(); ;
            var author = await db_Library.authors.ToListAsync();
            var type = await db_Library.types.ToListAsync();

            var LibDetails = new LibraryVM();

            LibDetails.Students = student.ToList().OrderByDescending(i => i.studentId).ToPagedList(pageNumber,pageSize);
            LibDetails.BorrowsList = borrow;
            LibDetails.Books = book.ToList().OrderByDescending(i => i.bookId).ToPagedList(pageNumberBooks,pageSizeBooks);
            LibDetails.AuthorsList = author;
            LibDetails.BookTypesList = type;

            return View(LibDetails);
        }
        public async Task<ActionResult> Maintain()
        {
            int pageSize = 10;
            int pageSizeTypes= 10;
            int pageSizeBorrows = 10;
            int pageNumber = (Request.QueryString["pageAuthors"] != null) ? int.Parse(Request.QueryString["pageAuthors"]) : 1;
            int pageNumberTypes = (Request.QueryString["pageTypes"] != null) ? int.Parse(Request.QueryString["pageTypes"]) : 1;
            int pageNumberBorrows = (Request.QueryString["pageBorrows"] != null) ? int.Parse(Request.QueryString["pageBorrows"]) : 1;



            var student = await db_Library.students.ToListAsync();
            var borrow = await db_Library.borrows.ToListAsync();
            var book = await db_Library.books.ToListAsync(); ;
            var author = await db_Library.authors.ToListAsync();
            var type = await db_Library.types.ToListAsync();

            var LibDetails = new LibraryVM();

            LibDetails.StudentsList = student;
            LibDetails.Borrows = borrow.OrderByDescending(b => b.borrowId).ToPagedList(pageNumberBorrows, pageSizeBorrows);
            LibDetails.BooksList = book;
            LibDetails.Authors = author.OrderByDescending(a => a.authorId).ToPagedList(pageNumber, pageSize); ;
            LibDetails.BookTypes= type.OrderByDescending(t => t.typeId).ToPagedList(pageNumberTypes, pageSizeTypes); ;

            return View(LibDetails);
        }

        public async Task<ActionResult> Report()
        {
            var student = await db_Library.students.ToListAsync();
            var borrow = await db_Library.borrows.ToListAsync();
            var book = await db_Library.books.ToListAsync(); ;
            var author = await db_Library.authors.ToListAsync();
            var type = await db_Library.types.ToListAsync();

            var LibDetails = new LibraryVM();

            LibDetails.StudentsList = student;
            LibDetails.BorrowsList = borrow;
            LibDetails.BooksList = book;
            LibDetails.AuthorsList = author;
            LibDetails.BookTypesList = type;

            return View(LibDetails);
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

        public async Task<ActionResult> CreatAuthor(string name, string surname)
        {
            try
            {
                db_Library.authors.Add(new author
                {
                    name = name,
                    surname = surname,
                   
                });

                await db_Library.SaveChangesAsync();
                return RedirectToAction("Maintain");
            }
            catch (Exception)
            {
                return RedirectToAction("Maintain");
            }
        }
        public async Task<ActionResult> CreatType(string name)
        {
            try
            {
                db_Library.types.Add(new type
                {
                    name = name,         
                });

                await db_Library.SaveChangesAsync();
                return RedirectToAction("Maintain");
            }
            catch (Exception)
            {
                return RedirectToAction("Maintain");
            }
        }
        public async Task<ActionResult> CreatBorrow(int? student, int? book)
        {
            var date = DateTime.Now;
            var stringDate = date.ToString("yyyy-mm-dd hh:mm:ss");

            try
            {
                db_Library.borrows.Add(new borrow
                {
                    studentId = student,
                    bookId = book,
                    takenDate = Convert.ToDateTime(stringDate),
                    broughtDate = null
                });

                await db_Library.SaveChangesAsync();
                return RedirectToAction("Maintain");
            }
            catch (Exception)
            {
                return RedirectToAction("Maintain");
            }
        }
    }
}