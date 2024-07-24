using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine.EventSystems;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    TowerBTN towerBTNPressed;

    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            UnityEngine.Vector2 mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePoint, UnityEngine.Vector2.zero);

            if (hit.collider.tag == "TowerSide")
            {
                hit.collider.tag = "TowerSideFull";
                PlaceTower(hit);
            }
        }

        if (spriteRenderer.enabled)
        {
            FollowMouse();
        }
    }
    public void PlaceTower(RaycastHit2D hit)
    {
        if (!EventSystem.current.IsPointerOverGameObject() && towerBTNPressed != null)
        {
            GameObject newTower = Instantiate(towerBTNPressed.TowerObject);
            newTower.transform.position = hit.transform.position;
            DisabledDrag();

            SpriteRenderer hitSpriteRenderer = hit.collider.GetComponent<SpriteRenderer>();
            if (hitSpriteRenderer != null)
            {
                hitSpriteRenderer.enabled = false;
            }
        }
    }

    public void SelectedTower(TowerBTN towerSelected)
    {
        towerBTNPressed = towerSelected;
        EnabledDrag(towerBTNPressed.DragSprite);
        Debug.Log("Pressed" + towerBTNPressed.gameObject);
    }
    public void FollowMouse()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new UnityEngine.Vector2(transform.position.x, transform.position.y);
    }
    public void EnabledDrag(Sprite sprite)
    {
        spriteRenderer.enabled = true;
        spriteRenderer.sprite = sprite;
    }
    public void DisabledDrag()
    {
        spriteRenderer.enabled = false;
    }
}
