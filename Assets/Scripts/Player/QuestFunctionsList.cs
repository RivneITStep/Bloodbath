using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class QuestFunctionsList
{
    public static bool ScarLUnlockCheck(Quest quest,Player player)
    {
        if(player.itemInHand && player.item.tag == "Weapon")
        {
            quest.DoResult();
            return true;
        }
        return false;
    }

    public static void ScalLUnclockDoResult(Player player)
    {
        player.unlockedGuns.Add(player.gunDataBase.GetById("Assault_scarl").GetComponent<GunInfo>());
    }


}

