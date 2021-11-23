using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using GamingTools;
using Cinemachine;

public class GameMaster : MonoBehaviour
{
    public Material mat;
    private GroundMeshGenorator mesh;

    public int mapX = 7;

    private List<List<GameObject>> Combatants = new List<List<GameObject>>();
    public CinemachineVirtualCamera cam;
    public Cinemachine3rdPersonAim aimCam;
    private BattleMaster battle;
    private int teams = 2;
    private SaveFile save;

    // Start is called before the first frame update
    void Start()
    {
        GenerateMap();
        GenerateNavSystem();
        BuildObjects();
        SetUpBattleMaster();
        MouseSpecialActions.setUp();
        UserInterface.CollectUI();
    }

    // Update is called once per frame
    void Update()
    {
        battle.Controls();
    }


    private void SetUpBattleMaster()
    {
        List<GameObject> PCs = new List<GameObject>();
        foreach(GameObject character in GameObject.FindGameObjectsWithTag("PlayerCharacter"))
        {
            PCs.Add(character);
        }
        Combatants.Add(PCs);
        for (int i = 1; i < teams; i++)
        {
            List<GameObject> otherTeam = new List<GameObject>();
            foreach (GameObject character in GameObject.FindGameObjectsWithTag("Team" + i.ToString()))
            {
                otherTeam.Add(character);
            }
            Combatants.Add(otherTeam);
        }

        battle = new BattleMaster(cam, Combatants, aimCam);

    }

    private void GenerateMap()
    {
        mesh = new GroundMeshGenorator(mat);
        mesh.Scale(Constants.MapConstants.map2_scale);
        mesh.GenerateMesh();
    }

    private void GenerateNavSystem()
    {
        AddingNav addNav = new AddingNav();
        addNav.addNavSurface(mesh.square1);
    }

    private void BuildObjects()
    {
        string[] playerList ={
            "PCs/Player",
            "PCs/Player",
            "PCs/Player"
        }; string[] enemyList ={
            "Enemies/Enemy",
            "Enemies/Enemy",
            "Enemies/Enemy"
        };
        string[] itemList ={
            "Terrain/Tree","Terrain/Tree","Terrain/Tree"
        };
                Spawner.SpawnGroups(itemList, mesh, 2, 2, 7, 7);
            
        Spawner.SpawnGroups(playerList, mesh, 1, 1, 1, 1);
        Spawner.SpawnGroups(enemyList, mesh, 9, 9, 1, 1);


        mesh.square1.GetComponent<NavMeshSurface>().BuildNavMesh();
    }

}
