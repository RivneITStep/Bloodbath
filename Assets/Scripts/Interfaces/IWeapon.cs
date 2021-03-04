using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Interfaces
{
    public interface IWeapon
    {

        

         void Fire();

         void Reload();

        

         int GetCurrAmmo();

         int GetAmmo();

         bool GetIsReloading();

         float GetCurrReloadTime();

         float GetReloadTime();


    }
}
