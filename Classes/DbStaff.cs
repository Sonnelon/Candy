using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data;
using System.Data.SqlClient;


namespace CandyCat.Classes
{
   public class DbStaff
    {


        public List<Sweets> GetData()
        {
            List<Sweets > listOfSweet = new List<Sweets>();


            SqlConnection sqlConnection = new SqlConnection(Connection.connString);

            string query = " use [Konfetka] Select S.id, S._name , S.price , (Select R.[recipe] from [dbo].[Recipe] R where S.pecipe = R.id), (Select TOS.[typeOfSweet] " +
                "from [dbo].[TypeOfSweet] TOS  where TOS.id = S.Type ) , [image] from [dbo].[Sweets] S";

            try
            {

                sqlConnection.Open();

              
                SqlCommand SqlCmd = new SqlCommand(query, sqlConnection);

                SqlDataReader reader = SqlCmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        listOfSweet.Add(new Sweets(int.Parse(reader[0].ToString()), reader[1].ToString(), float.Parse(reader[2].ToString()), reader[3].ToString(), reader[4].ToString(), reader[5].ToString() ?? ""));

                    }
                }

            }

            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }


            return listOfSweet;
        }

    }
}
