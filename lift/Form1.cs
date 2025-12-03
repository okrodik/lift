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
        Button[] buttons; //визуализация этажа
        Button[] buttons_strelki; //стрелки
        Button[] knopki; //кнопки

        int oldNomer; //старый этаж
        int newNomer; //новый этаж
        bool isMoving; //движение правда недвижение лодь
        public Form1()
        {
            InitializeComponent();
            createButtonPanelEtaz(); //функция создания поля

            oldNomer = 5; //изначальный этаж то есть 1
            newNomer = 0; //новый
            isMoving = false; 
        }

        private void createButtonPanelEtaz()
        {
            int x = 6; //кол чтсел
            buttons = new Button[6]; //создаем визуализацию
            buttons_strelki = new Button[10]; //стрелки
            knopki = new Button[6]; //кнопки

            int strelkaIndex = 0; //счет стрелок

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

                if (i == 0) //ПЕРВАЯ СТРЕЛКА
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
                else if (i == 5) //ПОСЛЕДНЯ СТРЕКЛА
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
                else //ОСТЛЬАНЫЕ СТРЕЛКИ
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

            int[] nashiChisla = { 6, 5, 4, 3, 2, 1 }; //ТЕКСТ ЧИСЕЛ

            for (int i = 0; i < 3; i++) //В ДВА РЯДА
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

        private void Vizov_Lift_Verh(object sender, EventArgs e) //КНОПКИ РАБОТА
        {
            Button clickedButton = (Button)sender; //ПОУЛЧАЕМ КАКАЯ КНОПКА
            int buttonNomer = Convert.ToInt32((string)clickedButton.Tag); //БЕРЕМ ИЗ ЕГО ТЕГА НОМЕР

            Buttons_Etaz_Kakoy(buttonNomer);  //ВЫЗЫВАЕМ ФУНКЦИЮ
        } //ПО ИДЕЕ МОЖНО ВСЕ ЭТИ ТРИ КНОПКИ В ОДНУ ЗАПИХНУТЬ, пусть останется для маштабирования
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

        private void Buttons_Etaz_Kakoy(int x) //проверка двигается ли лифт, и иницализирование нового этажа
        {
            if (isMoving)
            {
                MessageBox.Show("Лифт уже движется");
                return;
            }

            newNomer = x;
            MethodAsync();
        }


        public async Task MethodAsync() //метод рисовки
        {
            isMoving = true; //ЕСли лифт двигается
            pictureBox1.Image = Properties.Resources.closeDoor; //закрытие двери
            int step; //движение лифта
            for (int i = 0; i < 6; i++) //делаем все белым
            {
                buttons[i].BackColor = Color.White;
            }

            if (oldNomer > newNomer) //если страый номер больше нового минусуем этажи
            {
                step = -1; //спускаемся вниз
            }
            else
            {
                step = 1; //поднимаемся вверх
            }

            buttons[oldNomer].BackColor = Color.Red; //красим в красный текущий этаж типо движеться

            for (int i = oldNomer; i != newNomer; i += step)  //цикл для перехода по этажам
            {
                await Task.Delay(1000); //задежрка для красоты

                int etotNomer = i + step; //будущий этаж

                buttons[i].BackColor = Color.White; //красим предыдущий этаж белым

                buttons[etotNomer].BackColor = Color.Red; //красим в красный текущий этаж типо движеться

                oldNomer = etotNomer;  //делаем старый номер в предудщий
            }

            buttons[newNomer].BackColor = Color.Green; //красим зеленым типо мы приехзалт
            pictureBox1.Image = Properties.Resources.openDoor; //открываем двеврь
            oldNomer = newNomer; //новый этаж стал страым

            isMoving = false; //перестла двигаться
        }
    }
}
