using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CowaynGame
{
    class Game
    {
        private readonly Canvas GameBoard = null;
        public Tile[][] TileTab;
        private readonly Random rnd = new Random();
        private int width = 0;
        private int height = 0;
        private Boolean play = false;
        public Boolean Play
        {
            get { return play; }
            set { play = value; }
        }

        private Thread Process;

        public Game(int width, int height, Canvas gameBoard)
        {
            this.width = width;
            this.height = height;
            this.GameBoard = gameBoard;
            TileTab = new Tile[height][];
            for (int i = 0; i < height; i++)
            {
                TileTab[i] = new Tile[width];
            }
            Init();
            Process = (new Thread(() => {
                while(true)
                {
                    Thread.Sleep(200);
                    if (play)
                    {
                        MoveForward();
                    }
                }
                
            }));
            Process.Start();


        }

        public void MoveForward()
        {

            Parallel.ForEach(TileTab, tileRow =>
            {
                Parallel.ForEach(tileRow, tile =>
                {
                    tile.LoadNextState();
                }
                );
            }
            );

            Parallel.ForEach(TileTab, tileRow =>
            {
                Parallel.ForEach(tileRow, tile =>
                {
                    tile.SwitchToNextState();
                }
                );
            }
            );

        }

        public void ChangeTileColor(int x, int y)
        {
            TileTab[y][x].TileColor = rnd.Next(0, 10);
            TileTab[y][x].Refresh();
        }

        public void ChangeTileState(int x, int y)
        {
            Tile tile = TileTab[y][x];
            tile.State = !tile.State;
            tile.Refresh();
        }

        public void Reset()
        {
            Parallel.ForEach(TileTab, tileRow =>
            {
                Parallel.ForEach(tileRow, tile =>
                    {
                        tile.State = false;
                        tile.Refresh();
                    }
                );
            }
            );
        }

        private void Init()
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Tile mTile = new Tile();
                    mTile.Height = 15;
                    mTile.Width = 15;
                    mTile.Margin = new Thickness(j * 17, i * 17, 0, 0);
                    mTile.TileColor = rnd.Next(0,10);
                    mTile.Refresh();
                    TileTab[i][j] = mTile;
                    GameBoard.Children.Add(mTile);
                }
            }

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    try { TileTab[i][j].ListAdjacentTile.Add(TileTab[i - 1][ j]); } catch { };
                    try { TileTab[i][j].ListAdjacentTile.Add(TileTab[i + 1][ j]); } catch { };
                    try { TileTab[i][j].ListAdjacentTile.Add(TileTab[i - 1][ j - 1]); } catch { };
                    try { TileTab[i][j].ListAdjacentTile.Add(TileTab[i + 1][ j + 1]); } catch { };
                    try { TileTab[i][j].ListAdjacentTile.Add(TileTab[i - 1][ j + 1]); } catch { };
                    try { TileTab[i][j].ListAdjacentTile.Add(TileTab[i + 1][ j - 1]); } catch { };
                    try { TileTab[i][j].ListAdjacentTile.Add(TileTab[i][ j - 1]); } catch { };
                    try { TileTab[i][j].ListAdjacentTile.Add(TileTab[i][ j + 1]); } catch { };
                }
            }

            foreach (Tile tile in TileTab[0][0].ListAdjacentTile)
            {
                Console.WriteLine("ma bite");
            }
        }

    }
}
