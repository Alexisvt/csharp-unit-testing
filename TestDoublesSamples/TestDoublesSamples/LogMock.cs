using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDoublesSamples
{
    public class LogMock: ILog
    {
        private bool _expectedResult;
        public Dictionary<string, int> MethodCallCount;

        public bool Write(string msg)
        {
            AddOrIncrement(nameof(Write));
            return _expectedResult;
        }

        public LogMock(bool expectedResult)
        {
            _expectedResult = expectedResult;
            MethodCallCount = new Dictionary<string, int>();
        }

        private void AddOrIncrement(string methodName)
        {
            if (MethodCallCount.ContainsKey(methodName))
            {
                MethodCallCount[methodName]++;
            }
            else
            {
                MethodCallCount.Add(methodName, 1);
            }
        }
    }
}
