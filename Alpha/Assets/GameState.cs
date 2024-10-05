public static class GameState
{
    public static bool isTreeChopped = false;
    public static bool isAppleCollected = false;

    public static void ResetState()
    {
        isTreeChopped = false;
        isAppleCollected = false;
    }
}
