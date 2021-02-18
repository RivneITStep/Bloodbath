using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject hpObject;
    public GameObject armorObject;

    public GameObject hpTextObject;
    public GameObject ArmorTextObject;

    //public Gam

    private Slider hpSlider;
    private Slider armorSlider;

    void Start()
    {
        hpSlider = hpObject.GetComponent<Slider>();
        armorSlider = armorObject.GetComponent<Slider>();
    }

    public void SetHP(int hp)
    {
        hpSlider.value = hp;
        hpTextObject.GetComponent<Text>().text = hp.ToString();
    }

    public void SetArmor(int armor)
    {
        if(armor > 0)
        {
            armorObject.SetActive(true);
            armorSlider.value = armor;
            ArmorTextObject.GetComponent<Text>().text = armor.ToString();
        }
        else
        {
            armorObject.SetActive(false);
        }
    }


}
