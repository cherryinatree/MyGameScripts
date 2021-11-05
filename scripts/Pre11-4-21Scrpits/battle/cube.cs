using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public class cube : MonoBehaviour
{
    /***************************************************************
     * 
     * 
     * All the cool related variables are here at the top
     * 
     * *************************************************************/
     
    protected Color yellow = Color.yellow;
    protected Renderer rend1;
    protected Renderer rend;
    protected Texture shade;
    protected Material selected;
    protected Material grass;
    protected Material blue;
    protected Material red;
    protected Material purple;
    

    public bool first = true;
    public bool selectedCube = false;
    public bool movePotential = false;
    public bool attackCube = false;

   // public string phase = "normal";
    public enum CUBEPHASE { NORMAL = 0, MOVE = 1, ATTACK = 2, MAGIC = 3 };
    public CUBEPHASE myPhase = CUBEPHASE.NORMAL;

    public static GameObject current_cube;
    public static List<GameObject> allTheCubes = new List<GameObject>();
    public static List<GameObject> ActionCubes = new List<GameObject>();

    private goodGuy thegoodGuys;


    /***************************************************************
     * 
     *  allTheCubes is to reset variables for all cubes
     *  
     * 
     * 
     * *************************************************************/

    void Awake()
    {
        allTheCubes.Add(gameObject);
        selected = (Material)Resources.Load("Materials/Selected", typeof(Material));
        grass = (Material)Resources.Load("Materials/GrassHillAlbedo", typeof(Material));
        blue = (Material)Resources.Load("Materials/blue", typeof(Material));
        red = (Material)Resources.Load("Materials/red", typeof(Material));
        purple = (Material)Resources.Load("Materials/purple", typeof(Material));

    }
    

    public void CubeColor()
    {
        if (myPhase == CUBEPHASE.MOVE)
        {
            if (!selectedCube)
            {
                rend = gameObject.GetComponent<Renderer>();
                rend.material = blue;
            }
            else
            {

                rend = gameObject.GetComponent<Renderer>();
                rend.material = selected;
            }

        }
        else if (myPhase == CUBEPHASE.ATTACK)
        {
            if (!selectedCube)
            {
                rend = gameObject.GetComponent<Renderer>();
                rend.material = red;
            }
            else
            {
                rend = gameObject.GetComponent<Renderer>();
                rend.material = selected;
            }
        }
        else if (myPhase == CUBEPHASE.MAGIC)
        {
            if (!selectedCube)
            {
                rend = gameObject.GetComponent<Renderer>();
                rend.material = purple;
            }
            else
            {
                rend = gameObject.GetComponent<Renderer>();
                rend.material = selected;
            }
        }
        else if (selectedCube)
        {
            rend = gameObject.GetComponent<Renderer>();
            rend.material = selected;
        }
        else
        {

            rend = gameObject.GetComponent<Renderer>();
            rend.material = grass;
        }
        
        if(myPhase != CUBEPHASE.NORMAL && !ActionCubes.Contains(gameObject))
        {
            ActionCubes.Add(gameObject);
        }
    }
 
}
