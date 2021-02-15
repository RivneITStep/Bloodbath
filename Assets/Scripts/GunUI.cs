using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunUI : MonoBehaviour 
{

    public Button LeftBtn;
    public Button RightBtn;
    public GameObject gunShop_List;
    private GunShop gunShop;

    // Start is called before the first frame update
    void Start()
    {
        gunShop = gunShop_List.GetComponent<GunShop>();
        //LeftBtn.onClick.AddListener(delegate { gameObject.GetComponentInChildren<GunShop>().Prev(); });
        //RightBtn.onClick.AddListener(delegate { gameObject.GetComponentInChildren<GunShop>().Next(); });
        LeftBtn.onClick.AddListener(delegate {  gunShop.Prev(); });
        RightBtn.onClick.AddListener(delegate {   gunShop.Next(); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
