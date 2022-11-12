using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SmartDLL;

public class PrintingFeature : MonoBehaviour
{
    public string headerDirectory;

    //Part of a plugin intended to provide printing functionality to Unity, however testing has found that it doesn't work properly
    public SmartPrinter printer = new SmartPrinter();

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(printer);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PrintDocument(CarDetails[] carModels)
    {
        //Initialise a blank string used to store all of the information for each model
        string printedInformation = "";

        //Loop through each model within the array.
        for(int i = 0; i < carModels.Length; i++)
        {
            //Only used for the second loop onwards
            //Sets a newline for the sake of keeping information clean and separate
            if(printedInformation != "")
            {
                printedInformation += "\n";
            }

            //Set the name, release date, and number of colours in the info to their respective values
            printedInformation += "Name: " + carModels[i].modelName + "\n";
            printedInformation += "Model Release: " + carModels[i].modelReleaseDate + "\n";
            printedInformation += "Colours Available: " + carModels[i].GetCarColourNumber() + "\n";
            //Created the field for specs, then create a new line
            printedInformation += "Specs: " + "\n";

            //Separate the specs string into an array using the split method
            string[] SplitSpecsList = carModels[i].specs.Split(", ");

            //Loop through this secondary array, add each spec as a line in the information.
            for(int j = 0; j < SplitSpecsList.Length; j++)
            {
                printedInformation += SplitSpecsList[j] + "\n";
            }

            //Add the cost of the car to the information (Both normal and metallic finish costs)
            printedInformation += "£" + carModels[i].modelCost + "(£" + (carModels[i].modelCost + carModels[i].metallicCost) + " for metallic finish)" + "\n";

        }

        //Print to the console
        Debug.Log(printedInformation);
        //printer.PrintDocument("AAAAAA", @headerDirectory);
    }
}
