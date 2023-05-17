using BattleShipConsole;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BattleShip
{
    public partial class MainForm : Form
    {
        ShipList playerShips;
        Settings settings;

        public MainForm()
        {
            InitializeComponent();
            this.settings = new Settings();
            if (this.settings.numberShips > (this.settings.Size * this.settings.Size))
            {
                MessageBox.Show($"Number of ships ccan be more than {this.settings.Size} x  {this.settings.Size}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
            this.playerShips = new ShipList(settings);
            for (int i = 0; i < this.settings.Size; i++)
                this.dataGridView1.Columns.Add($"{i}", $"{i}");
            for (int i = 0; i < this.settings.Size; i++)
                this.dataGridView1.Rows.Add();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.settings.numberShips == this.playerShips.Count)
            {
                MessageBox.Show($"You can't add more than {this.settings.numberShips} ships", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Ship ship = this.playerShips.AddShip(e.RowIndex, e.ColumnIndex);
            if (ship == null)
                MessageBox.Show("The ship is already existed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
                this.dataGridView1[e.ColumnIndex, e.RowIndex].Value = ship;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.playerShips.Count != this.settings.numberShips)
            {
                MessageBox.Show("Not all ships are added!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            this.Hide();
            Form1 sistema = new Form1(this.playerShips);
            sistema.ShowDialog();
            this.Close();
        }
    }
}
