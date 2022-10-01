using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Carousel
{
    public string name;
    public GameObject wheel;
    public int slotCount;
    public List<Button> buttons;

    public Carousel(GameObject wheel, int slotCount)
    {
        string[] splits = wheel.name.Split('_');

        this.name = splits[0];
        this.wheel = wheel;
        this.slotCount = slotCount;

        Resources.LoadAll("DressUpAssets/" + name);

        //adding listeners to buttons (left, right, down)

        buttons = new List<Button>();

        foreach (Transform child in wheel.transform) 
        {
            if (child.name.Contains("bt_")) 
            { 
                Button childButton = child.gameObject.GetComponent<Button>();
                buttons.Add(childButton);
                childButton.onClick.AddListener(ButtonClicked);
            }
        }


        //adding listeners to thumbnail slots


    }

    void ButtonClicked()
    { 
        
    }

    public void AdvanceCarousel()
    {
        //check if max numbers, if so set to 1

    }

    public void ReverseCarousel()
    {
        //if on 1, do nothing
    }
}
