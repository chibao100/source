using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TinhLuong.BUS;
using MyEntity;
using TinhLuong.DTO;
using TinhLuong.Presentaion;

namespace TinhLuong.Presentaion
{
    public partial class Main : Form
    {
        BUS_NhanVien mybus = new BUS_NhanVien();
        public Main()
        {
            InitializeComponent();
        }
        bool flag = false;
        private void Main_Load(object sender, EventArgs e)
        {
            List<nhanvien2> list = mybus.DanhSach();
            for (int i = 0; i < list.Count; i++)
            {
                string tenchucvu = mybus.TenChucVu(list[i].chucvu);
                dataGridView1.Rows.Add(list[i].id, list[i].ma, list[i].ten, tenchucvu, list[i].ngach);
            }
            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.Rows[0].Selected = false;
            }
        }

      
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (flag == false)
            {
                flag = true;
            }
            else
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    DataGridViewRow row = dataGridView1.SelectedRows[0];
                    string id = row.Cells[0].Value.ToString();
                    string ma = row.Cells[1].Value.ToString();
                    string ten = row.Cells[2].Value.ToString();
                    Form1 a = new Form1(id, ma, ten);
                    a.ShowDialog();
                   
                }
            }
           
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            flag = false;
            string ma = textBox1.Text;
            List<nhanvien2> list = mybus.Tim(ma);
            for (int i = 0; i < list.Count; i++)
            {
                string tenchucvu = mybus.TenChucVu(list[i].chucvu);
                dataGridView1.Rows.Add(list[i].id, list[i].ma, list[i].ten, tenchucvu, list[i].ngach);
            }
            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.Rows[0].Selected = false;
            }
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

       
    }
}
