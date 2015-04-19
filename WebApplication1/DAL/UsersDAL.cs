using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppWN.ServerSide
{
    public class UsersDAL
    {
        private MySqlConnection conn = null;
        private MySqlConnectionStringBuilder sb = null;
        private MySqlCommand cmd = null;
        public UsersDAL()
        {
            /*
            sb = new MySqlConnectionStringBuilder();
            sb.Server = GeneralMethods.getSbServer();
            sb.UserID = GeneralMethods.getSbUserID();
            sb.Password = GeneralMethods.getSbPassword();
            sb.Database = GeneralMethods.getSbDataBase();
             * */
            sb = new MySqlConnectionStringBuilder();
            sb.Server = "f37fa280-507d-4166-b70e-a427013f0c94.mysql.sequelizer.com";
            sb.UserID = "lewtprebbcrycgkb";
            sb.Password = "S5zS2ExvQqZQrUK8dwSJvpv5dSvED4RwmijLrG55TEesXBTrAR3QDXPCGDPijZZU";
            sb.Database = "db1dca19b55ffe4e06a129a4650083dc91";
            sb.CharacterSet = "utf8";
            try
            {
                conn = new MySqlConnection(sb.ToString());
            }
            catch(MySqlException e)
            {
                Console.WriteLine("Error: {0}", e.ToString());
            }
        }
        public int GetBusinessID(string userName, string userPassword) // Return User Full Name If Exists
        {
            int businessId=0;
            conn.Open();
            string query =
                "select businessID from Users where Users.userName = '" + userName + "' and Users.userPassword = '" + userPassword + "'";
            cmd = new MySqlCommand(query, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            
            if(rdr.Read())
            {
                if (rdr[0] != DBNull.Value)
                    businessId = rdr.GetInt32(0);
                else
                    businessId = 0;              
            }
            rdr.Close();
            conn.Close();  
            return businessId;
        }

    }
}