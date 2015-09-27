using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        string conStr = "server=127.0.0.1;user=root;database=summer;password=482701;";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "summerDataSet.contayner". При необходимости она может быть перемещена или удалена.
            this.contaynerTableAdapter.Fill(this.summerDataSet.contayner);
            this.очиститьРабочуюОбластьToolStripMenuItem.Enabled = false;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form1_Click(object sender, EventArgs e)
        {
            /*using (System.Data.SqlClient.SqlConnection myConn = new System.Data.SqlClient.SqlConnection(@"Data Source=summerDataSet;Initial Catalog=summer;User ID=root;Password=482701"))
            {
                //myConn.Open();
                System.Data.SqlClient.SqlCommand myCmd = new System.Data.SqlClient.SqlCommand();
                myCmd.Connection = myConn;
                myCmd.CommandType = CommandType.StoredProcedure;
                myCmd.CommandText = "имя вашей процедуры";
                System.Data.SqlClient.SqlDataAdapter DA = new System.Data.SqlClient.SqlDataAdapter(myCmd);
                DataTable DT = new DataTable();
                DA.Fill(DT);
                dataGridView1.DataSource = DT;

            }
            MessageBox.Show("Hellow");*/
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void загрузкаToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void формированиеБазыДанныхToolStripMenuItem_Click(object sender, EventArgs e) // activeMenu = 1;
        {
            this.ChangedSelectMenu(1);
        }

        private void разработчикиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Данный программный продукт разрабатывали студенты Института компьютерных технологий и информационной безопасности при Южном Федеральном Университете.\n\nРазработчики:\nПрограммная часть: Писарев Владислав Дмитриевич\nГрафическое сопровождение: Глобенко Александр 'Отчестсво'\nАлгоритмами размещения занимался: Петренко Станислав Николаевич", "Информация о разработчиках", MessageBoxButtons.OK, MessageBoxIcon.Question);
        }

        private void ChangedSelectMenu(int activeMenu)
        {
            if (activeMenu == 0) MessageBox.Show("Переменная activeMenu равна 0 и отслеживание выбранного пункта меню невозможно!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error,MessageBoxDefaultButton.Button1);
            else
            {
                switch(activeMenu)
                {
                    case 1:
                    {
                        this.panel1.Visible = true;
                        this.panel1.Enabled = true;
                        //this.формированиеБазыДанныхToolStripMenuItem.BackColor = Color.Goldenrod;
                        this.формированиеБазыДанныхToolStripMenuItem.Enabled = false;
                        this.очиститьРабочуюОбластьToolStripMenuItem.Enabled = true;
                        break;
                    }
                    case 2:
                    {
                        this.panel1.Visible = false;
                        this.очиститьРабочуюОбластьToolStripMenuItem.Enabled = false;
                        this.формированиеБазыДанныхToolStripMenuItem.Enabled = true;
                        break;
                    }
                    default: MessageBox.Show("Неизвестная ошибка.\n\nInfo:\nfuncthion: ChangeSelectMenu\nMoment error: switch(acteveMenu)->default", "Error", MessageBoxButtons.OK,MessageBoxIcon.Error, MessageBoxDefaultButton.Button1); break;
                }
            }
        }

        private void panel1_EnabledChanged(object sender, EventArgs e)
        {
            this.UpdatePanelInfo();
        }

        private void очиститьРабочуюОбластьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ChangedSelectMenu(2);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.UpdatePanelInfo();
        }

        private void UpdatePanelInfo()
        {
            this.CheckInfo();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(this.comboBox2.SelectedIndex == 2)
            {
                this.textBox5.Enabled = true;
                this.label9.Enabled = true;
            }
            else
            {
                this.textBox5.Enabled = false;
                this.label9.Enabled = false;
            }
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string nextPort = null;
            if (this.comboBox2.SelectedIndex == 2)
            {
                nextPort = this.textBox5.Text;
            }
            this.InsertCont(this.textBox1.Text, this.comboBox1.SelectedIndex.ToString(), this.comboBox2.SelectedIndex.ToString(), this.textBox2.Text, this.textBox3.Text, this.textBox4.Text, nextPort, this.comboBox3.SelectedIndex.ToString(), this.dateTimePicker1.Value.ToString(), this.comboBox4.SelectedIndex.ToString());
        }

        private DataTable GetComments()
        {
            DataTable dt = new DataTable();

            string queryString = @"SELECT COUNT(*) FROM `contayner`";

            using (MySqlConnection con = new MySqlConnection(conStr))
            {
                MySqlCommand com = new MySqlCommand(queryString, con);

                try
                {
                    con.Open();

                    using (MySqlDataReader dr = com.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            dt.Load(dr);
                            MessageBox.Show("Kol:" + dt.Select());
                        }
                    }
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            return dt;
        }
        private void InsertCont(string idcon, string type, string nextTran, string UIDnextTran, string destination, string from, string nextport, string nowStats, string date, string prior)
        {
            string queryString = @"INSERT INTO `contayner` (`IDContayner`, `type`, `next_transport`, `transport_id`, `destination`, `from`, `port_id_next`, `now_stats`, `time_exit`, `priority`) VALUES ('" + idcon + "', '" + type + "', '" + nextTran + "', '" + UIDnextTran + "', '" + destination + "', '"+from+"', '"+nextport+"', '"+nowStats+"', '"+date+"', '"+prior+"');";
            using (MySqlConnection con = new MySqlConnection(conStr))
            {
                MySqlCommand com = new MySqlCommand(queryString, con);
                con.Open();
                com.ExecuteNonQuery();
                con.Close();
            }
            this.CheckInfo();
        }

        private void CheckInfo()
        {
            string queryString = @"SELECT COUNT(*) FROM `contayner`";
            using (MySqlConnection con = new MySqlConnection(conStr))
            {
                MySqlCommand com = new MySqlCommand(queryString, con);
                con.Open();
                long cout = (long)com.ExecuteScalar();
                con.Close();
                this.label2.Text = cout.ToString();
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show(dateTimePicker1.Value.ToUniversalTime().ToShortTimeString());
        }
    }
}


