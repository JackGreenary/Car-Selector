using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CustomisationManager : MonoBehaviour
{
    public bool selectCar;
    public List<GameObject> cars = new List<GameObject>();

    private Camera camera;
    private CarObj selectedCar;
    [SerializeField] private GameObject colourSlotPrefab;

    [SerializeField] private GameObject colourList;
    [SerializeField] private GameObject partList;
    [SerializeField] private GameObject carList;

    [SerializeField] private GameObject menuBtn;
    [SerializeField] private MenuLevel currentMenuLevel;

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

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
        selectCar = true;
        currentMenuLevel = MenuLevel.Parts;
    }

    // Update is called once per frame
    void Update()
    {
        if (selectCar)
        {
            ChangeMenu(false);
            selectCar = false;
        }
    }

    public void SelectCar(int carIndex)
    {
        selectedCar = cars[carIndex].GetComponent<CarObj>();
        camera.transform.position = new Vector3(selectedCar.transform.position.x, camera.transform.position.y, camera.transform.position.z);
        CarObj carObj = selectedCar.GetComponent<CarObj>();
        ChangeMenu(true);
        FindAnyObjectByType<CameraManager>().target = selectedCar.transform;
    }

    public void SelectPart(string carPart)
    {
        var myEnum = (CarPart)Enum.Parse(typeof(CarPart), carPart);

        List<Color32> colours = new List<Color32>();
        switch (myEnum)
        {
            case CarPart.Body:
                colours = selectedCar.bodyColours;
                break;
            case CarPart.Interior:
                colours = selectedCar.interiorColours;
                break;
            case CarPart.Windows:
                colours = selectedCar.windowsColours;
                break;
            case CarPart.Wheels:
                colours = selectedCar.wheelsColours;
                break;
            case CarPart.Lights:
                colours = selectedCar.lightsColours;
                break;
            case CarPart.Misc:
                colours = selectedCar.miscColours;
                break;
            default:
                break;
        }

        if (colours != null && colours.Count > 0)
        {
            // Clear existing colours
            if (colourList.transform.childCount > 0)
            {
                foreach (Transform child in colourList.transform) Destroy(child.gameObject);
            }

            // Spawn colour selections
            foreach (Color32 colour in colours)
            {
                GameObject colourSlot = Instantiate(colourSlotPrefab, colourList.transform);
                colourSlot.GetComponent<Image>().color = colour;

                colourSlot.GetComponent<ColourBtn>().part = myEnum.ToString();
            }
            ChangeMenu(true);
        }
        else
        {
            Debug.Log($"No colours found for {carPart}");
        }
    }

    public void SelectColour(string carPart, Color32 colour)
    {
        //Color32 colour = colourList.GetComponent<Image>().color;
        var myEnum = (CarPart)Enum.Parse(typeof(CarPart), carPart);

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
    }

    public void Back()
    {
        ChangeMenu(false);
    }

    private void ChangeMenu(bool isDeeper)
    {
        currentMenuLevel = isDeeper ? (MenuLevel)((int)currentMenuLevel + 1) : (MenuLevel)((int)currentMenuLevel - 1);
        Vector3 newPos = new Vector3();
        switch (currentMenuLevel)
        {
            case MenuLevel.Cars:
                newPos = carList.transform.position;
                iTween.MoveTo(carList, new Vector3(newPos.x, newPos.y + 200, newPos.z), .25f);
                newPos = partList.transform.position;
                iTween.MoveTo(partList, new Vector3(newPos.x + 1650, newPos.y, newPos.z), .25f);
                FindAnyObjectByType<CameraManager>().ResetCamera();
                break;
            case MenuLevel.Parts:
                if (isDeeper)
                {
                    newPos = carList.transform.position;
                    iTween.MoveTo(carList, new Vector3(newPos.x, newPos.y - 200, newPos.z), .25f);
                    newPos = partList.transform.position;
                    iTween.MoveTo(partList, new Vector3(newPos.x - 1650, newPos.y, newPos.z), .25f);
                }
                else
                {
                    newPos = partList.transform.position;
                    iTween.MoveTo(partList, new Vector3(newPos.x, newPos.y + 200, newPos.z), .25f);
                    newPos = colourList.transform.position;
                    iTween.MoveTo(colourList, new Vector3(newPos.x + 1650, newPos.y, newPos.z), .25f);
                }
                break;
            case MenuLevel.Colours:
                newPos = partList.transform.position;
                iTween.MoveTo(partList, new Vector3(newPos.x, newPos.y - 200, newPos.z), .25f);
                newPos = colourList.transform.position;
                iTween.MoveTo(colourList, new Vector3(newPos.x - 1650, newPos.y, newPos.z), .25f);
                break;
            default:
                break;
        }

        menuBtn.GetComponentInChildren<TextMeshProUGUI>().text = currentMenuLevel.ToString();

        if (currentMenuLevel == MenuLevel.Cars)
        {
            // Disable button if on car menu
            menuBtn.GetComponent<Button>().enabled = false;
        }
        else
        {
            menuBtn.GetComponent<Button>().enabled = true;
        }
    }
}
