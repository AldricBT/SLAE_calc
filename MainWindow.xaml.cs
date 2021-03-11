using System;
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
        /// <summary>
        /// Окно для ввода количества уравнений
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tb_numofeqn_TextChanged(object sender, TextChangedEventArgs e)
        {            
            TextBox textBox = (TextBox)sender;            
            if (tb_numofeqn.Text != "")
            {
                if (tb_numofeqn.Text[0] != '0')
                {
                    if (int.Parse(tb_numofeqn.Text) > Max_Eqv)
                    {
                        tb_numofeqn.Text = Convert.ToString(Max_Eqv);
                    }
                    if (int.Parse(tb_numofeqn.Text) < 2)
                    {
                        tb_numofeqn.Text = Convert.ToString(2);
                    }
                }
                else
                {                    
                    tb_numofeqn.Text = tb_numofeqn.Text.Remove(0,1);    //если первый символ строки "0", удаляет его
                }
            }
            else
            {
                tb_numofeqn.Text = Convert.ToString(2);
            }
            if (Panel1 != null)
            {
                Panel1_Initialized(sender, e);
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
            }
        }

        private void Panel1_Initialized(object sender, EventArgs e)
        {            
            NumOfEqn = int.Parse(tb_numofeqn.Text);
            slae = new SLAE(NumOfEqn);
            Panel1.Children.Clear();
            tb = new TextBox[NumOfEqn*(NumOfEqn + 1)];
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
                    tb[j + j_end].Tag = new Point(i,j);
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
            Point index = (Point)tb_help.Tag;
            slae.M[(int)index.X, (int)index.Y] = int.Parse(tb_help.Text);
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

        
    }
}
