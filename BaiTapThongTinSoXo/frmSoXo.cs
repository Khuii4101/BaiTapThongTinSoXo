using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaiTapThongTinSoXo
{
    public partial class frmSoXo : Form
    {
        private readonly DichVuSoXo soXoService;
        private bool _loading = false;

        // RSS của xskt chỉ chứa vài ngày gần đây
        private const int RSS_WINDOW_DAYS = 8;

        public frmSoXo()
        {
            InitializeComponent();

            soXoService = new DichVuSoXo();
            rdMienBac.Checked = true;

            Load += FrmSoXo_Load;
            rdMienBac.CheckedChanged += Rd_CheckedChanged;
            rdMienTrung.CheckedChanged += Rd_CheckedChanged;
            rdMienNam.CheckedChanged += Rd_CheckedChanged;
            dtpNgay.ValueChanged += DtpNgay_ValueChanged;
            btnTim.Click += BtnTim_Click;
            cmbTinh.SelectedIndexChanged += CmbTinh_SelectedIndexChanged;

            // DataGridView hiển thị đủ SoTrung
            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.Columns.Clear();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
            dataGridView1.DataBindingComplete += DataGridView1_DataBindingComplete;
        }

        private void DataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (!dataGridView1.Columns.Contains("SoTrung")) return;

            var c = dataGridView1.Columns["SoTrung"];
            c.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            c.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            c.FillWeight = 320f;

            if (dataGridView1.Columns.Contains("Dai")) dataGridView1.Columns["Dai"].FillWeight = 80f;
            if (dataGridView1.Columns.Contains("Giai")) dataGridView1.Columns["Giai"].FillWeight = 90f;
            if (dataGridView1.Columns.Contains("Ngay")) dataGridView1.Columns["Ngay"].FillWeight = 100f;
            if (dataGridView1.Columns.Contains("Tinh")) dataGridView1.Columns["Tinh"].FillWeight = 120f;

            dataGridView1.RowTemplate.Height = 36;
        }

        private async void FrmSoXo_Load(object sender, EventArgs e)
        {
            _loading = true;
            cmbTinh.DataSource = new List<string> { "Tất cả" };
            cmbTinh.Enabled = false;
            _loading = false;

            await LoadKetQuaAsync(false);
        }

        private async void Rd_CheckedChanged(object sender, EventArgs e)
        {
            if (_loading) return;
            await LoadKetQuaAsync(false);
        }

        private async void DtpNgay_ValueChanged(object sender, EventArgs e)
        {
            if (_loading) return;
            await LoadKetQuaAsync(false);
        }

        private async void CmbTinh_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_loading) return;
            await LoadKetQuaAsync(false);
        }

        private string GetMien()
        {
            if (rdMienTrung.Checked) return "trung";
            if (rdMienNam.Checked) return "nam";
            return "bac";
        }

        private void UpdateTinhFromList(string mien, List<KetQua> list)
        {
            var current = cmbTinh.SelectedItem as string ?? "Tất cả";

            var provinces = list
                .Select(k => (k.Tinh ?? "").Trim())
                .Where(s => !string.IsNullOrEmpty(s))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .OrderBy(s => s, StringComparer.OrdinalIgnoreCase)
                .ToList();

            if (mien != "bac") provinces.Insert(0, "Tất cả");

            _loading = true;
            cmbTinh.DataSource = provinces;
            cmbTinh.Enabled = (mien != "bac") && provinces.Count > 0;
            if (provinces.Contains(current, StringComparer.OrdinalIgnoreCase))
                cmbTinh.SelectedItem = provinces.First(p => string.Equals(p, current, StringComparison.OrdinalIgnoreCase));
            _loading = false;
        }

        // showEmptyMessage: true khi người dùng chủ động (Làm mới/Tìm)
        private async Task LoadKetQuaAsync(bool showEmptyMessage)
        {
            if (_loading) return;
            _loading = true;

            try
            {
                string mien = GetMien();
                DateTime pickDate = dtpNgay.Value.Date;

                bool useRss = (DateTime.Today - pickDate).TotalDays <= RSS_WINDOW_DAYS;
                List<KetQua> list;

                if (useRss)
                {
                    var all = await soXoService.LayKetQuaAsync(mien, pickDate);
                    list = all.Where(k => k.Ngay.Date == pickDate).ToList();
                    if (list.Count == 0)
                        list = await soXoService.LayKetQuaNgayAsync(mien, pickDate);
                }
                else
                {
                    list = await soXoService.LayKetQuaNgayAsync(mien, pickDate);
                }

                if (list.Count == 0)
                {
                    dataGridView1.DataSource = null;
                    UpdateTinhFromList(mien, list);
                    if (showEmptyMessage)
                        MessageBox.Show("Không tìm thấy dữ liệu cho ngày đã chọn.", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                UpdateTinhFromList(mien, list);

                string tinh = cmbTinh.SelectedItem?.ToString() ?? "Tất cả";
                if (mien != "bac" && !string.IsNullOrEmpty(tinh) && tinh != "Tất cả")
                    list = list.Where(k => string.Equals(k.Tinh, tinh, StringComparison.OrdinalIgnoreCase)).ToList();

                dataGridView1.DataSource = null;
                dataGridView1.DataSource = list;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _loading = false;
            }
        }

        private async void BtnTim_Click(object sender, EventArgs e)
        {
            if (_loading) return;

            string mien = GetMien();
            string tinh = cmbTinh.SelectedItem?.ToString() ?? "Tất cả";

            List<KetQua> list;
            try
            {
                DateTime pick = dtpNgay.Value.Date;
                bool useRss = (DateTime.Today - pick).TotalDays <= RSS_WINDOW_DAYS;

                if (useRss)
                {
                    var all = await soXoService.LayKetQuaAsync(mien, pick);
                    list = all.Where(k => k.Ngay.Date == pick).ToList();
                    if (list.Count == 0)
                        list = await soXoService.LayKetQuaNgayAsync(mien, pick);
                }
                else
                {
                    list = await soXoService.LayKetQuaNgayAsync(mien, pick);
                }

                if (mien != "bac" && !string.IsNullOrEmpty(tinh) && tinh != "Tất cả")
                    list = list.Where(k => string.Equals(k.Tinh, tinh, StringComparison.OrdinalIgnoreCase)).ToList();
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

            string Clean(string s) => Regex.Replace(s ?? "", @"\D+", "");
            string pattern = Clean(soCanDo);
            if (pattern.Length == 0)
            {
                MessageBox.Show("Số cần dò không hợp lệ.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool MatchAnyToken(string soTrung)
            {
                var tokens = Regex.Split(soTrung ?? "", @"[\s,\-;,]+")
                                  .Select(Clean)
                                  .Where(x => !string.IsNullOrEmpty(x));
                foreach (var tk in tokens)
                    if (tk.EndsWith(pattern)) return true;
                return false;
            }

            var ketQuaTim = list.Where(k => MatchAnyToken(k.SoTrung)).ToList();

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = ketQuaTim.Count > 0 ? ketQuaTim : list;

            if (ketQuaTim.Count == 0)
            {
                MessageBox.Show($"Không tìm thấy kết quả có đuôi {pattern}.",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private async void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtSoCanDo.Clear();
            await LoadKetQuaAsync(true);
        }
    }
}
