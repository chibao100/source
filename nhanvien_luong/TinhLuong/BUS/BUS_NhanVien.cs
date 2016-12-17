using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyEntity;
using TinhLuong.DTO;

namespace TinhLuong.BUS
{
    class BUS_NhanVien
    {
        salaryContext db = new salaryContext();

        public List<nhanvien2> DanhSach()
        {
            List<nhanvien> list = db.nhanvien.ToList();

            List<nhanvien2> list2 = new List<nhanvien2>();
            for (int i = 0; i < list.Count; i++)
            {
                var id_nhanvien = list[i].id;
                string ngach = "";
                string bac = "";
                var query2 = (from b in db.nhanvien_ngach
                              join c in db.ngach on b.id_ngach equals c.id
                              where b.id_nhanvien == id_nhanvien
                              select new
                              {
                                  ngach = c.ngach1,
                                  bac = b.bac,
                                  ngay = b.ngay
                              }).OrderByDescending(x => x.ngay);
                if (query2.Count() != 0)
                {
                    ngach = query2.FirstOrDefault().ngach;
                    bac = query2.FirstOrDefault().bac;
                }
               
                //
                string[] list_chucvu = new string[10];
                var MaxDate = (from d in db.nhanvien_chucvu
                               where d.id_nhanvien == id_nhanvien
                               select d.ngay).Max();

                var query3 = from b in db.nhanvien_chucvu
                             join c in db.chucvu on b.id_chucvu equals c.id
                             where b.id_nhanvien == id_nhanvien && b.ngay == MaxDate
                             select new
                             {
                                 ten = c.chuc_vu
                             };
                int v = 0;
                //
                if (query3 != null)
                {

                    foreach (var item in query3)
                    {
                        list_chucvu[v] = item.ten;
                        v++;
                    }
                }
                nhanvien2 h = new nhanvien2(id_nhanvien, list[i].ma, list[i].ten, list[i].gioi_tinh, list[i].ngay_sinh, list[i].dan_toc, list[i].ngay_vao_lam, list[i].dia_chi, list[i].so_cmnd, list_chucvu, ngach, bac);
                list2.Add(h);
            }
            return list2;
        }

        public List<nhanvien2> Tim(string ma)
        {
            List<nhanvien> list = db.nhanvien.Where(x => x.ma.Contains(ma)).ToList();

            List<nhanvien2> list2 = new List<nhanvien2>();
            for (int i = 0; i < list.Count; i++)
            {
                var id_nhanvien = list[i].id;
                string ngach = "";
                string bac = "";
                var query2 = (from b in db.nhanvien_ngach
                              join c in db.ngach on b.id_ngach equals c.id
                              where b.id_nhanvien == id_nhanvien
                              select new
                              {
                                  ngach = c.ngach1,
                                  bac = b.bac,
                                  ngay = b.ngay
                              }).OrderByDescending(x => x.ngay);
                if (query2.Count() != 0)
                {
                    ngach = query2.FirstOrDefault().ngach;
                    bac = query2.FirstOrDefault().bac;
                }
                //
                string[] list_chucvu = new string[10];
                var MaxDate = (from d in db.nhanvien_chucvu
                               where d.id_nhanvien == id_nhanvien
                               select d.ngay).Max();

                var query3 = from b in db.nhanvien_chucvu
                             join c in db.chucvu on b.id_chucvu equals c.id
                             where b.id_nhanvien == id_nhanvien && b.ngay == MaxDate
                             select new
                             {
                                 ten = c.chuc_vu
                             };
                int v = 0;
                //
                if (query3 != null)
                {

                    foreach (var item in query3)
                    {
                        list_chucvu[v] = item.ten;
                        v++;
                    }
                }
                nhanvien2 h = new nhanvien2(id_nhanvien, list[i].ma, list[i].ten, list[i].gioi_tinh, list[i].ngay_sinh, list[i].dan_toc, list[i].ngay_vao_lam, list[i].dia_chi, list[i].so_cmnd, list_chucvu, ngach, bac);
                list2.Add(h);
            }
            return list2;
        }


        public string TenChucVu(string[] list)
        {
            string s = "";
            for (int i = 0; i < list.Length; i++)
            {
                if (list[i] != null)
                {
                    s = s + list[i] + ", ";
                }
               
            }
            //s = s.Substring(0, s.Length - 2);
            return s;
        }
    }
}
