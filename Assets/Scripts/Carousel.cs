using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Carousel
{
    public string name;
    public GameObject wheel; //gameobject containing all ui things
    public int slotCount; //how many display slots there are
    public int resourceCount; //how many items there are
    public int currentPos; //which number is in the start position
    public List<Button> moveButtons;
    public List<Button> slotButtons;
    public List<Dictionary<string, Texture2D>> Assets;

    public Carousel(GameObject wheel, int resourceCount, int slotCount)
    {
        string[] splits = wheel.name.Split('_');

        this.name = splits[0];
        this.wheel = wheel;
        this.slotCount = slotCount;
        this.resourceCount = resourceCount;
        this.currentPos = 1;

        Assets = new List<Dictionary<string, Texture2D>>();

        //loop through each folder in relevant asset folder, add to dictionary 
        //keeps going until no more assets can be found
        int i = 1;
        bool valid = true;

        while (valid == true)
        {
            Texture2D[] allResources = Resources.LoadAll<Texture2D>("DressUpAssets/" + name + "/" + name + i);
            if (allResources != null && allResources.Length != 0)
            {
                Dictionary<string, Texture2D> currentDict = new Dictionary<string, Texture2D>();
                foreach (Texture2D texture in allResources)
                {
                    if (texture.name.Contains("Thumbnail"))
                    {
                        currentDict.Add("Thumbnail", texture);
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

        Debug.Log(Assets.Count);

        moveButtons = new List<Button>();
        slotButtons = new List<Button>();

        foreach (Transform child in wheel.transform) 
        {
            if (child.gameObject.GetComponent<Button>()) 
            {
                //adding listeners to move buttons (left, right, down)
                if (child.name.Contains("bt_"))
                {
                    Button childButton = child.gameObject.GetComponent<Button>();
                    moveButtons.Add(childButton);
                    childButton.onClick.AddListener(() => MoveButtonClicked(childButton));
                }
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

    void SlotButtonClicked(Button myButton)
    {
        Debug.Log(myButton);
        //CHANGE DICTIONARY IN DRESSUPSETTINGS
    }

    void AdvanceCarousel()
    {
        //check if max numbers, if so set to 1
        
    }

    void ReverseCarousel()
    {
        //if on 1, do nothing
    }
}

