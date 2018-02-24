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

        private void lstLogBook_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            lstLogBook.Hide();

            myFlightView.Show();
        }

        private void InitializeComponent()
        {
            this.lstLogBook = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.myFlightView = new PilotCenterTSZ.UI.MyFlightView();
            this.SuspendLayout();
            // 
            // lstLogBook
            // 
            this.lstLogBook.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8});
            this.lstLogBook.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstLogBook.FullRowSelect = true;
            this.lstLogBook.Location = new System.Drawing.Point(87, 3);
            this.lstLogBook.MultiSelect = false;
            this.lstLogBook.Name = "lstLogBook";
            this.lstLogBook.Size = new System.Drawing.Size(506, 513);
            this.lstLogBook.TabIndex = 0;
            this.lstLogBook.UseCompatibleStateImageBehavior = false;
            this.lstLogBook.View = System.Windows.Forms.View.Details;
            this.lstLogBook.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstLogBook_MouseDoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Callsign";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Departure";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Arrival";
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Aircraft";
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "FlightTime";
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Ldg. Rate";
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Overall";
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Ep\'s";
            // 
            // myFlightView
            // 
            this.myFlightView.Location = new System.Drawing.Point(0, 16);
            this.myFlightView.Name = "myFlightView";
            this.myFlightView.Size = new System.Drawing.Size(935, 500);
            this.myFlightView.TabIndex = 1;
            this.myFlightView.Visible = false;
            // 
            // MyLogBookCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.myFlightView);
            this.Controls.Add(this.lstLogBook);
            this.Name = "MyLogBookCtrl";
            this.Size = new System.Drawing.Size(935, 533);
            this.ResumeLayout(false);

        }
    }
}
