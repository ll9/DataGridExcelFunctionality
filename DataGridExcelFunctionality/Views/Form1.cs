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
            return string.Join("", Enumerable.Range(0, 10).Select(i => (char)random.Next(65, 127)));
        }
    }
}
