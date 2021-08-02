namespace Store.Core.Interfaces
{
    public interface ILoggerManager
    {
        void Warn(string message);
        void Error(string message);
        void Info(string message);
        void Debug(string message);
    }
}
