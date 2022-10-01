using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ApplyClothing : MonoBehaviour
{
    public Carousel hair_carousel;

    void Start()
    {
        hair_carousel = new Carousel(GameObject.Find("hair_carousel"), 6);
    }

    public void MoveCarousel()
    {
        GameObject btn = EventSystem.current.currentSelectedGameObject;
        //split name to get direction (buttons name bt_left, bt_right, bt_down)
        string[] splits = btn.name.Split('_');
        string direction = splits[1];
        //Debug.Log(direction);

        //get which carousel its for
        GameObject btnParent = btn.transform.parent.gameObject;
        //Debug.Log(btnParent.name);

        //find the actual carousel
        GameObject carousel = GameObject.Find(btnParent.name + "_carousel");
        //Debug.Log(carousel.name);

        if (direction == "right" || direction == "down")
        {
            //AdvanceCarousel(carousel);
        } else {
           // ReverseCarousel(carousel);
        }

    }
}