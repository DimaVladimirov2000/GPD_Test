using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.IO;

namespace GPD_Test
{ 
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            XLSX_Format.SetLicense();

            dataTab.ItemsSource = Anomalyes;
            
        }

        public ObservableCollection<Anomaly> Anomalyes = new();

        Anomaly selectedObj;

        int indexOfSelectedObj = -1;

        double zoom = 20;
        private void OpenFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFile();
        }

        private void OpenFile()
        {
            var ofd = new OpenFileDialog();

            ofd.Filter = "Excel files (*.xlsx)|*.xlsx|Excel text files (*.csv)|*.csv|All files (*.*)|*.*";

            if (ofd.ShowDialog() == true)
            {


                string ext = System.IO.Path.GetExtension(ofd.FileName);

                switch (ext)
                {
                    case ".xlsx":
                        {

                            OpenDataFromXLSX(ofd.FileName);

                            break;
                        }
                    case ".csv":
                        {

                            OpenDataFromCSV(ofd.FileName);

                            break;
                        }
                    default:
                        {
                            MessageBox.Show("Неподдерживаемый формат файла", "Внимание!");

                            break;
                        }
                }

            }
        }

        private void OpenDataFromXLSX(string path)
        {


            var data = XLSX_Format.GetData(path);

            Anomalyes.Clear();

            foreach (Anomaly a in data)
            {
                Anomalyes.Add(a);
            }

            DrawAnomalyesMap();
        }
        
        private void OpenDataFromCSV(string path)
        {

            var data = CSV_Format.GetData(path);

            Anomalyes.Clear();

            foreach (Anomaly a in data)
            {
                Anomalyes.Add(a);
            }

            DrawAnomalyesMap();

        }


       

        private void dataTab_MouseDown(object sender, MouseButtonEventArgs e)
        {
            selectedObj = (Anomaly)dataTab.SelectedItem;

            updateInfo();
        }

        void updateInfo()
        {
            if (selectedObj == null) return;

            infoPanel.Text = selectedObj.ToString();
        }

        void DrawAnomalyesMap()
        {



            Map.Children.Clear();

            Rectangle bounds = new Rectangle();

            bounds.Width = 20 * zoom;

            bounds.Height = 12 * zoom;

            bounds.Stroke = Brushes.DimGray;

            bounds.StrokeThickness = 2;

            Map.Children.Add(bounds);

            foreach(Anomaly a in Anomalyes)
            {
                Rectangle rct = new Rectangle();

                rct.Width = a.Width * zoom;

                if (rct.Width == 0) rct.Width = 1;

                rct.Height = a.Height * zoom;

                if (rct.Height == 0) rct.Height = 1;

                rct.StrokeThickness = 1;

                

                Canvas.SetLeft(rct, a.Distance * zoom);

                Canvas.SetTop(rct, a.Angle * zoom);

                TextBlock caption = new TextBlock();

                caption.Text = a.Name;

                Canvas.SetLeft(caption, a.Distance * zoom);

                Canvas.SetTop(caption, a.Angle * zoom - caption.FontSize * 1.5);

                if (a.IsDefect)
                {
                    rct.Stroke = Brushes.DarkRed;
                    rct.Fill = Brushes.Red;
                    caption.Foreground = Brushes.DarkRed;
                }
                else
                {
                    rct.Stroke = Brushes.Green;
                    rct.Fill = Brushes.GreenYellow;   
                    caption.Foreground = Brushes.Green;
                }

                rct.Tag = a;
                caption.Tag = a;

                rct.MouseDown += Rct_MouseDown;
                caption.MouseDown += Caption_MouseDown;

                Map.Children.Add(rct);
                Map.Children.Add(caption);
            }

            Map.UpdateLayout();
        }

        private void Caption_MouseDown(object sender, MouseButtonEventArgs e)
        {
            infoPanel.Text = ((Anomaly)(((TextBlock)sender).Tag)).ToString();
        }

        private void Rct_MouseDown(object sender, MouseButtonEventArgs e)
        {
            infoPanel.Text = ((Anomaly)(((Rectangle)sender).Tag)).ToString();
        }

        private void Map_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            

        }

        private void dataTab_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            DrawAnomalyesMap();
        }
    }
}
