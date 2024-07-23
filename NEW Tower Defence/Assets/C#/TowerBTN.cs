using UnityEngine;

public class TowerBTN : MonoBehaviour
{
    [SerializeField]
    GameObject towerObject;
    [SerializeField]
    Sprite dragSprite;

    public GameObject TowerObject
    {
        get
        {

            return towerObject;
        }
    }
    public Sprite DragSprite
    {
        get
        {

            return dragSprite;
        }
    }
}
