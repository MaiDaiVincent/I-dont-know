using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayUI : Singleton<GameplayUI>
{
    public int enemyAmount = 0;
    public int bossAmount = 1;
    public EnemyAmountHUD enemyAmountHUD;
    public GameObject endgamePanel;
    public Button restartButton;
    private ManagerRoot managerRoot => ManagerRoot.Instance;

    private void Start()
    {
        restartButton.onClick.AddListener(RestartClick);
        SetEnemyAmountInit();
    }
    void RestartClick()
    {
        managerRoot.ResetData();
        managerRoot.TransitionToScene(managerRoot.ManagerRootConfig.home);
    }
    void SetEnemyAmountInit()
    {
        foreach (var wave in SpawnerManager.Instance. waveList)
        {
            for (int i = 0; i < wave.spawnList.Count; i++)
            {
                enemyAmount += 1;
            }
        }
        enemyAmountHUD.SetAmountText(enemyAmount, bossAmount);
    }
    public void KillEnemy()
    {
        enemyAmount -= 1;
        enemyAmountHUD.SetAmountText(enemyAmount, bossAmount);

    }
    public void KillBoss()
    {
        bossAmount -= 1;
        enemyAmountHUD.SetAmountText(enemyAmount, bossAmount);
        endgamePanel.SetActive(true);
        GameplayManager.Instance.isEndGame = true;
    }
    public void SetEnd()
    {
        endgamePanel.SetActive(true);
        GameplayManager.Instance.isEndGame = true;
    }
}
