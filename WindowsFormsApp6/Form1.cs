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
using System.Configuration;
namespace WindowsFormsApp6
{
    public partial class Form1 : Form
    {
        private SqlConnection sqlConnection = null;
        private SqlConnection sqlConnection1 = null;
        public Form1()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            Load1111();
        }
        

        private void Load1111()
        {
            sqlConnection1 = new SqlConnection(ConfigurationManager.ConnectionStrings["Db_Test1"].ConnectionString);
            sqlConnection1.Open();
            if (sqlConnection1.State == ConnectionState.Open)
            {

                SqlCommand com1 = new SqlCommand("SELECT * FROM [dbo].[Warehouse]", sqlConnection1);
                SqlDataReader reader = com1.ExecuteReader();
                List<string[]> data = new List<string[]>();
                while (reader.Read())
                {
                    data.Add(new string[4]);
                    data[data.Count - 1][0] = reader[0].ToString();

                    data[data.Count - 1][1] = reader[1].ToString();

                    data[data.Count - 1][2] = reader[2].ToString();
                    data[data.Count - 1][3] = reader[3].ToString();
                }
                reader.Close();
                sqlConnection1.Close();
                foreach (string[] s in data)
                {
                    dataGridView2.Rows.Add(s);
                }
            }
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Db_Test"].ConnectionString);
            sqlConnection.Open();
            if (sqlConnection.State == ConnectionState.Open)
            {

                SqlCommand com1 = new SqlCommand("SELECT * FROM [dbo].[Product1]", sqlConnection);
                SqlDataReader reader = com1.ExecuteReader();
                List<string[]> data = new List<string[]>();
                while (reader.Read())
                {
                    data.Add(new string[4]);
                    data[data.Count - 1][0] = reader[0].ToString();

                    data[data.Count - 1][1] = reader[1].ToString();

                    data[data.Count - 1][2] = reader[2].ToString();
                    data[data.Count - 1][3] = reader[3].ToString();
                }
                reader.Close();
                sqlConnection.Close();
                foreach (string[] s in data)
                {
                    dataGridView1.Rows.Add(s);
                }
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (проданое_количествоTextBox.Text.Length > 0 && ценаTextBox.Text.Length > 0 && наименованиеTextBox.Text.Length > 0)
            {
                sqlConnection1 = new SqlConnection(ConfigurationManager.ConnectionStrings["Db_Test1"].ConnectionString);
                sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Db_Test"].ConnectionString);
                sqlConnection.Open();
                sqlConnection1.Open();
                if (sqlConnection.State == ConnectionState.Open)
                {
                    SqlCommand com3 = new SqlCommand("SELECT * FROM [dbo].[Product1]", sqlConnection);
                    SqlDataAdapter adap = new SqlDataAdapter();
                    DataTable tab = new DataTable();
                    adap.SelectCommand = com3;
                    adap.Fill(tab);
                    int i = tab.Rows.Count;
                    int b = i += 1;
                    SqlCommand com1 = new SqlCommand("INSERT INTO [dbo].[Product1] ([Id], [Наименование], [Цена], [Проданное кол-во]) VALUES ("+ b +", N'" + наименованиеTextBox.Text + "', N'" + ценаTextBox.Text + "', N'" + проданое_количествоTextBox.Text + "') ", sqlConnection);
                    SqlCommand com2 = new SqlCommand("INSERT INTO [dbo].[Warehouse] ([Id],[Наименование]) VALUES (" + b + ", N'" + наименованиеTextBox.Text + "') ", sqlConnection1);

                    SqlDataReader reader = com1.ExecuteReader();
                    SqlDataReader reader1 = com2.ExecuteReader();
                    dataGridView1.Rows.Clear();
                    dataGridView2.Rows.Clear();
                    clear();
                    Load1111();
                }
            }
            else
            {
                MessageBox.Show("Все поля должны быть заполнены!");
            }
        }
        private void ценаTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;

            if (!Char.IsDigit(number))
            {
                e.Handled = true;
            }

        }

