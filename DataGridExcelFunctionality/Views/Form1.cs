using DataGridExcelFunctionality.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataGridExcelFunctionality
{
    /// <summary>
    /// ACHTUNG:
    /// Bei datagridview 'ClipboardCopyMode' UNBEDINGT auf 'EnableWithoutHeaderText' setzen!!!
    /// </summary>
    public partial class Form1 : Form
    {
        private DataTable table;
        private Random random = new Random();

        public Form1()
        {
            InitializeComponent();

            var rowFiller = new RowFiller();

            table = new DataTable();
            table.Columns.Add("Name");
            table.Columns.Add("Ort");
            table.Columns.Add("Straße");
            rowFiller.addRows(10, table);


            dataGridView1.DataSource = table;
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
            {
                SetSelectedCellsNull();
            }
            else if (e.Control && e.KeyCode == Keys.V)
            {
                PasteToDataGridView();
            }
            else if (e.Control && e.KeyCode == Keys.X)
            {
                CutDataGridViewSelectedContent();
            }
        }

        private void CutDataGridViewSelectedContent()
        {
            Clipboard.SetText(dataGridView1.GetClipboardContent().GetText());
            SetSelectedCellsNull();
        }

        private void PasteToDataGridView()
        {
            var firstSelectedCell = dataGridView1.SelectedCells
                .Cast<DataGridViewCell>()
                .OrderBy(c => c.RowIndex)
                .ThenBy(c => dataGridView1.Columns[c.ColumnIndex].DisplayIndex).First();
            var x = dataGridView1.Columns[firstSelectedCell.ColumnIndex].DisplayIndex;
            var y = firstSelectedCell.RowIndex;

            string[][] items = Clipboard.GetText()
                .Split(new string[] { "\r\n" }, StringSplitOptions.None)
                .Select(row => row.Split('\t').ToArray()).ToArray();

            if (dataGridView1.SelectedCells.Count == 0)
            {
                return;
            }
            else if (dataGridView1.Rows.Count < (y + items.Count()))
            {
                return;
            }
            else if (dataGridView1.Columns.Count < (x + items.First().Count()))
            {
                return;
            }

            int DisplayIndexToValueIndex(int displayIndex)
            {
                return dataGridView1.Columns.Cast<DataGridViewColumn>()
                    .Where(c => c.DisplayIndex == displayIndex)
                    .Single()
                    .Index;
            }

            for (int yi = 0; yi < items.Count(); yi++)
            {
                for (int xi = 0; xi < items.First().Count(); xi++)
                {
                    dataGridView1[DisplayIndexToValueIndex(x + xi), y + yi].Value = items[yi][xi];
                }
            }

        }

        private void SetSelectedCellsNull()
        {
            foreach (DataGridViewCell cell in dataGridView1.SelectedCells)
            {
                cell.Value = DBNull.Value;
            }
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int rowIndex = dataGridView1.HitTest(e.X, e.Y).RowIndex;
                int colIndex = dataGridView1.HitTest(e.X, e.Y).ColumnIndex;

                if (rowIndex >= 0 && colIndex >= 0)
                {
                    if (!dataGridView1[colIndex, rowIndex].Selected)
                    {
                        dataGridView1.ClearSelection();
                        dataGridView1[colIndex, rowIndex].Selected = true;
                    }
                    dataGridView1[colIndex, rowIndex].Selected = true;
                    contextMenuStrip1.Show(dataGridView1, e.Location);
                }
            }
        }

        private void kopierenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(dataGridView1.GetClipboardContent().GetText());
        }

        private void ausschneidenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CutDataGridViewSelectedContent();
        }

        private void einfügenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PasteToDataGridView();
        }

        private void löschenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetSelectedCellsNull();
        }
    }
}
