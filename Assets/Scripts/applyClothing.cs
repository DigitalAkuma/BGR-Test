using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ApplyClothing : MonoBehaviour
{

    void Start()
    {
        Carousel hair_carousel = new Carousel(GameObject.Find("hair_carousel"), 4);
        Carousel lips_carousel = new Carousel(GameObject.Find("lips_carousel"), 2);
        Carousel eyes_carousel = new Carousel(GameObject.Find("eyes_carousel"), 2);
    }

}