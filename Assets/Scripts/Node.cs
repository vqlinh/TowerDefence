using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    BuildManager buildManager;
    private Renderer rend;
    public Color hoverColr;
    public Color notEnoughMoneyColor;
    private Color startColor;

    [HideInInspector]
    public GameObject turret;
    public TurretBlueprint turretBlueprint;
    public bool isUpgraded = false;

    public Vector3 positionOffset;

    void Start()
    {
        buildManager = BuildManager.instance;
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }

     void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            Debug.Log("IsPointerOverGameObject");
            return;
        }
        if (turret != null)
        {
            buildManager.SelectedNode(this);
            return;
        }
        if (!buildManager.CanBuild) return;
        BuildTurret(buildManager.GetTurretToBuild());
    }

    void BuildTurret(TurretBlueprint blueprint)
    {
        if (PlayerStats.Money < blueprint.cost)
        {
            Debug.Log("k du tien");
            return;
        }
        PlayerStats.Money -= blueprint.cost;
        GameObject _turret = GameObject.Instantiate(blueprint.prefabs, GetBuildPosition(), Quaternion.identity);
        turret = _turret;
        turretBlueprint = blueprint;
        GameObject effect = GameObject.Instantiate(buildManager.buildEffect,GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);
    }

    void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;
        if(!buildManager.CanBuild) return;
        if (buildManager.HasMoney) rend.material.color = hoverColr;
        else rend.material.color = notEnoughMoneyColor;

    }

    void OnMouseExit()
    {
        rend.material.color = startColor;

    }

    public void UpgradeTurret()
    {
        if (PlayerStats.Money < turretBlueprint.upgradeCost)
        {
            Debug.Log("k du tien nang cap");
            return;
        }
        PlayerStats.Money -= turretBlueprint.upgradeCost;

        Destroy(turret);

        // buid new a one
        GameObject _turret = GameObject.Instantiate(turretBlueprint.upgradePrefabs, GetBuildPosition(), Quaternion.identity);
        turret = _turret;
        GameObject effect = GameObject.Instantiate(buildManager.sellEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);
        isUpgraded = true;

        Debug.Log("turret upgrade");
    }
    public void SellTurret()
    {
        PlayerStats.Money += turretBlueprint.GetSellAmount();
        GameObject effect = GameObject.Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        Destroy(turret);    
        turretBlueprint = null;
    }
}
