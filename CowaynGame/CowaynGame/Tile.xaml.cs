using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Threading;

namespace CowaynGame
{
    /// <summary>
    /// Interaction logic for Tile.xaml
    /// </summary>
    public partial class Tile : UserControl, INotifyPropertyChanged
    {
        public List<Tile> ListAdjacentTile = new List<Tile>();

        private bool state = false;
        private bool nextState = false;
        public bool State
        {
            get
            {
                return state;
            }
            set
            {
                state = value;
                Refresh();
            }
        }

        private int tileColor = 0;

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public int TileColor
        {
            get { return tileColor; }
            set { tileColor = value % 11; }

        }

        public Tile()
        {
        }

        public void Refresh()
        {
              Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Send,
              new Action(() =>
              {
                  if (state)
                  {
                      switch (TileColor)
                      {

                          case 0:
                              this.Background = new SolidColorBrush(Color.FromRgb(0, 154, 205)); // DeepSky Blue
                              break;
                          case 1:
                              this.Background = new SolidColorBrush(Color.FromRgb(0, 245, 255)); // Turquoise
                              break;
                          case 2:
                              this.Background = new SolidColorBrush(Color.FromRgb(152, 251, 152)); // PaleGreen
                              break;
                          case 3:
                              this.Background = new SolidColorBrush(Color.FromRgb(255, 195, 15)); // DarkGoldenrod1
                              break;
                          case 4:
                              this.Background = new SolidColorBrush(Color.FromRgb(155, 0, 205)); // DeepSky Blue
                              break;
                          case 5:
                              this.Background = new SolidColorBrush(Color.FromRgb(0, 154, 0)); // DeepSky Blue
                              break;
                          case 6:
                              this.Background = new SolidColorBrush(Color.FromRgb(0, 0, 205)); // DeepSky Blue
                              break;
                          case 7:
                              this.Background = new SolidColorBrush(Color.FromRgb(255, 154, 205)); // DeepSky Blue
                              break;
                          case 8:
                              this.Background = new SolidColorBrush(Color.FromRgb(70, 154, 70)); // DeepSky Blue
                              break;
                          case 9:
                              this.Background = new SolidColorBrush(Color.FromRgb(200, 15, 15)); // DeepSky Blue
                              break;
                      }
                  }
                  else
                  {
                      this.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                  }
              }));

        }

        public void LoadNextState()
        {
            int activatedneighbour = 0;
            Parallel.ForEach(ListAdjacentTile, (tile) => {
                if (tile.State)
                {
                    activatedneighbour++;
                }
            });

            if (state)
            {
                nextState = (activatedneighbour == 2 || activatedneighbour == 3);
            }
            else
            {
                nextState = (activatedneighbour == 3);
            }
        }

        public void SwitchToNextState()
        {
            State = nextState;
        }
    }
}
