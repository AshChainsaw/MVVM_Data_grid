using MVVM_Data_grid.Model;
using MVVM_Data_grid.ViewModel;
using System.Windows;
using System.Windows.Input;

namespace MVVM_Data_grid
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Window : System.Windows.Window
    {
        

        public Window()
        {
            InitializeComponent();
            
            
            
            MainViewModel vm = new MainViewModel();
            DataContext = vm;

            vm.DeserializeList();
            
        }

        private void Window_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e) //Przesuwanie okna
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
        private bool isMax = false; // pomniejszanie okna do zmiany MVVM
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if(isMax)
                {
                    this.WindowState = WindowState.Normal;
                   
                    isMax=false;
                }else
                {
                    this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
                   this.MaxWidth = SystemParameters.MaximizedPrimaryScreenWidth;
                    this.WindowState = WindowState.Maximized;


                    isMax = true;
                }
            }
        }
    }
    
}