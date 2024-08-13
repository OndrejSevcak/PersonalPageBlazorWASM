namespace PersonalPage.Models.Tetris.Demo1
{
    public class Shape
    {
        //will also contain a small grid of 3x3 pixels that will represent the shape
        public Pixel[,] Grid { get; set; }

        public Shape(ShapeType shapeType)
        {
            Grid = new Pixel[3, 3];
            InitalizeGrid(shapeType);
        }

        private void InitalizeGrid(ShapeType shapeType)
        {
            switch (shapeType)
            {
                //since false is default value of boolean, we only need to set the true values
                case ShapeType.I:
                    Grid[2, 1] = new Pixel(2, 1, true);
                    Grid[1, 1] = new Pixel(1, 1, true);
                    Grid[0, 1] = new Pixel(0, 1, true);
                    break;

                case ShapeType.O:
                    Grid[0, 0] = new Pixel(0, 0, true);
                    Grid[0, 1] = new Pixel(0, 1, true);
                    Grid[1, 0] = new Pixel(1, 0, true);
                    Grid[1, 1] = new Pixel(1, 1, true);
                    break;

                case ShapeType.T:
                    Grid[0, 1] = new Pixel(0, 1, true);
                    Grid[1, 0] = new Pixel(1, 0, true);
                    Grid[1, 1] = new Pixel(1, 1, true);
                    Grid[1, 2] = new Pixel(1, 2, true);
                    break;

                case ShapeType.S:
                    Grid[0, 1] = new Pixel(0, 1, true);
                    Grid[0, 2] = new Pixel(0, 2, true);
                    Grid[1, 0] = new Pixel(1, 0, true);
                    Grid[1, 1] = new Pixel(1, 1, true);
                    break;

                case ShapeType.Z:
                    Grid[0, 0] = new Pixel(0, 0, true);
                    Grid[0, 1] = new Pixel(0, 1, true);
                    Grid[1, 1] = new Pixel(1, 1, true);
                    Grid[1, 2] = new Pixel(1, 2, true);
                    break;

                case ShapeType.J:
                    Grid[0, 1] = new Pixel(0, 1, true);
                    Grid[1, 1] = new Pixel(1, 1, true);
                    Grid[2, 0] = new Pixel(2, 0, true);
                    Grid[2, 1] = new Pixel(2, 1, true);
                    break;

                case ShapeType.L:
                    Grid[0, 1] = new Pixel(0, 1, true);
                    Grid[1, 1] = new Pixel(1, 1, true);
                    Grid[2, 1] = new Pixel(2, 1, true);
                    Grid[2, 2] = new Pixel(2, 2, true);
                    break;

                default:
                    throw new ArgumentException("Invalid shape type");
            }
        }
    }
}
