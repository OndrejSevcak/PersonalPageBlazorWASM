using PersonalPage.Models.Tetris;
using PersonalPage.Models.Tetris.Demo2;

namespace PersonalPage.Services.Tetris
{
    public static class TetrisHelper
    {
        public static Shape GenerateRandomShape()
        {
            char[] shapes = { 'I', 'J', 'L', 'S', 'Z', 'T', 'O' };

            Random random = new Random();
            var randomShape = random.GetItems(shapes, 1);

            return new Shape((ShapeType)Enum.Parse(typeof(ShapeType), randomShape[0].ToString()));
        }
    }
}
