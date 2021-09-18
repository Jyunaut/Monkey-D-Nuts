public static class GameEvent
{
    public delegate void LevelStart();
    public static event LevelStart OnLevelStart;
    public static void StartLevel() => OnLevelStart?.Invoke();

    public delegate void LevelComplete();
    public static event LevelComplete OnLevelComplete;
    public static void CompleteLevel() => OnLevelComplete?.Invoke();
}