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

        int elevator;
        public Form1()
        {
            InitializeComponent();
            createButtonPanelEtaz();
        }

        private void createButtonPanelEtaz()
        {
            int x = 6;
            buttons = new Button[6];
            buttons_strelki = new Button[12];
            knopki = new Button[6];
            for (int i = 0; i < 6; i++)
            {
                buttons[i] = new Button();
                buttons[i].Size = new Size(50, 50);
                buttons[i].Location = new Point(25, 50 + 55 * (i));
                buttons[i].Click += Button_Click;
                buttons[i].Text = x.ToString();
                buttons[i].Enabled = false;
                buttons[i].FlatAppearance.BorderColor = Color.Yellow;
                buttons[i].FlatAppearance.BorderSize = 2;
                x--;
                etaz.Controls.Add(buttons[i]);

                if (i == 0)
                {
                    buttons_strelki[1] = new Button();
                    buttons_strelki[1].Size = new Size(25, 20);
                    buttons_strelki[1].Location = new Point(buttons[0].Right + 5, buttons[0].Top + 25);
                    buttons_strelki[1].Click += Vizov_Lift;
                    buttons_strelki[1].Text = "↓";
                    buttons_strelki[1].Tag = i.ToString();
                    etaz.Controls.Add(buttons_strelki[1]);
                }
                else if (i == 5)
                {
                    buttons_strelki[10] = new Button();
                    buttons_strelki[10].Size = new Size(25, 20);
                    buttons_strelki[10].Location = new Point(buttons[5].Right + 5, buttons[5].Bottom - 45);
                    buttons_strelki[10].Click += Vizov_Lift;
                    buttons_strelki[10].Text = "↑";
                    buttons_strelki[10].Tag = i.ToString();
                    etaz.Controls.Add(buttons_strelki[10]);
                }
                else
                {
                    buttons_strelki[i * 2] = new Button();
                    buttons_strelki[i * 2].Size = new Size(25, 20);
                    buttons_strelki[i * 2].Location = new Point(buttons[i].Right + 5, buttons[i].Top + 5);
                    buttons_strelki[i * 2].Click += Vizov_Lift;
                    buttons_strelki[i * 2].Text = "↑";
                    buttons_strelki[i * 2].Tag = i.ToString();
                    etaz.Controls.Add(buttons_strelki[i * 2]);


                    buttons_strelki[(i * 2) + 1] = new Button();
                    buttons_strelki[(i * 2) + 1].Size = new Size(25, 20);
                    buttons_strelki[(i * 2) + 1].Location = new Point(buttons[i].Right + 5, buttons[i].Bottom - 25);
                    buttons_strelki[(i * 2) + 1].Click += Vizov_Lift;
                    buttons_strelki[(i * 2) + 1].Text = "↓";
                    buttons_strelki[(i * 2) + 1].Tag = i.ToString();
                    etaz.Controls.Add(buttons_strelki[(i * 2) + 1]);
                }
            }
            int y = 6;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    knopki[i] = new Button();
                    knopki[i].Size = new Size(25, 25);
                    knopki[i].Location = new Point(55 - 30 * j, 25 + 30 * i);
                    knopki[i].Click += Lift_Knopki;
                    knopki[i].Text = (y).ToString();
                    knopki[i].Tag = y.ToString();
                    knopka.Controls.Add(knopki[i]);
                    y--;
                }
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {

        }

        private void Vizov_Lift(object sender, EventArgs e)
        {
            //Button clickedButton = (Button)sender;
            //int buttonNomer = Convert.ToInt32((string)clickedButton.Tag);
            //Buttons_Etaz_Kakoy(buttonNomer);    
        }

        private void Lift_Knopki(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            int buttonNomer = Convert.ToInt32((string)clickedButton.Tag);
            Buttons_Etaz_Kakoy(buttonNomer);

            
        }

        private void Buttons_Etaz_Kakoy(int x)
        {
            for (int i = 0; i < 6; i++)
            {
                buttons[i].BackColor = Color.White;
            }

            buttons[x].BackColor = Color.Red;

            label1.Text = elevator.ToString();

            timer1.Start();
            elevator = x;
            timer1.Stop();


        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = @"Лифт находится в {x} этаже";
            label1.Text = elevator.ToString();
        }
    }
}
