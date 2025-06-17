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
using System.Xml.Linq;

namespace DemoGKV44
{
    public partial class EditMaterialForm : Form
    {
        private int article;
        private string connectionString;

        public EditMaterialForm(int article, string connectionString)
        {
            InitializeComponent();
            this.article = article;
            this.connectionString = connectionString;
            this.Text = "Редактирование материала";
            LoadMaterialData();
            LoadMaterialTypes();
        }

        private void LoadMaterialData()
        {
            string query = "SELECT * FROM [Материалы] WHERE [Артикул] = @Article";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Article", article);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtName.Text = reader["Наименование материала"].ToString();
                            cmbMaterialType.SelectedItem = reader["Тип материала"].ToString();
                            txtPrice.Text = reader["Цена единицы материала"].ToString();
                            txtStockQuantity.Text = reader["Количество на складе"].ToString();
                            txtMinQuantity.Text = reader["Минимальное количество"].ToString();
                            txtPackageQuantity.Text = reader["Количество в упаковке"].ToString();
                            txtUnit.Text = reader["Единица измерения"].ToString();
                        }
                    }
                }
            }
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

            // Обновление материала в базе данных
            string query = @"
                UPDATE [Материалы] SET
                    [Наименование материала] = @Name,
                    [Тип материала] = @Type,
                    [Цена единицы материала] = @Price,
                    [Количество на складе] = @StockQuantity,
                    [Минимальное количество] = @MinQuantity,
                    [Количество в упаковке] = @PackageQuantity,
                    [Единица измерения] = @Unit
                WHERE [Артикул] = @Article";

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
                        cmd.Parameters.AddWithValue("@Article", article);

                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Материал успешно обновлен!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении материала: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
