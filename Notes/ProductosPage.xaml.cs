using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Tarea.Models;
using System.Net.Http;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace Tarea
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductosPage : ContentPage
    {
        static readonly HttpClient client = new HttpClient();
        private string url;
        private ObservableCollection<Producto> producto;
        public ObservableCollection<Producto> Producto
        {
            get { return producto; }
            set { producto = value; }
        }
        public ProductosPage()
        {
            InitializeComponent();
            url = "http://hectorpaiz-001-site1.itempurl.com/api";
            Producto = new ObservableCollection<Producto>();
            cargar();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            cargar();
        }
        public async void cargar ()
        {
            var contenido = await client.GetStringAsync(url + "/Producto");
            var dato = JsonConvert.DeserializeObject<List<Producto>>(contenido);
            listView.ItemsSource = dato;
        }
        
        async void OnProductoAddedClicked(object sender, EventArgs e)
        {
            
             await Navigation.PushAsync(new ProductosEntryPage
            {
                BindingContext = new Producto()
            });
            
        }

        async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            
             if (e.SelectedItem != null)
            {
                Producto prod = (Producto) e.SelectedItem;
                await Navigation.PushAsync(new ProductosEntryPage
                {
                    productId = prod.idProducto,
                    BindingContext = e.SelectedItem as Producto
                });
            }
            
        }
    }
}