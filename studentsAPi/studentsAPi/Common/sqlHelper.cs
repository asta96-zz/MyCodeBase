using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace studentsAPi.Controllers
{
    public class sqlHelper
    {

        public static DataTable GetStudentData(int i)
        {
            DataTable dt = new DataTable();
            try
            {
                string connetionString;
                SqlConnection cnn;
                connetionString = "Data Source = IN2424616W1; Initial Catalog = workspace; Integrated Security = True";
                cnn = new SqlConnection(connetionString);
                cnn.Open();


                SqlCommand command;
                SqlDataReader dataReader;
                string sql, Output = "";
                sql = "select * from Persons where PersonID=" + i;
                command = new SqlCommand(sql, cnn);
                dataReader = command.ExecuteReader();
               
                dt.Load(dataReader);



                //MessageBox.Show("Connection Open  !");
                cnn.Close();
            }
            catch (Exception ex)
            {

            }

            return dt;
        }
    }
}