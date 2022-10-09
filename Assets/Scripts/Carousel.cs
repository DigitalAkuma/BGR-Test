using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Carousel : MonoBehaviour
{
    public string name;
    public GameObject wheel; //gameobject containing all ui things
    public int slotCount; //how many display slots there are
    public int resourceCount; //how many items there are
    public int currentPos; //which number is in the start position
    public List<Button> moveButtons;
    public List<Button> slotButtons;

    //All sprite assets referenced by string
    public List<Dictionary<string, Sprite>> Assets; //list of dictionaries containign assets ORDERED
    public GameObject selected; //the gameobject of the selection image
    public int selectedAsset; //which asset is selected;

    //Carousels - hair, lips, eyes
    public void Start()
    {
        string[] splits = wheel.name.Split('_');

        this.name = splits[0];
        this.currentPos = 1;
        this.selected = wheel.transform.Find("selected").gameObject; //thumbnail dashes
        this.selectedAsset = 0;

        Assets = new List<Dictionary<string, Sprite>>();

        //loop through each folder in relevant asset folder, add to dictionary 
        //keeps going until no more assets can be found
        int i = 1;
        bool valid = true;

        while (valid == true)
        {
            //Array of 2D Sprites[] from Resources/DressUpAssets/
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
                Assets.Add(currentDict);
                i++;
            }
            else
            {
                valid = false;
            }
        }

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
    void SlotButtonClicked(Button myButton)
    {
        if (myButton.gameObject.transform.Find("selected"))
        {
            selected.transform.SetParent(myButton.gameObject.transform.parent);
            selected.GetComponent<Image>().enabled = false;
            selectedAsset = 0;
        }
        else {
            selected.transform.SetParent(myButton.gameObject.transform);
            selected.transform.localPosition = new Vector3(-1, -1, 0);
            selected.GetComponent<Image>().enabled = true;
            string buttonName = myButton.gameObject.name;
            int slotNumber = (int)Char.GetNumericValue(buttonName[buttonName.Length - 1]);
            selectedAsset = currentPos - 1 + slotNumber;
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
            bt.gameObject.GetComponent<Image>().sprite = Assets[currentPos + count]["thumbnail"];
            if (bt.gameObject.transform.Find("selected"))
            {
                selected.transform.SetParent(bt.gameObject.transform.parent);
                selected.GetComponent<Image>().enabled = false;
            }
            if (currentPos + count + 1 == selectedAsset)
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
            bt.gameObject.GetComponent<Image>().sprite = Assets[currentPos + count]["thumbnail"];
            if (bt.gameObject.transform.Find("selected"))
            {
                selected.transform.SetParent(bt.gameObject.transform.parent);
                selected.GetComponent<Image>().enabled = false;
            }
            if (currentPos + count + 1 == selectedAsset)
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

