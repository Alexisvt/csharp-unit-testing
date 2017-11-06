using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace SimpleUnitTest
{
    [TestFixture]
    public class CodeCoverageOverSolveTests
    {
        [Test]
        public void Test()
        {
            var result = Solve.Quadratic(1, 10, 16);
        }

        [Test]
        public void ReturnExceptionTest()
        {
            Assert.Throws<Exception>(() =>
                Solve.Quadratic(1, 1, 1)
            );
        }
    }
}
