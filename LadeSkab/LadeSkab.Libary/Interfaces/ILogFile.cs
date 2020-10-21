namespace Ladeskab.Libary.interfaces
{
    public interface ILogFile
    {
        public void LogDoorLocked(int id);
        public void LogDoorUnlocked(int id);
    }
}