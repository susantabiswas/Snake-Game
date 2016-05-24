

namespace SnakeGame
{
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    };

    public class Settings
    {
        public static int Speed { get; set; }
        public static int Score { get; set; }
        public static int Points { get; set; }
        public static int Width { get; set; }
        public static int Height { get; set; }
        public static bool IsGameOver { get; set; }
        public static Direction InGameDirection { get; set; }

        public Settings()
        {
            Speed = 14;
            Score = 0;
            Points = 100;
            Width = 16;
            Height = 16;
            IsGameOver = false;
            InGameDirection = Direction.Right;
        }

    }
}
