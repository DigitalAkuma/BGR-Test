using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Carousel : MonoBehaviour
{
    public string name; //"hair_carousel" but only hair is grabbed
    public GameObject wheel; //gameobject containing all ui things
    public int slotCount; //how many display slots there are
    public int resourceCount; //how many items there are
    public int currentPos; //which number is in the start position
    public List<Button> moveButtons;
    public List<Button> slotButtons;

    //All sprite assets referenced by string
    public List<Dictionary<string, Sprite>> assetsDictionaryList; //list of dictionaries containign assets ORDERED
    public GameObject selected; //the gameobject of the selection image
    public int assetsIndex; //which asset dictionary is selected;

    //Reference to the AssetPool
    AssetPool assetPool;

    //Reference to the DressupModel
    DressupModel dressupModel;

    //Carousels - hair, lips, eyes
    public void Start()
    {
        string[] splits = wheel.name.Split('_');

        this.name = splits[0];
        this.currentPos = 1;
        this.selected = wheel.transform.Find("selected").gameObject; //thumbnail dashes
        this.assetsIndex = 0;

        assetsDictionaryList = new List<Dictionary<string, Sprite>>();

        //GameObject.Find() is fine as long as it's not used every frame or a lot
        assetPool = GameObject.Find("Dress-up Asset Pool").GetComponent<AssetPool>();
        dressupModel = GameObject.Find("dressup").GetComponent<DressupModel>();

        //loop through each folder in relevant asset folder, add to dictionary 
        //keeps going until no more assets can be found
        int i = 1;
        bool valid = true;

        while (valid == true)
        {
            //Grab a new sprite array at each iteration ... If it doesn't exist break loop
            //Array of 2D Sprites[] from Resources/DressUpAssets/name/name[i]
            //This loop searches for thumbnail textures to place into the slots
            Sprite[] allResources = Resources.LoadAll<Sprite>("DressUpAssets/" + name + "/" + name + i); 
            if (allResources != null && allResources.Length != 0)
            {
                Dictionary<string, Sprite> currentDict = new Dictionary<string, Sprite>();
                foreach (Sprite texture in allResources)
                {
                    if (texture.name.Contains("Thumbnail"))
                    {
                        currentDict.Add("thumbnail", texture);
                    }
                    else
                    {
                        currentDict.Add(texture.name, texture);
                    }
                }
                //dictionary contains all textures for corresponding carousel - referenced by their name
                assetsDictionaryList.Add(currentDict); 
                i++;
            }
            else
            {
                valid = false;
            }
        }

        //the number of subfolders = number of resources
        this.resourceCount = i - 1; 

        moveButtons = new List<Button>();
        slotButtons = new List<Button>();

        //Search for buttons in the carousel game object's children
        foreach (Transform child in wheel.transform) 
        {
            if (child.gameObject.GetComponent<Button>()) 
            {
                //adding listeners to move buttons (left, right, down, up)
                if (child.name.Contains("bt_"))
                {
                    Button childButton = child.gameObject.GetComponent<Button>();
                    moveButtons.Add(childButton);
                    childButton.onClick.AddListener(() => MoveButtonClicked(childButton));
                }
                DisableBackButton();
                //adding listeners to thumbnail slots
                //clicking on a slot button calls SlotButtonClicked
                //a slot button is any child with "slot" in its name
                if (child.name.Contains("slot"))
                {
                    Button childButton = child.gameObject.GetComponent<Button>();
                    slotButtons.Add(childButton);
                    childButton.onClick.AddListener(() => SlotButtonClicked(childButton));
                }             
            }
        }
    }

    void MoveButtonClicked(Button myButton)
    {
        string[] splits = myButton.name.Split('_');
        string direction = splits[1];

        if (direction == "right" || direction == "down")
        {
            AdvanceCarousel();
        } else 
        {
            ReverseCarousel();
        }
    }

    //Slot button clicked - selects a thumbnail
    //Selected game object's parent becomes the selected slot button (myButton)
    void SlotButtonClicked(Button myButton)
    {
        if (myButton.gameObject.transform.Find("selected"))
        {
            selected.transform.SetParent(myButton.gameObject.transform.parent);
            selected.GetComponent<Image>().enabled = false;
            assetsIndex = 0;
        }
        else {
            selected.transform.SetParent(myButton.gameObject.transform);
            selected.transform.localPosition = new Vector3(-1, -1, 0);
            selected.GetComponent<Image>().enabled = true;
            string buttonName = myButton.gameObject.name;
            int slotNumber = (int)Char.GetNumericValue(buttonName[buttonName.Length - 1]);
            assetsIndex = currentPos - 1 + slotNumber;
        }

        /*
         * The selected thumbnail asset is in the same folder as the sibling assets
         * It is also the key for that folder
         */

        Sprite thumbnail = myButton.gameObject.GetComponent<Image>().sprite;

        //The name string: ex "hair", helps find directory path
        //Name is based off game object carousel name
        //It also helps determine which assets we're dealing with
        switch (name)
        {
            case "hair":
                //Corresponding bundle is grabbed using given thumbnail
                HairTextureBundle hairBundle = assetPool.hair[thumbnail]; 
                dressupModel.ChangeHair(hairBundle.textureFront, hairBundle.textureBack, hairBundle.colourableFront, hairBundle.colourableBack);
                break;
            case "eyes":
                DressUpTextureBundle eyesBundle = assetPool.eyes[thumbnail];
                dressupModel.ChangeEyes(eyesBundle.background, eyesBundle.colourable, eyesBundle.texture);
                break;
            case "lips":
                DressUpTextureBundle lipsBundle = assetPool.lips[thumbnail];
                dressupModel.Change(name, lipsBundle.texture, lipsBundle.colourable);
                break;
            case "tops": 
                DressUpTextureBundle topsBundle = assetPool.tops[thumbnail];
                dressupModel.Change(name, topsBundle.texture, topsBundle.colourable);
                break;
            case "bottoms":
                DressUpTextureBundle bottomsBundle = assetPool.bottoms[thumbnail];
                dressupModel.Change(name, bottomsBundle.texture, bottomsBundle.colourable);
                break;
            case "sleeves":
                DressUpTextureBundle sleevesBundle = assetPool.sleeves[thumbnail];
                dressupModel.Change(name, sleevesBundle.texture, sleevesBundle.colourable);
                break;
            case "shoes":
                DressUpTextureBundle shoesBundle = assetPool.shoes[thumbnail];
                dressupModel.Change(name, shoesBundle.texture, shoesBundle.colourable);
                break;
            case "purses":
                DressUpTextureBundle pursesBundle = assetPool.purses[thumbnail];
                dressupModel.Change(name, pursesBundle.texture, pursesBundle.colourable);
                break;
            case "jewelry":
                DressUpTextureBundle jewelryBundle = assetPool.jewelry[thumbnail];
                dressupModel.Change(name, jewelryBundle.texture, jewelryBundle.colourable);
                break;
            case "more":
                DressUpTextureBundle moreBundle = assetPool.more[thumbnail];
                dressupModel.Change(name, moreBundle.texture, moreBundle.colourable);
                break;

        }
    }

    void AdvanceCarousel()
    {
        //check if max numbers, if so set to 1
        if (currentPos + slotCount - 1 == resourceCount)
        { 
            currentPos = 1;
            DisableBackButton();
        }
        else 
        { 
            currentPos++;
            EnableBackButton();
        }

        int count = -1;
        foreach (Button bt in slotButtons)
        {
            bt.gameObject.GetComponent<Image>().sprite = assetsDictionaryList[currentPos + count]["thumbnail"];
            if (bt.gameObject.transform.Find("selected"))
            {
                selected.transform.SetParent(bt.gameObject.transform.parent);
                selected.GetComponent<Image>().enabled = false;
            }
            if (currentPos + count + 1 == assetsIndex)
            {
                selected.transform.SetParent(bt.gameObject.transform);
                selected.transform.localPosition = new Vector3(-1, -1, 0);
                selected.GetComponent<Image>().enabled = true;
            }
            count++;
        }
    }

    void ReverseCarousel()
    {
        if (currentPos == 2)
        {
            DisableBackButton();
        }
        currentPos--;

        int count = -1;
        foreach (Button bt in slotButtons)
        {
            bt.gameObject.GetComponent<Image>().sprite = assetsDictionaryList[currentPos + count]["thumbnail"];
            if (bt.gameObject.transform.Find("selected"))
            {
                selected.transform.SetParent(bt.gameObject.transform.parent);
                selected.GetComponent<Image>().enabled = false;
            }
            if (currentPos + count + 1 == assetsIndex)
            {
                selected.transform.SetParent(bt.gameObject.transform);
                selected.transform.localPosition = new Vector3(-1, -1, 0);
                selected.GetComponent<Image>().enabled = true;
            }
            count++;
        }
    }

    void DisableBackButton()
    {
        foreach (Button bt in moveButtons)
        {
            GameObject btObj = bt.gameObject;
            if (btObj.name.Contains("left") || btObj.name.Contains("up"))
                btObj.GetComponent<Image>().enabled = false; //DISABLES BACK BUTTON
        }
    }

    void EnableBackButton()
    {
        foreach (Button bt in moveButtons)
        {
            GameObject btObj = bt.gameObject;
            if (btObj.name.Contains("left") || btObj.name.Contains("up"))
                btObj.GetComponent<Image>().enabled = true; //ENABLES BACK BUTTON
        }
    }
}

