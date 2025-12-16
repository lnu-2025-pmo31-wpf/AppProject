using System.Windows;

public class NavigationService
{
    public void Open(Window window)
    {
        window.Show();
    }

    public void Close(Window window)
    {
        window.Close();
    }
}
