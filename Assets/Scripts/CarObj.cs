using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarObj : MonoBehaviour
{
    public float handling;
    public float speed;
    public float acceleration;
    public GameObject prefab;
    public Sprite selectionIcon;

    public List<Color32> bodyColours = new List<Color32>();
    public List<Color32> interiorColours = new List<Color32>();
    public List<Color32> windowsColours = new List<Color32>();
    public List<Color32> wheelsColours = new List<Color32>();
    public List<Color32> lightsColours = new List<Color32>();
    public List<Color32> miscColours = new List<Color32>();

    public Material bodyMaterial;
    public Material interiorMaterial;
    public Material windowsMaterial;
    public Material wheelsMaterial;
    public Material lightsMaterial;
    public Material miscMaterial;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
