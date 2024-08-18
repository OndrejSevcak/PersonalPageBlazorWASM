namespace PersonalPage.Data
{
    public static class CodeSnippets
    {
        public static string RenderGrid()
        {
            return @"
            <div class=""d-flex justify-content-evenly align-items-center mt-5"">

                <div class=""board"">

                    @for (int row = 0; row < _service.GameBoard.Height; row++)
                    {
                        for (int col = 0; col < _service.GameBoard.Width; col++)
                        {
                            //reassignement to captured local variable is required
                            int _row = row;
                            int _col = col;

                            <div class=""pixel @(_service.GameBoard.Grid[_col, _row].IsOccupied ? ""bg-yellow"" : """")""
                                 data-row=""@row""
                                 data-col=""@col""
                                 @onclick=""@(() => LogClickedPixel(_col, _row))"">
                            </div>
                        }
                    }

                </div>

                <div class=""logs"">
                    <h5>Logs:</h5>
                    <ul>
                        @foreach (var log in logs.OrderByDescending(l => l.Item1).Take(20))
                        {
                            <li>@log.Item1 - @log.Item2</li>
                        }
                    </ul>
                </div>

            </div>
            ";
        }

        public static string PixelClass()
        {
            return @"
            public class Pixel
            {
                public int X { get; }
                public int Y { get; }

                public bool IsOccupied { get; set; }

                public Pixel(int x, int y)  //x = col, y = row
                {
                    X = x;
                    Y = y;
                }

                public Pixel(int x, int y, bool isOccupied)
                {
                    X = x;
                    Y = y;
                    IsOccupied = isOccupied;
                }

                public override string ToString()
                {
                    return $""Pixel [X = {X} : Y = {Y}]"";
                }
            }";
        }

        public static string BoardClass()
        {
            return @"
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
            ";
        }

        public static string GameService()
        {
            return @"
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
            ";
        }

        public static string LogClickedPixel()
        {
            return @"
            private Stack<(DateTime, string)> logs = new();

            private void LogClickedPixel(int col, int row)
            {
                _service.GameBoard.Grid[col, row].IsOccupied = true;

                logs.Push((DateTime.Now, $""Clicked pixel {row} - {col}""));
            }
            ";
        }

        public static string RenderGridDemo2()
        {
            return @"
            <div class=""d-flex justify-content-evenly align-items-center mt-5"">

                <div class=""board"">

                    @for (int row = 0; row < _service.GameBoard.Height; row++)
                    {
                        for (int col = 0; col < _service.GameBoard.Width; col++)
                        {
                            //reassignement to captured local variable is required
                            int _row = row;
                            int _col = col;

                            bool isOccupied = false;

                            //shape has a grid of 3x3 pixels, Shape only has a CurrentStartCol and CurrentStartRow indexes, so we need to check
                            //the remaining rows and grids
                            for (int shapeRow = 0; shapeRow < CurrentShape.Height; shapeRow++)
                            {
                                for (int shapeCol = 0; shapeCol < CurrentShape.Width; shapeCol++)
                                {
                                    if (CurrentShape!.CurrentStartRow + shapeRow == _row)
                                    {
                                        if (CurrentShape!.CurrentStartCol + shapeCol == _col && CurrentShape.Grid[shapeRow, shapeCol].IsOccupied)
                                        {
                                            isOccupied = true;
                                            break;
                                        }
                                    }
                                }
                            }

                            <div class=""pixel @(isOccupied ? CurrentShape.ColorClass : string.Empty)""
                                 data-row=""@row""
                                 data-col=""@col"">
                            </div>
                        }
                    }

                </div>

                <div>
                    <button type=""button"" class=""btn btn-danger"" @onclick=""GenerateNewShape"">Generate new shape</button>
                </div>

            </div>
            ";
        }

        public static string ShapeClassDemo2()
        {
            return @"
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
                            ColorClass = ""bg-warning"";
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
                            ColorClass = ""bg-warning"";
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
                            ColorClass = ""bg-warning"";
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

                            ColorClass = ""bg-warning"";
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
                            ColorClass = ""bg-warning"";
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
                            ColorClass = ""bg-warning"";
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
                            ColorClass = ""bg-warning"";
                            break;

                        default:
                            throw new ArgumentException(""Invalid shape type"");
                    }
                }
            }
            ";
        }

        public static string TetrisHelperClassDemo2()
        {
            return @"
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
            ";
        }

        public static string CodeSectionDemo2()
        {
            return @"
            private Shape? CurrentShape;

            protected override void OnInitialized()
            {
                CurrentShape = _service.CurrentShape ?? TetrisHelper.GenerateRandomShape();
            }

            private void GenerateNewShape()
            {
                CurrentShape = TetrisHelper.GenerateRandomShape();
            }
            ";
        }

        public static string BootstrapCols()
        {
            return @"
            <div class=""container"">
                <div class=""row"">
                    <div class=""col"">col</div>
                    <div class=""col"">col</div>
                    <div class=""col"">col</div>
                </div>
                <div class=""row"">
                    <div class=""col-md"">col-sm</div>
                    <div class=""col-md"">col-sm</div>
                    <div class=""col-md"">col-sm</div>
                </div>
            </div>
            ";
        }
    }
}
