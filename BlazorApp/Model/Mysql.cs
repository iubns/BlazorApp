using MySql.Data.MySqlClient;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace BlazorApp.Model
{
    public class Mysql
    {
        public static bool Login(string email, string password)
        {
            password = GetHash(password);
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
            catch (Exception e)
            {
                using (StreamWriter streamWriter = new StreamWriter(new FileStream("log.ini", FileMode.OpenOrCreate)))
                    streamWriter.WriteLine(e.ToString());
            }
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
            catch(Exception e){
                using (StreamWriter streamWriter = new StreamWriter(new FileStream("log.ini", FileMode.OpenOrCreate)))
                    streamWriter.WriteLine(e.ToString());
            }
            return false;
        }

        public static void Register(string email, string password)
        {
            password = GetHash(password);
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

        private static string GetHash(string value)
        {
            value += "iubns's erp";
            byte[] keyArray = Encoding.UTF8.GetBytes(value);
            SHA1Managed enc = new SHA1Managed();
            byte[] encodedKey = enc.ComputeHash(enc.ComputeHash(keyArray));
            StringBuilder myBuilder = new StringBuilder(encodedKey.Length);

            foreach (byte b in encodedKey)
            {
                myBuilder.Append(b.ToString("X"));
            }
            return myBuilder.ToString();
        }
    }
}
