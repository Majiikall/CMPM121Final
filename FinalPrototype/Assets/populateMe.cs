using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class populateMe : MonoBehaviour
{
    public GameObject levelLayout;
    private List<Transform> wallLocations = new List<Transform>();
    public GameObject obstacle;
    public GameObject enemy;

    // Start is called before the first frame update
    void Start()
    {
      Component[] features;
      features = levelLayout.GetComponentsInChildren<Transform>();

      float relativeLevelPos = 0.0f;
      foreach(Transform x in features)
      {
        if(x.name != "baseLevel")
        {
          wallLocations.Add(x);
        }
        else
        {
          relativeLevelPos = x.localPosition.z;
        }
      }

      Transform floorSize = wallLocations[0];

      Vector2[,] grid = generateGrid(floorSize.localScale.x * 10, floorSize.localScale.z * 10, relativeLevelPos);

      List<int> locations = new List<int>();

      for(int i = 1; i < 26; i++)
      {
        locations.Add(i);
        Debug.Log(locations[i-1]);
      }

      int count = 1;
      // var objectTransform = cubeTest.GetComponent<Transform>();
      foreach(Vector2 x in grid)
      {
        GameObject instantObj = gatherAssets(count);

        Instantiate(instantObj, new Vector3(x.x, 0, x.y), Quaternion.identity, gameObject.GetComponent<Transform>());
        count = count + 1;
      }
    }

    private GameObject gatherAssets(int count)
    {
      if(count % 2 == 0)
      {
        return obstacle;
      }
      return enemy;
    }

    private Vector2[,] generateGrid(float x, float y, float floorZ)
    {
      Vector2[,] toRet = new Vector2[5, 5];

      Vector2 startPoint = new Vector2(floorZ + (-x / 2), floorZ + (y / 2));

      float stepX = (x / 5);
      float stepY = (y / 5);
      float currX = startPoint.x + (stepX / 2);
      float currY = startPoint.y - (stepY / 2);

      for(int i = 0; i < 5; i++)
      {
        float tempX = currX;

        for(int z = 0; z < 5; ++z)
        {
          toRet[i,z] = new Vector2(tempX, currY);
          tempX += stepX;
        }

        currY -= stepY;
      }

      return toRet;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
