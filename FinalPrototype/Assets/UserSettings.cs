using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserSettings : MonoBehaviour
{
  [SerializeField]
  private int gridSectionsPerRow;

  public int returnGridSize()
  {
    return gridSectionsPerRow;
  }
}
