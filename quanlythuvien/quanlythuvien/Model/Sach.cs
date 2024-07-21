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
    internal class Sach
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
        public DataTable getBooks()
        {
            DataTable dt = new DataTable();
            try
            {
               KetNoi();

                string SQL = $"Select * from sach";
                MySqlDataAdapter adapter = new MySqlDataAdapter(SQL, connection);
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lấy dữ liệu: " + ex.Message);
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
        public DataTable findBooks(string name, string author)
        {
            DataTable dt = new DataTable();
            try
            {
                KetNoi();
                string SQL;
                if (String.IsNullOrEmpty(name))
                {
                     SQL = $"Select * from sach where TacGia Like '%{author}%' ";

                }
                else if (String.IsNullOrEmpty(author)) {
                    SQL = $"Select * from sach where TenSach Like '%{name}%' ";
                } else
                {
                 SQL = $"Select * from sach where TenSach Like '%{name}%' and TacGia Like '%{author}%' ";

                }
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
        // Lấy sách theo MaSach
        public DataTable getBookById(int id)
        {
            DataTable dt = new DataTable();
            try
            {
                KetNoi();

                string SQL = $"Select * from sach where MaSach = '{id}'";
                MySqlDataAdapter adapter = new MySqlDataAdapter(SQL, connection);
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lấy dữ liệu: " + ex.Message);
            }
            finally
            {
                NgatKetNoi();
            }
            return dt;
        }

        public int muonSach(int userId, int maSach, int gia )
        {
            int rowsAffected = -1;

            try
            {
                KetNoi();
                DateTime currentDate = DateTime.Now;
                string dateNow = currentDate.ToString("yyyy-MM-dd");
                MySqlCommand command = new MySqlCommand();
                command.Connection = connection;
                command.CommandText = $"Insert into phieumuonsach (MaNguoiMuon, MaSach, NgayMuon) Values ('{userId}', '{maSach}', '{dateNow}')";
                rowsAffected = command.ExecuteNonQuery();
                command.CommandText = $"Insert into hoadon (MaNguoiMuon, NgayLapHoaDon, MaSach , TongTien, TrangThai) Values ('{userId}', '{dateNow}', '{maSach}', '{gia}', 'Chưa thanh toán')";
                rowsAffected += command.ExecuteNonQuery();
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

        public DataTable dsSachDaMuon(int userId)
        {
            DataTable dt = new DataTable();
            try
            {
                KetNoi();

                string SQL = $"Select s.TenSach, s.TacGia, s.NamXuatBan, s.NhaXuatBan, s.Gia , s.NgayNhap from sach s join phieumuonsach p on s.Masach = p.MaSach where p.MaNguoiMuon = '{userId}'";
                MySqlDataAdapter adapter = new MySqlDataAdapter(SQL, connection);
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lấy dữ liệu: " + ex.Message);
            }
            finally
            {
                NgatKetNoi();
            }
            return dt;
        }

        public DataTable dsSachChuaThanhToan(int userId)
        {
            DataTable dt = new DataTable();
            try
            {
                KetNoi();

                string SQL = $"Select DISTINCT h.MaHoaDon,  s.TenSach, s.TacGia, s.NamXuatBan, s.NhaXuatBan, s.Gia , s.NgayNhap from sach s join hoadon h on s.Masach = h.MaSach where h.MaNguoiMuon = '{userId}' and h.TrangThai = 'Chưa thanh toán'";
                MySqlDataAdapter adapter = new MySqlDataAdapter(SQL, connection);
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lấy dữ liệu: " + ex.Message);
            }
            finally
            {
                NgatKetNoi();
            }
            return dt;
        }

        public int thanhToan(int userId, int maHD, int gia)
        {
            int rowsAffected = -1;

            try
            {
                KetNoi();
                DateTime currentDate = DateTime.Now;
                string dateNow = currentDate.ToString("yyyy-MM-dd");
                MySqlCommand command = new MySqlCommand();
                command.Connection = connection;
                command.CommandText = $"Insert into phieuthutien (MaNguoiThanhToan,  NgayThanhToan, MaHD) Values ('{userId}', '{dateNow}', '{maHD}')";
                rowsAffected = command.ExecuteNonQuery();
                command.CommandText = $"Update hoadon set TrangThai = 'Đã thanh toán' where MaHoaDon = '{maHD}'";
                rowsAffected += command.ExecuteNonQuery();
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

        public DataTable getSachTheoMaSach(int maSach)
        {
            DataTable dt = new DataTable();
            try
            {
                KetNoi();

                string SQL = $"Select * from sach where MaSach = '{maSach}' ";
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

        public DataTable getTop10Books()
        {
            DataTable dt = new DataTable();
            try
            {
                KetNoi();

                string SQL = $"Select * from sach order by NgayNhap desc limit 10";
                MySqlDataAdapter adapter = new MySqlDataAdapter(SQL, connection);
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lấy dữ liệu: " + ex.Message);
            }
            finally
            {
                NgatKetNoi();
            }
            return dt;
        }
    }
    }
