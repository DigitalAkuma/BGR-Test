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
    public StringGameObjectDictionary eyesReferences;

    public StringGameObjectDictionary hairReferences;

    public StringGameObjectDictionary lipsReferences;

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

    public void ChangeLips(Sprite texture, Sprite colourable)
    {
        //Set the key, which will reference the game object with the sprite to change
        ICollection<string> keys = lipsReferences.Keys;

        foreach (string key in keys)
        {
            switch (key)
            {
                case "texture":
                    if (texture != null) lipsReferences[key].GetComponent<Image>().sprite = texture;
                    else lipsReferences[key].GetComponent<Image>().sprite = blank;
                    break;
                case "colourable":
                    lipsReferences[key].GetComponent<Image>().sprite = colourable;
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
            switch (key)
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