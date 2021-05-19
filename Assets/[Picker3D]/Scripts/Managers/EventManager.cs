using UnityEngine.Events;

public static class EventManager
{
    public static UnityEvent OnLevelPassed = new UnityEvent();
    public static UnityEvent OnGameOver = new UnityEvent();
}
