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

    public int Pcount = 0; 
    public int Fcount = 0; 
    public int Wcount = 0; 
    public int Kcount = 0; 

    public int Wrequire = 0; 
    public int Frequire = 0; 
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
    
    void find_next_plant_index()
    {
        num_to_plant = 0;
        List<int> ids = new List<int>();
        foreach (var f in Flowers_collection)
        {
            ids.Add(f.getId());
        }
        ids.Sort();
        foreach (var i in ids)
        {
            if (i == num_to_plant + 1)
                num_to_plant++;
            else
                break;
        }
        Debug.Log(num_to_plant);
    }
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
    }

    // Update is called once per frame
    void Update()
    {
        List<flower> new_collection = new List<flower>();
        foreach (var f in Flowers_collection)
        {
            if (f.to_destroy)
            {
                flower.Destroy(f);
                
            }
            else
            {
                new_collection.Add(f);
                f.update();
            }
        }
        Flowers_collection = new_collection;
        

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
        if(Input.GetKeyDown("t"))
        {
            UnWaterFlower(flower_index);
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
        
        foreach (var f in Flowers_collection)
        {
            if (f.getId() == flower_ind +1)
            {
                if (f.getState() == state.PLANTED)
                {
                    f.setTime(0.0f);
                    f.increment_water_level();
                }
            }
        }
        
    }


    public void UnWaterFlower(int flower_ind)
    {
        foreach (var f in Flowers_collection)
        {
            if (f.getId() == flower_ind + 1)
            {
                if (f.getState() != state.SEED)
                {
                    if (f.get_water_level() > 0)
                    {
                        f.setTime(0.0f);
                        f.decrement_water_level();
                    }
                }
            }
        }
    }


    public void PlantFlower()
    {
        find_next_plant_index();
        flower f = (flower)flower.CreateInstance("flower");
        Flowers_collection.Add(f);
        f.setId(num_to_plant);
        f.setState(state.PLANTED);
            
            
        
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
        if (weed_index < 5)
        {
            allChildren[weed_index++].SetActive(true);
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