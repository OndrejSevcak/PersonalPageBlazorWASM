using PersonalPage.Models.Tetris.Demo2;

namespace PersonalPage.Services.Tetris.Demo1
{
    public class GameService
    {
        public Board GameBoard { get; set; }

        public GameService()
        {
            GameBoard = new Board(10, 20);
            InitializeGame();
        }

        public Shape CurrentShape { get; set; }
        public Shape NextShape { get; set; }

        private void InitializeGame()
        {
            CurrentShape = TetrisHelper.GenerateRandomShape();
            NextShape = TetrisHelper.GenerateRandomShape();
        }
    }
}
