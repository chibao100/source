using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using MyEntity;
using TinhLuong.DTO;
using System.Globalization;
using System.Windows.Forms;
using System.Text.RegularExpressions;
namespace TinhLuong
{
    class BUS_Luong
    {
        salaryContext db;

        public List<SubLuong> Luong{get;set;}
        public List<SubLuong> ChucVu { get; set; }
        public List<SubLuong> Ngach { get; set; }

        CultureInfo culture = (CultureInfo)CultureInfo.CurrentCulture.Clone();
        
           
        public BUS_Luong()
        {
            Luong = new List<SubLuong>();
            ChucVu = new List<SubLuong>();
            Ngach = new List<SubLuong>();
            db = new salaryContext();
            culture.NumberFormat.NumberDecimalSeparator = ",";
        }



        public void Tinh(int id_nhanvien, DateTime ngay)
        {
            List<nhanvien_ngach> a = db.nhanvien_ngach.Where(x => x.id_nhanvien == id_nhanvien).OrderBy(y => y.ngay).ToList();

            List<nhanvien_chucvu> b = db.nhanvien_chucvu.Where(x => x.id_nhanvien == id_nhanvien && x.totnhat == "true").OrderBy(y => y.ngay).ToList();
           
            DateTime limit_day = ngay;
            DateTime firt_day = new DateTime(limit_day.Year, limit_day.Month, 1);
            //
            for (DateTime i = firt_day; i <= limit_day; i = i.AddDays(1))
            {
                SubLuong ngach1 = ngach(a, i);
                SubLuong chucvu1 = chucvu(b, i);
                //Ngach
                if (Ngach.Count == 0)
                {
                    Ngach.Add(ngach1);
                }
                else
                {
                    int k = -1;
                    for (int j = 0; j < Ngach.Count; j++)
                    {
                        if (Ngach[j].heso == ngach1.heso && Ngach[j].phucap == ngach1.phucap)
                        {
                            k = j;
                            break;
                        }
                    }
                    if (k == -1)
                    {
                        Ngach.Add(ngach1);
                    }
                    else
                    {
                        Ngach[k].count = Ngach[k].count + 1;
                    }
                }

                //ChucVu
                if (ChucVu.Count == 0)
                {
                    ChucVu.Add(chucvu1);
                }
                else
                {
                    int k = -1;
                    for (int j = 0; j < ChucVu.Count; j++)
                    {
                        if (ChucVu[j].heso == chucvu1.heso)
                        {
                            k = j;
                            break;
                        }
                    }
                    if (k == -1)
                    {
                        ChucVu.Add(chucvu1);
                    }
                    else
                    {
                        ChucVu[k].count = ChucVu[k].count + 1;
                    }
                }

                //Luong
                if (Luong.Count == 0)
                {
                    SubLuong y = new SubLuong(ngach1.heso + chucvu1.heso, ngach1.phucap, ngach1.count);
                    Luong.Add(y);
                }
                else
                {
                    int k = -1;
                    SubLuong y = new SubLuong(ngach1.heso + chucvu1.heso, ngach1.phucap, ngach1.count);
                    for (int j = 0; j < Luong.Count; j++)
                    {
                        if (Luong[j].heso == y.heso && Luong[j].phucap == y.phucap)
                        {
                            k = j;
                            break;
                        }
                    }
                    if (k == -1)
                    {
                        Luong.Add(y);
                    }
                    else
                    {
                        Luong[k].count = Luong[k].count + 1;
                    }
                }
                
            }
        }

