using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace КП
{
    public partial class Form3 : Form
    {
        database DataBase = new database();
        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataBase.openConnection();

            var name = textBox5.Text;
            var fam = textBox4.Text;
            var group = textBox3.Text;
            var project = textBox2.Text;
            var sr_ball = textBox1.Text;

            var addquery = $"insert into project_db (type_nam, type_fam, type_group, type_project, type_sr) values('{name}', '{fam}', '{group}', '{project}', '{sr_ball}')";
            var command = new SqlCommand(addquery, DataBase.getConnection());
            command.ExecuteNonQuery();

            MessageBox.Show("Запись успешно добалена в таблицу!", "Успех!", MessageBoxButtons.OK, MessageBoxIcon.Information);

            DataBase.closeConnection();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }
    }
}
