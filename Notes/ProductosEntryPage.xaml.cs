using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Tarea.Models;
using System.Net.Http;
using Newtonsoft.Json;

namespace Tarea
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductosEntryPage : ContentPage
    {
        static readonly HttpClient client = new HttpClient();
        public int productId;
        public int ProductId { get; set; }
        private string url;
        public ProductosEntryPage(int productId = 0)
        {
            this.productId = productId;
            url = "http://hectorpaiz-001-site1.itempurl.com/api/Producto";
            InitializeComponent();
        }
        public async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            
            var producto = (Producto)BindingContext;
            string data = JsonConvert.SerializeObject(producto);
            // lblResponse.Text = data + productId;
            StringContent body = new StringContent(data, Encoding.UTF8, "application/json");
            if (productId == 0)
            {
                await create(body);
            } else
            {
                await update(body);
            }
            await Navigation.PopAsync();
            
        }

        private async Task create (StringContent data)
        {
            
            HttpResponseMessage response = await client.PostAsync(url, data);
            lblResponse.Text = await response.Content.ReadAsStringAsync();
        }

        private async Task update (StringContent data)
        {
            string endpoint = $"{url}/{productId.ToString()}";
            HttpResponseMessage response = await client.PutAsync(endpoint, data);
            lblResponse.Text = await response.Content.ReadAsStringAsync();
        }
        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {

            if (productId != 0)
            {
                string endpoint = $"{url}/{productId.ToString()}";
                HttpResponseMessage response = await client.DeleteAsync(endpoint);
            }
            
            await Navigation.PopAsync();
            
        }

        private void price_TextChanged(object sender, TextChangedEventArgs e)
        {
            try {
                int priceInt = int.Parse(price.Text);
                iPriceWithoutIva.Text = (priceInt - priceInt * 0.13).ToString();
                iTotalPrice.Text = price.Text;
            }
            catch (Exception error)
            {
                iPriceWithoutIva.Text = "0";
                iTotalPrice.Text = "0";
            }
        }
    }
}