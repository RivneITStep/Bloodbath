using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class Page
    {
        public int emenentsMaxCount;
        public List<GameObject> components = new List<GameObject>();


        public void AddElement(GameObject component)
        {
            
                components.Add(component);
               
            
            

        }

    }
}
