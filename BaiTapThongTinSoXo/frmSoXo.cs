using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaiTapThongTinSoXo
{
    public partial class frmSoXo : Form
    {
        private readonly DichVuSoXo soXoService;

        public frmSoXo()
        {
            InitializeComponent();

            DateTime now = DateTime.Now;
            dtpNgay.MinDate = now.AddDays(-6);
            dtpNgay.MaxDate = now;

            soXoService = new DichVuSoXo();
            rdMienBac.Checked = true;

            Load += FrmSoXo_Load;
            rdMienBac.CheckedChanged += Rd_CheckedChanged;
            rdMienTrung.CheckedChanged += Rd_CheckedChanged;
            rdMienNam.CheckedChanged += Rd_CheckedChanged;
            dtpNgay.ValueChanged += DtpNgay_ValueChanged;
            btnTim.Click += BtnTim_Click;

            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void FrmSoXo_Load(object sender, EventArgs e)
        {
            LoadKetQua();
        }

        private void Rd_CheckedChanged(object sender, EventArgs e)
        {
            LoadKetQua();
        }

        private void DtpNgay_ValueChanged(object sender, EventArgs e)
        {
            LoadKetQua();
        }

        private void LoadKetQua()
        {
            try
            {
                string mien = GetMien();
                List<KetQua> list = soXoService.LayKetQua(mien, dtpNgay.Value);

                dataGridView1.DataSource = null;
                if (list.Count > 0)
                {
                    dataGridView1.DataSource = list;
                }
                else
                {
                    MessageBox.Show("Không có dữ liệu ngày này.", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetMien()
        {
            if (rdMienTrung.Checked) return "trung";
            if (rdMienNam.Checked) return "nam";
            return "bac";
        }

        private void BtnTim_Click(object sender, EventArgs e)
        {
            string mien = GetMien();
            List<KetQua> list;
            try
            {
                list = soXoService.LayKetQua(mien, dtpNgay.Value);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string soCanDo = txtSoCanDo.Text.Trim();
            if (string.IsNullOrEmpty(soCanDo))
            {
                MessageBox.Show("Vui lòng nhập số cần dò.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var ketQuaTim = new List<KetQua>();
            foreach (var kq in list)
            {
                if (!string.IsNullOrEmpty(kq.SoTrung) && kq.SoTrung.Contains(soCanDo))
                    ketQuaTim.Add(kq);
            }

            dataGridView1.DataSource = null;
            if (ketQuaTim.Count > 0)
            {
                dataGridView1.DataSource = ketQuaTim;
            }
            else
            {
                MessageBox.Show("Không tìm thấy kết quả chứa số " + soCanDo,
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtSoCanDo.Clear();
            LoadKetQua();
        }
    }
}
