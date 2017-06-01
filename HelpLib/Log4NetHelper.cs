using System;
using System.IO;
using log4net;
using log4net.Config;

namespace HelpLib
{
    public class Log4NetHelper
    {
        private readonly Type _name;

        public Log4NetHelper(Type name)
        {
            _name = name;
        }

        public void WriteLog(object sender,ExceptionEventArgs exceptionEventArgs)
        {
            InitLog4Net();
            var logger = LogManager.GetLogger(_name);

            logger.Error(sender.ToString(),exceptionEventArgs.Exception);
        }

        private void InitLog4Net()
        {
            var directory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log4net.config");
            var logConfig = new FileInfo(directory);
            XmlConfigurator.ConfigureAndWatch(logConfig);
        }

        private void Test()
        {
            WriteLog(this,new ExceptionEventArgs(new Exception("测试")));
        }

    }



    public class ExceptionEventArgs : EventArgs
    {

        public Exception Exception
        {
            get { return _exception; }
        }

        private Exception _exception;

        public ExceptionEventArgs(Exception exception = null)
        {
            _exception = exception;
        }


    }

}
