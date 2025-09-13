using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Globalization;
using System.Text.RegularExpressions;

namespace BaiTapThongTinSoXo
{
    internal class DichVuSoXo
    {
        public List<KetQua> LayKetQua(string mien, DateTime ngay)
        {
            string rssUrl;
            if (mien == "bac")
                rssUrl = "https://xskt.com.vn/rss-feed/mien-bac-xsmb.rss";
            else if (mien == "trung")
                rssUrl = "https://xskt.com.vn/rss-feed/mien-trung-xsmt.rss";
            else
                rssUrl = "https://xskt.com.vn/rss-feed/mien-nam-xsmn.rss";

            var ketQuaList = new List<KetQua>();
            var doc = new XmlDocument();
            doc.Load(rssUrl);

            XmlNodeList items = doc.SelectNodes("//item");
            if (items == null) return ketQuaList;

            foreach (XmlNode item in items)
            {
                DateTime ngayItem = LayNgay(item);
                if (ngayItem.Date != ngay.Date) continue;

                string desc = item.SelectSingleNode("description")?.InnerText ?? "";
                if (string.IsNullOrWhiteSpace(desc)) continue;

                // Bỏ tag HTML
                desc = Regex.Replace(desc, "<.*?>", "").Trim();

                // Tách các cặp giải - số
                var matches = Regex.Matches(desc, @"(ĐB|[0-9]+):\s*([0-9\- ]+)");
                foreach (Match m in matches)
                {
                    string giaiRaw = m.Groups[1].Value.Trim();
                    string so = m.Groups[2].Value.Trim();

                    string giai = ChuanHoaGiai(giaiRaw);

                    ketQuaList.Add(new KetQua
                    {
                        Dai = TenMien(mien),
                        Giai = giai,
                        SoTrung = so,
                        Ngay = ngayItem
                    });
                }
            }

            return ketQuaList;
        }

        private DateTime LayNgay(XmlNode item)
        {
            string title = item.SelectSingleNode("title")?.InnerText ?? "";
            // Bắt "NGÀY dd/MM"
            var match = Regex.Match(title, @"NGÀY\s+(\d{1,2})/(\d{1,2})", RegexOptions.IgnoreCase);
            if (match.Success)
            {
                int day = int.Parse(match.Groups[1].Value);
                int month = int.Parse(match.Groups[2].Value);
                return new DateTime(DateTime.Now.Year, month, day);
            }
            return DateTime.MinValue;
        }

        private string ChuanHoaGiai(string text)
        {
            string t = text.ToUpper();
            if (t == "ĐB") return "Đặc biệt";
            if (t == "1") return "Giải nhất";
            if (t == "2") return "Giải nhì";
            if (t == "3") return "Giải ba";
            if (t == "4") return "Giải tư";
            if (t == "5") return "Giải năm";
            if (t == "6") return "Giải sáu";
            if (t == "7") return "Giải bảy";
            return text;
        }

        private string TenMien(string mien)
        {
            if (mien == "bac") return "Miền Bắc";
            if (mien == "trung") return "Miền Trung";
            return "Miền Nam";
        }
    }
}
