using UnityEngine;
using System.Collections;
using System;
/*
public class Something
{

    public void Hello()
    {

        Vec

        Level NewLevel = new Level();

        int i;

        i = Level._MY_STATIC_INT;
        i = NewLevel._MY_STATIC_INT;
        i = NewLevel._MY_STATIC_INT2;
        i = Level._MY_STATIC_INT2;
    }
}*/



[ExecuteInEditMode]
public class Level : MonoBehaviour
{
    public static int _MY_STATIC_INT = 12;

    public int _MY_STATIC_INT2 = 13;

    [Header("Ground Block")]
    public GameObject GroundBlock;

    [Header("Block Prefabs")]
    public GameObject[] Prefabs = new GameObject[1];

    //[Header("Prefab Size")]
    [Range(1, 100), HideInInspector]
    public float PrefabSizeX = 5f;
    [Range(1, 100), HideInInspector]
    public float PrefabSizeY = 5f;

    //[Header("Grid Size")]
    [Range(1, 100), HideInInspector]
    public int PrefabCountX = 1;
    [Range(1, 100), HideInInspector]
    public int PrefabCountY = 1;

    // Use this for initialization
    void Start()
    {
        //Prefabs = new GameObject[1];
    }

    // Update is called once per frame
    void Update()
    {

    }

    void RemoveAllChildren()
    {
        while (transform.childCount > 0)
            DestroyImmediate(transform.GetChild(transform.childCount - 1).gameObject);
    }

    //private Random Rn = new Random();

    public bool UpdateLevel()
    {
        if (Prefabs.Length == 0) return false;

        RemoveAllChildren();

        int y1 = Convert.ToInt32(-Mathf.Floor(PrefabCountY / 2f));
        int y2 = Convert.ToInt32(Mathf.Ceil(PrefabCountY / 2f));
        int x1 = Convert.ToInt32(-Mathf.Floor(PrefabCountX / 2f));
        int x2 = Convert.ToInt32(Mathf.Ceil(PrefabCountX / 2f));

        string _Name = "";
        int i = 0;
        for (int y = y1; y < y2; y++)
        {
            for (int x = x1; x < x2; x++)
            {
                _Name = string.Format("Block [{0:D2},{1:D2}]", x, y);

                GameObject GO = Instantiate(GroundBlock, new Vector3((x * PrefabSizeX) + transform.position.x, transform.position.y, (y * PrefabSizeY) + transform.position.z), GroundBlock.transform.rotation) as GameObject;


                GO.transform.parent = transform;
                GO.name = _Name;

                BaseBlock BB = GO.GetComponent<BaseBlock>();
                BB.ParentLevel = this;
                BB.GroubdBlock = GroundBlock;

                GameObject Pre = Instantiate(Prefabs[i], new Vector3((x * PrefabSizeX) + transform.position.x, transform.position.y, (y * PrefabSizeY) + transform.position.z), Prefabs[i].transform.rotation) as GameObject;
                Pre.transform.parent = GO.transform;
                BB.SurfaceBlocks.Add(Pre);
                i++;
                i %= Prefabs.Length;
            }
        }

        return true;
    }
}
