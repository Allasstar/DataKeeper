using DataKeeper.Core.Base;
using DataKeeper.Core.Primitives;
using UnityEngine;

namespace Test.Example
{
    public static class Model
    {
        public static Register<MeshRenderer> MR = new ();
        public static Register<SOBase> SO = new ();

        public static RegisterActivator<object> Any = new ();
    }
}