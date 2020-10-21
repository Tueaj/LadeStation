namespace Ladeskab
{
    public interface ILogFile
    {
        public void LogDoorLocked(int id);
        public void LogDoorUnlocked(int id);
    }
}