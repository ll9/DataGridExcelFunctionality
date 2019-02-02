using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGridExcelFunctionality.Utils
{
    internal class RowFiller
    {
        private Random random = new Random();

        public void addRows(int n, DataTable table)
        {
            for (int i = 0; i < n; i++)
            {
                var row = table.NewRow();
                for (int ii = 0; ii < row.Table.Columns.Count; ii++)
                {
                    row[ii] = GetRandomString();
                }
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
    }
}
