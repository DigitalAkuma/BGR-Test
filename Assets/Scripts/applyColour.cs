using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class applyColour : MonoBehaviour
{

    public TextAsset textFile;

    //Left these incase you need them


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }




    // This is a string reader that I have created to make your time easier, it's in it's base stages as you will need to add things to make it work.

    string ColourPicker(TextAsset textFile) {

        //colourContent converts the text file into text, and is then split into lines using the '!' in the text file as an indicator that the text file has a new line.
        //The colourList then converts it into a list so we can compare it to the player index when they are scrolling through the colours.
        var colourContent = textFile.text;
        var lines = colourContent.Split(" !");
        colourList = new List<string>(lines);

        // colourIndex is the amount of objects within the list object (how many colours we have)
        int colourIndex = colourList.Count;


        //If the playerindex is within bounds, pick the colour, split it into 4 parts and return the list object.
        // this means when applying the colour we would put it as gameobject.colour = new colour34(colourWheel[0], colourWheel[1], colourWheel[2], colourWheel[3]

        // This is a super fucked way of doing it ngl, but this is a really good base to work with so we can apply this script anywhere we need a recolour.
        if (playerIndex == colourIndex) {

            List<String> colourWheel = colourIndex[playerIndex].ToString();
            colourWheel.split(", ");

            return colourWheel;

            
        }
        else if(playerIndex > colourList.Count){
            Debug.log("We fucked up.");
            //We can also deactivate the scroller gameobject here.

        }

    }
}
