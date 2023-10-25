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
    public class StundentsController : Controller
    {
        public LibraryEntities db_Library = new LibraryEntities();

    }
}