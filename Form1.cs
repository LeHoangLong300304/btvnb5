using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace btapvnb5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Text files (*.txt)|*.txt|Rich Text Format (*.rtf)|*.rtf|All files (*.*)|*.*";
                openFileDialog.Title = "Chọn tập tin văn bản";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        if (openFileDialog.FileName.EndsWith(".txt"))
                        {
                            richText.LoadFile(openFileDialog.FileName, RichTextBoxStreamType.PlainText);
                        }
                        else if (openFileDialog.FileName.EndsWith(".rtf"))
                        {
                            richText.LoadFile(openFileDialog.FileName, RichTextBoxStreamType.RichText);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi mở tập tin: " + ex.Message);
                    }
                }
            }

        }
        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem == toolStripButtonNew) // Giả sử toolStripButtonNew là nút để tạo văn bản mới
            {
                // Xóa nội dung của RichTextBox
                richText.Clear();

                // Cập nhật tiêu đề của form
                this.Text = "New Document";
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            toolStripComboBox1.Text = "Tahoma";
            toolStripComboBox2.Text = "14";
            foreach (FontFamily font in new InstalledFontCollection().Families)
            {
                toolStripComboBox1.Items.Add(font.Name);
            }
            List<int> listSize = new List<int> { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 };
            foreach (var s in listSize)
            {
                toolStripComboBox2.Items.Add(s);
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            {
                TạoVănBảnMới();
            }
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            if (richText.SelectionFont != null)
            {
                Font currentFont = richText.SelectionFont;
                FontStyle newFontStyle = currentFont.Underline ? FontStyle.Regular : FontStyle.Underline;
                richText.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size, newFontStyle);
            }
            else
            {
                richText.SelectionFont = new Font(richText.Font.FontFamily, richText.Font.Size, FontStyle.Underline);
            }
        }

        private void toolStripComboBox2_Click(object sender, EventArgs e)
        {
            if (toolStripComboBox2.SelectedItem != null &&
        int.TryParse(toolStripComboBox2.SelectedItem.ToString(), out int selectedSize))
            {
                if (richText.SelectionLength > 0)
                {
                    richText.SelectionFont = new Font(richText.SelectionFont.FontFamily, selectedSize, richText.SelectionFont.Style);
                }
                else
                {
                    richText.Font = new Font(richText.Font.FontFamily, selectedSize, richText.Font.Style);
                }
            }
        }

        private void toolStripComboBox1_Click(object sender, EventArgs e)
        {
            if (toolStripComboBox1.SelectedItem != null)
            {
                string fontName = toolStripComboBox1.SelectedItem.ToString();
                if (richText.SelectionFont != null)
                {
                    richText.SelectionFont = new Font(fontName, richText.SelectionFont.Size, richText.SelectionFont.Style);
                }
                else
                {
                    richText.Font = new Font(fontName, richText.Font.Size, richText.Font.Style);
                }
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (richText.SelectionLength > 0)
            {
                richText.SelectionFont = new Font(richText.SelectionFont, richText.SelectionFont.Style | FontStyle.Bold);
            }
            else
            {
                richText.Font = new Font(richText.Font, richText.Font.Style | FontStyle.Bold);
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (richText.SelectionFont != null)
            {
                Font currentFont = richText.SelectionFont;
                FontStyle newFontStyle = currentFont.Italic ? FontStyle.Regular : FontStyle.Italic;
                richText.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size, newFontStyle);
            }
            else
            {
                richText.SelectionFont = new Font(richText.Font.FontFamily, richText.Font.Size, FontStyle.Italic);
            }
        }

        private void toolStripButtonNew_Click(object sender, EventArgs e)
        {
            TạoVănBảnMới();
        }
        private void TạoVănBảnMới()
        {
            richText.Clear();
            richText.Font = new Font("Tahoma", 14, FontStyle.Regular);

        }

        private bool isNewDocument = true;
        private string currentFilePath = string.Empty;
        private bool isDocumentModified = false;
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentFilePath))
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Rich Text Format (*.rtf)|*.rtf";
                    saveFileDialog.Title = "Lưu nội dung văn bản";
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {

                            richText.SaveFile(saveFileDialog.FileName, RichTextBoxStreamType.RichText);
                            currentFilePath = saveFileDialog.FileName;
                            isDocumentModified = false;
                            MessageBox.Show("Lưu văn bản thành công!");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Lỗi khi lưu tập tin: " + ex.Message);
                        }
                    }
                }
            }
            else
            {
                try
                {
                    richText.SaveFile(currentFilePath, RichTextBoxStreamType.RichText);
                    isDocumentModified = false;
                    MessageBox.Show("Văn bản đã được lưu thành công!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi lưu tập tin: " + ex.Message);
                }
            }
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentFilePath))
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Rich Text Format (*.rtf)|*.rtf";
                    saveFileDialog.Title = "Lưu nội dung văn bản";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            richText.SaveFile(saveFileDialog.FileName, RichTextBoxStreamType.RichText);
                            currentFilePath = saveFileDialog.FileName;
                            isDocumentModified = false;
                            MessageBox.Show("Lưu văn bản thành công!");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Lỗi khi lưu tập tin: " + ex.Message);
                        }
                    }
                }
            }
            else
            {

                try
                {
                    richText.SaveFile(currentFilePath, RichTextBoxStreamType.RichText);
                    isDocumentModified = false;
                    MessageBox.Show("Văn bản đã được lưu thành công!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi lưu tập tin: " + ex.Message);
                }
            }

        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát?", "Xác nhận thoát",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }

        }
    }
}
