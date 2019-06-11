using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Commands : MonoBehaviour
{
	public GameObject Weeds;
    public List<flower> Flowers_collection = new List <flower>();
    public Sprite FlowerSprite;
    [SerializeField] Animator tree_animator;
    [SerializeField] Animator mushroom_animator;
    [SerializeField] GameObject timer;
    private bool waiting; 

	private int WeedsCount; 
	private int FlowerCount; 
	private int WaterCount; 
	private int maxi = -99; 
	private int flowermax = -99; 

    private int Pcount; 
    private int Fcount; 
    private int Wcount; 
    private int Kcount; 

    public int Wrequire; 
    private int Frequire; 
    public int Prequire = 0; 

    private float weedcounter; 
   [SerializeField] float weedinterval = 10.0f; 

    private float flowercounter; 
    private float flowerinterval = 10.0f;
    private float flower_timer;
    private GameObject water_timer_obj;
    private int flower_index;
    private int num_to_plant;
    private int weed_index;
    private int num_seeds;
    private bool isDay;
    public bool has_watered;

    public GameObject bucket;
    public GameObject wolves_sound;

    // Start is called before the first frame update
    void Start()
    {
        weedcounter = weedinterval;
        weed_index = 0;
        flowercounter = flowerinterval; 
        WeedsCount = 0; 
        FlowerCount = 0; 
        WaterCount = 0;
        flower_timer = -1.0f;
        num_to_plant = 0;
        has_watered = false;
        num_seeds = 15;
        Fcount = 0;
        Wcount = 0;
        Kcount = 0;
        Frequire = 5;
        bucket = GameObject.Find("water_bucket");
        wolves_sound = (GameObject) Instantiate(Resources.Load("wolf"));
    }

    // Update is called once per frame
    void Update()
    {
        isDay = GameObject.Find("Scriptholder").GetComponent<DayNight>().IsDay;

        if (Input.GetKeyDown("p"))
        {
            ClearWeed();
            weedcounter = weedinterval; 
            //flowercounter = flowerinterval; 
        }

        if (Input.GetKeyDown("1"))
        {
            flower_index = 0;
        }

        if (Input.GetKeyDown("2"))
        {
                flower_index = 1;
        }

        if (Input.GetKeyDown("3"))
        { 
                flower_index = 2;
        }

        if (Input.GetKeyDown("4"))
        {
                flower_index = 3;
        }

        if (Input.GetKeyDown("5"))
        {
                flower_index = 4;
        }
        if (Input.GetKeyDown("6"))
        {
            flower_index = 5;
        }
        if (Input.GetKeyDown("7"))
        {
            flower_index = 6;
        }
        if (Input.GetKeyDown("8"))
        {
            flower_index = 7;
        }
        if (Input.GetKeyDown("9"))
        {
            flower_index = 8;
        }

        if (Input.GetKeyDown("f"))
        {
        	PlantFlower(); 
            weedcounter = weedinterval; 
        }

        if (Input.GetKeyDown("w"))
        {
        	WaterFlower(flower_index); 
            //weedcounter = weedinterval; 
            //flowercounter = flowerinterval; 
        }
        if (Input.GetKeyDown("k"))
        {
            
            TreeChop(); 
            weedcounter = weedinterval; 
            
        }
        if (Input.GetKeyDown("m"))
        {
            touchMushroom();
        }
        if (Input.GetKeyDown("b"))
        {
            if (!isDay)
                wolves_sound.GetComponent<AudioSource>().Play();
        }
        if (weedcounter < 0.0f)
        {
            GrowGrass(); 
            weedcounter = weedinterval; 
        }
        if (weedcounter < 0.0f)
        {
            //WitherFlower(); 
            //weedcounter = weedinterval; 
        }
        else
        {
            weedcounter = weedcounter - Time.deltaTime;
            //flowercounter = flowercounter - Time.deltaTime; 

        }
        
    }
    public void touchMushroom()
    {
        mushroom_animator.SetTrigger("touch");
    }
    public void TreeChop()
    {
        Kcount ++;
        Debug.Log("where we choppin boys");
        if (Kcount > 2 && Kcount < 5)
        {
        tree_animator.SetTrigger("Start");
        }
        if (Kcount > 5&& Kcount < 7)
        {
            tree_animator.SetTrigger("Treefall02");
        }
         if (Kcount > 7 && Kcount < 10)
        {
            tree_animator.SetTrigger("Fall");
        }
        if (Kcount > 25 && Kcount < 30)
        {
            tree_animator.SetTrigger("04");
        }
        if (Kcount > 30 && Kcount < 35)
        {
            tree_animator.SetTrigger("05");
        }
        if (Kcount > 35 && Kcount < 40)
        {
            tree_animator.SetTrigger("06");
        }


    }


    public void WaterFlower(int flower_ind)
    {
        Wcount++;
        if (isDay)
        {
            if (Wcount > Wrequire)
            {
                bucket.GetComponent<Animator>().SetTrigger("start_water");
                has_watered = true;
                Debug.Log("will water tonight!");
                Wcount = 0;

            }
        }
        else
        {
            Wcount = 0;
        }
        
    }




    public void PlantFlower()
    {
        Fcount++;
        Debug.Log("frequire is:" + Frequire);
        Debug.Log("fcount is:" + Fcount);

        if (num_seeds > 0)
        {
            if (Fcount > Frequire)
            {
                num_seeds--;
                Fcount = 0;
                flower f = (flower)flower.CreateInstance("flower");
                Flowers_collection.Add(f);
                Debug.Log("planted!");
                f.incState();
            }
        }
        else
        {
            Fcount = 0;
        }
        
       
    }

    public void ClearWeed()
	{

        int i = 0;
        Pcount++;
        WeedsCount++;
        //Array to hold all child obj
        GameObject[] allChildren = new GameObject[Weeds.transform.childCount];

        //Find all child obj and store to that array
        foreach (Transform child in Weeds.transform)
        {
            if (child.gameObject.activeSelf)
            {
                allChildren[i] = child.gameObject;
                i += 1;
            }
        }
        
        if (Pcount > Prequire)
        {
            Prequire++;
            
            
                DestroyImmediate(allChildren[0]);
                
            
        }

        

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
        if (weed_index < 15)
        {
            allChildren[weed_index].SetActive(true);
            allChildren[weed_index++].gameObject.GetComponent<Animator>().SetTrigger("grow");
        }
        


    }

    /*public void WitherFlower()
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

        allChildren[Random.Range(0, maxi)].SetActive(false);
        print("flower wither");


    }*/

    IEnumerator wait()
    {
        waiting = true;
        yield return new WaitForSeconds(10.0f);
        tree_animator.SetTrigger("Regen");
        waiting = false;

    }
}