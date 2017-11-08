using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDoublesSamples
{
    /// <summary>
    /// Stub sample class
    /// </summary>
    public class NullLogWithResult: ILog
    {
        private readonly bool _expectedResult;

        public NullLogWithResult(bool expectedResult)
        {
            _expectedResult = expectedResult;
        }

        public bool Write(string msg)
        {
            return _expectedResult;
        }
    }
}
