using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DressUpSettings : MonoBehaviour
{
    public static DressUpSettings Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        Dictionary<string, int> DressUpConfig = new Dictionary<string, int>();

        //DressUpConfig.Add("skin", );
        DressUpConfig.Add("hair", 1);
        //DressUpConfig.Add("hair_colour", );
        DressUpConfig.Add("eyes", 1);
        //DressUpConfig.Add("eyes_colour", );
        DressUpConfig.Add("lips", 4);
        //DressUpConfig.Add("lips_colour", );
        DressUpConfig.Add("tops", 9);
        //DressUpConfig.Add("tops_colour", );
        DressUpConfig.Add("sleeves", 3);
        //DressUpConfig.Add("sleeves_colour", );
        DressUpConfig.Add("bottoms", 2);
        //DressUpConfig.Add("bottoms_colour", );
        DressUpConfig.Add("shoes", 5);
        //DressUpConfig.Add("shoes_colour", );
        DressUpConfig.Add("purses", 7);
        //DressUpConfig.Add("purses_colour", );
        DressUpConfig.Add("jewelry", 2);
        //DressUpConfig.Add("jewelry_colour", );
        DressUpConfig.Add("more", 1);
        //DressUpConfig.Add("more_colour", );
    }
}
