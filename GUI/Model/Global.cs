using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.Model
{
    public class Global
    {
        #region ---- Member variables ----

        private static string _AppPath = string.Empty;



        #endregion

        #region ---- Properties ----

        /// <summary>
        /// Gets the app path.
        /// </summary>
        /// <value>The app path.</value>
        public static string AppPath
        {
            get
            {
                if (string.IsNullOrEmpty(_AppPath))
                {
                    _AppPath = AppDomain.CurrentDomain.BaseDirectory;
                }

                return _AppPath;
            }
        }



        /// <summary>
        /// Gets or sets a value indicating whether this instance is login by supper admin.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is login by supper admin; otherwise, <c>false</c>.
        /// </value>
        public static bool IsLoginBySupperAdmin
        {
            get;
            set;
        }

        #endregion
    }
}
