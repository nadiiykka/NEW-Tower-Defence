using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine.EventSystems;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    TowerBTN towerBTNPressed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            UnityEngine.Vector2 mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePoint, UnityEngine.Vector2.zero);

            PlaceTower(hit);
        }
    }
    public void PlaceTower(RaycastHit2D hit)
    {
        if (!EventSystem.current.IsPointerOverGameObject() && towerBTNPressed != null)
        {
            GameObject newTower = Instantiate(towerBTNPressed.TowerObject);
            newTower.transform.position = hit.transform.position;
        }
    }
    public void SelectedTower(TowerBTN towerSelected)
    {
        towerBTNPressed = towerSelected;
        Debug.Log("Pressed" + towerBTNPressed.gameObject);
    }
}
