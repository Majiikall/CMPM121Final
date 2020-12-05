using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class populateMe : MonoBehaviour
{
    public GameObject levelLayout;
    private List<Transform> wallLocations = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {
      Component[] features;
      features = levelLayout.GetComponentsInChildren<Transform>();


      foreach(Transform x in features)
      {
        if(x.name != "baseLevel")
        {
          wallLocations.Add(x);
        }
      }

      Transform floorSize = wallLocations[0];

      Vector2[,] grid = generateGrid(floorSize.localScale.x * 10, floorSize.localScale.z * 10);

      foreach(Vector2 x in grid)
      {
        Debug.Log(x);
      }
    }

    private Vector2[,] generateGrid(float x, float y)
    {
      Vector2[,] toRet = new Vector2[5, 5];

      for(int i = 0; i < 5; i++)
      {
        for(int z = 0; z < 5; ++z)
        {
          Debug.Log("Here");
          toRet[i,z] = new Vector2(1.0f, 1.0f);
          Debug.Log(toRet[i, z]);
        }
      }

      return toRet;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
