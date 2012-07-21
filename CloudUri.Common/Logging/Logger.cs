using System.Collections;
using log4net;
using log4net.Config;
using log4net.Repository.Hierarchy;

namespace CloudUri.Common.Logging
{
    /// <summary>
    /// Log4net wrapper
    /// </summary>
    public static class Logger
    {
        /// <summary>
        /// Gets the current logger
        /// </summary>
        public static ILog Log { get; private set; }

        static Logger()
        {
            XmlConfigurator.Configure();
            Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        }
    }
}