using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.AI;

public class BattleMaster
{

    private List<List<GameObject>> Combatants = new List<List<GameObject>>();
    private List<GameObject> ourTurn = new List<GameObject>();
    private GameObject focusOnMe;
    private GameObject freeLookObject;


    private CinemachineVirtualCamera cam;
    public Cinemachine3rdPersonAim aimCam;

    private int currentTurn = 0;
    private int currentFocus = 0;
    private int outOfMoves = 0;

    private bool freeLook = false;
    private bool runOnce = false;

    public static string ActionChosen = null;

    public enum BATTLESTATE
    {
        IDLE = 0, MOVE = 1, ATTACK = 2, MAGIC = 3,
        ITEM = 4, SPECIAL = 5
    };

    static BATTLESTATE battleState = BATTLESTATE.IDLE;

    public BattleMaster(CinemachineVirtualCamera cam, List<List<GameObject>> Combatants, Cinemachine3rdPersonAim aimCam)
    {
        freeLookObject = GameObject.Find("FreeLookObject");
        this.Combatants = Combatants;
        ourTurn = this.Combatants[0];
        focusOnMe = ourTurn[0];
        this.cam = cam;
        cam.Follow = focusOnMe.transform;
        cam.LookAt = focusOnMe.transform;
        var transposer = cam.GetCinemachineComponent<CinemachineTransposer>();
        transposer.m_FollowOffset = Constants.CameraConstants.Default;
        this.aimCam = aimCam;
        this.aimCam.enabled = false;
        updateFreeLook(focusOnMe);
    }

    public void Controls()
    {
        CameraMovement();
        ChangeBattleState();
        CharacterActions();
        InfoPanel();
    }


    private void InfoPanel()
    {
        if (battleState == BATTLESTATE.IDLE)
        {
            if (MouseSpecialActions.MouseListenerTag() == "PlayerCharacter" || MouseSpecialActions.MouseListenerTag() == "Team1" ||
               MouseSpecialActions.MouseListenerTag() == "Team2")
            {
                GameObject character = MouseSpecialActions.MouseListenerGameObject();

                UserInterface.ShowCharacterInfo(character.GetComponent<CharacterController>().myStats);
            }
            else
            {
                UserInterface.TurnOffOrOn("CharacterInfoPanel", false);
            }
        }
    }

    private void CharacterActions()
    {
        if (focusOnMe.GetComponent<CharacterController>().myStats.actionsLeft > 0)
        {
            Movement();
        }
        else
        {
            SwitchToFollow();
        }
        if(battleState == BATTLESTATE.ATTACK)
        {
            SelectTarget();
            AttackMenu();
            if(ActionChosen != null)
            {
                string targetType="";
                for (int i = 0; i < Constants.ActionsList.Actions.GetLength(0); i++)
                {
                    if(Constants.ActionsList.Actions[i,0] == ActionChosen)
                    {
                        targetType = Constants.ActionsList.Actions[i, 1];
                    }
                }
                if(targetType == "Enemy")
                {

                    BattleMechanics.Attack(focusOnMe, Targeting.target, BattleMechanics.AttackType.Attack, ActionChosen);
                }
                ActionChosen = null;
            }
        }

        if (battleState == BATTLESTATE.MAGIC)
        {
            SelectTarget();
            MagicMenu();
            if (ActionChosen != null)
            {
                string targetType;
                for (int i = 0; i < Constants.SpellList.spells.GetLength(0); i++)
                {
                    if (Constants.SpellList.spells[i, 0] == ActionChosen)
                    {
                        targetType = Constants.SpellList.spells[i, 1];
                    }
                }
                BattleMechanics.Attack(focusOnMe, Targeting.target, BattleMechanics.AttackType.Attack, ActionChosen);
                ActionChosen = null;
            }
        }
        if (battleState == BATTLESTATE.ITEM)
        {
            SelectTarget();
            ItemMenu();
        }


    }

    private void SelectTarget()
    {
        if (runOnce)
        {
            Targeting.SelectTarget(Combatants, focusOnMe, cam, aimCam);
            
            UserInterface.TurnOffOrOn("ChancePanel");
            UserInterface.ChanceToHitPanel(BattleMechanics.HitChance(focusOnMe, Targeting.target, BattleMechanics.AttackType.Attack));
            UserInterface.ShowCharacterInfo(Targeting.target.GetComponent<CharacterController>().myStats);
            runOnce = false;
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            Targeting.TargetPrevious();
            UserInterface.ChanceToHitPanel(BattleMechanics.HitChance(focusOnMe, Targeting.target, BattleMechanics.AttackType.Attack));
            UserInterface.ShowCharacterInfo(Targeting.target.GetComponent<CharacterController>().myStats);
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            Targeting.TargetNext();
            UserInterface.ChanceToHitPanel(BattleMechanics.HitChance(focusOnMe, Targeting.target, BattleMechanics.AttackType.Attack));
            UserInterface.ShowCharacterInfo(Targeting.target.GetComponent<CharacterController>().myStats);
        }
    }

    public static void SwitchRight()
    {

    }

