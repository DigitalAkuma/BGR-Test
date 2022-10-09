using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Data structure for all dress up assets
 */
public class AssetPool : MonoBehaviour
{
    [SerializeField]
    StringDressTextureDictionary assetDictionary;

    private void Start()
    {
        //loop through each folder in relevant asset folder, add to dictionary 
        //keeps going until no more assets can be found
        int i = 1;
        bool valid = true;

        while (valid == true)
        {
            //Array of 2D Sprites[] from Resources/DressUpAssets/
            Sprite[] allResources = Resources.LoadAll<Sprite>("DressUpAssets/" + name + "/" + name + i);
            if (allResources != null && allResources.Length != 0)
            {
                Dictionary<string, Sprite> currentDict = new Dictionary<string, Sprite>();
                foreach (Sprite texture in allResources)
                {
                   //do something
                }
                i++;
            }
            else
            {
                valid = false;
            }
        }
    }
}

/*
 * Notes on storing asset information...
 * 
 * Each asset has a default version and a colourable version stored in the same folder
 * 
 * Certain assets have different rules. Some hairstyles have a front and a back.
 * The eyes have a background with an eyeball that can change colors.
 * 
 * Certain eyes are closed and don't change color at all (leaving just the default texture)
 * 
 * The colourable and the textures must be combined into one... It might be easier to ignore the default asset 
 * and instead opt for a default color.
 * 
 * 
 * Key: there needs to be a way to relate the thumbnails to their corresponding colourables and textures
 * 
 */

[Serializable]
public class DressUpTextureBundle 
{
    public Sprite colourable;
    public Sprite textureOrBackground;
}

[Serializable]
public class StringDressTextureDictionary : SerializableDictionary<string, DressUpTextureBundle>
{

}

