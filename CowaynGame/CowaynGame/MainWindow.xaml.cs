using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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

namespace CowaynGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Game board;
        
        public MainWindow()
        {
            InitializeComponent();
            board = new Game(20,20,GameBoard);
            
        }

        private void OnClick(object sender, RoutedEventArgs e)
        {
            MouseButtonEventArgs click = (MouseButtonEventArgs) e ;
            Point pos = click.GetPosition(GameBoard);
            int x = (int)pos.X / 17;
            int y = (int) pos.Y / 17;
            Console.WriteLine("x :" + x + "     y :" + y);
            board.ChangeTileColor(x,y);
            board.ChangeTileState(x,y);
        }

        private void PausePlayBt_OnClick(object sender, RoutedEventArgs e)
        {
            board.Play = !board.Play;
            if (board.Play)
                PausePlayBt.Content = "Pause";
            else
                PausePlayBt.Content = "Play";

            //board.MoveForward();
        }

        private void ResetBt_OnClick(object sender, RoutedEventArgs e)
        {
            board.Reset();
        }
    }
}
