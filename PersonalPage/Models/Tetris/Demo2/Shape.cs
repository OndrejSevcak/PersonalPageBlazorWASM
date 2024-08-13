namespace PersonalPage.Models.Tetris.Demo2
{
    public class Shape
    {
        //will also contain a small grid of 3x3 pixels that will represent the shape
        public Pixel[,] Grid { get; set; }

        public int Width => Grid.GetLength(0);
        public int Height => Grid.GetLength(1);

        public string ColorClass { get; private set; }

        /// <summary>
        /// Game board row index of the first Pixel[0,0] of shapes grid
        /// </summary>
        public int CurrentStartRow { get; private set; } 

        /// <summary>
        /// Column game board index of the first Pixel[0,0] of shapes grid
        /// </summary>
        public int CurrentStartCol { get; private set; }

        public ShapeType Type { get; private set; }

        public Shape(ShapeType shapeType)
        {
            Grid = new Pixel[3, 3];
            InitalizeGrid(shapeType);
            CurrentStartRow = 5;    //top row
            CurrentStartCol = 4;    //assuming 10 col grid 
            Type = shapeType;
        }

        private void InitalizeGrid(ShapeType shapeType)
        {
            switch (shapeType) 
            {
                case ShapeType.I:
                    Grid[0, 0] = new Pixel(0, 0);
                    Grid[0, 1] = new Pixel(0, 1, true);
                    Grid[0, 2] = new Pixel(0, 2);
                    Grid[1, 0] = new Pixel(1, 0);
                    Grid[1, 1] = new Pixel(1, 1, true);
                    Grid[1, 2] = new Pixel(1, 2);
                    Grid[2, 0] = new Pixel(2, 0);
                    Grid[2, 1] = new Pixel(2, 1, true);
                    Grid[2, 2] = new Pixel(2, 2);
                    ColorClass = "bg-warning";
                    break;

                case ShapeType.O:
                    Grid[0, 0] = new Pixel(0, 0, true);
                    Grid[0, 1] = new Pixel(0, 1, true);
                    Grid[0, 2] = new Pixel(0, 2);
                    Grid[1, 0] = new Pixel(1, 0, true);
                    Grid[1, 1] = new Pixel(1, 1, true);
                    Grid[1, 2] = new Pixel(1, 2);
                    Grid[2, 0] = new Pixel(2, 0);
                    Grid[2, 1] = new Pixel(2, 1);
                    Grid[2, 2] = new Pixel(2, 2);
                    ColorClass = "bg-warning";
                    break;

                case ShapeType.T:
                    Grid[0, 0] = new Pixel(0, 0, true);
                    Grid[0, 1] = new Pixel(0, 1, true);
                    Grid[0, 2] = new Pixel(0, 2, true);
                    Grid[1, 0] = new Pixel(1, 0);
                    Grid[1, 1] = new Pixel(1, 1, true);
                    Grid[1, 2] = new Pixel(1, 2);
                    Grid[2, 0] = new Pixel(2, 0);
                    Grid[2, 1] = new Pixel(2, 1);
                    Grid[2, 2] = new Pixel(2, 2);
                    ColorClass = "bg-warning";
                    break;

                case ShapeType.S:
                    Grid[0, 0] = new Pixel(0, 0, true);
                    Grid[0, 1] = new Pixel(0, 1, true);
                    Grid[0, 2] = new Pixel(0, 2);
                    Grid[1, 0] = new Pixel(1, 0);
                    Grid[1, 1] = new Pixel(1, 1, true);
                    Grid[1, 2] = new Pixel(1, 2, true);
                    Grid[2, 0] = new Pixel(2, 0);
                    Grid[2, 1] = new Pixel(2, 1);
                    Grid[2, 2] = new Pixel(2, 2);

                    ColorClass = "bg-warning";
                    break;

                case ShapeType.Z:
                    Grid[0, 0] = new Pixel(0, 0);
                    Grid[0, 1] = new Pixel(0, 1, true);
                    Grid[0, 2] = new Pixel(0, 2, true);
                    Grid[1, 0] = new Pixel(1, 0, true);
                    Grid[1, 1] = new Pixel(1, 1, true);
                    Grid[1, 2] = new Pixel(1, 2);
                    Grid[2, 0] = new Pixel(2, 0);
                    Grid[2, 1] = new Pixel(2, 1);
                    Grid[2, 2] = new Pixel(2, 2);
                    ColorClass = "bg-warning";
                    break;

                case ShapeType.J:
                    Grid[0, 0] = new Pixel(0, 0, true);
                    Grid[0, 1] = new Pixel(0, 1, true);
                    Grid[0, 2] = new Pixel(0, 2, true);
                    Grid[1, 0] = new Pixel(1, 0, true);
                    Grid[1, 1] = new Pixel(1, 1);
                    Grid[1, 2] = new Pixel(1, 2);
                    Grid[2, 0] = new Pixel(2, 0);
                    Grid[2, 1] = new Pixel(2, 1);
                    Grid[2, 2] = new Pixel(2, 2);
                    ColorClass = "bg-warning";
                    break;

                case ShapeType.L:
                    Grid[0, 0] = new Pixel(0, 0, true);
                    Grid[0, 1] = new Pixel(0, 1, true);
                    Grid[0, 2] = new Pixel(0, 2, true);
                    Grid[1, 0] = new Pixel(1, 0);
                    Grid[1, 1] = new Pixel(1, 1);
                    Grid[1, 2] = new Pixel(1, 2, true);
                    Grid[2, 0] = new Pixel(2, 0);
                    Grid[2, 1] = new Pixel(2, 1);
                    Grid[2, 2] = new Pixel(2, 2);
                    ColorClass = "bg-warning";
                    break;

                default:
                    throw new ArgumentException("Invalid shape type");
            }
        }
    }
}
