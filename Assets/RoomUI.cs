using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomUI : MonoBehaviour
{
    [SerializeField] private Button returnButton;

    private ManagerRoot managerRoot => ManagerRoot.Instance;

    private void Awake()
    {
        returnButton.onClick.AddListener(OnReturnClick);
    }
    private void Start()
    {
        
    }
    private void OnReturnClick()
    {
        managerRoot.ResetData();
        managerRoot.TransitionToScene(managerRoot.ManagerRootConfig.home);

    }
}
