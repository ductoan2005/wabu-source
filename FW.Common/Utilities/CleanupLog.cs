using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace FW.Common.Utilities
{
    public class CleanupLog
    {
        #region Constructor
        public CleanupLog()
        {
        }
        #endregion

        #region - Methods -
        /// <summary>
        /// Cleans up. Auto configures the cleanup based on the log4net configuration
        /// </summary>
        /// <param name="date">Anything prior will not be kept.</param>
        public void CleanUp(DateTime date)
        {
            var directory = HttpContext.Current.Server.MapPath("~/Logs");
            DirectoryInfo dicinfo = new DirectoryInfo(directory);
            FileInfo[] files = dicinfo.GetFiles().ToArray();
            foreach (FileInfo fileinfo in files)
            {
                if (fileinfo.Name.Contains("ActionLog") && fileinfo.CreationTime.Date < date.Date)
                {
                    fileinfo.Delete();
                }
            }

        }
        #endregion
    }
}
