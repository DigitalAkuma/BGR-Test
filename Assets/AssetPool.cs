using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Data structure for all dress up assets
 */
public class AssetPool : MonoBehaviour
{
    //All sprite assets referenced by string
    public List<Dictionary<string, Sprite>> Assets; //list of dictionaries containign assets ORDERED
    

    private void Start()
    {
        Assets = new List<Dictionary<string, Sprite>>();
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
 */