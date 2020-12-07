using System.Collections;
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
      }

      int count = 1;
      // var objectTransform = cubeTest.GetComponent<Transform>();
      while(!enemies || !obstacles)
      {
        int currRan = Random.Range(0, locations.Count);
        while(currRan > 20 || !locations.Contains(currRan))
        {
          currRan = Random.Range(0, locations.Count);
        }
        GameObject instantObj = gatherAssets();

        if(count == 7)
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
      float row = Mathf.Floor(toConv / 5);
      float column = toConv - (row * 5);

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
      prob = prob - (enemyCount/10);
      if(prob < 0.0f)
      {
        prob = 0.0f;
      }

      Debug.Log(prob);
      bool finalChoice = (prob > Random.Range(-1, 100.0f));

      prevEnemy = null;
      prevObstacle = false;

      if(!finalChoice)
      {
        enemyCount = enemyCount + 1;
        return enemy;
      }
      return obstacle;
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
