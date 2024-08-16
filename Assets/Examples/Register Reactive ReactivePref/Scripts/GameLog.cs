using DataKeeper.SingletonPattern;
using UnityEngine;

public class GameLog : Singleton<GameLog>
{
    public void Heal(string log)
    {
        Debug.Log($"<color=green>{log}</color>");
    }
    
    public void Damage(string log)
    {
        Debug.Log($"<color=red>{log}</color>");
    }
}
