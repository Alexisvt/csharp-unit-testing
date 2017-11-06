using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.dotMemoryUnit;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace SimpleUnitTest.Properties
{
    [TestFixture]
    public class DotMemoryTest
    {
        [Test]
        public void SolveTye_Zero_AllocateObjectsInMemory()
        {
            dotMemory.Check(memory =>
            {
                Assert.That(memory.GetObjects(
                    where => where.Type.Is<Solve>()
                    ).ObjectsCount, Is.EqualTo(0));
            });
        }
    }
}
