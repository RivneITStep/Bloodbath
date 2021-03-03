using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Quest
{

    public string quest_id;

    public bool isActive;

    public QuestResult result;

    public Player player;

    public delegate bool checkCompile(Quest quest, Player player);
        
    public delegate void doResult(Player player);

    public checkCompile check;

    public doResult doresult;
        

    public QuestResult CheckComplite()
    {
        if(check(this, player))
        {
            isActive = false;
            return result;
        }
        return null;

           
    }

    public void DoResult()
    {
            doresult(player);
    }

    
}

