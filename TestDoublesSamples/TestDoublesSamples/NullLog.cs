using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDoublesSamples
{
    /// <summary>
    /// Null pattern class
    /// </summary>
    public class NullLog: ILog
    {
        public bool Write(string msg)
        {
            return true;
        }
    }
}
