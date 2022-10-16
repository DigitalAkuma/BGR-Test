using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorCarousel : MonoBehaviour
{
    public string name;  //"hair_carousel" but only hair is grabbed
    public List<Button> moveButtons;
    public List<Slot> slots; //slots positioned in inspector, children of this transform
   // GameObject selected; //the gameobject of the selection image

    //Reference to game object colourables
    public List<GameObject> colourables;

    //Colors selected in inspector, shares ordering with slots
    public List<Color32> colorList;

    // Start is called before the first frame update
    void Start()
    {
        name = gameObject.name;
        string[] splits = this.name.Split('_');

        name = splits[0];

        switch(name)
        {
            case "skin":
                colorList = DressUpColors.colorGroup_Skin;
                break;
            case "hair":
                colorList = DressUpColors.colorGroup_Hair;
                break;
            case "lips":
                colorList = DressUpColors.colorGroup_Lips;
                break;
            default:
                colorList = DressUpColors.colorGroup_General;
                break;
        }

        //selected = this.transform.Find("color_selected").gameObject; //thumbnail dashes

        int i = 0;
        Slot newSlot;
        foreach (Transform child in this.transform)
        {
            //adding listeners to move buttons (left, right, down, up)
            if (child.name.Contains("bt_"))
            {
                Button childButton = child.gameObject.GetComponent<Button>();
                moveButtons.Add(childButton);
                childButton.onClick.AddListener(() => MoveButtonClicked(childButton));
            }

            if (child.name.Contains("slot"))
            {
                GameObject slotObj = child.gameObject;
                Button slotButton = slotObj.AddComponent<Button>();
                newSlot = slotObj.AddComponent<Slot>();
                newSlot.colorIndex = i;
                newSlot.slotObj = slotObj;
                newSlot.slotObj.GetComponent<Image>().color = colorList[i++]; //get i then increment
                slotButton.onClick.AddListener(() => SlotButtonClicked(slotButton, newSlot));
                slots.Add(newSlot);
            }
        }
    }

    /*
     * On click: Change the color of the assigned colourable(s)
     */
    void SlotButtonClicked(Button button, Slot givenSlot)
    {
        //selected.transform.localPosition = new Vector3(-1, -1, 0);
        foreach (GameObject colourable in colourables)
        {
            colourable.GetComponent<Image>().color = button.gameObject.GetComponent<Image>().color;
        }
    }    

    void MoveButtonClicked(Button button)
    {
        string[] splits = button.name.Split('_');
        string direction = splits[1];

        if (direction == "right" || direction == "down")
        {
            AdvanceCarousel();
        }
        else
        {
            ReverseCarousel();
        }
    }

    /*
     * When advancing/reversing, each carousel slot's color needs to be updated.
     * For each slot,
     * change its color to +1 or -1 the color index
     */
    void AdvanceCarousel()
    {
        foreach(Slot slot in slots)
        {
            int newIndex = slot.colorIndex;
            slot.colorIndex++;

            if (slot.colorIndex > colorList.Count-1)
            {
                newIndex = 0;
            }
            else
            {
                newIndex++;
            }

            slot.slotObj.GetComponent<Image>().color = colorList[newIndex];
            slot.colorIndex = newIndex;
        }
    }

    void ReverseCarousel()
    {
        foreach (Slot slot in slots)
        {
            int newIndex = slot.colorIndex;
            slot.colorIndex--;

            if (slot.colorIndex < 0)
            {
                newIndex = colorList.Count - 1;
            }
            else
            {
                newIndex--;
            }

            slot.slotObj.GetComponent<Image>().color = colorList[newIndex];
            slot.colorIndex = newIndex;
        }
    }
}


public class Slot : MonoBehaviour
{
    public GameObject slotObj;
    public int colorIndex;
    public Color color;

    private void Update()
    {
        color = slotObj.GetComponent<Image>().color;
    }
}


