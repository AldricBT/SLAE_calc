using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
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
using Microsoft.Win32;

namespace SLAE_calc
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int Max_Eqv = 100;    //Максимальное количество уравнений  
        int NumOfEqn;
        SLAE slae;

        public MainWindow()
        {
            slae = new SLAE(2);
            InitializeComponent();
            NumOfEqn = int.Parse(Tb_numofeqn.Text);            
            DataTable_Refresh();
        }
        private void Tb_numofeqn_TextChanged(object sender, TextChangedEventArgs e)
        {   
            if (Tb_numofeqn.Text != "")
            {
                if (int.Parse(Tb_numofeqn.Text) > Max_Eqv)
                {
                    Tb_numofeqn.Text = Convert.ToString(Max_Eqv);
                }
                if (int.Parse(Tb_numofeqn.Text) < 2)
                {
                    Tb_numofeqn.Text = "2";
                }
            }
            else
            {
                Tb_numofeqn.Text = Convert.ToString(2);
            }
            if (massive != null)
            {
                DataTable_Refresh();
            }
        }
        /// <summary>
        /// Кнопка "Увеличение кол-ва уравнений на 1"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpArrow_Click(object sender, RoutedEventArgs e)
        {            
            if (int.Parse(Tb_numofeqn.Text) < Max_Eqv)
            {
                Tb_numofeqn.Text = Convert.ToString(int.Parse(Tb_numofeqn.Text) + 1);
            }
            DataTable_Refresh();
        }
        /// <summary>
        /// Кнопка "Уменьшение кол-ва уравнений на 1"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BotArrow_Click(object sender, RoutedEventArgs e)
        {
            if (int.Parse(Tb_numofeqn.Text) > 2)
            {
                Tb_numofeqn.Text = Convert.ToString(int.Parse(Tb_numofeqn.Text) - 1);
            }
            DataTable_Refresh();
        }

        
        
        /// <summary>
        /// Кнопка Exit в меню
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// Кнопка Save... в меню
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {//привязан хоткей Ctrl+S
            SaveFileDialog dlg = new SaveFileDialog();
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
        /// <summary>
        /// Кнопка Load...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog()
            {
                CheckFileExists = false,
                CheckPathExists = true,
                Multiselect = false,
                Title = "Выберите файл"
            };

            if (dlg.ShowDialog() == true)
            {
                string filename = dlg.FileName;
                slae.Load(filename);
            }
            Tb_numofeqn.Text = $"{slae.M.GetLength(0)}";
            DataTable_Refresh();
            
        }
        /// <summary>
        /// Хот-кей Ctrl+S на сохранение
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyboardDevice.Modifiers == (ModifierKeys.Control) && e.Key == Key.S)
            {
                MenuItem_Click_1(sender, e);
            }
        }

        private void DataTable_Refresh()
        {
            NumOfEqn = int.Parse(Tb_numofeqn.Text);
            slae.Resize(NumOfEqn);
            massive.RowHeight = 30;
            massive.ColumnWidth = 30;
            massive.Width = (NumOfEqn + 1) * massive.RowHeight;
            massive.Height = (NumOfEqn ) * massive.RowHeight;
            DataTable dt = new DataTable();
            for (int i = 0; i < slae.M.GetLength(1); i++)
            {
                dt.Columns.Add(i.ToString(), typeof(double));
            }

            for (int i = 0; i < slae.M.GetLength(0); i++)
            {
                DataRow dr = dt.NewRow();
                for (int j = 0; j < slae.M.GetLength(1); j++)
                {
                    dr[j] = slae.M[i,j];
                }
                dt.Rows.Add(dr);
            }
            massive.ItemsSource = dt.DefaultView;
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
            if (isnum == true)
            {
                return true;
            }
            else if (s == "")
            {
                return false;
            }
            else if (s[0] == '-')
            {
                return true;
            }
            else
            {
                return false;
            }
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
            return isnum;
        }

        /// <summary>
        /// Data Random
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            DataGenerator d = new RandG();
            slae.M = d.Generate(NumOfEqn);
            DataTable_Refresh();
        }
        /// <summary>
        /// Data Symmetry
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            DataGenerator d = new SymRandG();
            slae.M = d.Generate(NumOfEqn);
            DataTable_Refresh();
        }

        private void MenuItem_Click_5(object sender, RoutedEventArgs e)
        {
            slae.Solve(slae.M);
        }
    }
}
