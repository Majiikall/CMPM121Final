using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class controlFlow : MonoBehaviour
{
    [SerializeField]
    [Range(5, 25)]
    public int gridSectionsPerRow;

    public GameObject level;

    // Start is called before the first frame update
    void Start()
    {
      level.GetComponent<populateMe>().populateLevel(gridSectionsPerRow);

      List<GameObject> currentObjs = level.GetComponent<populateMe>().returnSceneObjs();

      Component[] levelLayout;
      levelLayout = level.GetComponentsInChildren<Transform>();

      // Transform baseTransform = levelLayout[0].GetComponent<Transform>();
      // var newPos = baseTransform.localPosition;
      // newPos.z += 140;

      GameObject nextLevel = Instantiate(level, new Vector3(0, 0, 140), Quaternion.identity, gameObject.GetComponent<Transform>());

      // Transform levelLayout2 = nextLevel.GetComponent<Transform>();
      // levelLayout2.localPosition = newPos;

      nextLevel.GetComponent<populateMe>().populateLevel(gridSectionsPerRow + 5);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
