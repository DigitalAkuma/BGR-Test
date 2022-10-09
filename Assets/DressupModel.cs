using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Contains references to all the parts of the model that can be changed/dressed up
 * Public methods for changing textures by passing in a texture
 */
public class DressupModel : MonoBehaviour
{
    public List<GameObject> changeables;

    private void Start()
    {
    }

    public void ChangeEyes(Sprite background, Sprite colourable, Sprite defaultSprite)
    {

    }

    public void ChangeEyeColor(Sprite colourable)
    {

    }

    public void ChangeLips(Sprite texture, Sprite colourable, Sprite defaultSprite)
    {

    }

    public void ChangeHairFront(Sprite texture, Sprite colourable, Sprite defaultSprite)
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