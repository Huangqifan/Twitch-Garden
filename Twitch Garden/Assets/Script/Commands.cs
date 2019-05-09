using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Commands : MonoBehaviour
{
	public GameObject Weeds;
	public GameObject Flowers; 
	public Sprite FlowerSprite; 
	private int WeedsCount; 
	private int FlowerCount; 
	private int WaterCount; 
	private int maxi = -99; 
	private int flowermax = -99; 


    // Start is called before the first frame update
    void Start()
    {
        WeedsCount = 0; 
        FlowerCount = 0; 
        WaterCount = 0; 
    }

    // Update is called once per frame
    void Update()
    {
    	if (Input.GetKeyDown("p"))
        {
           ClearWeed(); 
        }

        if (Input.GetKeyDown("f"))
        {
        	PlantFlower(); 
        }

        if (Input.GetKeyDown("w"))
        {
        	WaterFlower(); 
        }
        
    }

    public void WaterFlower()
    {
    int i = 0;

    //Array to hold all child obj
    GameObject[] allChildren = new GameObject[Flowers.transform.childCount];

    //Find all child obj and store to that array
    foreach (Transform child in Flowers.transform)
    {
    	Debug.Log(child.gameObject); 
        allChildren[i] = child.gameObject;
        i += 1;
    }

    if (i > flowermax)
    {
    	flowermax = i; 
    }

    if (WaterCount < flowermax)
    {
    	   if( allChildren[WaterCount].active)
    	   {
    	   	Debug.Log(allChildren[WaterCount]); 
    	   	allChildren[WaterCount].GetComponent<SpriteRenderer>().sprite = FlowerSprite;
    	   	WaterCount++; 
    	   }


    }
    }


    public void PlantFlower()
    {
    int i = 0;

    //Array to hold all child obj
    GameObject[] allChildren = new GameObject[Flowers.transform.childCount];

    //Find all child obj and store to that array
    foreach (Transform child in Flowers.transform)
    {
        allChildren[i] = child.gameObject;
        i += 1;
    }

    if (i > flowermax)
    {
    	flowermax = i; 
    }

    if (FlowerCount < flowermax)
    allChildren[FlowerCount].SetActive(true);


    FlowerCount++; 

    }

    public void ClearWeed()
	{
    int i = 0;

    //Array to hold all child obj
    GameObject[] allChildren = new GameObject[Weeds.transform.childCount];

    //Find all child obj and store to that array
    foreach (Transform child in Weeds.transform)
    {
        allChildren[i] = child.gameObject;
        i += 1;
    }

    if (i > maxi)
    {
    	maxi = i; 
    }

    if (WeedsCount < maxi)
    DestroyImmediate(allChildren[0]);

    WeedsCount++; 

	}
}
