public class AppState
{
    public bool IsUserLoggedIn { get; set; } = false;
    public event Action? OnChange;

    public void NotifyStateChanged() => OnChange?.Invoke();
}