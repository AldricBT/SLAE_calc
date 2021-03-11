﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SLAE_calc
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int Max_Eqv = 10;    //Максимальное количество уравнений  
        int NumOfEqn;
        SLAE slae;
        TextBox[] tb;

        public MainWindow()
        {            
            InitializeComponent();
        }
        private void tb_numofeqn_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsNumOfEqn(e.Text);
            TextBox textBox = (TextBox)sender;
            if (IsNumOfEqn(e.Text))
            {
                if (e.Text != "")
                {
                    if (e.Text[0] != '0')
                    {
                        if (int.Parse(e.Text) > Max_Eqv)
                        {
                            tb_numofeqn.Text = Convert.ToString(Max_Eqv);
                        }
                        if (int.Parse(tb_numofeqn.Text) < 2)
                        {
                            tb_numofeqn.Text = "2";
                        }
                    }
                    else
                    {
                        tb_numofeqn.Text = tb_numofeqn.Text.Remove(0, 1);    //если первый символ строки "0", удаляет его
                    }
                }
                else
                {
                    tb_numofeqn.Text = Convert.ToString(2);
                }
                if (Panel1 != null)
                {
                    Panel1_Refresh(sender, e);
                }
            }            
        }
        /// <summary>
        /// Кнопка "Увеличение кол-ва уравнений на 1"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpArrow_Click(object sender, RoutedEventArgs e)
        {
            if (int.Parse(tb_numofeqn.Text) < Max_Eqv)
            {
                tb_numofeqn.Text = Convert.ToString(int.Parse(tb_numofeqn.Text) + 1);
                Panel1_Initialized(sender, e);
            }
        }
        /// <summary>
        /// Кнопка "Уменьшение кол-ва уравнений на 1"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BotArrow_Click(object sender, RoutedEventArgs e)
        {
            if (int.Parse(tb_numofeqn.Text) > 2)
            {
                tb_numofeqn.Text = Convert.ToString(int.Parse(tb_numofeqn.Text) - 1);
                Panel1_Initialized(sender, e);
            }
        }

        private void Panel1_Initialized(object sender, EventArgs e)
        {            
            NumOfEqn = int.Parse(tb_numofeqn.Text);
            slae = new SLAE(NumOfEqn);
            Panel1.Children.Clear();
            tb = new TextBox[NumOfEqn * (NumOfEqn + 1)];
            int j_end = 0;

            for (int i = 0; i < NumOfEqn; i++)
            {
                StackPanel sp = new StackPanel
                {
                    Name = "Panel" + i + 2,
                    Orientation = Orientation.Horizontal
                };
                Panel1.Children.Add(sp);
                for (int j = 0; j < NumOfEqn + 1; j++)
                {
                    //Добавление TextBox куда вводятся коэф. матрицы
                    //Можно прокачать, чтоб не удалялись уже введенные значения! 
                    tb[j + j_end] = new TextBox();
                    tb[j + j_end].Text = "0";
                    tb[j + j_end].Tag = new Point(i, j);
                    tb[j + j_end].TextChanged += MyTextChange;
                    sp.Children.Add(tb[j + j_end]);

                    //Добавление TextBlock'ов переменных
                    if (j < NumOfEqn - 1)
                    {
                        sp.Children.Add(new TextBlock
                        {
                            Text = $"x{SubIndex(j)} + "
                        });
                    }
                    else if (j == NumOfEqn - 1)
                    {
                        sp.Children.Add(new TextBlock
                        {
                            Text = $"x{SubIndex(j)} = "
                        });
                    }

                    slae.M[i, j] = int.Parse(tb[j + j_end].Text);   //Перенос данных с GUI в Matrix
                }
                j_end += NumOfEqn;
            }
                     
        }

        /// <summary>
        /// Обновляет измененную переменную в Matrix
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MyTextChange(object sender, EventArgs e)
        {
            //обновляет измененную переменную в Matrix
            TextBox tb_help = sender as TextBox;
            if (IsNum(tb_help.Text))
            {
                Point index = (Point)tb_help.Tag;
                slae.M[(int)index.X, (int)index.Y] = int.Parse(tb_help.Text);
            }            
        }
       
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {//можно привязать хоткей Ctrl+S
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "test";
            dlg.DefaultExt = ".txt";
            dlg.Filter = "Text documents (.txt)|*.txt";
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                string filename = dlg.FileName;
                slae.Save(filename);
            }
            
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyboardDevice.Modifiers == (ModifierKeys.Control) && e.Key == Key.S)
            {
                MenuItem_Click_1(sender, e);
            }
        }

        /// <summary>
        /// Запись числа с нижней индексацией
        /// </summary>
        /// <param name="j">Число (максимум двузначное)</param>
        /// <returns>Строка с нижней индексацией</returns>
        private string SubIndex(int j)
        {
            string sub_index = null;
            if (j < 9)
            {//нижний индекс для цифры
                sub_index = ((char)(8321 + j)).ToString();
            }
            else if ((j > 8) && (j < 99))
            {//нижний индекс для двузначных чисел      
                sub_index = ((char)(8320 + ((j + 1) / 10))).ToString() + ((char)(8320 + ((j + 1) % 10))).ToString();
            }
            return sub_index;
        }
        /// <summary>
        /// Является ли строка числом
        /// </summary>
        /// <param name="s">Строка</param>
        /// <returns>True, если число</returns>
        private bool IsNum(string s)
        {
            int num;
            bool isnum = int.TryParse(s, out num);
            return isnum;
        }
        /// <summary>
        /// Проверяет адекватность ввода количества уравнений
        /// </summary>
        /// <param name="s">Строка на вводе</param>
        /// <returns></returns>
        private bool IsNumOfEqn(string s)
        {
            int num;
            bool isnum = int.TryParse(s, out num);
            if (isnum)
            {
                if ((num < 2) || (num > Max_Eqv))
                {
                    return false;
                }
            }
            return isnum;
        }


        private void Panel1_Refresh(object sender, TextCompositionEventArgs e)
        {
            NumOfEqn = int.Parse(e.Text);
            slae = new SLAE(NumOfEqn);
            Panel1.Children.Clear();
            tb = new TextBox[NumOfEqn * (NumOfEqn + 1)];
            int j_end = 0;

            for (int i = 0; i < NumOfEqn; i++)
            {
                StackPanel sp = new StackPanel
                {
                    Name = "Panel" + i + 2,
                    Orientation = Orientation.Horizontal
                };
                Panel1.Children.Add(sp);
                for (int j = 0; j < NumOfEqn + 1; j++)
                {
                    //Добавление TextBox куда вводятся коэф. матрицы
                    //Можно прокачать, чтоб не удалялись уже введенные значения! 
                    tb[j + j_end] = new TextBox();
                    tb[j + j_end].Text = "0";
                    tb[j + j_end].Tag = new Point(i, j);
                    tb[j + j_end].TextChanged += MyTextChange;
                    sp.Children.Add(tb[j + j_end]);

                    //Добавление TextBlock'ов переменных
                    if (j < NumOfEqn - 1)
                    {
                        sp.Children.Add(new TextBlock
                        {
                            Text = $"x{SubIndex(j)} + "
                        });
                    }
                    else if (j == NumOfEqn - 1)
                    {
                        sp.Children.Add(new TextBlock
                        {
                            Text = $"x{SubIndex(j)} = "
                        });
                    }

                    slae.M[i, j] = int.Parse(tb[j + j_end].Text);   //Перенос данных с GUI в Matrix
                }
                j_end += NumOfEqn;
            }
        }

        private void OnPasting(object sender, DataObjectPastingEventArgs e)
        {
            var stringData = (string)e.DataObject.GetData(typeof(string));
            if (stringData == null || !IsNum(stringData))
            {
                e.CancelCommand();
            }
        }
        
        private void tb_numofeqn_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back || e.Key == Key.Delete)
            {
                e.Handled = false;                
            }
        }
    }
}
