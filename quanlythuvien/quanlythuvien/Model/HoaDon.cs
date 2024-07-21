using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace quanlythuvien.Model
{
    internal class HoaDon
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

        public DataTable getHoaDon()
        {
            DataTable dt = new DataTable();
            try
            {
                KetNoi();
               
               string  SQL = $"Select * from hoadon ";
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

        public DataTable getHoaDonTheoMaHD(int maHD)
        {
            DataTable dt = new DataTable();
            try
            {
                KetNoi();

                string SQL = $"Select * from hoadon where MaHoaDon = '{maHD}' ";
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
                MessageBox.Show("Error : " + ex.Message);
            }
            finally
            {
                NgatKetNoi();
            }

            return rowsAffected;
        }

        public int ThongKeHoaDon(int nam, int thang, int ngay)
        {
            try
            {
                KetNoi();
                MySqlCommand command = new MySqlCommand();
                command.Connection = connection;
                string sql;
                if (nam <= 0)
                {
                    sql = $"select sum(TongTien) from hoadon where Month(NgayLapHoaDon) = '{thang}' and Day(NgayLapHoaDon) = '{ngay}' and TrangThai =  'Đã thanh toán' ";
                }
                else if (thang <= 0)
                {
                    sql = $"select sum(TongTien) from hoadon where Year(NgayLapHoaDon) = '{nam}'";

                }
                else if (ngay <= 0) {
                    sql = $"select sum(TongTien) from hoadon where Year(NgayLapHoaDon) = '{nam}' and Month(NgayLapHoaDon) = '{thang}' and TrangThai =  'Đã thanh toán'";
                } else {
                    sql = $"select sum(TongTien) from hoadon where NgayLapHoaDon = '{nam:0000}-{thang:00}-{ngay:00}' and TrangThai =  'Đã thanh toán'";

                }
                command.CommandText = sql;
                object total = command.ExecuteScalar();
                if (string.IsNullOrEmpty(total.ToString()))
                {
                    return 0; 
                }
                else
                {
                    return int.Parse(total.ToString()); 
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

            return 0;
        }
    }
}
