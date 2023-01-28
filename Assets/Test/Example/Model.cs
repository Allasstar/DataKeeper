using ActionStage.Core.Base;
using ActionStage.Core.Primitives;
using UnityEngine;

namespace ActionStage
{
    public static class Model
    {
        public static Register<MeshRenderer> MR = new ();
        public static Register<SOBase> SO = new ();

        public static RegisterActivator<object> Any = new ();
    }
}