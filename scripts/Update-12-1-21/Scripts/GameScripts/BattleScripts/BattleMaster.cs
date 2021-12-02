using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.AI;

public class BattleMaster
{

    private int currentTurn = 0;
    private int currentFocus = 0;
    private int outOfMoves = 0;

    private bool freeLook = false;
    private bool runOnce = false;
    private bool isGameOver = false;

    public static string ActionChosen = null;

    public enum BATTLESTATE
    {
        IDLE = 0, MOVE = 1, ATTACK = 2, MAGIC = 3,
        ITEM = 4, SPECIAL = 5
    };

    public static BATTLESTATE battleState;

    public BattleMaster()
    {
        battleState = BATTLESTATE.IDLE;
        SetUpSingleton();
        BattleCamera.updateFreeLook();
    }

    public void Controls()
    {
        if (BattleSingleton.Instance.NumberOfTeams > 1)
        {
            BattleCamera.CameraMovement();
            ChangeBattleState();
            BattleStates();
        }
        else
        {
            if (!isGameOver)
            {
                isGameOver = true;
                EndGame();
            }
        }
    }

    private void SetUpSingleton()
    {
        BattleSingleton.Instance.cam.Follow = BattleSingleton.Instance.focusOnMe.transform;
        BattleSingleton.Instance.cam.LookAt = BattleSingleton.Instance.focusOnMe.transform;
        BattleSingleton.Instance.transposer = BattleSingleton.Instance.cam.GetCinemachineComponent<CinemachineTransposer>();
        BattleSingleton.Instance.transposer.m_FollowOffset = Constants.CameraConstants.Default;
        BattleSingleton.Instance.aimCam.enabled = false;
        BattleSingleton.Instance.battleState = battleState;
    }

    private void Movement()
    {
        if (battleState == BATTLESTATE.MOVE)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (Vector3.Distance(hit.point, BattleSingleton.Instance.focusOnMe.transform.position) < BattleSingleton.Instance.focusOnMe.GetComponent<CharacterController>().myStats.speed)
                {
                    NavMeshPath path = new NavMeshPath();
                    if (NavMesh.CalculatePath(BattleSingleton.Instance.focusOnMe.transform.position, hit.point, NavMesh.AllAreas, path))
                    {

                        List<Vector3> corners = new List<Vector3>();
                        BattleSingleton.Instance.focusOnMe.GetComponent<LineRenderer>().positionCount = path.corners.Length;
                        if (path.corners.Length > 1)
                            BattleSingleton.Instance.focusOnMe.GetComponent<LineRenderer>().SetPositions(path.corners);

                        if (Input.GetMouseButtonDown(0) && path.corners.Length > 1)
                        {
                            BattleSingleton.Instance.focusOnMe.GetComponent<NavMeshAgent>().SetDestination(hit.point);
                            ResetBattleState();
                            BattleSingleton.Instance.focusOnMe.GetComponent<LineRenderer>().positionCount = 0;
                            BattleSingleton.Instance.focusOnMe.GetComponent<CharacterController>().myStats.actionsLeft -= 1;
                        }
                    }
                }
            }

        }
    }


    private void BattleStates()
    {
        if (battleState == BATTLESTATE.IDLE)
        {
            MouseSpecialActions.ShowStatCard();
        }
        if (BattleSingleton.Instance.focusOnMe.GetComponent<CharacterController>().myStats.actionsLeft > 0)
        {
            Movement();
        }
        else
        {
            BattleCamera.SwitchToFollow();
        }
        if(battleState == BATTLESTATE.ATTACK)
        {
            Attacking();
        }
        if (battleState == BATTLESTATE.MAGIC)
        {
            UseMagic();
        }
        if (battleState == BATTLESTATE.ITEM)
        {
            SelectTarget();
            ItemMenu();
        }
        BattleSingleton.Instance.battleState = battleState;
    }

    private void SelectTarget()
    {
        if (runOnce)
        {
            Targeting.SelectTarget();
            runOnce = false;
            if (Targeting.target == null)
            {
                battleState = BATTLESTATE.IDLE;
                return;
            }
            UserInterface.InfoChance();
            UserInterface.TurnOffOrOn("ChancePanel");

        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            Targeting.TargetPrevious();
            UserInterface.InfoChance();
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            Targeting.TargetNext();
            UserInterface.InfoChance();
        }
    }

    private void Attacking()
    {
        SelectTarget();
        if(Targeting.target == null) return;
        AttackMenu();
        ChooseAction(BattleMechanics.AttackType.Attack);
    }

    private void UseMagic()
    {
        SelectTarget();
        if (Targeting.target == null) return;
        ChooseAction(BattleMechanics.AttackType.Magic);
        MagicMenu();
    }

    private void ChooseAction(BattleMechanics.AttackType attackType)
    {
        string[,] ActionList = null;
        if (attackType == BattleMechanics.AttackType.Magic)
        {
            ActionList = Constants.SpellList.spells;
        }
        if (attackType == BattleMechanics.AttackType.Attack)
        {
            ActionList = Constants.ActionsList.Actions;
        }
        if (ActionChosen != null && ActionList != null)
        {
            Debug.Log(ActionChosen);
            string targetType = "";
            for (int i = 0; i < ActionList.GetLength(0); i++)
            {
                if (ActionList[i, 0] == ActionChosen)
                {
                    targetType = ActionList[i, 1];
                }
            }
            if (targetType == "Enemy")
            {
                BattleMechanics.Attack(BattleSingleton.Instance.focusOnMe, Targeting.target, attackType, ActionChosen);
            }
            ActionChosen = null;
        }
    }

    public void AttackMenu()
    {
        UserInterface.AttackPanel(BattleSingleton.Instance.focusOnMe.GetComponent<CharacterController>().myStats);
    }
    public void MagicMenu()
    {
        UserInterface.MagicPanel(BattleSingleton.Instance.focusOnMe.GetComponent<CharacterController>().myStats);
    }
    public void ItemMenu()
    {
        UserInterface.ItemsPanel(BattleSingleton.Instance.focusOnMe.GetComponent<CharacterController>().myStats);
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
            runOnce = true;
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
        BattleSingleton.Instance.aimCam.enabled = false;
        var transposer = BattleSingleton.Instance.cam.GetCinemachineComponent<CinemachineTransposer>();
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

    public void EndGame()
    {
        EndBattle.EndTheBattle();
    }
}
