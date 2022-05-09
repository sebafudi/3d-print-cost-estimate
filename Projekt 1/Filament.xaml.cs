using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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
  public class FilamentObj {
    public FilamentObj(string picture, string type, string color, string manufacturer, int rating, string price, string density) {
      this.picture = picture;
      this.type = type;
      this.color = color;
      this.manufacturer = manufacturer;
      this.rating = rating;
      this.price = price;
      this.density = density;
    }
    public string picture;
    public string type;
    public string color;
    public string manufacturer;
    public int rating;
    public string price;
    public string density;
  }

  public partial class Filament : Window {


    public Filament() {
      InitializeComponent();
      List<FilamentObj> products = new List<FilamentObj>();
      if (File.Exists(@"db.json")) {
        using (StreamReader file = File.OpenText(@"db.json")) {
          JsonSerializer serializer = new JsonSerializer();
          products = (List<FilamentObj>)serializer.Deserialize(file, typeof(List<FilamentObj>));
        }
        foreach (var product in products) {
          MaterialDesignThemes.Wpf.Card card = CreateCard(product);
          filementContainer.Children.Add(card);
        }
      }
    }

    private void Button_Click(object sender, RoutedEventArgs e) {
      var w = new AddFilament();
      if (w.ShowDialog() == true) {
        List<FilamentObj> products = new List<FilamentObj>();
        if (File.Exists(@"db.json")) {
          using (StreamReader file = File.OpenText(@"db.json")) {
            JsonSerializer serializer = new JsonSerializer();
            products = (List<FilamentObj>)serializer.Deserialize(file, typeof(List<FilamentObj>));
          }
        }
        FilamentObj filament = new FilamentObj(w.fPicture, w.fType.ToString(), w.fColor.ToString(), w.fManufacturer.ToString(), w.fRating, w.fPrice.ToString(), w.fDensity.ToString());
        products.Add(filament);

        string json = JsonConvert.SerializeObject(products, Formatting.Indented);
        File.WriteAllText(@"db.json", json);
        MaterialDesignThemes.Wpf.Card card = CreateCard(filament);
        filementContainer.Children.Add(card);
      }
    }

    private MaterialDesignThemes.Wpf.Card CreateCard(FilamentObj filament) {
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

      FileStream stream = new FileStream(filament.picture, FileMode.Open, FileAccess.Read);
      BitmapImage src = new BitmapImage();
      src.BeginInit();
      src.StreamSource = stream;
      src.EndInit();

      image.Source = src;
      Grid.SetRow(image, 0);
      image.Margin = new Thickness(2);
      image.HorizontalAlignment = HorizontalAlignment.Center;
      image.Width = 100;
      image.VerticalAlignment = VerticalAlignment.Center;

      var type = new System.Windows.Controls.TextBlock();
      type.Text = filament.type;
      type.Style = (Style)FindResource("MaterialDesignHeadline6TextBlock");
      Grid.SetRow(type, 1);
      type.Margin = new Thickness(2);
      type.HorizontalAlignment = HorizontalAlignment.Center;
      type.VerticalAlignment = VerticalAlignment.Center;

      var color = new System.Windows.Controls.TextBlock();
      color.Text = filament.color;
      color.Style = (Style)FindResource("MaterialDesignHeadline6TextBlock");
      Grid.SetRow(color, 2);
      color.Margin = new Thickness(2);
      color.HorizontalAlignment = HorizontalAlignment.Center;
      color.VerticalAlignment = VerticalAlignment.Center;

      var manu = new System.Windows.Controls.TextBlock();
      manu.Text = filament.manufacturer;
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
      rating.Value = filament.rating;

      var price = new System.Windows.Controls.TextBlock();
      price.Text = filament.price + " zł/kg";
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
      confirmButton.Tag = filament.price + " " + filament.density;

      grid.Children.Add(image);
      grid.Children.Add(type);
      grid.Children.Add(color);
      grid.Children.Add(manu);
      grid.Children.Add(rating);
      grid.Children.Add(price);
      grid.Children.Add(confirmButton);
      card.Content = grid;
      return card;
    }
    public double price = 1;
    public double density = 1;
    private void Select_Filament(object sender, RoutedEventArgs e) {
      this.DialogResult = true;
      var button = sender as Button;
      var values = button.Tag.ToString();
      String [] strlist = values.Split(" ", 2);

      double.TryParse(strlist [0].Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out price);
      double.TryParse(strlist [1].Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out density);

      this.Close();
    }
  }
}