        private SubLuong ngach(List<nhanvien_ngach> a, DateTime i)
        {
            float heso = 0;
            float phucap = 0;
            int count = 1;
            nhanvien_ngach b = new nhanvien_ngach { id_ngach = 0, id_nhanvien = 0, bac = "0", ngay = i.AddDays(1)};
            if (a.Count == 0)
            {
                return new SubLuong(heso, phucap, count);
            }
            else
            {
                List<nhanvien_ngach> w = new List<nhanvien_ngach>();
                for (int v = 0; v < a.Count; v++)
                {
                    w.Add(a[v]);
                }
                w.Add(b);   
               
                for (int j = 0; j < w.Count - 1; j++)
                {
                    DateTime date1 = (DateTime)w[j].ngay;
                    DateTime date2 = (DateTime)w[j + 1].ngay;
                    //
                    if (DateTime.Compare(i.Date, date1.Date) >= 0 && DateTime.Compare(i.Date, date2.Date) < 0)
                    {
                        int id_ngach = w[j].id_ngach;
                        ngach t = db.ngach.Where(x => x.id == id_ngach).FirstOrDefault<ngach>();
                        if (t != null)
                        {
                            HeSoNgach(t, w[j].bac, ref heso, ref phucap);
                        }
                       
                        break;
                    }
                }
                return new SubLuong(heso, phucap, count);
            }             
        }
        private SubLuong chucvu(List<nhanvien_chucvu> a, DateTime i)
        {
            float heso = 0;
            float phucap = 0;
            int count = 1;
            nhanvien_chucvu b = new nhanvien_chucvu { id_chucvu = 0, id_nhanvien = 0, ngay = i.AddDays(1), totnhat = "false" };
            if (a.Count == 0)
            {
                return new SubLuong(heso, phucap, count);
            }
            else
            {
                List<nhanvien_chucvu> w = new List<nhanvien_chucvu>();
                for (int v = 0; v < a.Count; v++)
                {
                    w.Add(a[v]);
                }
                w.Add(b);
                
                for (int j = 0; j < w.Count - 1; j++)
                {
                    DateTime date1 = (DateTime)w[j].ngay;
                    DateTime date2 = (DateTime)w[j + 1].ngay;
                    //
                    
                    if (DateTime.Compare(i.Date,date1.Date) >= 0 &&DateTime.Compare(i.Date,date2.Date) < 0)
                    {
                       
                        int id_chucvu = w[j].id_chucvu;
                        chucvu t = db.chucvu.Where(x => x.id == id_chucvu).FirstOrDefault<chucvu>();
                        if (t != null)
                        {
                            heso = t.he_so;
                        }
                        
                        break;
                    }
                }
            }
            return new SubLuong(heso, phucap, count);
        }


        private void HeSoNgach(ngach t, string bac, ref float heso,ref float phucap)
        {

            switch (bac)
            {
                case "1": heso = heso + float.Parse(t.C_1, culture);
                    break;
                case "2": heso = heso + float.Parse(t.C_2, culture);
                    break;
                case "3": heso = heso + float.Parse(t.C_3, culture);
                    break;
                case "4": heso = heso + float.Parse(t.C_4, culture);
                    break;
                case "5": heso = heso + float.Parse(t.C_5, culture);
                    break;
                case "6":
                    if (t.C_6.Contains("VK"))
                    {
                        heso = heso + float.Parse(t.C_5, culture);
                        phucap = phucap + float.Parse(t.C_6.Remove(0, 3), culture);
                    }
                    else
                    {
                        heso = heso + float.Parse(t.C_6, culture);
                    }
                    break;
                case "7":
                    if (t.C_7.Contains("VK"))
                    {
                        heso = heso + float.Parse(t.C_5, culture);
                        phucap = phucap + float.Parse(t.C_7.Remove(0, 3), culture);
                    }
                    else
                    {
                        heso = heso + float.Parse(t.C_7, culture);
                    }
                    break;

                case "8":
                    if (t.C_7.Contains("VK"))
                    {
                        heso = heso + float.Parse(t.C_5, culture);
                        phucap = phucap + float.Parse(t.C_8.Remove(0, 3), culture);
                    }
                    else
                    {
                        heso = heso + float.Parse(t.C_7, culture);
                        phucap = phucap + float.Parse(t.C_8.Remove(0, 3), culture);
                    }
                    break;
            }
        }


        public string HienThiTien(float total_luong2)
        {
            CultureInfo cul = CultureInfo.GetCultureInfo("vi-VN");
            String luongString = double.Parse(total_luong2.ToString()).ToString("#,###", cul.NumberFormat);
            return luongString + "  VNĐ";
        }


        public int dinhmuc()
        {
            var query = from b in db.luong
                        where b.id == 1
                        select b.dinhmuc;
            int dinhmuc = query.Single();
            return dinhmuc;
        }
    }
}
