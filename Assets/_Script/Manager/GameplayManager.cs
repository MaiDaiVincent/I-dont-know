using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EventManager;
using static UnityEngine.Rendering.DebugUI;

public class GameplayManager : Singleton<GameplayManager>
{
    public bool isEndGame = false;

    private void Start()
    {
        AudioManager.Instance.PlayCombatTheme();
    }
}
