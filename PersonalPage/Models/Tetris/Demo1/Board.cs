namespace PersonalPage.Models.Tetris.Demo1
{
    public class Board
    {
        public Pixel[,] Grid { get; set; }

        public Board(int width, int height)
        {
            Grid = new Pixel[width, height];
            InitializeGrid();
        }

        public int Width => Grid.GetLength(0);
        public int Height => Grid.GetLength(1);

        
        public void InitializeGrid()
        {
            for (int row = 0; row < Height; row++)
            {
                for(int col = 0; col < Width; col++)
                {
                    Grid[col, row] = new Pixel(col, row);
                }
            }
        }
    }
}

