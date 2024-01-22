public static class GameState
{
    public static int MazeWidth { get; private set; }
    public static int MazeDepth { get; private set; }

    static GameState()
    {
        MazeWidth = 5; // Default value
        MazeDepth = 5; // Default value
    }

    public static void IncreaseMazeSize()
    {
        MazeWidth += 2; // Increment size
        MazeDepth += 2;
    }

    public static void ResetMazeSize()
    {
       MazeWidth = 5; // Default value
       MazeDepth = 5; // Default value 
    }
}
