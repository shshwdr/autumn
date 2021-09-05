using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenePositionManager : Singleton<ScenePositionManager>
{

    public Dictionary<string, Transform> positionDict = new Dictionary<string, Transform>();

    void addChild(Transform t)
    {
        foreach(Transform ct in t)
        {
            if (ct.childCount > 0)
            {
                addChild(ct);
            }
            else
            {
                positionDict[ct.name] = ct;
            }

        }
    }
    private void Awake()
    {
        addChild(transform);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
