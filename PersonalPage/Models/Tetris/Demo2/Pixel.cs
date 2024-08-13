namespace PersonalPage.Models.Tetris.Demo2
{
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
            return $"Pixel [X = {X} : Y = {Y}]";
        }
    }

    //Could be a good candidate for a Struct to make it a value type

    //Or Record as reference type with value-based equality 
}
