using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeerSpawn : MonoBehaviour
{
    [SerializeField] int startingNumDeer = 1;
    [SerializeField] int leastNumDeer = 1;
    [SerializeField] Deer deer = null;
    int numDeer;
    //delay between spawns
    float coolDown = 3f;
    float timeSinceSpawned = Mathf.Infinity;

    // Start is called before the first frame update
    void Start()
    {
        numDeer = startingNumDeer;
    }

    // Update is called once per frame
    void Update()
    {
        if (numDeer < leastNumDeer && timeSinceSpawned > coolDown)
        {
            //MAKE DEER
            numDeer++;
            Deer deerInstance = Instantiate(deer, gameObject.transform.position, Quaternion.identity);
            timeSinceSpawned = 0;
        }
        timeSinceSpawned += Time.deltaTime;
    }

    public void deadDeer()
    {
        numDeer--;
    }
}
