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
using System.Net.Http;
using Newtonsoft.Json;

namespace _12_i_backend_lekerdezes
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            createtextblock();
        }

        async void createtextblock()
        {
            kacsak.Children.Clear();
            TextBlock oneblock = new TextBlock();
            
            HttpClient client = new HttpClient();
            string url = "http://127.0.0.1:3000/kacsa";
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                string stringresponse = await response.Content.ReadAsStringAsync();
                List<Class1> Kacsalist = JsonConvert.DeserializeObject<List<Class1>>(stringresponse);
                foreach (Class1 item in Kacsalist)
                {
                    oneblock.Text = $"Kacsa neve: {item.name}";
                    kacsak.Children.Add(oneblock);
                }
            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message);
            }
        }

        async void AddKacsa(object s, EventArgs e) {
            HttpClient client = new HttpClient();
            string url = "http://127.1.1.1:3000/kacsa";
            
            try
            {
                var jsonObject = new
                {
                    name = nev.Text,
                    length = hossz.Text
                };

                string jsonData = JsonConvert.SerializeObject(jsonObject);
                StringContent data = new StringContent(jsonData, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(url, data);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }

            

            //MessageBox.Show($"Kacsa neve: {nev.Text}, kacsa hossza: {hossz.Text}cm");
        }
    }
}
