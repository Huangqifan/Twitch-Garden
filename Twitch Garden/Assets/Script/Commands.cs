using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Commands : MonoBehaviour
{
	public GameObject Weeds;
	public GameObject Flowers; 
	public Sprite FlowerSprite;
    [SerializeField] Animator animator;
    private bool waiting; 

	private int WeedsCount; 
	private int FlowerCount; 
	private int WaterCount; 
	private int maxi = -99; 
	private int flowermax = -99; 

    public int Pcount = 0; 
    public int Fcount = 0; 
    public int Wcount = 0; 
    public int Kcount = 0; 

    public int Wrequire = 5; 
    public int Frequire = 5; 
    public int Prequire = 5; 

    private float weedcounter; 
    private float weedinterval = 30.0f; 

    private float flowercounter; 
    private float flowerinterval = 30.0f; 



    // Start is called before the first frame update
    void Start()
    {
        weedcounter = weedinterval; 
        flowercounter = flowerinterval; 
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
            weedcounter = weedinterval; 
            flowercounter = flowerinterval; 

 
        }

        if (Input.GetKeyDown("f"))
        {
        	PlantFlower(); 
            weedcounter = weedinterval; 


        }

        if (Input.GetKeyDown("w"))
        {
        	WaterFlower(); 
            weedcounter = weedinterval; 
            flowercounter = flowerinterval; 

        }
        if (Input.GetKeyDown("k"))
        {
            
            TreeChop(); 
            weedcounter = weedinterval; 
            
        }
        if (weedcounter < 0.0f)
        {
            GrowGrass(); 
            weedcounter = weedinterval; 
        }
        if (weedcounter < 0.0f)
        {
            WitherFlower(); 
            weedcounter = weedinterval; 
        }
        else
        {
            weedcounter = weedcounter - Time.deltaTime;
            flowercounter = flowercounter - Time.deltaTime; 

        }
        
    }

    public void TreeChop()
    {
        Kcount ++; 
        if (Kcount > 10 && Kcount < 15)
        {
        animator.SetTrigger("Start");
        }
        if (Kcount > 15&& Kcount < 20)
        {
            animator.SetTrigger("TreeFall02");
        }
         if (Kcount > 20 && Kcount < 25)
        {
            animator.SetTrigger("Fall");
        }
        if (Kcount > 25 && Kcount < 30)
        {
            animator.SetTrigger("04");
        }
        if (Kcount > 30 && Kcount < 35)
        {
            animator.SetTrigger("05");
        }
        if (Kcount > 35 && Kcount < 40)
        {
            animator.SetTrigger("06");
        }


    }

    public void WaterFlower()
    {
    int i = 0;
    Wcount ++; 



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

        if (Wcount > Wrequire)
        {
        Wrequire = Wrequire + 5; 
        if (WaterCount < flowermax)
        {
    	   if( allChildren[WaterCount].active)
    	   {
    	   	Debug.Log(allChildren[WaterCount]); 
    	   	allChildren[WaterCount].GetComponent<SpriteRenderer>().sprite = FlowerSprite;
            //change to set trigger for animator later 
    	   	WaterCount++; 
    	   }
        }
        }
    }


    public void PlantFlower()
    {
    int i = 0;
    Fcount ++; 
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

    if (Fcount > Frequire)
    {
    Frequire = Frequire + 5; 
    if (FlowerCount < flowermax)
    {
    allChildren[FlowerCount].SetActive(true);
    }


    FlowerCount++; 

    }
}

    public void ClearWeed()
	{
    int i = 0;
    Pcount++; 

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

    if (Pcount > Prequire)
    {
    Prequire = Prequire + 5; 
    if (WeedsCount < maxi)
    DestroyImmediate(allChildren[0]);
    }

    WeedsCount++; 

	}

    public void GrowGrass()
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

    allChildren[Random.Range(0,maxi)].SetActive(true);
    print("grass grow"); 


    }

     public void WitherFlower()
    {
    int i = 0;

    //Array to hold all child obj
    GameObject[] allChildren = new GameObject[Flowers.transform.childCount];

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

    allChildren[Random.Range(0,maxi)].SetActive(false);
    print("flower wither"); 


    }

    IEnumerator wait()
    {
        waiting = true;
        yield return new WaitForSeconds(10.0f);
        animator.SetTrigger("Regen");
        waiting = false;

    }
}
