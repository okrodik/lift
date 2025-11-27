using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lift
{
    public partial class Form1 : Form
    {
        Button[] buttons;
        Button[] buttons_strelki;
        Button[] knopki;

        int oldNomer;
        int newNomer;
        bool isMoving;
        public Form1()
        {
            InitializeComponent();
            createButtonPanelEtaz();

            oldNomer = 0;
            newNomer = 0;
            isMoving = false;
        }

        private void createButtonPanelEtaz()
        {
            int x = 6;
            buttons = new Button[6];
            buttons_strelki = new Button[10];
            knopki = new Button[6];

            int strelkaIndex = 0;

            for (int i = 0; i < 6; i++)
            {
                buttons[i] = new Button();
                buttons[i].Size = new Size(50, 50);
                buttons[i].Location = new Point(25, 50 + 55 * (i));
                buttons[i].Text = x.ToString();
                buttons[i].Enabled = false;
                buttons[i].FlatAppearance.BorderSize = 2;
                buttons[i].FlatAppearance.BorderColor = Color.Yellow;
                buttons[i].BackColor = Color.White;
                x--;
                etaz.Controls.Add(buttons[i]);

                if (i == 0)
                {
                    buttons_strelki[strelkaIndex] = new Button();
                    buttons_strelki[strelkaIndex].Size = new Size(25, 20);
                    buttons_strelki[strelkaIndex].Location = new Point(buttons[0].Right + 5, buttons[0].Top + 25);
                    buttons_strelki[strelkaIndex].Click += Vizov_Lift_Vniz;
                    buttons_strelki[strelkaIndex].Text = "↓";
                    buttons_strelki[strelkaIndex].Tag = i.ToString();
                    etaz.Controls.Add(buttons_strelki[strelkaIndex]);
                    strelkaIndex++;
                }
                else if (i == 5)
                {
                    buttons_strelki[strelkaIndex] = new Button();
                    buttons_strelki[strelkaIndex].Size = new Size(25, 20);
                    buttons_strelki[strelkaIndex].Location = new Point(buttons[5].Right + 5, buttons[5].Bottom - 45);
                    buttons_strelki[strelkaIndex].Click += Vizov_Lift_Verh;
                    buttons_strelki[strelkaIndex].Text = "↑";
                    buttons_strelki[strelkaIndex].Tag = i.ToString();
                    etaz.Controls.Add(buttons_strelki[strelkaIndex]);
                    strelkaIndex++;
                }
                else
                {
                    buttons_strelki[strelkaIndex] = new Button();
                    buttons_strelki[strelkaIndex].Size = new Size(25, 20);
                    buttons_strelki[strelkaIndex].Location = new Point(buttons[i].Right + 5, buttons[i].Top + 5);
                    buttons_strelki[strelkaIndex].Click += Vizov_Lift_Verh;
                    buttons_strelki[strelkaIndex].Text = "↑";
                    buttons_strelki[strelkaIndex].Tag = i.ToString();
                    etaz.Controls.Add(buttons_strelki[strelkaIndex]);
                    strelkaIndex++;

                    buttons_strelki[strelkaIndex] = new Button();
                    buttons_strelki[strelkaIndex].Size = new Size(25, 20);
                    buttons_strelki[strelkaIndex].Location = new Point(buttons[i].Right + 5, buttons[i].Bottom - 25);
                    buttons_strelki[strelkaIndex].Click += Vizov_Lift_Vniz;
                    buttons_strelki[strelkaIndex].Text = "↓";
                    buttons_strelki[strelkaIndex].Tag = i.ToString();
                    etaz.Controls.Add(buttons_strelki[strelkaIndex]);
                    strelkaIndex++;
                }
            }

            int[] nashiChisla = { 6, 5, 4, 3, 2, 1 };

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    int index = i * 2 + j;
                    int chislo = nashiChisla[index];
                    knopki[index] = new Button();
                    knopki[index].Size = new Size(25, 25);
                    knopki[index].Location = new Point(55 - 30 * j, 25 + 30 * i);
                    knopki[index].Click += Lift_Knopki;
                    knopki[index].Text = chislo.ToString();
                    knopki[index].Tag = (6 - chislo).ToString(); 
                    knopka.Controls.Add(knopki[index]);
                }
            }
        }

        private void Vizov_Lift_Verh(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;

            int buttonNomer = Convert.ToInt32((string)clickedButton.Tag);

            Buttons_Etaz_Kakoy(buttonNomer);
  
  
        }
        private void Vizov_Lift_Vniz(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;

            int buttonNomer = Convert.ToInt32((string)clickedButton.Tag);
    
            Buttons_Etaz_Kakoy(buttonNomer);

            
        }

        private void Lift_Knopki(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            int buttonNomer = Convert.ToInt32((string)clickedButton.Tag);

            

            Buttons_Etaz_Kakoy(buttonNomer);         
        }

        private void Buttons_Etaz_Kakoy(int x)
        {
            if (isMoving)
            {
                MessageBox.Show("Лифт уже движется");
                return;
            }

            newNomer = x;
            MethodAsync();
        }


        public async Task MethodAsync()
        {
            isMoving = true;
            pictureBox1.Image = Properties.Resources.closeDoor;
            int step;
            for (int i = 0; i < 6; i++)
            {
                buttons[i].BackColor = Color.White;
            }

            if (oldNomer > newNomer)
            {
                step = -1;
            }
            else
            {
                step = 1;
            }

            buttons[oldNomer].BackColor = Color.Red;

            for (int i = oldNomer; i != newNomer; i += step)
            {
                await Task.Delay(1000);

                int currentFloor = i + step;

                buttons[i].BackColor = Color.White;

                buttons[currentFloor].BackColor = Color.Red;

                oldNomer = currentFloor; 
            }

            buttons[newNomer].BackColor = Color.Green;
            pictureBox1.Image = Properties.Resources.openDoor;
            oldNomer = newNomer;

            isMoving = false;
        }
    }
}
