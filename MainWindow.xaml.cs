using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using System.Threading;
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
using System.Globalization;
using SLAE_core;

namespace SLAE_calc
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int Max_Eqv = 100;    //Максимальное количество уравнений  
        SLAE slae;
        DataTable dt = new DataTable();
        public MainWindow()
        {
            slae = new SLAE(2);
            InitializeComponent();
            slae.NumOfEqn = int.Parse(Tb_numofeqn.Text);            
            DataTable_Refresh();
            UpArrow.Click += Refresh_solu_panel;
            BotArrow.Click += Refresh_solu_panel;
            Tb_numofeqn.TextChanged += Refresh_solu_panel;     
            
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
            if (slae.Load_error)
            {
                MessageBox.Show("Ошибка загрузки файла", "Ошибка!");
            }
            else
            {
                Tb_numofeqn.Text = $"{slae.M.GetLength(0)}";
                DataTable_Refresh();
                Fill_solu_panel();
            }            
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
            slae.NumOfEqn = int.Parse(Tb_numofeqn.Text);
            slae.Resize(int.Parse(Tb_numofeqn.Text));
            slae.IsSolved = false;
            massive.RowHeight = 30;
            massive.ColumnWidth = 30;
            massive.Width = (slae.NumOfEqn + 1) * massive.RowHeight;
            massive.Height = (slae.NumOfEqn) * massive.RowHeight;
            dt = new DataTable();
            //dt.Columns.Clear();
            //dt.Rows.Clear();
            //dt.Clear();
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
            if (solu_panel != null)
            {
                Fill_solu_panel();
            }
            dt.ColumnChanged += Dt_ColumnChanged;
            
        }
                
        private void Dt_ColumnChanged(object sender, DataColumnChangeEventArgs e)
        {
            if (dt.Rows.IndexOf(e.Row) != -1)
            {
                slae.M[dt.Rows.IndexOf(e.Row), e.Column.Ordinal] = (double)e.ProposedValue;
                slae.IsSolved = false;
                if (solu_panel != null)
                {
                    Fill_solu_panel();
                }
            }
            
            //slae.Roots.Solution_vector[1] = 2;
        }

        /// <summary>
        /// Заполняет столбец решений
        /// </summary>
        private void Fill_solu_panel()
        {
            Refresh_solu_panel();
            if (slae.IsSolved)
            {
                if (slae.Roots.Solution_exist)
                {
                    for (int i = 0; i < slae.NumOfEqn; i++)
                    {
                        solu_panel.Children.Add(new TextBlock
                        {
                            Text = $"x{SubIndex(i)} = {Math.Round(slae.Roots.Solution_vector[i], 3)}",
                            FontSize = 18,
                            FontFamily = new FontFamily("Times New Roman"),
                            Height = 20,
                            FontStyle = FontStyles.Normal
                        });
                    }
                    solu_panel.Children.Add(new TextBlock
                    {
                        Text = $"Погрешность решения {slae.Roots.Error.ToString("E", CultureInfo.InvariantCulture)}",
                        FontSize = 18,
                        FontFamily = new FontFamily("Times New Roman"),
                        FontStyle = FontStyles.Normal
                    });
                }
                else
                {
                    solu_panel.Children.Add(new TextBlock
                    {
                        Text = "Решения нет"
                    });
                }
            }           
            
        }
        private void Refresh_solu_panel(object sender, RoutedEventArgs e)
        {
            slae.IsSolved = false;            
            solu_panel.Children.Clear();
        }
        private void Refresh_solu_panel()
        {
            solu_panel.Children.Clear();
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
            slae.Generate(d);
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
            slae.Generate(d);
            DataTable_Refresh();
        }

        /// <summary>
        /// Gauss
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Click_5(object sender, RoutedEventArgs e)
        {
            //Thread solver = new Thread(delegate() { new Gauss(); });
            //solver.Start();
            //ProgressBar_Solve(new Gauss());
            slae.Method = new Gauss();
            slae.Solve();
            Fill_solu_panel();
        }
        /// <summary>
        /// Zeidel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Click_6(object sender, RoutedEventArgs e)
        {
            //ProgressBar_Solve(new Zeidel());
            slae.Method = new Zeidel();
            slae.Solve();
            Fill_solu_panel();
        }

        //private void ProgressBar_Solve(Solver method)
        //{
        //    TextBlock tt = new TextBlock();
        //    tt.Text = "Hi";

        //    StackPanel panel_bar = new StackPanel();
        //    ProgressBar bar = new ProgressBar
        //    {
        //        Height = 30,
        //        Width = 180,
        //        Maximum = 2 * slae.M.GetLength(0),
        //        Minimum = 0,
        //    };
        //    Window w1 = new Window
        //    {
        //        Title = "Прогресс решения",
        //        MaxHeight = 200,
        //        MaxWidth = 600,
        //        MinHeight = 200,
        //        MinWidth = 600,
        //        Content = panel_bar,
        //    };

        //    panel_bar.Children.Add(bar);
        //    panel_bar.Children.Add(tt);
        //    w1.Show();
        //    slae.Solve(slae.M, method);
        //    Fill_solu_panel();
        //}
    }
}