        private void ценаTextBox_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            clear();
        }
        public void clear()
        {
            ценаTextBox.Text = "";
            проданое_количествоTextBox.Text = "";
            наименованиеTextBox.Text = "";
        }
        private void проданое_количествоTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;

            if (!Char.IsDigit(number))
            {
                e.Handled = true;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Db_Test"].ConnectionString);
            sqlConnection.Open();
            if (sqlConnection.State == ConnectionState.Open)
            {
                dataGridView1.Rows.Clear();

                SqlCommand com1 = new SqlCommand("SELECT * FROM [dbo].[Product1] WHERE Наименование Like N'"+ textBox1.Text+"%'", sqlConnection);
                SqlDataReader reader = com1.ExecuteReader();
                List<string[]> data = new List<string[]>();
                while (reader.Read())
                {
                    data.Add(new string[4]);
                    data[data.Count - 1][0] = reader[0].ToString();

                    data[data.Count - 1][1] = reader[1].ToString();

                    data[data.Count - 1][2] = reader[2].ToString();
                    data[data.Count - 1][3] = reader[3].ToString();
                }
                reader.Close();
                sqlConnection.Close();
                foreach (string[] s in data)
                {
                    dataGridView1.Rows.Add(s);
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked == true)
            {
                sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Db_Test"].ConnectionString);
                sqlConnection1 = new SqlConnection(ConfigurationManager.ConnectionStrings["Db_Test1"].ConnectionString);

                sqlConnection.Open();
                sqlConnection1.Open();
                if (sqlConnection.State == ConnectionState.Open)
                {
                    dataGridView1.Rows.Clear();
                    dataGridView2.Rows.Clear();
                    SqlCommand com1 = new SqlCommand("SELECT * FROM [dbo].[Product1] ORDER BY [Проданное кол-во] DESC;", sqlConnection);
                    
                    SqlDataReader reader = com1.ExecuteReader();
                    List<string[]> data = new List<string[]>();
                    List<string[]> data1 = new List<string[]>();

                    while (reader.Read())
                    {
                        data.Add(new string[4]);
                        data[data.Count - 1][0] = reader[0].ToString();

                        data[data.Count - 1][1] = reader[1].ToString();

                        data[data.Count - 1][2] = reader[2].ToString();
                        data[data.Count - 1][3] = reader[3].ToString();
                        

                    }
                    reader.Close();
                    sqlConnection.Close();
                    foreach (string[] s in data)
                    {
                        dataGridView1.Rows.Add(s);
                    }
                    
                }
            }
            else
            {
                dataGridView2.Rows.Clear();
                dataGridView1.Rows.Clear();
                Load1111();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(textBox3.Text.Length > 0 && textBox4.Text.Length>0 && comboBox1.Text.Length > 0)
            {
                sqlConnection1 = new SqlConnection(ConfigurationManager.ConnectionStrings["Db_Test1"].ConnectionString);
                
                sqlConnection1.Open();
                if (sqlConnection1.State == ConnectionState.Open)
                {
                    
                    SqlCommand com2 = new SqlCommand("UPDATE [dbo].[Warehouse] SET [Остаток на складе] = '" + textBox4.Text + "', [Нужно ли еще заказывать] = N'" + comboBox1.Text + "' WHERE [Id] = " + textBox3.Text+"; ", sqlConnection1);

                  
                    SqlDataReader reader1 = com2.ExecuteReader();
                    dataGridView1.Rows.Clear();
                    dataGridView2.Rows.Clear();
                    clear();
                    Load1111();
                }
            }
            else
            {
                MessageBox.Show("AS");
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;

            if (!Char.IsDigit(number))
            {
                e.Handled = true;
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;

            if (!Char.IsDigit(number))
            {
                e.Handled = true;
            }
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
           if(checkBox2.Checked == true)
            {
                sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Db_Test"].ConnectionString);
                sqlConnection1 = new SqlConnection(ConfigurationManager.ConnectionStrings["Db_Test1"].ConnectionString);
                sqlConnection.Open();
                sqlConnection1.Open();
                if (sqlConnection1.State == ConnectionState.Open)
                {
                    dataGridView2.Rows.Clear();

                    SqlCommand com1 = new SqlCommand("SELECT * FROM [dbo].[Warehouse] WHERE Наименование Like N'К%' AND [Остаток на складе] > 5", sqlConnection1);
                    SqlDataReader reader = com1.ExecuteReader();
                    List<string[]> data = new List<string[]>();
                    while (reader.Read())
                    {
                        data.Add(new string[4]);
                        data[data.Count - 1][0] = reader[0].ToString();

                        data[data.Count - 1][1] = reader[1].ToString();

                        data[data.Count - 1][2] = reader[2].ToString();
                        data[data.Count - 1][3] = reader[3].ToString();
                    }
                    reader.Close();
                    sqlConnection1.Close();
                    foreach (string[] s in data)
                    {
                        dataGridView1.Rows.Clear();
                        dataGridView2.Rows.Add(s);
                    }
                }
            }
            else
            {
                dataGridView1.Rows.Clear();
                Load1111();
            }
        }

        private void Товар_Click(object sender, EventArgs e)
        {

        }
    }
}
