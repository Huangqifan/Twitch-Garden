using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class water_level_controller : MonoBehaviour
{
    public List<GameObject> droplets = new List<GameObject>();
    private int current_droplet;
    private Material empty;
    private Material filled;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnEnable()
    {
        current_droplet = -1;
        foreach (Transform child in transform)
        {
            droplets.Add(child.gameObject);
        }

        empty = Resources.Load<Material>("Materials/empty");
        filled = Resources.Load<Material>("Materials/filled");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void increment()
    {
        current_droplet++;
        droplets[current_droplet].GetComponent<Renderer>().material = filled;
    }
    public void decrement()
    {
        droplets[current_droplet].GetComponent<Renderer>().material = empty;
        current_droplet--;
    }
}
