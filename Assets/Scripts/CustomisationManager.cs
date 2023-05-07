using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CustomisationManager : MonoBehaviour
{
    // Variables to hold car related data
    public bool selectCar;
    public List<GameObject> cars = new List<GameObject>();
    public CarObj selectedCar;

    // Variables for UI stat elements
    public TextMeshProUGUI handlingVal;
    public Image handlingBar;
    public TextMeshProUGUI accelerationVal;
    public Image accelerationBar;
    public TextMeshProUGUI speedVal;
    public Image speedBar;

    // Variables for the main camera
    private Camera camera;

    // Variable to hold the hardcoded colour library
    private ColourLibrary colourLib;

    // Variable for CostCalculator methods
    [SerializeField] private CostCalculator costCalculator;

    // Variables for various prefabs and game objects
    [SerializeField] private GameObject colourSlotPrefab;
    [SerializeField] private GameObject colourList;
    [SerializeField] private GameObject partList;
    [SerializeField] private GameObject carList;
    [SerializeField] private GameObject menuBtn;

    // Variable for current menu status
    [SerializeField] private MenuLevel currentMenuLevel;
    [SerializeField] private GameObject moveTo;

    public enum CarPart
    {
        Body,
        Interior,
        Windows,
        Wheels,
        Lights,
        Misc
    }

    public enum MenuLevel
    {
        Cars = 1,
        Parts = 2,
        Colours = 3
    }
    
    void Start()
    {
        // Initialize variables
        colourLib = GetComponent<ColourLibrary>();
        camera = Camera.main;
        selectCar = true;
        currentMenuLevel = MenuLevel.Parts;
        ResetCarColours();
    }

    void Update()
    {
        if (selectCar)
        {
            // Change menu and deselect car
            ChangeMenu(false);
            selectCar = false;
        }
    }

    public void SelectCar(int carIndex)
    {
        // Select a car to customise
        selectedCar = cars[carIndex].GetComponent<CarObj>();
        camera.transform.position = new Vector3(selectedCar.transform.position.x, camera.transform.position.y, camera.transform.position.z);
        CarObj carObj = selectedCar.GetComponent<CarObj>();
        ChangeMenu(true);
        FindAnyObjectByType<CameraManager>().target = selectedCar.transform;

        // Set the car stats
        handlingBar.fillAmount = selectedCar.handling / 10;
        handlingVal.text = selectedCar.handling.ToString();
        accelerationBar.fillAmount = selectedCar.acceleration / 10;
        accelerationVal.text = selectedCar.acceleration.ToString();
        speedBar.fillAmount = selectedCar.speed / 10;
        speedVal.text = selectedCar.speed.ToString();
    }

    public void SelectPart(string carPart)
    {
        // Select a part to customise
        var myEnum = (CarPart)Enum.Parse(typeof(CarPart), carPart);

        List<PartColour> colourObjs = new List<PartColour>();
        colourObjs = colourLib.GetColours(myEnum);

        if (colourObjs != null && colourObjs.Count > 0)
        {
            // Clear existing colours
            if (colourList.transform.childCount > 0)
            {
                foreach (Transform child in colourList.transform) Destroy(child.gameObject);
            }

            // Spawn colour selections
            foreach (PartColour colour in colourObjs)
            {
                GameObject colourSlot = Instantiate(colourSlotPrefab, colourList.transform);
                colourSlot.GetComponent<Image>().color = colour.color;
                colourSlot.GetComponentInChildren<TextMeshProUGUI>().text = colour.price.ToString();

                colourSlot.GetComponent<ColourBtn>().part = myEnum.ToString();
            }
            ChangeMenu(true);
        }
        else
        {
            Debug.Log($"No colours found for {carPart}");
        }
    }

    public void SelectColour(string carPart, Color32 colour, float price)
    {
        // We get the part
        var myEnum = (CarPart)Enum.Parse(typeof(CarPart), carPart);

        // Set the car part model colour
        switch (myEnum)
        {
            case CarPart.Body:
                selectedCar.bodyMaterial.color = colour;
                break;
            case CarPart.Interior:
                selectedCar.interiorMaterial.color = colour;
                break;
            case CarPart.Windows:
                selectedCar.windowsMaterial.color = colour;
                break;
            case CarPart.Wheels:
                selectedCar.wheelsMaterial.color = colour;
                break;
            case CarPart.Lights:
                selectedCar.lightsMaterial.color = colour;
                break;
            case CarPart.Misc:
                selectedCar.miscMaterial.color = colour;
                break;
            default:
                break;
        }
        // Update the total cost
        costCalculator.UpdateCost(myEnum, new PartColour(colour, "", price));
    }

    public void Back()
    {
        // Go back to the previous menu
        ChangeMenu(false);
    }

    private void ChangeMenu(bool isDeeper)
    {
        // Change the menu level and adjust the UI positions accordingly
        currentMenuLevel = isDeeper ? (MenuLevel)((int)currentMenuLevel + 1) : (MenuLevel)((int)currentMenuLevel - 1);
        Vector3 newPos = new Vector3();
        switch (currentMenuLevel)
        {
            case MenuLevel.Cars:
                // Move to car list
                newPos = carList.transform.position;
                iTween.MoveTo(carList, new Vector3(newPos.x, newPos.y + 400, newPos.z), .25f);
                newPos = partList.transform.position;
                iTween.MoveTo(partList, new Vector3(newPos.x + 1650, newPos.y, newPos.z), .25f);
                FindAnyObjectByType<CameraManager>().ResetCamera();

                // When the car is deselected reset the cost value
                costCalculator.ResetCost();

                // Reset stats
                handlingBar.fillAmount = 0;
                handlingVal.text = "";
                accelerationBar.fillAmount = 0;
                accelerationVal.text = "";
                speedBar.fillAmount = 0;
                speedVal.text = "";

                ResetCarColours();
                break;
            case MenuLevel.Parts:
                // Move to part list
                if (isDeeper)
                {
                    // From the car list
                    newPos = carList.transform.position;
                    iTween.MoveTo(carList, new Vector3(newPos.x, newPos.y - 400, newPos.z), .25f);
                    newPos = partList.transform.position;
                    iTween.MoveTo(partList, new Vector3(newPos.x - 1650, newPos.y, newPos.z), .25f);
                }
                else
                {
                    // From the colour list
                    newPos = partList.transform.position;
                    iTween.MoveTo(partList, new Vector3(newPos.x, newPos.y + 200, newPos.z), .25f);
                    newPos = colourList.transform.position;
                    iTween.MoveTo(colourList, new Vector3(newPos.x + 1650, newPos.y, newPos.z), .25f);
                }
                break;
            case MenuLevel.Colours:
                // Move to colour list
                newPos = partList.transform.position;
                iTween.MoveTo(partList, new Vector3(newPos.x, newPos.y - 200, newPos.z), .25f);
                newPos = colourList.transform.position;
                iTween.MoveTo(colourList, new Vector3(moveTo.transform.position.x, newPos.y, newPos.z), .25f);
                break;
            default:
                break;
        }

        menuBtn.GetComponentInChildren<TextMeshProUGUI>().text = currentMenuLevel.ToString();

        if (currentMenuLevel == MenuLevel.Cars)
        {
            // Disable button if on car menu
            menuBtn.GetComponent<Button>().interactable = false;
        }
        else
        {
            // Otherwise show
            menuBtn.GetComponent<Button>().interactable = true;
        }
    }

    private void ResetCarColours()
    {
        // Set car colours to grey
        foreach (GameObject car in cars)
        {
            CarObj carObj = car.GetComponent<CarObj>();

            carObj.bodyMaterial.color = Color.gray;
            carObj.interiorMaterial.color = Color.gray;
            carObj.windowsMaterial.color = Color.gray;
            carObj.wheelsMaterial.color = Color.gray;
            carObj.lightsMaterial.color = Color.gray;
            carObj.miscMaterial.color = Color.gray;
        }
    }
}
