using Microsoft.Win32;
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
using System.Windows.Shapes;

namespace Projekt_1 {
  /// <summary>
  /// Interaction logic for Window1.xaml
  /// </summary>
  public partial class AddFilament : Window {
    public string fType {
      get { return filType.Text; }
    }
    public string fColor {
      get { return filColor.Text; }
    }
    public string fManufacturer {
      get { return filManufacturer.Text; }
    }
    public int fRating {
      get { return filRating.Value; }
    }
    public string fPicture {
      get { return pictureSrc; }
    }
    private string pictureSrc = "";
    public float fPrice {
      get { return float.Parse(filPrice.Text); }
    }
    public string fDensity {
      get { return filDensity.Text; }
    }
    public AddFilament() {
      InitializeComponent();
    }
    private void Add_Filament(object sender, RoutedEventArgs e) {
      this.DialogResult = true;
      this.Close();
    }

    private void Button_Click(object sender, RoutedEventArgs e) {
      var openFileDialog = new OpenFileDialog();
      openFileDialog.Filter = "Image Files (*.bmp;*.png;*.jpg)|*.bmp;*.png;*.jpg";
      if (openFileDialog.ShowDialog() == true) {
        pictureSrc = openFileDialog.FileName;
      }
    }
  }
}
