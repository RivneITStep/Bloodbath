using Assets.Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCGunShop : MonoBehaviour, INPCMenu
{


    public GameObject npc_menu_parent;

    public GameObject menuUI_Prefab;

    private GameObject menuUI;

    public GameObject playerObject;

    private Player player;

    public void OpenMenu()
    {
        if (menuUI == null)
        {
            menuUI = Instantiate(menuUI_Prefab, npc_menu_parent.transform);

            player = playerObject.GetComponent<Player>();

            menuUI.GetComponent<GunUI>().gunShop_List.GetComponent<GunShop>().player = player;

            menuUI.GetComponent<GunUI>().gunShop_List.GetComponent<GunShop>().OpenShop();
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