    private void Movement()
    {
        if (battleState == BATTLESTATE.MOVE)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (Vector3.Distance(hit.point, focusOnMe.transform.position) < focusOnMe.GetComponent<CharacterController>().myStats.speed)
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
                            ResetBattleState();
                            focusOnMe.GetComponent<LineRenderer>().positionCount = 0;
                            focusOnMe.GetComponent<CharacterController>().myStats.actionsLeft -= 1;
                        }
                    }
                }
            }
        }
    }

    public void AttackMenu()
    {
        UserInterface.AttackPanel(focusOnMe.GetComponent<CharacterController>().myStats);
    }
    public void MagicMenu()
    {
        UserInterface.MagicPanel(focusOnMe.GetComponent<CharacterController>().myStats);
    }
    public void ItemMenu()
    {
        UserInterface.ItemsPanel(focusOnMe.GetComponent<CharacterController>().myStats);
    }

    private void ChangeBattleState()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            battleState = BATTLESTATE.MOVE;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            battleState = BATTLESTATE.ATTACK;
            UserInterface.TurnAllOff();
            runOnce = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            battleState = BATTLESTATE.MAGIC;
            UserInterface.TurnAllOff();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            battleState = BATTLESTATE.ITEM;
            UserInterface.TurnAllOff();
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            battleState = BATTLESTATE.SPECIAL;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Clear();
        }
    }

    private void Clear()
    {

        UserInterface.TurnAllOff();
        battleState = BATTLESTATE.IDLE;
        aimCam.enabled = false;
        var transposer = cam.GetCinemachineComponent<CinemachineTransposer>();
        transposer.m_FollowOffset = Constants.CameraConstants.Default;
    }
    public static void RemoteChangeBattleState(BATTLESTATE action)
    {

        battleState = action;
       
    }

    private void ResetBattleState()
    {
        battleState = BATTLESTATE.IDLE;
    }

    private void CameraMovement()
    {

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Clear();
            SwitchToFollow();
            //updateFreeLook(focusOnMe);
        }

        cam.m_Lens.FieldOfView -= Input.mouseScrollDelta.y * 2;
        if (cam.m_Lens.FieldOfView > 80)
        {
            cam.m_Lens.FieldOfView = 80;
        }
        if (cam.m_Lens.FieldOfView < 5)
        {
            cam.m_Lens.FieldOfView = 5;
        }

        if (Input.GetMouseButton(1))
        {
            SwitchToFreeLook();
            freeLookObject.transform.Rotate(0, Input.GetAxis("Mouse X") * 2, 0);
        }
        if (Input.GetKey(KeyCode.W))
        {
            SwitchToFreeLook();
            freeLookObject.transform.position += freeLookObject.transform.forward;
        }
        if (Input.GetKey(KeyCode.A))
        {
            SwitchToFreeLook();
            freeLookObject.transform.position -= freeLookObject.transform.right;
        }
        if (Input.GetKey(KeyCode.D))
        {
            SwitchToFreeLook();
            freeLookObject.transform.position += freeLookObject.transform.right;
        }
        if (Input.GetKey(KeyCode.S))
        {
            SwitchToFreeLook();
            freeLookObject.transform.position -= freeLookObject.transform.forward;
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            NextTurn();
            SwitchToFollow();
        }


    }

    public void NextTurn()
    {
        Clear();
        if (currentTurn == Combatants.Count - 1)
        {
            currentTurn = 0;
        }
        else
        {
            currentTurn++;
        }
        foreach(GameObject item in ourTurn)
        {
            item.GetComponent<CharacterController>().myStats.actionsLeft = item.GetComponent<CharacterController>().myStats.actions;
        }
        ourTurn = Combatants[currentTurn];
        SwitchToFollow();
    }
    private void updateFreeLook(GameObject copy)
    {
        freeLookObject.transform.position = copy.transform.position;
        freeLookObject.transform.rotation = copy.transform.rotation;
    }
    private void SwitchToFreeLook()
    {
        if (!freeLook)
        {
            cam.Follow = freeLookObject.transform;
            cam.LookAt = freeLookObject.transform;
            freeLook = true;
        }
    }
    private void SwitchToFollow()
    {
        Clear();
        if (outOfMoves != ourTurn.Count)
        {
            if (currentFocus == ourTurn.Count - 1)
            {
                currentFocus = 0;
            }
            else
            {
                currentFocus++;
            }
            if (ourTurn[currentFocus].GetComponent<CharacterController>().myStats.actionsLeft > 0)
            {
                focusOnMe = ourTurn[currentFocus];
                cam.Follow = focusOnMe.transform;
                cam.LookAt = focusOnMe.transform;
                updateFreeLook(focusOnMe);
                freeLook = false;
                outOfMoves = 0;
            }
            else
            {
                outOfMoves++;
                SwitchToFollow();
            }
        }
        else
        {
            outOfMoves = 0;
            foreach (GameObject character in ourTurn)
            {
                character.GetComponent<CharacterController>().myStats.actionsLeft = character.GetComponent<CharacterController>().myStats.actions;
            }
            NextTurn();
        }
    }
}
