using BattleShipConsole;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BattleShip
{
    public partial class Form1 : Form
    {
        ShipList playersShips;
        ShipList computerShips;
        Settings settings;
        int roundNum = 1;
        int playerScore = 0;
        int computerScore = 0;
        int numDamagedPlayerShips = 0;
        int numDamagedComputerShips = 0;

        public Form1(ShipList playerShips)
        {
            InitializeComponent();
            this.settings = new Settings();
            this.playersShips = playerShips;
            this.computerShips = new ShipList(this.settings);
            this.computerShips.Generate();
            for (int i = 0; i < this.settings.Size; i++)
                this.dataGridView1.Columns.Add($"{i}",$"{i}");
            for (int i = 0; i < this.settings.Size; i++)
            {
                Ship[] playerSh = new Ship[this.settings.Size];
                for (int j = 0; j < this.settings.Size; j++)
                    playerSh[j] = this.playersShips[i, j];
                this.dataGridView1.Rows.Add(playerSh);
            }
            for (int i = 0; i < this.settings.Size; i++)
                this.dataGridView2.Columns.Add($"{i}", $"{i}");
            for (int i = 0; i < this.settings.Size; i++)
            {
                Ship[] computerShips = new Ship[this.settings.Size];
                for (int j = 0; j < this.settings.Size; j++)
                    computerShips[j] = this.computerShips[i, j];
                this.dataGridView2.Rows.Add(computerShips);
            } 
            string str = "The game will use the grid size defined in the settings file" + "\r\n";
            str += $"Playing grid size set as ({this.settings.Size} X {this.settings.Size})" + "\r\n";
            str += $"Maximum number of ships allowed as {this.settings.numberShips}" + "\r\n";
            str += $"Multiple hits allowed per ships set as {this.settings.hitsAllowed}" + "\r\n";
            str += "Computer Ships Visible: ";
            if (this.settings.computerShipsVisible)
                str += "ON";
            else
                str += "OFF";
            textBox1.Text = str;
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.computerShips[e.RowIndex, e.ColumnIndex].Damaged)
            {
                MessageBox.Show("The ship is already destroyed!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string str1 = $"Beginning round {this.roundNum}" + "\r\n";            
            if (this.computerShips[e.RowIndex, e.ColumnIndex] != null)
            {
                this.computerShips[e.RowIndex, e.ColumnIndex].HitMade();
                if (this.computerShips[e.RowIndex, e.ColumnIndex].Damaged)
                {
                    textBox2.Text = $"Unfortunately, the Computer Ship {this.computerShips[e.RowIndex, e.ColumnIndex].Name} has been destroyed";
                    this.numDamagedComputerShips++;
                    if (this.numDamagedComputerShips == this.settings.numberShips)
                    {
                        MessageBox.Show("Congratulations!!! Player Wins!!!!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        string str = $"Player Wins. Final Score Player({this.playerScore}) and Computer({this.computerScore})";
                        File.WriteAllText("outcome.txt", str);
                        return;
                    }
                }
                else if (this.computerShips[e.RowIndex, e.ColumnIndex].Hit)
                    textBox2.Text = "PLAYER HITTTTTT!!!!";
                this.playerScore += 10;
            }
            else
                textBox2.Text = "PLAYER MISSSS!!!!";

            int compxPos = new CoordinateGenerator(1, 5).Generate();
            int compyPos = new CoordinateGenerator(1, 5).Generate();
            if (this.playersShips[compxPos, compyPos] != null)
            {
                this.playersShips[compxPos, compyPos].HitMade();
                if (this.playersShips[compxPos, compyPos].Damaged)
                {
                    textBox4.Text = $"Unfortunately, the Player Ship {this.playersShips[compxPos, compyPos].Name} has been destroyed";
                    this.numDamagedPlayerShips++;
                    if (this.numDamagedPlayerShips == this.settings.numberShips)
                    {
                        MessageBox.Show("Congratulations!!! Computer Wins!!!!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        string str = $"Computer Wins. Final Score Player({this.playerScore}) and Computer({this.computerScore})";
                        File.WriteAllText("outcome.txt", str);
                        return;
                    }
                }
                else if (this.playersShips[compxPos, compyPos].Hit)
                    textBox4.Text = "COMPUTER HITTTTTT!!!!";
                this.computerScore += 10;
            }
            else
                textBox4.Text = "COMPUTER MISSSSS!!!!";
            dataGridView1.Refresh();
            str1 += $"Player Score: {this.playerScore}" + "\r\n";
            str1 += $"Computer Score: {this.computerScore}";
            label3.Text = str1;
            this.roundNum++;
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
