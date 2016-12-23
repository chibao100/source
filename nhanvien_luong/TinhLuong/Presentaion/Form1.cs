using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyEntity;
using TinhLuong.DTO;
using TinhLuong.BUS;

using System.Text.RegularExpressions;
using System.Globalization;

namespace TinhLuong
{
    public partial class Form1 : Form
    {
        BUS_Luong Mybus;
        public Form1(string id, string ma, string ten)
        {
            InitializeComponent();
            //
            textBox4.Text = id;
            textBox1.Text = ma;
            
        }

      

        private void button1_Click(object sender, EventArgs e)
        {
           
            Mybus  = new BUS_Luong();
            int idnhanvien = Int32.Parse(textBox4.Text);
            int dinhmuc = Mybus.dinhmuc();
            DateTime ngay = dateTimePicker1.Value;

            Mybus.Tinh(idnhanvien, ngay);

            MessageBox.Show("Ok");
          
            //
            dataGridView1.Rows.Clear();
            dataGridView2.Rows.Clear();
            dataGridView3.Rows.Clear();
            //
            List<SubLuong> Ngach = Mybus.Ngach;
            List<SubLuong> ChucVu = Mybus.ChucVu;
            List<SubLuong> Luong = Mybus.Luong;
            //

            for (int i = 0; i < Ngach.Count; i++)
            {
                dataGridView2.Rows.Add(Ngach[i].heso, Ngach[i].phucap, Ngach[i].count);
            }
            //
            for (int i = 0; i < ChucVu.Count; i++)
            {
                dataGridView3.Rows.Add(ChucVu[i].heso, ChucVu[i].count);
            }
            //
            float total_luong = 0;
            for (int i = 0; i < Luong.Count; i++)
            {
                float luong = ((Luong[i].heso * dinhmuc) + (Luong[i].phucap * Luong[i].heso * dinhmuc)) * Luong[i].count / 30;
                total_luong = total_luong + luong;
                dataGridView1.Rows.Add(Luong[i].heso, Luong[i].phucap, Luong[i].count, luong.ToString("N0"));
            }
            float total_luong2 = total_luong - (total_luong * 0.07f);
            textBox3.Text = Mybus.HienThiTien(total_luong2);
             
        }

        
             
    }
}
