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

    public void ChangeEyes(Sprite background, Sprite colourable)
    {
        //Set the key, which will reference the game object with the sprite to change
        ICollection<string> keys = eyesReferences.Keys;

        foreach (string key in keys)
        {
            switch(key)
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

    public void ChangeLips(Sprite texture, Sprite colourable)
    {

    }

    public void ChangeHair(Sprite textureFront, Sprite textureBack, Sprite colourableFront, Sprite colourableBack)
    {

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