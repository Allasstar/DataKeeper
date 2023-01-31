using System.Collections.Generic;
using DataKeeper.Base;
using DataKeeper.Generic;
using UnityEngine;

namespace Test.Example
{
    public static class DK
    {
        public static Register<MeshRenderer> MR = new ();
        public static Register<SO> SO = new ();

        public static RegisterActivator<object> Any = new ();

        public static void Test()
        {
            var l = new List<string>();
        }
    }
}