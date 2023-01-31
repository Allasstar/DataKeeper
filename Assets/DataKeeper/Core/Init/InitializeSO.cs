using DataKeeper.Core.Base;
using UnityEngine;

namespace DataKeeper.Core.Init
{
    public class InitializeSO
    {
        public InitializeSO()
        {
            var sos = Resources.LoadAll<SOBase>("");
            foreach (var so in sos)
            {
                so.Initialize();
            }
        }
    }
}