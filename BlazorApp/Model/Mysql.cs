using MySql.Data.MySqlClient;
using System;
using System.IO;

namespace BlazorApp.Model
{
    public class Mysql
    {
        public static bool Login(string email, string password)
        {
            string sql = $"select * from user where email='{email}' and password='{password}'";
            try
            {
                using (StreamReader streamReader = new StreamReader(new FileStream("mysql.ini", FileMode.Open)))
                using (MySqlConnection connection = new MySqlConnection(streamReader.ReadToEnd()))
                {
                    MySqlCommand command = new MySqlCommand()
                    {
                        Connection = connection,
                        CommandText = sql
                    };
                    connection.Open();
                    MySqlDataReader resultData = command.ExecuteReader();
                    if (resultData.Read() == false)
                    {
                        return false;
                    }
                    return true;
                }
            }
            catch{}
            return false;
        }
        public static bool CanIUseThisEmail(string email)
        {
            string sql = $"select * from user where email='{email}'";
            try
            {
                using (StreamReader streamReader = new StreamReader(new FileStream("mysql.ini", FileMode.Open)))
                using (MySqlConnection connection = new MySqlConnection(streamReader.ReadToEnd()))
                {
                    MySqlCommand command = new MySqlCommand()
                    {
                        Connection = connection,
                        CommandText = sql
                    };
                    connection.Open();
                    MySqlDataReader resultData = command.ExecuteReader();
                    if (resultData.HasRows)
                    {
                        return false;
                    }
                    return true;
                }
            }
            catch{}
            return false;
        }

        public static void Register(string email, string password)
        {
            string sql = $"insert into user (email, password) values ('{email}', '{password}')";
            try
            {
                using (StreamReader streamReader = new StreamReader(new FileStream("mysql.ini", FileMode.Open)))
                using (MySqlConnection connection = new MySqlConnection(streamReader.ReadToEnd()))
                {
                    MySqlCommand command = new MySqlCommand()
                    {
                        Connection = connection,
                        CommandText = sql
                    };
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch{}
        }
    }
}
