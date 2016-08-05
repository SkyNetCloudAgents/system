using log4net;
using log4net.Appender;
using log4net.Layout;
using System.Runtime.CompilerServices;

namespace SkyNet.Log
{
    using System.IO;

    static class Initializer
    {
        #region Fields

        private const string _conversionPattern = "%date %level %logger - %message %exception%newline";

        #endregion

        #region Methods

        public static ILog CreateFile(string filePath, string loggerName = null,
            [CallerMemberName] string caller = null)
        {
            var fileAppender = new FileAppender()
            {
                AppendToFile = true,
                LockingModel = new FileAppender.MinimalLock(),
                File = filePath
            };
            var pl = new PatternLayout()
            {
                ConversionPattern = _conversionPattern,
            };
            pl.ActivateOptions();
            fileAppender.Layout = pl;
            fileAppender.ActivateOptions();
            log4net.Config.BasicConfigurator.Configure(fileAppender);
            return LogManager.GetLogger(loggerName ?? caller);
        }

        public static ILog CreateAuditTrail(string appName, string logSection = "CustomEventLog",
            string loggerName = null, [CallerMemberName] string caller = null)
        {
            var eventLogAppender = new EventLogAppender()
            {
                ApplicationName = appName ?? "[SkyNetCloud module]",
                LogName = logSection,
            };
            var pl = new PatternLayout()
            {
                ConversionPattern = _conversionPattern,
            };
            pl.ActivateOptions();
            eventLogAppender.Layout = pl;
            eventLogAppender.ActivateOptions();
            log4net.Config.BasicConfigurator.Configure(eventLogAppender);
            return LogManager.GetLogger(loggerName ?? caller);
        }

        public static ILog CreateConsole(string loggerName = null,
            [CallerMemberName] string caller = null)
        {
            var consoleAppender = new ConsoleAppender();
            var pl = new PatternLayout()
            {
                ConversionPattern = _conversionPattern,
            };
            pl.ActivateOptions();
            consoleAppender.Layout = pl;
            consoleAppender.ActivateOptions();
            log4net.Config.BasicConfigurator.Configure(consoleAppender);
            return LogManager.GetLogger(loggerName ?? caller);
        }

        private static Stream GenerateStreamFromString(string str)
        {
            var stream = new MemoryStream();
            using (var writer = new StreamWriter(stream) { AutoFlush = true })
            {
                writer.Write(str);
            }
            stream.Position = 0;
            return stream;
        }

        public static ILog CreateCustom(string xml, string loggerName = null, [CallerMemberName] string caller = null)
        {
            using (var stream = GenerateStreamFromString(xml))
            {
                log4net.Config.XmlConfigurator.Configure(stream);
            }
            return LogManager.GetLogger(loggerName ?? caller);
        }

        #endregion
    }
}
