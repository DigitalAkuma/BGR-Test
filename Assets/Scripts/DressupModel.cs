using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Contains references to all the parts of the model that can be changed/dressed up
 * Public methods for changing textures by passing in a texture
 */
public class DressupModel : MonoBehaviour
{
    /*
     * Key = game object name
     * Ex: background, colourable
     * 
     */
    public StringGameObjectDictionary eyesReferences;
    public StringGameObjectDictionary hairReferences;
    public StringGameObjectDictionary lipsReferences;
    public StringGameObjectDictionary topsReferences;
    public StringGameObjectDictionary sleevesReferences;
    public StringGameObjectDictionary jewelryReferences;
    public StringGameObjectDictionary shoesReferences;
    public StringGameObjectDictionary bottomsReferences;
    public StringGameObjectDictionary pursesReferences;
    public StringGameObjectDictionary moreReferences;

    [SerializeField]
    Sprite blank;

    public void ChangeEyes(Sprite background, Sprite colourable, Sprite texture)
    {
        //Set the key, which will reference the game object with the sprite to change
        ICollection<string> keys = eyesReferences.Keys;

        if (colourable == null && background == null)
        {
            eyesReferences["background"].GetComponent<Image>().sprite = texture;
            eyesReferences["colourable"].GetComponent<Image>().sprite = texture;
        }
        else
        {
            foreach (string key in keys)
            {
                switch (key)
                {
                    case "background":
                        eyesReferences[key].GetComponent<Image>().sprite = background;
                        break;
                    case "colourable":
                        eyesReferences[key].GetComponent<Image>().sprite = colourable;
                        break;
                }
            }
        }
        
    }

    public void Change(string type, Sprite texture, Sprite colourable)
    {
        //Set the key, which will reference the game object with the sprite to change
        ICollection<string> keys = null;
        StringGameObjectDictionary references = null;

        switch (type)
        {
            case "lips":
                keys = lipsReferences.Keys;
                references = lipsReferences;
                break;
            case "tops":
                keys = topsReferences.Keys;
                references = topsReferences;
                break;
            case "bottoms":
                keys = bottomsReferences.Keys;
                references = bottomsReferences;
                break;
            case "sleeves":
                keys = sleevesReferences.Keys;
                references = sleevesReferences;
                break;
            case "shoes":
                keys = shoesReferences.Keys;
                references = shoesReferences;
                break;
            case "purses":
                keys = pursesReferences.Keys;
                references = pursesReferences;
                break;
            case "jewelry":
                keys = jewelryReferences.Keys;
                references = jewelryReferences;
                break;
            case "more":
                keys = moreReferences.Keys;
                references = moreReferences;
                break;
        }

        foreach (string key in keys)
        {
            switch (key)
            {
                case "texture":
                    if (texture != null) references[key].GetComponent<Image>().sprite = texture;
                    else references[key].GetComponent<Image>().sprite = blank;
                    break;
                case "colourable":
                    references[key].GetComponent<Image>().sprite = colourable;
                    break;
            }
        }
    }

    public void ChangeHair(Sprite textureFront, Sprite textureBack, Sprite colourableFront, Sprite colourableBack)
    {
        //Set the key, which will reference the game object with the sprite to change
        ICollection<string> keys = hairReferences.Keys;

        foreach (string key in keys)
        {
            switch (key) //Key = game object name
            {
                case "texture_front":
                    if (textureFront != null) hairReferences[key].GetComponent<Image>().sprite = textureFront;
                    else hairReferences[key].GetComponent<Image>().sprite = blank;
                    break;
                case "texture_back":
                    if (textureBack != null) hairReferences[key].GetComponent<Image>().sprite = textureBack;
                    else hairReferences[key].GetComponent<Image>().sprite = blank;
                    break;
                case "colourable_front":
                    hairReferences[key].GetComponent<Image>().sprite = colourableFront;
                    break;
                case "colourable_back":
                    hairReferences[key].GetComponent<Image>().sprite = colourableBack;
                    break;
            }
        }
    }

    /*
     * colourable - replaces default if we are applying colors
     * 
     */
}


/*
 * Resources/DressUpAssets
 */

/*
 * Notes
 * 'dressup' game obj parent for applying textures
 * 
 * wheel game object contains all UI for carousels
 */

[Serializable]
public class StringGameObjectDictionary : SerializableDictionary<string, GameObject> { }