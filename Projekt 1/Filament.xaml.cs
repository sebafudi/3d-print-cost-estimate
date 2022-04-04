using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Projekt_1 {
  /// <summary>
  /// Interaction logic for Filament.xaml
  /// </summary>
  public partial class Filament : Window {
    public Filament() {
      InitializeComponent();
    }

    private void Button_Click(object sender, RoutedEventArgs e) {
      new AddFilament().ShowDialog();
    }
    private void Select_Filament(object sender, RoutedEventArgs e) {
      this.Close();
    }
  }
}
