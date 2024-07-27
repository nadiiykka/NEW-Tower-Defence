using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    public TowerBTN towerBTNPressed { get; set; }
    SpriteRenderer spriteRenderer;

    private static TowerManager instance;
    public static TowerManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<TowerManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject("TowerManager");
                    instance = obj.AddComponent<TowerManager>();
                }
            }
            return instance;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer is missing on TowerManager object.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            UnityEngine.Vector2 mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePoint, UnityEngine.Vector2.zero);

            if (hit.collider != null && hit.collider.CompareTag("TowerSide"))
            {
                hit.collider.tag = "TowerSideFull";
                PlaceTower(hit);
            }
        }

        if (spriteRenderer != null && spriteRenderer.enabled)
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
        if (towerBTNPressed != null)
        {
            EnabledDrag(towerBTNPressed.DragSprite);
            Debug.Log("Pressed" + towerBTNPressed.gameObject);
        }
    }

    public void FollowMouse()
    {
        if (spriteRenderer != null)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new UnityEngine.Vector2(transform.position.x, transform.position.y);
        }
    }

    public void EnabledDrag(Sprite sprite)
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = true;
            spriteRenderer.sprite = sprite;
        }
    }

    public void DisabledDrag()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = false;
        }
    }
}
