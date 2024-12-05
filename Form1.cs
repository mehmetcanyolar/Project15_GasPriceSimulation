using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project15_GasPriceSimulation
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        double totalPrice;
        double dieselPrice = 0;
        double gasolinePrice =0;
        double lpgPrice = 0;
        double gasAmount;
        double dieselAmount;
        double lpgAmount;
        int count;

      

        private void btnStart_Click(object sender, EventArgs e)
        {
            gasAmount = Convert.ToDouble(txtGasAmount.Text);
            lpgAmount = Convert.ToDouble(txtGasAmount.Text);
            dieselAmount = Convert.ToDouble(txtGasAmount.Text);
           
            timer1.Start();
            timer1.Interval = 80;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Text = count.ToString();


            if (rdbGas.Checked)
            {
                count++;
                if (count <= gasAmount)
                {

                    totalPrice += gasolinePrice;
                    txtTotalPrice.Text = totalPrice.ToString() + " ₺";
                }
                else
                {
                    txtTotalPrice.Text = totalPrice.ToString() + " ₺";
                }
                progressBar1.Value += 1;

                if (progressBar1.Value == 99)
                {
                    timer1.Stop();

                }
            }
            if (rdbDiesel.Checked)
            {
                count++;
                if (count <= dieselAmount)
                {

                    totalPrice += dieselPrice;
                    txtTotalPrice.Text = totalPrice.ToString() + " ₺";
                }
                else
                {
                    txtTotalPrice.Text = totalPrice.ToString() + " ₺";
                }
                progressBar1.Value += 1;

                if (progressBar1.Value == 99)
                {
                    timer1.Stop();

                }
            }

            if (rdbLpg.Checked)
            {
                count++;
                if (count <= lpgAmount)
                {

                    totalPrice += lpgPrice;
                    txtTotalPrice.Text = totalPrice.ToString() + " ₺";
                }
                else
                {
                    txtTotalPrice.Text = totalPrice.ToString() + " ₺";
                }

                progressBar1.Value += 3;

                if (progressBar1.Value == 99)
                {
                    timer1.Stop();

                }
            }
            

            
        }

        public async void Form1_Load(object sender, EventArgs e)
        {
            MessageBox.Show("API YAKIT  VERİLERİ ALINIYOR...");



            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://akaryakit-fiyatlari.p.rapidapi.com/fuel/istanbul"),
                Headers =
    {
        { "x-rapidapi-key", "8e2153b60cmshad87d4c02fb4d8fp1ddcabjsned53ab11d2ad" },
        { "x-rapidapi-host", "akaryakit-fiyatlari.p.rapidapi.com" },
    },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                var json = JObject.Parse(body);
                //var gasolineJsonValue = json["data"][16]["prices"][0] ["benzin"];
                //var dieselJsonValue = json["data"][16]["prices"][0] ["motorin"];
                //var lpgJsonValue = json["data"][16]["prices"][0] ["lpg"];

                // "₺" simgesini kaldırıp, "," ondalık ayracını "." ile değiştirelim
                var gasolineJsonValue = json["data"][17]["prices"][0]["benzin"].ToString().Replace("₺", "").Replace(",", ".");
                gasolinePrice = double.Parse(gasolineJsonValue, CultureInfo.InvariantCulture);

                var dieselJsonValue = json["data"][17]["prices"][0]["motorin"].ToString().Replace("₺", "").Replace(",", ".");
                dieselPrice = double.Parse(dieselJsonValue, CultureInfo.InvariantCulture);

                var lpgJsonValue = json["data"][17]["prices"][0]["lpg"].ToString().Replace("₺", "").Replace(",", ".");
                lpgPrice = double.Parse(lpgJsonValue, CultureInfo.InvariantCulture);


                txtGasolinePrice.Text = gasolineJsonValue.ToString() + " ₺";
                txtDieselPrice.Text = dieselJsonValue.ToString() + " ₺";
                txtLpgPrice.Text = lpgJsonValue.ToString() + " ₺";

                gasolinePrice = double.Parse(gasolineJsonValue.ToString());
                dieselPrice = double.Parse(dieselJsonValue.ToString());
                lpgPrice = double.Parse(lpgJsonValue.ToString());

            }



        }
    }
}
