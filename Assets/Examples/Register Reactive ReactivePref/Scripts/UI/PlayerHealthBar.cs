using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class PlayerHealthBar : MonoBehaviour
{
    private Slider _healthBar;
    private PlayerDataSO _playerData;

    private void Awake()
    {
        _playerData = DK.Data.Get<PlayerDataSO>();
        _healthBar = GetComponent<Slider>();

        if (_healthBar.maxValue < _playerData.PlayerHealth.Value)
        {
            _healthBar.maxValue = _playerData.PlayerHealth.Value;
        }
    }

    private void OnEnable()
    {
        _playerData.PlayerHealth.AddListener(OnHealthChanged, true);
    }

    private void OnDisable()
    {
        _playerData.PlayerHealth.RemoveListener(OnHealthChanged);
    }
    
    private void OnHealthChanged(int value)
    {
        _healthBar.value = value;
    }
}
