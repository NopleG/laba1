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
using System.IO;

namespace КП
{
    
    enum RowState
    {
        Existed,
        New,
        Modified,
        ModifiedNew,
        Deleted
    }
    public partial class Form2 : Form
    {
        database DataBase = new database();

        int selectedRow;
        public Form2()
        {
            InitializeComponent();
        }


        public void Wewe(int ind)
        {
            if(ind == 0)
            {
                dataGridView1.Rows.Clear();
                dataGridView1.Columns.Clear();
                dataGridView1.Columns.Add("id_knigi", "id");
                dataGridView1.Columns.Add("name", "Название");
                dataGridView1.Columns.Add("data_vidachi", "Дата выдачи");
                dataGridView1.Columns.Add("id_chitatelya", "ID читателя");
                dataGridView1.Columns.Add("id_postavki", "ID поставки");
                dataGridView1.Columns.Add("id_sotrudnika", "ID сотрудника");
                dataGridView1.Columns.Add("new", String.Empty);
                groupBox3.Visible = false;
                groupBox4.Visible = false;
                RefreshDataGrid(dataGridView1, "Kniga");
            }

            if (ind == 1)
            {
                dataGridView1.Rows.Clear();
                dataGridView1.Columns.Clear();
                dataGridView1.Columns.Add("id_chitatelya", "id");
                dataGridView1.Columns.Add("name", "Имя");
                dataGridView1.Columns.Add("surname", "Фамилия");
                dataGridView1.Columns.Add("new", String.Empty);
                groupBox3.Visible = false;
                groupBox4.Visible = false;
                RefreshDataGrid(dataGridView1, "Chitatel");
            }

            if (ind == 2)
            {
                dataGridView1.Rows.Clear();
                dataGridView1.Columns.Clear();
                dataGridView1.Columns.Add("id_sotrudnika", "id");
                dataGridView1.Columns.Add("name", "Имя");
                dataGridView1.Columns.Add("surname", "Фамилия");
                dataGridView1.Columns.Add("doljnost", "Должность");
                dataGridView1.Columns.Add("new", String.Empty);
                groupBox3.Visible = false;
                groupBox4.Visible = false;
                RefreshDataGrid(dataGridView1, "Sotrudnik");
            }

            if (ind == 3)
            {
                dataGridView1.Rows.Clear();
                dataGridView1.Columns.Clear();
                dataGridView1.Columns.Add("id_postavki", "id");
                dataGridView1.Columns.Add("data_postavki", "Дата поставки");
                dataGridView1.Columns.Add("new", String.Empty);
                groupBox3.Visible = false;
                groupBox4.Visible = false;
                RefreshDataGrid(dataGridView1, "Postavka");
            }
        }
        private void CreateColumns()
        {
            dataGridView1.Columns.Add("id", "id");
            dataGridView1.Columns.Add("type_nam", "Имя");
            dataGridView1.Columns.Add("type_fam", "Фамилия");
            dataGridView1.Columns.Add("type_group", "Группа");
            dataGridView1.Columns.Add("type_project", "Дисциплина");
            dataGridView1.Columns.Add("type_sr", "Средний балл");
            dataGridView1.Columns.Add("new", String.Empty);
        }

