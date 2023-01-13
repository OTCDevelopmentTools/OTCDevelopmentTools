namespace Bird.Client.Mtchmkr.Portable.Models
{
    public class ShellNotification
    {

    }
    public interface IShell
    {
        void UpdateEvent(ShellNotification shell);
    }
}