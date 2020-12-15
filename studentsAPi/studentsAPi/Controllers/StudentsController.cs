using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace studentsAPi.Controllers
{
    public class StudentsController : ApiController
    {
        [HttpGet]
        public Persons Voice(int i)
        {
            Persons p1 = new Persons();
            DataTable dt = sqlHelper.GetStudentData(i);
            p1.FirstName = Convert.ToString(dt.Rows[0]["FirstName"]);
            p1.LastName= Convert.ToString(dt.Rows[0]["LastName"]);
            p1.Address= Convert.ToString(dt.Rows[0]["Address"]);
            p1.City= Convert.ToString(dt.Rows[0]["City"]);
            return p1;

        }
    }
}
