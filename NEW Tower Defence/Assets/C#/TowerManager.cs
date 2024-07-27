using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    public TowerBTN towerBTNPressed { get; set; }
    SpriteRenderer spriteRenderer;
    private List<TowerControl> TowerList = new List<TowerControl>();
    private List<Collider2D> BuildList = new List<Collider2D>();
    private Collider2D buildTile;

    private static TowerManager instance;
    public static TowerManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogError("TowerManager instance not found!");
            }
            return instance;
        }
    }

    private List<GameObject> buildSites = new List<GameObject>();

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer is missing on TowerManager object.");
        }
    }

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        buildTile = GetComponent<Collider2D>();
        spriteRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePoint, Vector2.zero);

            if (hit.collider != null && hit.collider.CompareTag("TowerSide"))
            {
                buildTile = hit.collider;
                buildTile.tag = "TowerSideFull";
                RegisterBuildSite(buildTile);
                buildSites.Add(hit.collider.gameObject);
                PlaceTower(hit);
            }
        }

        if (spriteRenderer != null && spriteRenderer.enabled)
        {
            FollowMouse();
        }
    }

    public void RegisterBuildSite(Collider2D buildTag)
    {
        BuildList.Add(buildTag);
    }

    private void RegisterTower(TowerControl tower)
    {
        TowerList.Add(tower);
    }

    public void RenameTagBuildSite()
    {
        foreach (Collider2D buildTag in BuildList)
        {
            buildTag.tag = "TowerSide";
            
            // Включаємо SpriteRenderer
            SpriteRenderer spriteRenderer = buildTag.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.enabled = true;
            }
        }
        BuildList.Clear();
    }

    public void DestroyAllTowers()
    {
        foreach (TowerControl tower in TowerList)
        {
            Destroy(tower.gameObject);
        }
        TowerList.Clear();
    }

    public void PlaceTower(RaycastHit2D hit)
    {
        if (!EventSystem.current.IsPointerOverGameObject() && towerBTNPressed != null)
        {
            TowerControl newTower = Instantiate(towerBTNPressed.TowerObject);
            newTower.transform.position = hit.transform.position;
            BuyTower(towerBTNPressed.TowerPrice);
            RegisterTower(newTower);
            DisabledDrag();

            SpriteRenderer hitSpriteRenderer = hit.collider.GetComponent<SpriteRenderer>();
            if (hitSpriteRenderer != null)
            {
                hitSpriteRenderer.enabled = false;
            }
        }
    }

    public void BuyTower(int price)
    {
        Manager.Instance.substractMoney(price);
    }

    public void SelectedTower(TowerBTN towerSelected)
    {
        if (towerSelected.TowerPrice <= Manager.Instance.TotalMoney)
        {
            towerBTNPressed = towerSelected;
            if (towerBTNPressed != null)
            {
                EnabledDrag(towerBTNPressed.DragSprite);
            }
        }
    }

    public void FollowMouse()
    {
        if (spriteRenderer != null)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector2(mousePosition.x, mousePosition.y);
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
