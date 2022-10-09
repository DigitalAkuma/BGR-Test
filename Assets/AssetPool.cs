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
//[ExecuteInEditMode]
public class AssetPool : MonoBehaviour
{
    [SerializeField]
    List<StringDressTextureDictionary> eyes = new List<StringDressTextureDictionary>();
    [SerializeField]
    List<StringDressTextureDictionary> hair = new List<StringDressTextureDictionary>();
    [SerializeField]
    List<StringDressTextureDictionary> lips = new List<StringDressTextureDictionary>();
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
    public Sprite? colourable;
    public Sprite? texture;
    public Sprite? background;
}

[Serializable]
public class StringDressTextureDictionary : SerializableDictionary<string, DressUpTextureBundle>
{

}

