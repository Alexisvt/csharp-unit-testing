using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImpromptuInterface;

namespace TestDoublesSamples
{
    public class Null<T>: DynamicObject where T: class
    {
        public static T Instance => new Null<T>().ActLike();

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            result = Activator.CreateInstance(typeof(T).GetMethod(binder.Name).ReturnType);

            return true;
        }
    }
}
