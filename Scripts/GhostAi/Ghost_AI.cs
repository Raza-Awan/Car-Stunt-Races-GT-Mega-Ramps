using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost_AI : MonoBehaviour
{
    public Ghost_SO ghost_SO;

    private float moveSpeed;
    public float maxMoveSpeed;
    public float acceleration;
    public float rotSpeed;

    public static bool canMove;

    public static bool ghostHasFinished;

    int targetWaypointIndex;

    // Start is called before the first frame update
    void Start()
    {
        canMove = false;
        ghostHasFinished = false;
    }

    // Update is called once per frame
    void Update()
    {
        Invoke(nameof(GhostCanMove), 3f);

        if (ghost_SO.canRace == true && canMove == true)
        {
            moveSpeed += Time.deltaTime * acceleration;
            moveSpeed = Mathf.Clamp(moveSpeed, 0, maxMoveSpeed);
            transform.position = Vector3.MoveTowards(transform.position, ghost_SO.positions[targetWaypointIndex], moveSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, ghost_SO.rotations[targetWaypointIndex], moveSpeed * Time.deltaTime);

            if (targetWaypointIndex >= ghost_SO.positions.Count - 1)
            {
                return;
            }
            else
            {
                if (transform.position == ghost_SO.positions[targetWaypointIndex])
                {
                    targetWaypointIndex++;
                }
            }

            if (targetWaypointIndex >= ghost_SO.positions.Count - 1 && ghostHasFinished == false)
            {
                ghostHasFinished = true;
            }
        }

        if (ghostHasFinished == true)
        {
            LeaderBoard.INSTANCE.finishedVehiclesList.Add(gameObject);
            ghostHasFinished = false;
            gameObject.GetComponent<Ghost_AI>().enabled = false;
        }
    }

    private void GhostCanMove()
    {
        canMove = true;
    }
}
