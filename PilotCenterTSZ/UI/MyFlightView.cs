using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;

namespace PilotCenterTSZ.UI
{
    public partial class MyFlightView : UserControl
    {
        public MyFlightView()
        {
            InitializeComponent();

            FlightMap();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Hide();

            var a = this.Parent as MyLogBookCtrl;

            a.lstLogBook.Show();


        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            gMapControl1.DragButton = MouseButtons.Left;
            gMapControl1.MapProvider = GMapProviders.GoogleSatelliteMap;


            //gMapControl1.MinZoom = 5;
            //gMapControl1.MaxZoom = 5500;
            //gMapControl1.Zoom = 100;

            FlightMap();

        }
        

        public void FlightMap()
        {

            GMapOverlay polyOverlay = new GMapOverlay("polygons");
            List<PointLatLng> points = new List<PointLatLng>();
            points.Add(new PointLatLng(-25.969562, 32.585789));
            points.Add(new PointLatLng(-25.966205, 52.588171));
            GMapPolygon polygon = new GMapPolygon(points, "mypolygon");
            polygon.Fill = new SolidBrush(Color.FromArgb(50, Color.Red));
            polygon.Stroke = new Pen(Color.Red, 1);
            polyOverlay.Polygons.Add(polygon);
            gMapControl1.Overlays.Add(polyOverlay);

        }
    }
}