        private void ReadSingleRow(DataGridView dgw, IDataRecord record, string name)
        {
            if (name == "Kniga")
            {
                dgw.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetString(2), record.GetInt32(3), record.GetInt32(4), record.GetInt32(5), RowState.ModifiedNew);
            }
            if (name == "Chitatel")
            {
                dgw.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetString(2), RowState.ModifiedNew);
            }
            if (name == "Sotrudnik")
            {
                dgw.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetString(2), record.GetString(3), RowState.ModifiedNew);
            }
            if (name == "Postavka")
            {
                dgw.Rows.Add(record.GetInt32(0), record.GetString(1), RowState.ModifiedNew);
            }
        }

        private void RefreshDataGrid(DataGridView dgw, string name)
        {
            dgw.Rows.Clear();
            string queryString = $"select * from {name}";
            SqlCommand command = new SqlCommand(queryString, DataBase.getConnection());
            DataBase.openConnection();
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRow(dgw, reader, name);
            }

            reader.Close();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            //CreateColumns();
            //RefreshDataGrid(dataGridView1);

            //dataGridView1.Columns[6].Visible = false;
            label8.Visible = false;
            textBox1.Visible = false;
            groupBox3.Visible = false;
            groupBox4.Visible = false;
            groupBox1.Visible = false;
            groupBox2.Visible = false;
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedRow = e.RowIndex;

            if(e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[selectedRow];

                textBox7.Text = row.Cells[0].Value.ToString();
                textBox6.Text = row.Cells[1].Value.ToString();
                textBox5.Text = row.Cells[2].Value.ToString();
                textBox4.Text = row.Cells[3].Value.ToString();
                textBox3.Text = row.Cells[4].Value.ToString();
                textBox2.Text = row.Cells[5].Value.ToString();

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            RefreshDataGrid(dataGridView1, "Kniga");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 frm3 = new Form3();
            frm3.Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Search(dataGridView1);
        }

        private void Search(DataGridView dgw)
        {
            dgw.Rows.Clear();

            string searchString = $"select * from project_db where concat (id, type_nam, type_fam, type_group, type_project, type_sr) like '%" + textBox1.Text + "%'";

            SqlCommand command = new SqlCommand(searchString, DataBase.getConnection());

            DataBase.openConnection();

            SqlDataReader read = command.ExecuteReader();

            while (read.Read())
            {
                ReadSingleRow(dgw, read, "Kniga");
            }

            read.Close();
        }

        private void Search1(DataGridView dgw)
        {
            dgw.Rows.Clear();

            string searchString = $"select * from project_db where concat (type_project, id) like '%{textBox8.Text}%'";

            SqlCommand command = new SqlCommand(searchString, DataBase.getConnection());

            DataBase.openConnection();

            SqlDataReader read = command.ExecuteReader();

            while (read.Read())
            {
                ReadSingleRow(dgw, read, "Kniga");
            }

            read.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            deleteRow();
        }


        private void Update()
        {
            DataBase.openConnection();

            for(int index = 0; index < dataGridView1.Rows.Count; index++)
            {
                
                var rowState = (RowState)(dataGridView1.Rows[index].Cells[6].Value);

                if(rowState == RowState.Existed)
                {
                    continue;
                }

                if(rowState == RowState.Modified)
                {
                    
                    var id = dataGridView1.Rows[index].Cells[0].Value.ToString();
                    var name = dataGridView1.Rows[index].Cells[1].Value.ToString();
                    var fam = dataGridView1.Rows[index].Cells[2].Value.ToString();
                    var group = dataGridView1.Rows[index].Cells[3].Value.ToString();
                    var project = dataGridView1.Rows[index].Cells[4].Value.ToString();
                    var sr = dataGridView1.Rows[index].Cells[5].Value.ToString();

                    var changequery = $"update project_db set type_nam = '{name}', type_fam = '{fam}', type_group = '{group}', type_project = '{project}', type_sr = '{sr}' where id = '{id}'";
                    var command = new SqlCommand(changequery, DataBase.getConnection());

                    command.ExecuteNonQuery();
                }

                if(rowState == RowState.Deleted)
                {
                    var id = Convert.ToInt32(dataGridView1.Rows[index].Cells[0].Value);
                    var deleequery = $"delete from project_db where id = {id}";
                    var command = new SqlCommand(deleequery, DataBase.getConnection());
                    command.ExecuteNonQuery();
                }
            }

            DataBase.closeConnection();
        }

        private void deleteRow()
        {
            int index = dataGridView1.CurrentCell.RowIndex;
            dataGridView1.Rows[index].Visible = false;


            dataGridView1.Rows[index].Cells[6].Value = RowState.Deleted;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Update();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            change();
        }

        private void change()
        {
            var selectedRowIndex = dataGridView1.CurrentCell.RowIndex;
            var id = textBox7.Text;
            var name = textBox6.Text;
            var fam = textBox5.Text;
            var group = textBox4.Text;
            var project = textBox3.Text;
            var sr = textBox2.Text;

            dataGridView1.Rows[selectedRow].SetValues(id, name, fam, group, project, sr);
            dataGridView1.Rows[selectedRow].Cells[6].Value = RowState.Modified;


        }

        private void button6_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
            groupBox2.Visible = false;
            groupBox3.Visible = true;
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            
        }

        private void матанализToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            Search1(dataGridView1);
            richTextBox1.Text = Convert.ToString(dataGridView1.Rows.Count);
        }

        private void Search2(DataGridView dgw, string searchString)
        {
            dgw.Rows.Clear();

            

            SqlCommand command = new SqlCommand(searchString, DataBase.getConnection());

            DataBase.openConnection();

            SqlDataReader read = command.ExecuteReader();

            while (read.Read())
            {
                ReadSingleRow(dgw, read, "Kniga");
            }

            read.Close();
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    string searchString = $"select * from project_db where type_sr <= 3 AND type_project LIKE '%{textBox8.Text}%'";
                    Search2(dataGridView1, searchString);
                    richTextBox2.Text = Convert.ToString(dataGridView1.Rows.Count);
                    break;

                case 1:
                    string searchString1 = $"select * from project_db where type_sr > 3 AND type_sr <= 4 AND type_project LIKE '%{textBox8.Text}%'";
                    Search2(dataGridView1, searchString1);
                    richTextBox2.Text = Convert.ToString(dataGridView1.Rows.Count);
                    break;

                case 2:
                    string searchString2 = $"select * from project_db where type_sr > 4 AND type_sr <= 5 AND type_project LIKE '%{textBox8.Text}%'";
                    Search2(dataGridView1, searchString2);
                    richTextBox2.Text = Convert.ToString(dataGridView1.Rows.Count);
                    break;

                case 3:
                    string searchString3 = $"select * from project_db where type_sr >= 3 AND type_project LIKE '%{textBox8.Text}%'";
                    Search2(dataGridView1, searchString3);
                    richTextBox2.Text = Convert.ToString(dataGridView1.Rows.Count);
                    break;
                case 4:
                    string searchString4 = $"select * from project_db where type_sr >= 4 AND type_project LIKE '%{textBox8.Text}%'";
                    Search2(dataGridView1, searchString4);
                    richTextBox2.Text = Convert.ToString(dataGridView1.Rows.Count);
                    break;
                case 5:
                    string searchString5 = $"select * from project_db where type_sr >= 2 AND type_project LIKE '%{textBox8.Text}%'";
                    Search2(dataGridView1, searchString5);
                    richTextBox2.Text = Convert.ToString(dataGridView1.Rows.Count);
                    break;
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            deleteRow();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = true;
            groupBox2.Visible = true;
            groupBox3.Visible = false;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Update();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            groupBox3.Visible = false;
            groupBox4.Visible = true;
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button13_Click(object sender, EventArgs e)
        {
            groupBox3.Visible = true;
            groupBox4.Visible = false;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            int asd = 0;
            int eng = 0;
            int hist = 0;
            int kse = 0;
            int matan = 0;

            for(int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                if(Convert.ToString(dataGridView1.Rows[i].Cells[4].Value) == "АСД")
                {
                    asd++;
                }
                if (Convert.ToString(dataGridView1.Rows[i].Cells[4].Value) == "Английский язык")
                {
                    eng++;
                }
                if (Convert.ToString(dataGridView1.Rows[i].Cells[4].Value) == "История")
                {
                    hist++;
                }
                if (Convert.ToString(dataGridView1.Rows[i].Cells[4].Value) == "Естествознание")
                {
                    kse++;
                }
                if (Convert.ToString(dataGridView1.Rows[i].Cells[4].Value) == "Матанализ")
                {
                    matan++;
                }
            }

            textBox13.Text = Convert.ToString(asd);
            textBox12.Text = Convert.ToString(eng);
            textBox11.Text = Convert.ToString(hist);
            textBox10.Text = Convert.ToString(kse);
            textBox9.Text = Convert.ToString(matan);
        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e)
        {
            SaveFileDialog savefile = new SaveFileDialog();
            savefile.DefaultExt = ".txt";
            savefile.Filter = "Text files|*.txt";
            if(savefile.ShowDialog() == System.Windows.Forms.DialogResult.OK && savefile.FileName.Length > 0)
            {
                using(StreamWriter sw = new StreamWriter(savefile.FileName, true))
                {
                    sw.WriteLine("АСД: " + textBox13.Text);
                    sw.WriteLine("Английский язык: " + textBox12.Text);
                    sw.WriteLine("История: " + textBox11.Text);
                    sw.WriteLine("Естествознание: " + textBox10.Text);
                    sw.WriteLine("Матанализ :" + textBox9.Text);
                    sw.Close();
                }
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int ind = comboBox2.SelectedIndex;
            if(ind == 0) 
            {
                Wewe(0);
            }

            if (ind == 1)
            {
                Wewe(1);
            }

            if (ind == 2)
            {
                Wewe(2);
            }

            if (ind == 3)
            {
                Wewe(3);
            }
        }
    }
}
