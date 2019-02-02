using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataGridExcelFunctionality
{
    public partial class Form1 : Form
    {
        private DataTable table;
        private Random random = new Random();

        public Form1()
        {
            InitializeComponent();

            table = new DataTable();
            table.Columns.Add("Name");
            table.Columns.Add("Ort");
            addRows(10);


            dataGridView1.DataSource = table;
        }

        private void addRows(int v)
        {
            for (int i = 0; i < v; i++)
            {
                var row = table.NewRow();
                row[0] = GetRandomString();
                row[1] = GetRandomString();
                table.Rows.Add(row);
            }
        }

        private string GetRandomString()
        {
            var words = new string[]
            {
                "sponge",
"uptight",
"report",
"start",
"amusing",
"toys",
"beef",
"tense",
"store",
"dusty",
"grease",
"nonchalant",
"expert",
"boat",
"talk",
"push",
"liquid",
"circle",
"deceive",
"behavior",
"wide",
"psychotic",
"tasteful",
"synonymous",
"moaning",
"shock",
"hose",
"bells",
"hot",
"health",
"aloof",
"neat",
"adjoining",
"comb",
"zebra",
"oafish",
"early",
"zesty",
"rule",
"brown",
"fact",
"heavenly",
"thoughtless",
"elated",
"fireman",
"guiltless",
"greedy",
"appreciate",
"chemical",
"magenta",
"oranges",
"sour",
"oatmeal",
"lyrical",
"ring",
"hilarious",
"friend",
"minute",
"picture",
"pocket",
"spiders",
"serious",
"nappy",
"noise",
"standing",
"nebulous",
"fragile",
"bite",
"drain",
"seed",
"gabby",
"wind",
"terrible",
"purple",
"interest",
"three",
"hook",
"note",
"book",
"eye",
"messy",
"efficient",
"protest",
"thunder",
"desert",
"safe",
"pet",
"squalid",
"office",
"condemned",
"wail",
"spiky",
"bee",
"goofy",
"pump",
"trashy",
"describe",
"symptomatic",
"abnormal",
"breezy",
            };
            return words[random.Next(words.Count() - 1)];
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
                if (!dataGridView1.Rows[cell.RowIndex].Selected)
                {
                    cell.Value = DBNull.Value;
                }
            }
        }
    }
}
