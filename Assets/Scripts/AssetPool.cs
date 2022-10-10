using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using System.IO;
using System.Linq;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;

/*
 * Data structure for all dress up assets grabbed from Resources folder
 */
[ExecuteInEditMode]
public class AssetPool : MonoBehaviour
{

    public SpriteDressTextureDictionary eyes;

    public SpriteHairTextureDictionary hair;

    public SpriteDressTextureDictionary lips;

    //called when something in the scene has changed (executes in editor not runtime)
    private void Update()
    {
        GrabHairAssets();
        GrabLipsAssets();
        GrabEyeAssets();
    }

    void GrabEyeAssets()
    {
        eyes = new SpriteDressTextureDictionary();
        int i = 1;
        string path = "DressUpAssets/Eyes";
        Sprite[] resources;
        DressUpTextureBundle bundle;

        for (i = 1; i <= 8; i++)
        {
            bundle = new DressUpTextureBundle();
            resources = Resources.LoadAll<Sprite>(path + "/" + "eyes" + i);
            Sprite thumbnailKey = null;
            

            foreach (Sprite sprite in resources)
            {

                if (sprite.name.Contains("Thumbnail"))
                {
                    thumbnailKey = sprite;
                    continue;
                }

                switch (sprite.name)
                {
                    case "colourable":
                        bundle.colourable = sprite;
                        break;
                    case "default":
                        bundle.texture = sprite;
                        break;
                    case "background":
                        bundle.background = sprite;
                        break;
                }
            }

            eyes.Add(thumbnailKey, bundle);
        }
    }

    void GrabHairAssets()
    {
        hair = new SpriteHairTextureDictionary();
        int i = 1;
        string path = "DressUpAssets/Hair";
        Sprite[] resources;
        HairTextureBundle bundle;

        for (i = 1; i <= 15; i++)
        {
            bundle = new HairTextureBundle();
            resources = Resources.LoadAll<Sprite>(path + "/" + "hair" + i);
            Sprite thumbnailKey = null;


            foreach (Sprite sprite in resources)
            {
                if (sprite.name.Contains("Thumbnail"))
                {
                    thumbnailKey = sprite;
                    continue;
                }

                switch (sprite.name)
                {
                    case "colourable_front":
                        bundle.colourableFront = sprite;
                    break;
                    case "colourable_back":
                        bundle.colourableBack = sprite;
                    break;
                    case "texture_front":
                        bundle.textureFront = sprite;
                    break;
                    case "texture_back":
                        bundle.textureBack = sprite;
                    break;
                }
            }

            hair.Add(thumbnailKey, bundle);
        }

    }

    void GrabLipsAssets()
    {
        lips = new SpriteDressTextureDictionary();
        int i = 1;
        string path = "DressUpAssets/Lips";
        Sprite[] resources;
        DressUpTextureBundle bundle;

        for (i = 1; i <= 5; i++)
        {
            bundle = new DressUpTextureBundle();
            resources = Resources.LoadAll<Sprite>(path + "/" + "lips" + i);
            Sprite thumbnailKey = null;
            

            foreach (Sprite sprite in resources)
            {
                if (sprite.name.Contains("Thumbnail"))
                {
                    thumbnailKey = sprite;
                    continue;
                }

                switch (sprite.name)
                {
                    case "colourable":
                        bundle.colourable = sprite;
                        break;
                    case "texture":
                        bundle.texture = sprite;
                        break;
                    case "background":
                        bundle.background = sprite;
                        break;
                }
            }

            lips.Add(thumbnailKey, bundle);
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
    public Sprite? colourable;
    public Sprite? texture;
    public Sprite? background;
}

[Serializable]
public class HairTextureBundle 
{
    public Sprite? colourableFront;
    public Sprite? colourableBack;
    public Sprite? textureFront;
    public Sprite? textureBack;
}

//Key: Thumbnail sprite
//Value: Texture bundle
[Serializable]
public class SpriteDressTextureDictionary : SerializableDictionary<Sprite, DressUpTextureBundle> { }

[Serializable]
public class SpriteHairTextureDictionary : SerializableDictionary<Sprite, HairTextureBundle> { }

