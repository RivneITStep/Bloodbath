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
       // hpSlider = hpObject.GetComponent<Slider>();

        //armorSlider = armorObject.GetComponent<Slider>();
    }

    public void SetHP(int hp)
    {
        if(hpSlider == null)
            hpSlider = hpObject.GetComponent<Slider>();

        hpSlider.value = hp;
        hpTextObject.GetComponent<Text>().text = hp.ToString();
    }

    public void SetArmor(int armor)
    {
        if(armorSlider == null)
            armorSlider = armorObject.GetComponent<Slider>();
        if (armor > 0)
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
