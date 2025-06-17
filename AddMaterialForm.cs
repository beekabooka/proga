using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DemoGKV44
{
    public partial class AddMaterialForm : Form
    {
        private string connectionString;

        public AddMaterialForm(string connectionString)
        {
            InitializeComponent();
            this.connectionString = connectionString;
            this.Text = "Добавление материала";
            LoadMaterialTypes();
        }

        private void LoadMaterialTypes()
        {
            string query = "SELECT [Тип материала] FROM [Типы материалов]";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cmbMaterialType.Items.Add(reader["Тип материала"]);
                    }
                }
            }

            if (cmbMaterialType.Items.Count > 0)
                cmbMaterialType.SelectedIndex = 0;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Валидация данных
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Введите наименование материала!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!decimal.TryParse(txtPrice.Text, out decimal price) || price < 0)
            {
                MessageBox.Show("Введите корректную цену (положительное число)!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!float.TryParse(txtStockQuantity.Text, out float stockQuantity) || stockQuantity < 0)
            {
                MessageBox.Show("Введите корректное количество на складе (положительное число)!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!float.TryParse(txtMinQuantity.Text, out float minQuantity) || minQuantity < 0)
            {
                MessageBox.Show("Введите корректное минимальное количество (положительное число)!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(txtPackageQuantity.Text, out int packageQuantity) || packageQuantity <= 0)
            {
                MessageBox.Show("Введите корректное количество в упаковке (целое положительное число)!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtUnit.Text))
            {
                MessageBox.Show("Введите единицу измерения!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Добавление материала в базу данных
            string query = @"
                INSERT INTO [Материалы] (
                    [Наименование материала],
                    [Тип материала],
                    [Цена единицы материала],
                    [Количество на складе],
                    [Минимальное количество],
                    [Количество в упаковке],
                    [Единица измерения]
                ) VALUES (
                    @Name, @Type, @Price, @StockQuantity, @MinQuantity, @PackageQuantity, @Unit
                )";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Name", txtName.Text);
                        cmd.Parameters.AddWithValue("@Type", cmbMaterialType.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@Price", price);
                        cmd.Parameters.AddWithValue("@StockQuantity", stockQuantity);
                        cmd.Parameters.AddWithValue("@MinQuantity", minQuantity);
                        cmd.Parameters.AddWithValue("@PackageQuantity", packageQuantity);
                        cmd.Parameters.AddWithValue("@Unit", txtUnit.Text);

                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Материал успешно добавлен!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении материала: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
