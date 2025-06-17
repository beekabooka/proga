# proga

using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
// начальная форма
namespace DemoGKV44
{
    public partial class Form1 : Form
    {
        private string connectionString = "Data Source=DESKTOP-BS13CG5;Initial Catalog=MaterialsDB;Integrated Security=True";

        public Form1()
        {
            InitializeComponent();
            LoadMaterialsData();
        }

        private void LoadMaterialsData()
        {
            panelMaterials.Controls.Clear();
            string query = "SELECT * FROM [Материалы]";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    int index = 0;
                    while (reader.Read())
                    {
                        // Создаем TextBox для материала
                        TextBox materialTextBox = new TextBox
                        {
                            Multiline = true,
                            Width = panelMaterials.Width - 30,
                            Height = 120,
                            ReadOnly = true,
                            Text = $"{reader["Тип материала"]} | {reader["Наименование материала"]}\r\n" +
                                   $"Минимальное количество: {reader["Минимальное количество"]} {reader["Единица измерения"]}\r\n" +
                                   $"Количество на складе: {reader["Количество на складе"]} {reader["Единица измерения"]}\r\n" +
                                   $"Цена: {Convert.ToDecimal(reader["Цена единицы материала"]):N4} р / {reader["Единица измерения"]}",
                            Location = new Point(10, index * 125),
                            Tag = reader["Артикул"],
                            BorderStyle = BorderStyle.FixedSingle,
                            Font = new Font("Segoe UI", 9.5f),
                            BackColor = Color.White
                        };

                        // Рассчитываем стоимость партии
                        decimal batchCost = CalculateBatchCost(reader);

                        // Создаем Label для стоимости партии (справа, по центру)
                        Label costLabel = new Label
                        {
                            Text = $"Стоимость партии: {batchCost:N2} р",
                            Location = new Point(materialTextBox.Width - 220, 50),
                            AutoSize = true,
                            Font = new Font("Segoe UI", 9.5f),
                            BackColor = Color.Transparent,
                            Parent = materialTextBox
                        };

                        materialTextBox.Click += MaterialTextBox_Click;
                        panelMaterials.Controls.Add(materialTextBox);
                        index++;
                    }
                }
            }
        }

        private decimal CalculateBatchCost(SqlDataReader reader)
        {
            decimal price = Convert.ToDecimal(reader["Цена единицы материала"]);
            float minQty = Convert.ToSingle(reader["Минимальное количество"]);
            float currentQty = Convert.ToSingle(reader["Количество на складе"]);
            int packageQty = Convert.ToInt32(reader["Количество в упаковке"]);

            if (currentQty >= minQty)
                return 0;

            float needed = minQty - currentQty;
            int packages = (int)Math.Ceiling(needed / packageQty);
            return (decimal)(packages * packageQty) * price;
        }

        private void MaterialTextBox_Click(object sender, EventArgs e)
        {
            TextBox clickedTextBox = (TextBox)sender;
            int article = (int)clickedTextBox.Tag;

            EditMaterialForm editForm = new EditMaterialForm(article, connectionString);
            if (editForm.ShowDialog() == DialogResult.OK)
            {
                LoadMaterialsData();
            }
        }

        private void btnAddMaterial_Click(object sender, EventArgs e)
        {
            AddMaterialForm addForm = new AddMaterialForm(connectionString);
            if (addForm.ShowDialog() == DialogResult.OK)
            {
                LoadMaterialsData();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadMaterialsData();
        }
    }
}