public static class DressUpColors
{
    public static List<Color32> colorGroup_Skin = new List<Color32>{ //11 placeholders
        new Color32(255, 243, 230, 255),
        new Color32(253, 220, 210, 255),
        new Color32(246, 211, 176, 255),
        new Color32(223, 176, 118, 255),
        new Color32(204, 125, 54, 255),
        new Color32(165, 105, 51, 255),
        new Color32(133, 71, 14, 255),
        new Color32(105, 51, 11, 255),
        new Color32(138, 102, 68, 255),
        new Color32(80, 40, 10, 255),
        new Color32(116, 67, 48, 255),
        new Color32(124, 57, 49, 255),
        new Color32(191, 130, 85, 255),
        new Color32(251, 191, 255, 255),
        new Color32(254, 143, 195, 255),
        new Color32(241, 103, 103, 255),
        new Color32(253, 243, 104, 255),
        new Color32(102, 223, 92, 255),
        new Color32(93, 243, 165, 255),
        new Color32(173, 240, 252, 255),
        new Color32(59, 103, 214, 255),
        new Color32(141, 96, 244, 255)
    };

    public static List<Color32> colorGroup_Hair = new List<Color32>{ //17 placeholders
        new Color32(117, 47, 22, 255),
        new Color32(147, 167, 176, 255),
        new Color32(146, 66, 151, 255),
        new Color32(42, 55, 107, 255),
        new Color32(219, 43, 113, 255),
        new Color32(179, 131, 85, 255),
        new Color32(0, 0, 0, 255),
        new Color32(77, 77, 77, 255),
        new Color32(114, 191, 68, 255),
        new Color32(14, 20, 14, 255),
        new Color32(243, 229, 201, 255),
        new Color32(181, 128, 82, 255),
        new Color32(239, 217, 177, 255),
        new Color32(204, 175, 89, 255),
        new Color32(138, 102, 68, 255),
        new Color32(233, 199, 101, 255),
        new Color32(128, 87, 59, 255),
        new Color32(247, 238, 205, 255),
        new Color32(35, 43, 34, 255),
        new Color32(234, 232, 222, 255),
        new Color32(237, 199, 89, 255),
        new Color32(144, 97, 64, 255),
        new Color32(113, 72, 42, 255),
        new Color32(185, 65, 18, 255),
        new Color32(84, 65, 43, 255),
        new Color32(229, 213, 186, 255),
        new Color32(231, 216, 192, 255),
        new Color32(188, 75, 163, 255)
    };

    public static List<Color32> colorGroup_Eyes = new List<Color32>{ //4 placeholders
        new Color32(0, 113, 143, 255),
        new Color32(124, 144, 44, 255),
        new Color32(102, 80, 30, 255),
        new Color32(188, 143, 217, 255),
        new Color32(68, 214, 248, 255),
        new Color32(37, 27, 1, 255),
        new Color32(105, 175, 0, 255),
        new Color32(146, 164, 174, 255),
        new Color32(121, 163, 221, 255),
        new Color32(173, 141, 72, 255),
        new Color32(166, 170, 133, 255),
        new Color32(170, 182, 203, 255),
        new Color32(94, 185, 202, 255),
        new Color32(89, 55, 34, 255),
        new Color32(123, 62, 51, 255),
        new Color32(71, 102, 178, 255),
        new Color32(165, 132, 84, 255),
        new Color32(99, 133, 177, 255),
        new Color32(115, 101, 105, 255),
        new Color32(44, 101, 56, 255),
        new Color32(89, 101, 104, 255),
        new Color32(89, 84, 101, 255),
        new Color32(74, 92, 110, 255),
        new Color32(62, 117, 143, 255),
        new Color32(118, 197, 176, 255),
        new Color32(160, 195, 188, 255),
        new Color32(193, 235, 230, 255),
        new Color32(203, 229, 235, 255),
        new Color32(200, 236, 206, 255),
        new Color32(210, 209, 180, 255),
        new Color32(202, 188, 129, 255),
        new Color32(223, 204, 174, 255),
        new Color32(222, 177, 163, 255),
        new Color32(216, 109, 111, 255),
        new Color32(86, 138, 98, 255),
        new Color32(101, 79, 115, 255)
    };

