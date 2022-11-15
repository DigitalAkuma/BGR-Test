using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

/*
 * Data structure for all dress up assets grabbed from Resources folder
 */
[ExecuteInEditMode]
public class AssetPool : MonoBehaviour
{
    // Step 1

    public SpriteDressTextureDictionary eyes;
    public SpriteHairTextureDictionary hair;
    public SpriteDressTextureDictionary lips;

    // Step 2 

    public SpriteDressTextureDictionary tops;
    public SpriteDressTextureDictionary sleeves;
    public SpriteDressTextureDictionary bottoms;

    // Step 3

    public SpriteDressTextureDictionary shoes;
    public SpriteDressTextureDictionary purses;
    public SpriteDressTextureDictionary jewelry;
    public SpriteDressTextureDictionary more;

    //called when something in the scene has changed (executes in editor not runtime)
    private void Update()
    {
        GrabHairAssets();
        GrabEyeAssets();
        GrabAssets("Lips");
        
        GrabAssets("Tops");
        GrabAssets("Sleeves");
        GrabAssets("Bottoms");

        GrabAssets("Shoes");
        GrabAssets("Purses");
        GrabAssets("Jewelry");
        GrabAssets("More");
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


    void GrabAssets(string folderName)
    {
        SpriteDressTextureDictionary newDictionary = new SpriteDressTextureDictionary();

        string path = "DressUpAssets/" + folderName;
        string iopath = "Assets\\Resources\\" + path.Replace('/', '\\');
        Sprite[] resources;
        DressUpTextureBundle bundle;

        int folders = System.IO.Directory.GetDirectories(iopath).Length;

        for (int i = 1; i <= folders; i++)
        {
            bundle = new DressUpTextureBundle();
            resources = Resources.LoadAll<Sprite>(path + "/" + folderName.ToLower() + i);
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

            newDictionary.Add(thumbnailKey, bundle);
        }

        switch(folderName)
        {
            case "Tops": tops = newDictionary; break;
            case "Eyes": eyes = newDictionary; break;
            case "Lips": lips = newDictionary; break;
            case "Sleeves": sleeves = newDictionary; break;
            case "Bottoms": bottoms = newDictionary; break;
            case "Shoes": shoes = newDictionary; break;
            case "Purses": purses = newDictionary; break;
            case "Jewelry": jewelry = newDictionary; break;
            case "More": more = newDictionary; break;
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

