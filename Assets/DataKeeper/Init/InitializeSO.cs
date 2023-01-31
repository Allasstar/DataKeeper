using DataKeeper.Base;
using UnityEngine;

namespace DataKeeper.Init
{
    public class InitializeSO
    {
        public InitializeSO()
        {
            var sos = Resources.LoadAll<SO>("");
            foreach (var so in sos)
            {
                so.Initialize();
            }
        }
    }
}