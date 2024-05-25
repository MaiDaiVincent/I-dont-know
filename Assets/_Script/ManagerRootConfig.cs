using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "ManagerRootConfig", menuName = "Config/ManagerRoot")]
public class ManagerRootConfig : ScriptableObject
{
    public string mainMenu = "Main Menu";
    public string teamSetup = "TeamSetup";
    public string home = "Home";
    public string room = "Room";
    public AvailableFungiConfig availableFungiConfig;
    public AvailableBossConfig availableBossConfig;
}
