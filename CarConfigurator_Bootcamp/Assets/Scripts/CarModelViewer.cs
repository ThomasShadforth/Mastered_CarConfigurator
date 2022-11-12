using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;

public class CarModelViewer : MonoBehaviour
{
    //Gets the floor on which the model sits. Used for rotation
    public GameObject modelFloor;
    //The model that is currently displayed. Used as a means of directly accesing the car details attached to it
    public CarDetails model;

    //The point on which the car model is set when it is instantiated (Ensures they all appear in the same position when switching between them)
    [SerializeField] Transform modelOrigin;

    //The rate and speed at which the model floor is rotated when pressing the rotation buttons
    public Vector3 viewRot;
    public int rotSpeed;

    //The modifier (Or direction) in which rotation is taking place
    public int rotModifier;
    //Used to check whether or not the rotation buttons are being held down
    bool pointerDown;

    //stores a list of all available models
    public CarDetails[] models;

    //Stores the index for the currently selected model (Used for when the user switches the model they want to view)
    public int selectedModel;

    [SerializeField] GameObject[] colourButtons;

    //UI Fields for all of the model's details
    [SerializeField] TextMeshProUGUI modelNameText;
    [SerializeField] TextMeshProUGUI modelCostText;
    [SerializeField] TextMeshProUGUI modelReleaseText;
    [SerializeField] TextMeshProUGUI modelSpecText;
    //Used to toggle whether the car's material is set to metallic or not
    [SerializeField] Toggle metallicToggle;
    //Stores the printing feature
    [SerializeField] PrintingFeature printingComponent;
    // Start is called before the first frame update
    void Start()
    {
        //Set the colour buttons and the model details for the initial model on display
        SetColourButtons(true);
        SetModelDetail();
    }

    private void FixedUpdate()
    {
        
    }

    

    // Update is called once per frame
    void Update()
    {
        if (pointerDown)
        {
            RotateModel(rotModifier);
        }
    }

    public void DisableRotate()
    {
        //Triggered when the longpressrelease event is triggered (Releasing the button, or moving the mouse cursor off of it)
        //Sets pointerDown to false, disabling rotation
        pointerDown = false;
    }

    public void SetRotate(int rotationModifier)
    {
        //Method that is executed by the unity event in the ButtonLongPress script (Specifically when a long press is triggered)
        //Set pointer down to true, and sets the modifier to the respective directional button being pressed
        pointerDown = true;
        rotModifier = rotationModifier;
    }

    public void RotateModel(int rotationModifier)
    {
        //While the button is being held down, rotate the model floor which; in turn, rotates the model
        modelFloor.transform.Rotate(viewRot * (rotSpeed * rotationModifier) * Time.deltaTime);
        
    }

    public void SetColourButtons(bool isLoading)
    {
        if (isLoading)
        {
            for (int i = 0; i < model.GetCarColourNumber(); i++)
            {
                colourButtons[i].SetActive(true);
                colourButtons[i].GetComponent<Button>().image.color = model.GetCarColour(i);
            }
        }
        else
        {
            for(int i = 0; i < colourButtons.Length; i++)
            {
                colourButtons[i].SetActive(false);
            }
        }
    }

    public void ChangeModelColour(int modelColourIndex)
    {
        //Call the model's set colour method
        model.SetCarColour(modelColourIndex);
    }

    public void SetCarModel(int indexChange)
    {
        //Change the index for the selected model
        selectedModel += indexChange;

        
        //If it is greater than the length - 1 or less than the minimum, set to the opposite (Prevents out of bounds exceptions)
        if(selectedModel > models.Length - 1)
        {
            selectedModel = 0;
        }

        if(selectedModel < 0)
        {
            selectedModel = models.Length - 1;
        }

        //Deactivate the buttons

        SetColourButtons(false);

        //Destroy the model (Rely on this for now, create an animation laterRRR
        Destroy(model.gameObject);

        //Instantiate the model, set the default colour, activate the buttons
        model = Instantiate(models[selectedModel], modelOrigin.position, modelFloor.transform.rotation);
        model.transform.parent = modelOrigin;
        model.SetCarColour(0);
        SetColourButtons(true);
        metallicToggle.isOn = false;
        SetModelDetail();
    }

    public void SetCarMetallic()
    {
        //Gets the current status of the toggle, passes it as a boolean parameter into the set car metallic method in the car details script
        model.SetCarMetallic(metallicToggle.isOn);
        //Adjusts the details for the model to accommodate for the change in price (Due to metallic material)
        SetModelDetail();
    }

    public void PrintButtonPressed()
    {
        printingComponent.PrintDocument(models);
    }

    void SetModelDetail()
    {
        //Set each of the detail fields to their respective values
        modelNameText.text = model.modelName;
        modelCostText.text = "£" + model.CalculateCarCost();
        modelReleaseText.text = model.modelReleaseDate;

        //Split the specs written to be displayed in a listed format using newline
        modelSpecText.text = "";

        string[] specSplit;

        specSplit = model.specs.Split(", ");

        Debug.Log(specSplit.Length);

        for(int i = 0; i < specSplit.Length; i++)
        {
            modelSpecText.text += specSplit[i] + "\n";
        }

        //modelSpecText.text = model.specs;
    }
}
