using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class DataBase : MonoBehaviour
    {
        public List<Item> Items = new List<Item>();
        //{
        //    //new Weapon("Subg_mp7","MP-7","guns_3",12, Rarity.Common),*
        //    //new Weapon("Subg_mp5","MP-5","guns_4",13, Rarity.Common),*
        //    //new Weapon("Assault_m4", "Colt M4", "guns_9",18, Rarity.Common),
        //    //new Weapon("Assault_m4a4s", "M4A4-S", "guns_10",17, Rarity.Rare),*
        //    //new Weapon("Assault,m4a1", "M4A1", "guns_12",18, Rarity.Common),
        //    //new Weapon("Assault_g3sg1", "G3SG1","guns_11",21, Rarity.Rare),
        //    //new Weapon("Assault_scarl", "SCAR-L","guns_15",19, Rarity.Rare),
        //    //new Weapon("Assault_hkg63", "HK G36","guns_13",18, Rarity.Common),
        //    //new Weapon("Assault_sigsg", "SIG SG 550","guns_14",20, Rarity.Rare),
        //    //new Weapon("Assault_qbz", "QBZ95","guns_8",18, Rarity.Rare),
        //    //new Weapon("Assault_famas","Famas","guns_21",18, Rarity.Common),
        //    //new Weapon("Shotgun_cm350m","CM350M","guns_16",10, Rarity.Rare),
        //    //new Weapon("Shotgun_obrez","Obrez","guns_17",15, Rarity.Rare),
        //    //new Weapon("SniperR_M24","M24","guns_18",100, Rarity.Epic),
        //    //new Weapon("SniperR_barrett","Barrett M82A1","guns_26",220, Rarity.Legendary),
        //    //new Weapon("MachineG_m249", "M249","guns_20",23, Rarity.Epic),
        //    //new Weapon("MachineG,m60","M60", "guns_27",25, Rarity.Epic),
        //    //new Weapon("RocketL_rgw90","RGW 90","guns_24",600, Rarity.Epic),
        //    //new Weapon("RocketL_Igla","Igla", "guns_25",720, Rarity.Legendary),
        //    //new Weapon("Pistol_af1","Arsenal Firearms AF1","guns_22",11, Rarity.Common),
        //    //new Weapon("Pistol_m9","Beretta M9","guns_0",10, Rarity.Common),*
        //    //new Weapon("Pistol_m87","Beretta 87 Target","guns_2",12, Rarity.Common)
        //};
        public Item GetItemById(string id)
        {
            foreach(var item in Items)
            {
                if (item.Item_ID == id)
                    return item;
            }
            return null;
        }



    }

}
