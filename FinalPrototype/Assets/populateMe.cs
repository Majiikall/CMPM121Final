﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class populateMe : MonoBehaviour
{
    public GameObject levelLayout;
    private List<Transform> wallLocations = new List<Transform>();
    public GameObject obstacle;
    public GameObject enemy;

    private bool enemies = false;
    private bool obstacles = false;

    private bool prevObstacle = false;
    private GameObject prevEnemy;
    private int enemyCount = 0;

    [SerializeField]
    [Range(5, 25)]
    public int gridSectionsPerRow;

    private int currGridSize = 0;

    // Start is called before the first frame update
    void Start()
    {
      currGridSize = gridSectionsPerRow * gridSectionsPerRow;

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

      Transform obstacleTransform = obstacle.GetComponent<Transform>();

      var newScale = obstacleTransform.localScale;

      newScale.x = ((floorSize.localScale.x * 10)/gridSectionsPerRow);
      newScale.z = ((floorSize.localScale.z * 10)/gridSectionsPerRow);

      obstacleTransform.localScale = newScale;

      Vector2[,] grid = generateGrid(floorSize.localScale.x * 10, floorSize.localScale.z * 10, relativeLevelPos);

      List<int> locations = new List<int>();

      for(int i = 1; i < currGridSize + 1; i++)
      {
        locations.Add(i);
      }

      int count = 1;
      // var objectTransform = cubeTest.GetComponent<Transform>();
      while(!enemies || !obstacles)
      {
        int currRan = Random.Range(0, locations.Count);
        while(currRan > currGridSize - gridSectionsPerRow || !locations.Contains(currRan))
        {
          currRan = Random.Range(0, locations.Count);
        }
        GameObject instantObj = gatherAssets();

        if(count == Mathf.Floor(currGridSize * 0.25f))
        {
          enemies = true;
          obstacles = true;
        }
        count = count + 1;
        Vector2 index = convertToCoord(currRan);

        prevObstacle = (instantObj == obstacle);
        if(instantObj == enemy)
        {
          prevEnemy = instantObj;
        }

        locations.Remove(currRan);
        Vector2 positionsNew = grid[(int)index.x, (int)index.y];
        Instantiate(instantObj, new Vector3(positionsNew.x, 0, positionsNew.y), Quaternion.identity, gameObject.GetComponent<Transform>());
        Debug.Log(instantObj);
      }
    }

    private Vector2 convertToCoord(int toConv)
    {
      float row = Mathf.Floor(toConv / gridSectionsPerRow);
      float column = toConv - (row * gridSectionsPerRow);

      return new Vector2((int)row, (int)column);
    }

    private GameObject gatherAssets()
    {
      float prob = 100.0f;

      if(prevObstacle == true)
      {
        prob = prob * Random.Range(0.90f, 0.95f);
      }
      if(prevEnemy == enemy)
      {
        prob = prob * Random.Range(0.00f, 0.30f);
      }

      prob = prob - (((float)enemyCount/4.5f) * 100);
      if(prob < 0.0f)
      {
        prob = 0.0f;
      }

      Debug.Log(prob);
      bool finalChoice = (prob > Random.Range(-1, 100.0f));
      Debug.Log(finalChoice);

      prevEnemy = null;
      prevObstacle = false;

      if(finalChoice == true)
      {
        enemyCount = enemyCount + 1;
        return enemy;
      }
      return obstacle;
    }

    private Vector2[,] generateGrid(float x, float y, float floorZ)
    {
      Vector2[,] toRet = new Vector2[gridSectionsPerRow, gridSectionsPerRow];

      Vector2 startPoint = new Vector2(floorZ + (-x / 2), floorZ + (y / 2));

      float stepX = (x / gridSectionsPerRow);
      float stepY = (y / gridSectionsPerRow);
      float currX = startPoint.x + (stepX / 2);
      float currY = startPoint.y - (stepY / 2);

      for(int i = 0; i < gridSectionsPerRow; i++)
      {
        float tempX = currX;

        for(int z = 0; z < gridSectionsPerRow; ++z)
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
