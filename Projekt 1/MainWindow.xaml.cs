using Microsoft.Win32;
using System.IO;
using System.Windows;
using HelixToolkit.Wpf;
using System.Windows.Controls.Primitives;

namespace Projekt_1 {
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window {

    public MainWindow() {
      InitializeComponent();
      //new AddFilament().Show();
    }

    private double filamentPrice = 0;
    private double filamentDensity = 0;
    private double objectVolume = 0;

    private void Button_Click(object sender, RoutedEventArgs e) {
      var openFileDialog = new OpenFileDialog();
      openFileDialog.Filter = "STL files (*.stl)|*.stl";
      if (openFileDialog.ShowDialog() == true) {
        File.ReadAllText(openFileDialog.FileName);
        objectVolume = Calc_Volume(openFileDialog.FileName);
        var stlReader = new StLReader();
        var stlFile = stlReader.Read(openFileDialog.FileName);
        Update_Price();
        if (stlFile != null) {
          modelDisplay.Content = stlFile;
          modelView.ZoomExtents();
        }
      }
    }

    private void Slider_ValueChanged(
        object sender,
        RoutedPropertyChangedEventArgs<double> e) {
      Update_Price();
    }

    private void Update_Price() {
      double infill = infillSlider.Value / 100;
      double price = (objectVolume * filamentDensity * infill * 0.4 + objectVolume * filamentDensity * 0.6) / 1000 * filamentPrice;
      if (priceElement != null) {
        if (objectVolume > 0 && filamentDensity > 0 && filamentPrice > 0) {
          priceElement.Text = $"Szacowany koszt: {price.ToString("0.00")} PLN";
        } else {
          priceElement.Text = "";
        }
      }
    }

    private void Button_Click_1(object sender, RoutedEventArgs e) {
      var w = new Filament();
      if (w.ShowDialog() == true) {
        filamentPrice = w.price;
        filamentDensity = w.density;
        Update_Price();
      }
    }
    private double Calc_Volume(string FilePath) {
      double [] normal = new double [3];
      double [] vertex1 = new double [3];
      double [] vertex2 = new double [3];
      double [] vertex3 = new double [3];
      ushort count;
      double sum = 0;

      using (BinaryReader reader = new BinaryReader(File.Open(FilePath, FileMode.Open))) {
        reader.BaseStream.Seek(84, SeekOrigin.Begin);
        while (reader.BaseStream.Position != reader.BaseStream.Length) {
          for (int i = 0; i < 3; i++)
            normal [i] = reader.ReadSingle();
          for (int i = 0; i < 3; i++)
            vertex1 [i] = reader.ReadSingle();
          for (int i = 0; i < 3; i++)
            vertex2 [i] = reader.ReadSingle();
          for (int i = 0; i < 3; i++)
            vertex3 [i] = reader.ReadSingle();
          count = reader.ReadUInt16();
          sum = sum + ((((-1) * vertex3 [0] * vertex2 [1] * vertex1 [2]) + vertex2 [0] * vertex3 [1] * vertex1 [2] + vertex3 [0] * vertex1 [1] * vertex2 [2] - vertex1 [0] * vertex3 [1] * vertex2 [2] - vertex2 [0] * vertex1 [1] * vertex3 [2] + vertex1 [0] * vertex2 [1] * vertex3 [2]) / 6);
        }
        return sum / 1000;
      }
    }
  }
}
