using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


namespace Assets.Scripts.Helpers
{
    public class SpriteHelper
    {
        public static Texture2D LoadPNG(string filePath)
        {

            Texture2D tex = null;
            byte[] fileData;

            if (File.Exists(filePath))
            {
                fileData = File.ReadAllBytes(filePath);
                tex = new Texture2D(15, 15);
                tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
            }
            return tex;
        }
    }
}
