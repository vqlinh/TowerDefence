using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public GameObject missleLauncherPrefabs;
    public GameObject standardTurretPrefabs;
    public GameObject laserTurretPrefabs;
    private TurretBlueprint turretToBuild;
    public GameObject buildEffect;
    public GameObject sellEffect;
    private Node selectedNode;
    public NodeUi nodeUi;

    public static BuildManager instance;
    private void Awake()
    {
        instance = this;
    }

    public bool CanBuild { get { return turretToBuild != null; } } // tr? v? true n?u k null
    public bool HasMoney { get { return PlayerStats.Money >= turretToBuild.cost; } } 

    public void BuildTurretOn(Node node)
    {
  

    }

    public void SelectTurretToBuild(TurretBlueprint turret)
    {
        turretToBuild = turret;
        DeselectNode();
    }   

    public void SelectedNode(Node node)
    {
        if (selectedNode==node)
        {
            DeselectNode();
            return;
        }
        selectedNode=node;
        turretToBuild=null;
        nodeUi.SetTarget(node);

    }
    public void DeselectNode()
    {
        selectedNode=null;
        nodeUi.Hide();
    }

    public TurretBlueprint GetTurretToBuild()
    {
        return turretToBuild;
    }

}
