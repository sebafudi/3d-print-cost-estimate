using Microsoft.Win32;
using System.IO;
using System.Windows;
using HelixToolkit.Wpf;

namespace Projekt_1 {
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window {

    public MainWindow() {
      InitializeComponent();
      //new AddFilament().Show();
    }

    private void Button_Click(object sender, RoutedEventArgs e) {
      var openFileDialog = new OpenFileDialog();
      openFileDialog.Filter = "STL files (*.stl)|*.stl";
      if (openFileDialog.ShowDialog() == true) { 
        File.ReadAllText(openFileDialog.FileName);
        var stlReader = new StLReader();
        var stlFile = stlReader.Read(openFileDialog.FileName);
        if (stlFile != null) {
          modelDisplay.Content = stlFile;
          modelView.ZoomExtents();
        }
      }
    }

    private void Button_Click_1(object sender, RoutedEventArgs e) {
      new Filament().ShowDialog();
    }
  }
}
