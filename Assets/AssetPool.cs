using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using System.IO;
using System.Linq;

/*
 * Data structure for all dress up assets grabbed from Resources folder
 */
[ExecuteInEditMode]
public class AssetPool : MonoBehaviour
{
    [SerializeField]
    List<StringDressTextureDictionary> assetDictionaries;

    [SerializeField]
    List<string> resources_dressUpAssets_subdirectories;

    private void Start()
    {

        resources_dressUpAssets_subdirectories = System.IO.Directory.GetDirectories("DressUpAssets/").ToList<string>();
        
        //Fill up the dictionaries by going through the resources directory, matching asset names

        /*
         * Algorithm for directory names...
         * 
         * Root directory = DressUpAssets/
         * Subs = Body, Bottoms, Eyes, ... = brand new StringDressTextureDictionary per sub folder
         * 
         * For each *subfolder* found within sub...
         * - Create a new entry in the dictionary
         * - Set the entry's string key to *subfolder* name 
         * - Look inside that folder and grab the Sprites called colourable and texture and place them
         * into the DressUpTextureBundle
         * 
         */

        //Array of 2D Sprites[] from a subdirectory
        Sprite[] resources;
        string subDirectory;

        foreach (string sub in resources_dressUpAssets_subdirectories)
        {
            assetDictionaries.Add(new StringDressTextureDictionary());
            subDirectory = "DressUpAssets/" + sub;
            //resources = Resources.LoadAll<Sprite>(subDirectory + "/" + name + i);
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
    public Sprite texture;
    public Sprite background;
}

[Serializable]
public class StringDressTextureDictionary : SerializableDictionary<string, DressUpTextureBundle>
{

}

