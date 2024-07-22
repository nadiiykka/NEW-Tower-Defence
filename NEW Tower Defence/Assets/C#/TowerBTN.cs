using UnityEngine;

public class TowerBTN : MonoBehaviour
{
    [SerializeField]
    GameObject towerObject;

    public GameObject TowerObject
    {
        get
        {
            
            return towerObject;
        }
    }
}
