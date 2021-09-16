using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCPathFinding: CharacterMove
{
    Path path;
    NPC npc;
    Seeker seeker;
    int currentWaypoint = 0;
    Vector3 target;
    public float moveSpeed = 3;
    public float nextWaypointDistance = 0.3f;

    protected override void Awake()
    {
        base.Awake();
        seeker = GetComponent<Seeker>();
        npc = GetComponent<NPC>();
        //renderer = GetComponentInChildren<IsometricSpineRenderer>();
    }
    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 1;
            for (; currentWaypoint < path.vectorPath.Count - 1; currentWaypoint++)
            {
                float dToCurrent = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
                float dToNext = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint + 1]);
                if (dToCurrent >= dToNext)
                {
                    currentWaypoint++;
                }
                else
                {
                    Vector2 dirToCurrent = (Vector2)path.vectorPath[currentWaypoint] - rb.position;
                    Vector2 dirToNext = (Vector2)path.vectorPath[currentWaypoint + 1] - rb.position;
                    float dot = Vector2.Dot(dirToCurrent, dirToNext);
                    if (dot <= 0)
                    {
                        currentWaypoint++;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }
    public void setTarget(Transform t)
    {
        target = t.position;

        seeker.StartPath(rb.position, target, OnPathComplete);
    }

    private void FixedUpdate()
    {
        if (path == null)
        {
            animator.SetFloat("speed", 0);
            //renderer.setDirection(Vector2.zero);
            return;
        }

        if (DialogueUtils.Instance.isInDialogue)
        {
            return;
        }
        bool reachedEndOfPath;
        if (currentWaypoint >= path.vectorPath.Count)
        {
            path = null;
            reachedEndOfPath = true;
            npc.finishPath();
            animator.SetFloat("speed", 0);
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        rb.MovePosition(rb.position + direction * Time.deltaTime * moveSpeed);

        animator.SetFloat("speed", 1);
        testFlip(direction);
        //renderer.setDirection(direction);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance)
        {

            currentWaypoint++;

        }
    }
}
