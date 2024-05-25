using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerRoot : Singleton<ManagerRoot>
{
    public List<FungusNameType> actionFungusNameList = new List<FungusNameType>();
    public BossNameType actionBossNameType = BossNameType.None;

    [Space(20)]
    [SerializeField] private TransitionController transitionController;
    [SerializeField] private Animator transitionAnim;

    public ManagerRootConfig ManagerRootConfig { get => managerRootConfig; }
    [SerializeField] private ManagerRootConfig managerRootConfig;
    public void GetNameTypeFungusPicked(List<FungusSlot> fungusSlotList)
    {
        
        foreach(var fungusSlot in fungusSlotList)
        {
            actionFungusNameList.Add(fungusSlot.FungusPackedConfig.fungusNameType);
        }
    }
    public void GetNameTypeBossChose(List<BossSlot> bossSlotList)
    {
        foreach(var bossSlot in bossSlotList)
        {
            if (bossSlot.isSelecting)
            {
                actionBossNameType = bossSlot.BossPackedConfig.bossNameType;
                break;
            }

        }
    }
    public void TransitionToScene(string sceneName)
    {
        StartCoroutine(ProgressTransitionToScene(sceneName));
    }
    IEnumerator ProgressTransitionToScene(string sceneName)
    {

        transitionController.gameObject.SetActive(true);
        AudioManager.Instance.PlayTransition();

        yield return new WaitForSeconds(0.5f);

        var asyncOperator = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncOperator.isDone)
        {
            yield return new WaitForSeconds(0.5f);
            transitionAnim.SetBool("IsOpen", true);
            AudioManager.Instance.PlayTransition();


            float process = Mathf.Clamp01(asyncOperator.progress / 0.9f);
            
            yield return new WaitForSeconds(0.5f);
            transitionController.gameObject.SetActive(false);

            yield return null;
        }
    }
    public void ResetData()
    {
        actionFungusNameList.Clear();
        actionBossNameType = BossNameType.None;
    }
}
