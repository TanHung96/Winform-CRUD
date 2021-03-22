using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NguyenDangTanHung_5951071126
{
    public partial class Form1 : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-SQL77HJC\SQLEXPRESS;Initial Catalog=DemoCRUD2;Integrated Security=True");
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GetStudentsRecord();
        }
        private void GetStudentsRecord()
        {

            SqlCommand cmd = new SqlCommand("SELECT * from StudentsTB", con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();
            dgvSV.DataSource = dt;


        }
        private bool IsValidData()
        {
            if (txtHSV.Text == string.Empty
                || txtTSV.Text == string.Empty
                || txtDiaChi.Text == string.Empty ||
                string.IsNullOrEmpty(txtSDT.Text) ||
                 string.IsNullOrEmpty(txtSBD.Text))
            {
                MessageBox.Show("Có chỗ chưa nhập dữ liệu !!!", "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;

            }
            return true;

        }
        private void btnadd_Click(object sender, EventArgs e)
        {
            if (IsValidData())
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO StudentsTb VALUES (@Name,@FatherName,@rollnumber,@address,@Moblie)", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Name", txtHSV.Text);
                cmd.Parameters.AddWithValue("@FatherName", txtTSV.Text);
                cmd.Parameters.AddWithValue("@rollNumber", txtSBD.Text);
                cmd.Parameters.AddWithValue("@address", txtDiaChi.Text);
                cmd.Parameters.AddWithValue("@Moblie", txtSDT.Text);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                GetStudentsRecord();

            }
        }
        public int StudentID;
        private void dgvSV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            StudentID = Convert.ToInt32(dgvSV.Rows[index].Cells[0].Value);
            txtHSV.Text = dgvSV.Rows[index].Cells[1].Value.ToString();
            txtTSV.Text = dgvSV.Rows[index].Cells[2].Value.ToString();
            txtSBD.Text = dgvSV.Rows[index].Cells[3].Value.ToString();
            txtDiaChi.Text = dgvSV.Rows[index].Cells[4].Value.ToString();
            txtSDT.Text = dgvSV.Rows[index].Cells[5].Value.ToString();
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            if (IsValidData())
            {
                SqlCommand cmd = new SqlCommand("UPDATE StudentsTb SET Name = @Name, FatherName = @FartherName,rollnumber = @rollnumber,address = @address,moblie = @moblie WHERE StudentID =@ID", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Name", txtHSV.Text);
                cmd.Parameters.AddWithValue("@FartherName", txtTSV.Text);
                cmd.Parameters.AddWithValue("@rollNumber", txtSBD.Text);
                cmd.Parameters.AddWithValue("@address", txtDiaChi.Text);
                cmd.Parameters.AddWithValue("@moblie", txtSDT.Text);
                cmd.Parameters.AddWithValue("@ID", this.StudentID);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                GetStudentsRecord();

            }
            else
            {
                MessageBox.Show("Cập nhật lỗi !!!", "Lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (IsValidData())
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM StudentsTb WHERE StudentID =@ID", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@ID", this.StudentID);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                GetStudentsRecord();

            }
            else
            {
                MessageBox.Show("Cập nhật lỗi !!!", "Lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("BẠN CÓ MUỐN THOÁT", "THÔNG BÁO", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                this.Close();
            }
        }
    }
}
