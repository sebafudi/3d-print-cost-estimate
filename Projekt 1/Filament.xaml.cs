using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Projekt_1 {
  /// <summary>
  /// Interaction logic for Filament.xaml
  /// </summary>
  public partial class Filament : Window {
    public Filament() {
      InitializeComponent();
    }

    private void Button_Click(object sender, RoutedEventArgs e) {
    var w = new AddFilament();
      if (w.ShowDialog() == true) {
        var card = new MaterialDesignThemes.Wpf.Card();
        card.Margin = new Thickness(10);
        card.Padding = new Thickness(10);
        card.Width = 200;
        var grid = new System.Windows.Controls.Grid();


        var rowDefinition = new System.Windows.Controls.RowDefinition();
        rowDefinition.Height = new GridLength(100, GridUnitType.Pixel);
        grid.RowDefinitions.Add(rowDefinition);

        for (int i = 0; i < 6; i++) {
          var rowDefinitionAuto = new System.Windows.Controls.RowDefinition();
          rowDefinitionAuto.Height = new GridLength(0, GridUnitType.Auto);
          grid.RowDefinitions.Add(rowDefinitionAuto);
        }

        var image = new System.Windows.Controls.Image();
        image.Source = new BitmapImage(new Uri(w.fPicture));
        Grid.SetRow(image, 0);
        image.Margin = new Thickness(2);
        image.HorizontalAlignment = HorizontalAlignment.Center;
        image.Width = 100;
        image.VerticalAlignment = VerticalAlignment.Center;

        var type = new System.Windows.Controls.TextBlock();
        type.Text = w.fType.ToString();
        type.Style = (Style)FindResource("MaterialDesignHeadline6TextBlock");
        Grid.SetRow(type, 1);
        type.Margin = new Thickness(2);
        type.HorizontalAlignment = HorizontalAlignment.Center;
        type.VerticalAlignment = VerticalAlignment.Center;

        var color = new System.Windows.Controls.TextBlock();
        color.Text = w.fColor.ToString();
        color.Style = (Style)FindResource("MaterialDesignHeadline6TextBlock");
        Grid.SetRow(color, 2);
        color.Margin = new Thickness(2);
        color.HorizontalAlignment = HorizontalAlignment.Center;
        color.VerticalAlignment = VerticalAlignment.Center;

        var manu = new System.Windows.Controls.TextBlock();
        manu.Text = w.fManufacturer.ToString();
        manu.Style = (Style)FindResource("MaterialDesignHeadline6TextBlock");
        Grid.SetRow(manu, 3);
        manu.Margin = new Thickness(2);
        manu.HorizontalAlignment = HorizontalAlignment.Center;
        manu.VerticalAlignment = VerticalAlignment.Center;

        var rating = new MaterialDesignThemes.Wpf.RatingBar();
        rating.Margin = new Thickness(2);
        rating.HorizontalAlignment = HorizontalAlignment.Center;
        rating.VerticalAlignment = VerticalAlignment.Center;
        Grid.SetRow(rating, 4);
        rating.IsReadOnly = true;
        rating.Value = w.fRating;

        var price = new System.Windows.Controls.TextBlock();
        price.Text = w.fPrice.ToString() + " zł/kg";
        price.Style = (Style)FindResource("MaterialDesignHeadline6TextBlock");
        Grid.SetRow(price, 5);
        price.Margin = new Thickness(2);
        price.HorizontalAlignment = HorizontalAlignment.Center;
        price.VerticalAlignment = VerticalAlignment.Center;

        var confirmButton = new System.Windows.Controls.Button();
        confirmButton.Margin = new Thickness(10, 5, 10, 5);
        Grid.SetRow(confirmButton, 6);
        confirmButton.Content = "Wybierz";
        confirmButton.Click += Select_Filament;

        grid.Children.Add(image); 
        grid.Children.Add(type);
        grid.Children.Add(color);
        grid.Children.Add(manu);
        grid.Children.Add(rating);
        grid.Children.Add(price);
        grid.Children.Add(confirmButton);
        card.Content = grid;
        filementContainer.Children.Add(card);
      }
    }
    private void Select_Filament(object sender, RoutedEventArgs e) {
      this.Close();
    }
  }
}
