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

        private LogType _logType;
        private string _details;

        public void SetLog(LogType logType, Color color, string logText, string logTime, string details)
        {
            _logType = logType;
            _image.color = color;
            _logText.text = logText;
            _logTime.text = logTime;
            _details = details;
        }
    }
}
