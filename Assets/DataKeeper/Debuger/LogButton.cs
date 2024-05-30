using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DataKeeper.Debuger
{
    public class LogButton : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private TextMeshProUGUI _logText;
        [SerializeField] private TextMeshProUGUI _logTime;

        public LogType LogType { get; private set; }
        public string LogText { get; private set; }
        public string LogDetails { get; private set; }

        public void SetLog(LogType logType, Color color, string logText, string logTime, string logDetails)
        {
            LogType = logType;
            LogText = logText;
            LogDetails = logDetails;

            _image.color = color;
            _logText.text = logText;
            _logTime.text = logTime;
        }
    }
}
