using System;
using DataKeeper.Generic;
using UnityEngine;

namespace DataKeeper.Debuger
{
    public class DebugCanvas : MonoBehaviour
    {
        [SerializeField] private LogButton _logButtonTemplate;
        [SerializeField] private Transform _logContainer;

        private void OnEnable()
        {
            Console.ReactiveLogData.AddListener(Log);
        }

        private void OnDisable()
        {
            Console.ReactiveLogData.RemoveListener(Log);
        }
        
        private void Log(int index, LogData data, ListChangedEvent change)
        {
            var l = Instantiate(_logButtonTemplate);
            l.transform.parent = _logContainer;
            l.gameObject.SetActive(true);

            var color = Color.black;
            switch (data.Type)
            {
                case LogType.Error:
                    color = Color.red;
                    break;
                case LogType.Assert:
                    color = Color.blue;
                    break;
                case LogType.Warning:
                    color = Color.yellow;
                    break;
                case LogType.Log:
                    color = Color.cyan;
                    break;
                case LogType.Exception:
                    color = Color.magenta;
                    break;
            }
            l.SetLog(data.Type, color, data.Condition, Time.time.ToString(), data.Stacktrace);
        }
    }
}
