using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using GamingTools;
using Cinemachine;

public class HomeGameMaster : MonoBehaviour
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

    private GameObject freeLookObject;
    private GameObject focusOnMe;

    // Start is called before the first frame update
    void Start()
    {
        GenerateMap();
        GameObject.Find("Ground").transform.localScale *= 10;
        GenerateNavSystem();
        BuildObjects();
        MouseSpecialActions.setUp();
        UserInterface.CollectUI();
        List<GameObject> PCs = new List<GameObject>();
        foreach (GameObject character in GameObject.FindGameObjectsWithTag("PlayerCharacter"))
        {
            PCs.Add(character);
        }
        Combatants.Add(PCs);
        SetUp();
    }

    public void SetUp()
    {
        freeLookObject = GameObject.Find("FreeLookObject");
        focusOnMe = GameObject.Find(Combatants[0][0].name);
        cam.Follow = focusOnMe.transform;
        cam.LookAt = focusOnMe.transform;
        var transposer = cam.GetCinemachineComponent<CinemachineTransposer>();
        transposer.m_FollowOffset = Constants.CameraConstants.Default;
        aimCam.enabled = false;
        updateFreeLook(focusOnMe);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }


    private void Movement()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                        NavMeshPath path = new NavMeshPath();
                        if (NavMesh.CalculatePath(focusOnMe.transform.position, hit.point, NavMesh.AllAreas, path))
                        {

                            List<Vector3> corners = new List<Vector3>();
                            focusOnMe.GetComponent<LineRenderer>().positionCount = path.corners.Length;
                            if (path.corners.Length > 1)
                                focusOnMe.GetComponent<LineRenderer>().SetPositions(path.corners);

                            if (Input.GetMouseButtonDown(0) && path.corners.Length > 1)
                            {
                                focusOnMe.GetComponent<NavMeshAgent>().SetDestination(hit.point);
                                focusOnMe.GetComponent<LineRenderer>().positionCount = 0;
                                focusOnMe.GetComponent<CharacterController>().myStats.actionsLeft -= 1;
                            }
                        }
                    
                
                }
        }
    }
    private void updateFreeLook(GameObject copy)
    {
        freeLookObject.transform.position = copy.transform.position;
        freeLookObject.transform.rotation = copy.transform.rotation;
    }
    private void GenerateMap()
    {
        mesh = new GroundMeshGenorator(mat);
        mesh.Scale(Constants.MapConstants.home_scale);
        mesh.Noise(1,1,1,1,1,1);
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
        }; 
        string[] itemList ={
            "Terrain/Tree","Terrain/Tree","Terrain/Tree"
        };
        Spawner.SpawnGroups(itemList, mesh, 2, 2, 7, 7);

        Spawner.SpawnGroups(playerList, mesh, 1, 1, 1, 1);


        mesh.square1.GetComponent<NavMeshSurface>().BuildNavMesh();
    }
}
