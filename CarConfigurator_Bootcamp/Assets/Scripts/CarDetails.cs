using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDetails : MonoBehaviour
{
    public string modelName;
    public string modelReleaseDate;

    public float modelCost;
    public float metallicCost;

    public string specs;
    [SerializeField] Color[] modelColours;

    [SerializeField] GameObject carBody;

    bool isMetallicFinish;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.O)){
            SetCarColour(1);
        }
    }

    public void SetCarColour(int selectedColourIndex)
    {
        carBody.GetComponent<Renderer>().material.SetColor("_Color", modelColours[selectedColourIndex]);
    }

    public void SetCarMetallic(bool isMetallic)
    {
        carBody.GetComponent<Renderer>().material.SetFloat("_Metallic", isMetallic ? 1f : 0f);
        isMetallicFinish = isMetallic;
    }

    public void GetCarMetallic()
    {
        carBody.GetComponent<Renderer>().material.GetFloat("_Metallic");
    }

    public int GetCarColourNumber()
    {
        return this.modelColours.Length;
    }

    public Color GetCarColour(int colourIndex)
    {
        return modelColours[colourIndex];
    }

    public float CalculateCarCost()
    {
        return modelCost + (isMetallicFinish ? metallicCost : 0f);
    }

    ///To Do:
    /// - Potentially add ability to add accessories to car (Such as spoiler, more exhaust, etc.)
    /// AAAA
}
