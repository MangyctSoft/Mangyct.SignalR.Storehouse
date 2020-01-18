using Mangyct.SignalR.Storehouse.Database.Repositories;
using System;
using System.Windows.Forms;

namespace Mangyct.SignalR.Storehouse.AppService
{
    public partial class FormBalance : Form
    {
        private int id;
        private string editBox;
        private bool balance;

        public FormBalance(int id, string text, string editBox, bool balance)
        {
            InitializeComponent();
            this.id = id;
            this.balance = balance;
            this.buttonBalance.Text = text;
            this.editBox = editBox;
            this.textBoxBalance.Text = editBox;
            if (string.IsNullOrEmpty(editBox))
            {
                labelBalance.Text = "Количество:";
            }
            else
            {
                labelBalance.Text = "Цена:";
            }
        }

        private void buttonBalance_Click(object sender, EventArgs e)
        {
            ProductRepository repoProduct = new ProductRepository();
            var productUpdate = repoProduct.FindById(id);
            if (string.IsNullOrEmpty(editBox))
            {
                int count = int.Parse(this.textBoxBalance.Text);
                productUpdate.Count = balance ? productUpdate.Count += count : productUpdate.Count -= count;
                repoProduct.UpdateCount(productUpdate, count, balance);
            }
            else
            {
                decimal price = decimal.Parse(this.textBoxBalance.Text);            
                repoProduct.UpdatePrice(productUpdate, price);
            }

            Close();
        }
    }
}
