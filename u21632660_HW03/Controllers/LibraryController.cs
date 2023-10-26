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
    public class LibraryController : Controller
    {
        //Connection to database using Ent. Framework
        public LibraryEntities db_Library = new LibraryEntities();

        //Async actions that return views
        public async Task<ActionResult> AuthorEditor(int authorId)
        {
            var author = await db_Library.authors.ToListAsync();
            var LibDetails = new LibraryVM();

            LibDetails.AuthorsList = author.Where(aut => aut.authorId == authorId).ToList();

            return View(LibDetails);
        }

        public async Task<ActionResult> TypeEditor(int typeId)
        {
            var types = await db_Library.types.ToListAsync();
            var LibDetails = new LibraryVM();

            LibDetails.BookTypesList = types.Where(t => t.typeId == typeId).ToList();

            return View(LibDetails);
        }
        public async Task<ActionResult> BorrowEditor(int borrowId)
        {
            var borrows = await db_Library.borrows.ToListAsync();
            var student = await db_Library.students.ToListAsync();
            var book = await db_Library.books.ToListAsync(); 

            var LibDetails = new LibraryVM();

            LibDetails.BorrowsList = borrows.Where(t => t.borrowId == borrowId).ToList();
            LibDetails.StudentsList = student.ToList();
            LibDetails.BooksList = book.ToList();

            return View(LibDetails);
        }

        //All functions bellow are async actions that proccess the data into the database (async edits)
        public async Task<ActionResult> EditAuthor(int authorId, string name, string surname)
        {
            var authorToEdit = db_Library.authors.Where(a => a.authorId == authorId).FirstOrDefault();

            try
            {
                authorToEdit.name = name;
                authorToEdit.surname = surname;

                await db_Library.SaveChangesAsync();
                return RedirectToAction("Maintain", "Home");
            }
            catch (Exception)
            {
                return RedirectToAction("Maintain", "Home");
            }
        }
        public async Task<ActionResult> EditType(int typeId, string name)
        {
            var typeToEdit = db_Library.types.Where(t => t.typeId == typeId).FirstOrDefault();

            try
            {
                typeToEdit.name = name;

                await db_Library.SaveChangesAsync();
                return RedirectToAction("Maintain", "Home");
            }
            catch (Exception)
            {
                return RedirectToAction("Maintain", "Home");
            }
        }
        public async Task<ActionResult> EditBorrow(int borrowId, string dateIn, int? student, int? book)
        {
            var borrowToEdit = db_Library.borrows.Where(b => b.borrowId == borrowId).FirstOrDefault();

            try
            {
                borrowToEdit.studentId = student;
                borrowToEdit.bookId = book;
                borrowToEdit.broughtDate = Convert.ToDateTime(dateIn);

                await db_Library.SaveChangesAsync();
                return RedirectToAction("Maintain", "Home");
            }
            catch (Exception)
            {
                return RedirectToAction("Maintain", "Home");
            }
        }

        //All functions bellow are async actions that proccess the data out of the database (async Delete)
        public async Task<ActionResult> DelAuthor(int authorId)
        {
            var authorToDel = db_Library.authors.Where(a => a.authorId == authorId).FirstOrDefault();

            try
            {
                //delete item found by id
                db_Library.authors.Remove(authorToDel);

                //save changes asysnchronusly
                await db_Library.SaveChangesAsync();

                //redirect
                return RedirectToAction("Maintain", "Home");
            }
            catch (Exception)
            {
                return RedirectToAction("Maintain", "Home");
            }
        }
        public async Task<ActionResult> DelType(int typeId)
        {
            var typeToDel = db_Library.types.Where(b => b.typeId == typeId).FirstOrDefault();

            try
            {
                //delete item found by id
                db_Library.types.Remove(typeToDel);

                //save changes asysnchronusly
                await db_Library.SaveChangesAsync();

                //redirect
                return RedirectToAction("Maintain", "Home");
            }
            catch (Exception)
            {
                return RedirectToAction("Maintain", "Home");
            }
        }
        public async Task<ActionResult> DelBorrow(int borrowId)
        {
            var borrowToDel = db_Library.borrows.Where(b => b.borrowId == borrowId).FirstOrDefault();

            try
            {
                //delete item found by id
                db_Library.borrows.Remove(borrowToDel);

                //save changes asysnchronusly
                await db_Library.SaveChangesAsync();

                //redirect
                return RedirectToAction("Maintain", "Home");
            }
            catch (Exception)
            {
                return RedirectToAction("Maintain", "Home");
            }
        }

    }
}