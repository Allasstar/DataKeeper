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

            var color = data.Type switch
            {
                LogType.Error => Color.red,
                LogType.Assert => Color.blue,
                LogType.Warning => Color.yellow,
                LogType.Log => Color.cyan,
                LogType.Exception => Color.magenta,
                _ => Color.black
            };

            l.SetLog(data.Type, color, data.Condition, Time.time.ToString(), data.Stacktrace);
        }
    }
}
