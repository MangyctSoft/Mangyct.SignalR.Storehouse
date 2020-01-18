using Mangyct.SignalR.Storehouse.Database.Repositories;
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Mangyct.SignalR.Storehouse.AppService
{
    public partial class ProductForm : Form
    {
        static ListBox box;
        static DataGridView dataGrid;

        public ProductForm()
        {
            InitializeComponent();
            box = this.listBox1;
            dataGrid = this.dataGridView1;
        }

        private void ProductForm_Load(object sender, EventArgs e)
        {
            PrintBox("!!!Нельзя добавить/ продать товар без стоимости.Нажми в поле \"Цена\" нужного товара.");
            PrintBox("SQL зависимость запущена");
            var connectionString = ConfigurationManager.ConnectionStrings["StorehouseDB"].ConnectionString;

            SqlDependency.Stop(connectionString);
            SqlDependency.Start(connectionString);

            StartListening();

            InitializeDataGridView();

            PopulateRows();
        }     

        private static void StartListening()
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["StorehouseDB"].ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(@"SELECT [ProductId], [Count], [PriceId], [IsDelete] FROM [dbo].[Products] ", connection))
                {
                    // Make sure the command object does not already have
                    // a notification object associated with it.
                    command.Notification = null;
                    //  creates a new dependency for the SqlCommand
                    SqlDependency dependency = new SqlDependency(command);
                    //  creates an event handler for the notification of data
                    //      changes in the database.
                    dependency.OnChange += new OnChangeEventHandler(Dependency_OnChange);

                    if (connection.State == System.Data.ConnectionState.Closed)
                        connection.Open();
                    command.ExecuteReader();
                }
            }
            PrintBox("Прослушивание...");
        }
        /// <summary>
        /// Печать лога в textBox'e
        /// </summary>
        /// <param name="text">Текст</param>
        private static void PrintBox(string text)
        {
            // из фоновых потоков работать с контролами нельзя
            // придётся смаршаллировать вызов в UI-поток
            if (box.InvokeRequired)
            {
                box.Invoke((MethodInvoker)(() => { box.Items.Add($"{DateTime.Now.ToString("dd.MM.yyyy || hh:mm:ss")} || {text}"); }));
            }
            else
            {
                box.Items.Add($"{DateTime.Now.ToString("dd.MM.yyyy || hh:mm:ss")} || {text}");
            }
        }

        private static void Dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            PrintBox("Изменения пойманы! Запуск обновления для веб-сервера...");
            PopulateRows();
            UpdateHub();
            StartListening();
        }

        private static void UpdateHub()
        {
            var connection = new HubConnection("http://localhost:53116");
            var hub = connection.CreateHubProxy("StorehouseHub");
            try
            {
                connection.Start().Wait();
                hub.Invoke("Show");
            }
            catch (Exception)
            {
                PrintBox("Веб-сервер не отвечает.");
            }
            
        }
        /// <summary>
        /// Формирование шапки таблицы
        /// </summary>
        private void InitializeDataGridView()
        {
            // Create an unbound DataGridView by declaring a column count.
            dataGridView1.ColumnCount = 4;
            dataGridView1.ColumnHeadersVisible = true;

            // Set the column header style.
            DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();

            columnHeaderStyle.BackColor = Color.Beige;
            columnHeaderStyle.Font = new Font("Verdana", 10, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle = columnHeaderStyle;

            // Set the column header names.
            dataGridView1.Columns[0].Name = "ID";
            dataGridView1.Columns[1].Name = "Наименование";
            dataGridView1.Columns[1].Width = 250;
            dataGridView1.Columns[2].Name = "Кол-во";
            dataGridView1.Columns[3].Name = "Цена";

            var bcol = new DataGridViewButtonColumn[]
            {
                new DataGridViewButtonColumn()
                {
                    HeaderText = " ",
                    Text = "Внести",
                    Name = "btnAdd",
                    UseColumnTextForButtonValue = true
                },
                 new DataGridViewButtonColumn()
                {
                    HeaderText = " ",
                    Text = "Продать",
                    Name = "btnSale",
                    UseColumnTextForButtonValue = true
                },
                  new DataGridViewButtonColumn()
                {
                    HeaderText = " ",
                    Text = "Удалить",
                    Name = "btnDelete",
                    UseColumnTextForButtonValue = true
                }
            };
            dataGridView1.Columns.AddRange(bcol);
        }
        /// <summary>
        /// Заполнение таблицы
        /// </summary>
        private static void PopulateRows()
        {
            ProductRepository repo = new ProductRepository();
            var products = repo.GetWithInclude(p => p.PriceStores).Select(s => new
            {
                id = s.ProductId,
                name = s.Name,
                count = s.Count,
                price = s.PriceStores.FirstOrDefault(f => f.PriceId == s.PriceId.GetValueOrDefault())?.Price ?? 0.0M
            });

            if (dataGrid.InvokeRequired)
            {
                dataGrid.Invoke((MethodInvoker)(() => { dataGrid.Rows.Clear(); }));
            }
            else
            {
                dataGrid.Rows.Clear();
            }

            // Заполнение строк.
            List<DataGridViewRow> rows = new List<DataGridViewRow>();
            foreach (var item in products)
            {
                rows.Add(new DataGridViewRow());
                rows[rows.Count - 1].CreateCells(dataGrid,
                    item.id.ToString(),
                    item.name,
                    item.count.ToString(),
                    item.price.ToString()
                );
            }
                       
            if (dataGrid.InvokeRequired)
            {
                dataGrid.Invoke((MethodInvoker)(() => { dataGrid.Rows.AddRange(rows.ToArray()); }));
            }
            else
            {
                dataGrid.Rows.AddRange(rows.ToArray());
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var formProductAdd = new FormAddProduct();
            formProductAdd.Owner = this;
            formProductAdd.ShowDialog();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int id = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
            string price = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
           
            if (e.ColumnIndex == 3)
            {
                var formBalance = new FormBalance(id, "Изменить", price, true);
                formBalance.Owner = this;
                formBalance.ShowDialog();
            }

            if (decimal.Parse(price) > 0)
            {
                if (e.ColumnIndex == 4)
                {
                    var formBalance = new FormBalance(id, "Добавить", "", true);
                    formBalance.Owner = this;
                    formBalance.ShowDialog();
                }
              
                if (e.ColumnIndex == 5)
                {
                    var formBalance = new FormBalance(id, "Продать", "", false);
                    formBalance.Owner = this;
                    formBalance.ShowDialog();
                }
            }

            if (e.ColumnIndex == 6)
            {
                string message = $"Удалить {dataGridView1.Rows[e.RowIndex].Cells[1].Value}?";
                string title = "Удаление";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result = MessageBox.Show(message, title, buttons);
                if (result == DialogResult.Yes)
                {
                    ProductRepository repo = new ProductRepository();
                    repo.Remove(id);                  
                }
                else
                {
                    //  
                }
            }
        }
    }
}
