using Mangyct.SignalR.Storehouse.Database.Models;
using Mangyct.SignalR.Storehouse.Database.Repositories;
using System;
using System.Windows.Forms;

namespace Mangyct.SignalR.Storehouse.AppService
{
    public partial class FormAddProduct : Form
    {
        public FormAddProduct()
        {
            InitializeComponent();
            textBoxProductPrice.Enabled = false;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            ProductRepository repo = new ProductRepository();
            Product productAdd = new Product
            {
                Name = textBoxProductName.Text,
                //Price = decimal.Parse(textBoxProductPrice.Text)
                //Price = 0
            };
            repo.Create(productAdd);
            this.Hide();
        }
    }
}
