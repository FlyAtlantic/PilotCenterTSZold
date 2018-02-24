using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PilotCenterTSZ.UI
{
    public partial class MyLogBookCtrl : UserControl
    {
        public MyLogBookCtrl()
        {
            InitializeComponent();
            GetLogBook();
        }

        public void GetLogBook()
        {
            lstLogBook.Items.Clear();
            foreach (LogBook l in LogBook.Get())
            {
                lstLogBook.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                lstLogBook.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                lstLogBook.Items.Add(new ListViewItem(new string[]
                {
                    l.Callsign,
                    l.Departure,
                    l.Arrival,
                    l.Aircraft,
                    l.FlightTime.ToString(),
                    l.FtPerMin.ToString(),
                    l.Sum.ToString(),
                    l.Eps.ToString()

                }));

            }
        }

    }
}
