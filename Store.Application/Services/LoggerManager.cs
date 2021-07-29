using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using Store.Core.Interfaces;

namespace Store.Application.Services
{
    public class LoggerManager: ILoggerManager
    {
        private readonly Logger logger = LogManager.GetCurrentClassLogger();
        public void Warn(string message)
        {
            logger.Warn(message);
        }
        public void Info(string message)
        {
            logger.Info(message);
        }
        public void Error(string message)
        {
            logger.Error(message);
        }
        public void Debug(string message)
        {
            logger.Debug(message);
        }
    }
}