    public static List<Color32> colorGroup_Lips = new List<Color32> { //4 placeholders
        new Color32(197, 18, 76, 255),
        new Color32(181, 56, 26, 255),
        new Color32(198, 68, 75, 255),
        new Color32(229, 56, 147, 255),
        new Color32(250, 135, 178, 255),
        new Color32(254, 211, 234, 255),
        new Color32(114, 10, 99, 255),
        new Color32(158, 10, 23, 255),
        new Color32(219, 40, 114, 255),
        new Color32(222, 177, 163, 255),
        new Color32(216, 109, 111, 255),
        new Color32(248, 188, 166, 255),
        new Color32(167, 219, 225, 255),
        new Color32(253, 227, 112, 255),
        new Color32(255, 128, 52, 255),
        new Color32(222, 225, 231, 255),
        new Color32(247, 239, 193, 255),
        new Color32(205, 177, 34, 255),
        new Color32(250, 134, 241, 255),
        new Color32(141, 61, 152, 255),
        new Color32(131, 171, 173, 255),
        new Color32(202, 170, 147, 255),
        new Color32(203, 38, 41, 255),
        new Color32(248, 233, 202, 255),
        new Color32(255, 255, 255, 255),
        new Color32(160, 157, 204, 255),
        new Color32(251, 199, 215, 255),
        new Color32(34, 26, 2, 255),
        new Color32(177, 214, 137, 255),
        new Color32(224, 151, 198, 255),
        new Color32(118, 50, 37, 255),
        new Color32(41, 52, 104, 255),
        new Color32(37, 154, 150, 255),
        new Color32(236, 110, 154, 255),
        new Color32(37, 154, 150, 255)
    };

    public static List<Color32> colorGroup_General = new List<Color32>{
        new Color32(241, 122, 163, 255),
        new Color32(224, 151, 198, 255),
        new Color32(236, 110, 110, 255),
        new Color32(252, 44, 169, 255),
        new Color32(219, 40, 114, 255),
        new Color32(243, 13, 105, 255),
        new Color32(203, 38, 41, 255),
        new Color32(255, 0, 0, 255),
        new Color32(255, 128, 52, 255),
        new Color32(255, 190, 56, 255),
        new Color32(255, 245, 0, 255),
        new Color32(253, 227, 112, 255),
        new Color32(247, 239, 193, 255),
        new Color32(205, 177, 34, 255),
        new Color32(207, 255, 104, 255),
        new Color32(161, 205, 140, 255),
        new Color32(88, 148, 60, 255),
        new Color32(31, 118, 56, 255),
        new Color32(161, 246, 216, 255),
        new Color32(108, 182, 186, 255),
        new Color32(13, 38, 125, 255),
        new Color32(0, 18, 178, 255),
        new Color32(64, 116, 223, 255),
        new Color32(136, 163, 218, 255),
        new Color32(18, 185, 239, 255),
        new Color32(167, 219, 225, 255),
        new Color32(148, 255, 249, 255),
        new Color32(141, 61, 152, 255),
        new Color32(164, 109, 162, 255),
        new Color32(125, 44, 146, 255),
        new Color32(65, 41, 104, 255),
        new Color32(203, 147, 227, 255),
        new Color32(250, 134, 241, 255),
        new Color32(222, 225, 231, 255),
        new Color32(181, 176, 172, 255),
        new Color32(34, 26, 2, 255),
        new Color32(255, 255, 255, 255),
        new Color32(118, 50, 37, 255),
        new Color32(144, 97, 64, 255),
        new Color32(202, 170, 147, 255)
    };
}