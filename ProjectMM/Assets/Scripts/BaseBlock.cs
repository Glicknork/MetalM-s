using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;


public class BaseBlock : MonoBehaviour
{
    public Level ParentLevel;
    public GameObject GroubdBlock;
    public List<GameObject> SurfaceBlocks = new List<GameObject>();


    [Space(20f)]
    public bool IsAnimating = false;
    public float Counter = 0f;
    public float CounterRate = 0.2f;

    [Space(20f)]
    public Vector3 Target;
    public Vector3 Orig;



    // Use this for initialization
    void Start()
    {
        //Rotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnGUI()
    {

    }

    private static Color Selected = new Color(0f, 1f, 0f, 1f);
    private static Color DeSelected = new Color(1f, 0.0f, 1f, 0.8f);

    void OnDrawGizmos()
    {
        if (Selection.activeGameObject != null && Selection.activeGameObject.Equals(gameObject))
        {
            Gizmos.color = Selected;
            Gizmos.DrawWireCube(new Vector3(transform.position.x, transform.position.y + 2.5f, transform.position.z), new Vector3(5, 5, 5));
        }
        else
        {
            Gizmos.color = DeSelected;
            Gizmos.DrawWireCube(new Vector3(transform.position.x, transform.position.y, transform.position.z), new Vector3(5, 0, 5));
        }
    }

    void FixedUpdate()
    {
        if (IsAnimating)
        {
            transform.position = Vector3.Lerp(Orig, Target, Counter);
            Counter += CounterRate;

            if (Counter >= 1.0)
            {
                transform.position = Vector3.Lerp(transform.position, Orig, 1.0f);
                IsAnimating = false;
            }
        }
    }

    public void StartCounter()
    {
        Counter = 0;
        IsAnimating = true;
        Orig = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Target = new Vector3(transform.position.x, transform.position.y - 5.0f, transform.position.z);
    }

    void OnMouseDown()
    {
        if (IsAnimating) return;
        Debug.Log("══════════════════════════════\nClick: " + gameObject.name);
        StartCounter();
    }
}
