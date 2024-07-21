using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;

namespace quanlythuvien.Model
{
    internal class NguoiDung
    {
        public MySqlConnection connection;
        public void KetNoi()
        {
            if (connection == null)
            {
                connection = new MySqlConnection();
                connection.ConnectionString = $"server=localhost;port=3306;user=root;password=080804Dat;database=qltv";
            }
            if (connection.State == System.Data.ConnectionState.Closed ||
                connection.State == System.Data.ConnectionState.Broken)
            {


                connection.Open();
            }
        }
        public void NgatKetNoi()
        {
            if (connection.State == System.Data.ConnectionState.Open)
                connection.Close();
        }
        // Thực hiện câu lệnh sql
        public int Sql(string SQL)
        {
            int rowsAffected = -1;

            try
            {
                KetNoi(); 
                MySqlCommand command = new MySqlCommand();
                command.Connection = connection;
                command.CommandText = SQL;
                rowsAffected = command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error updating data: " + ex.Message);
            }
            finally
            {
                NgatKetNoi(); 
            }

            return rowsAffected;
        }
        // Lấy id
        public int getId(string SQL)
        {
            int result = -1;
            try
            {
                KetNoi();
                MySqlCommand command = new MySqlCommand();
                command.Connection = connection;
                command.CommandText = SQL;
               object user = command.ExecuteScalar();
                if (user != null)
                {
                     int id = int.Parse( user.ToString());
                    return id;
                    
                } else
                {
                    return -1;
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error updating data: " + ex.Message);
            }
            finally
            {
                NgatKetNoi();
            }

            return result;
        }


        public int Login(string SQL)
        {
            int result = -1;
            string role;
            try
            {
                KetNoi();
                MySqlCommand command = new MySqlCommand();
                command.Connection = connection;
                command.CommandText = SQL;
                object user = command.ExecuteScalar();
                if (user != null)
                {
                    role = user.ToString();
                    if (role == "admin")
                    {
                        return 1;
                    }
                    else if (role == "user")
                    {
                        return 0;
                    }

                }
                else
                {
                    return -1;
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error updating data: " + ex.Message);
            }
            finally
            {
                NgatKetNoi();
            }

            return result;
        }

        public DataTable getUserById(int id)
        {
            DataTable dt = new DataTable();
            try
            {
                KetNoi();

                string SQL = $"Select * from nguoidung where id = '{id}'";
                MySqlDataAdapter adapter = new MySqlDataAdapter(SQL, connection);
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi lấy dữ liệu: " + ex.Message);
            }
            finally
            {
                NgatKetNoi();
            }
            return dt;
        }
        // Kiểm tra mật khẩu cũ đúng hay không
        public string getMatKhauById(string SQL)
        {
            try
            {
                KetNoi();
                MySqlCommand command = new MySqlCommand();
                command.Connection = connection;
                command.CommandText = SQL;
                object user = command.ExecuteScalar();
                if (user != null)
                {
                    
                    return user.ToString();

                }
               
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error updating data: " + ex.Message);
            }
            finally
            {
                NgatKetNoi();
            }

            return null;
        }

        public DataTable getUsers()
        {
            DataTable dt = new DataTable();
            try
            {
                KetNoi();

                string SQL = $"Select * from nguoidung ";
                MySqlDataAdapter adapter = new MySqlDataAdapter(SQL, connection);
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi lấy dữ liệu: " + ex.Message);
            }
            finally
            {
                NgatKetNoi();
            }
            return dt;
        }


        public DataTable getNguoidungTheoid(int id)
        {
            DataTable dt = new DataTable();
            try
            {
                KetNoi();

                string SQL = $"Select * from nguoidung where id = '{id}' ";
                MySqlDataAdapter adapter = new MySqlDataAdapter(SQL, connection);
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi lấy dữ liệu: " + ex.Message);
            }
            finally
            {
                NgatKetNoi();
            }
            return dt;
        }
    }
}
