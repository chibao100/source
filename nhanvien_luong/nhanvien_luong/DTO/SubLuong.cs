using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace nhanvien_luong.DTO
{
    public class SubLuong
    {
        public float heso{ get; set; }
        public float phucap { get; set; }
        public int count { get; set; }

        public SubLuong(float heso, float phucap, int count)
        {
            this.heso = heso;
            this.phucap = phucap;
            this.count = count;
        }
    }
}