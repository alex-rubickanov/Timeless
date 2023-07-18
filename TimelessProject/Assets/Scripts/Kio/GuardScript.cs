using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardScript : MonoBehaviour
{
    public static event System.Action OnGuardHasSpottedPlayer;

    [SerializeField] Transform pathHolder;

    [SerializeField] float guardSpeed = 5;
    [SerializeField] float guardPause = 0.3f;
    [SerializeField] float guardTurnTime = 90;
    [SerializeField] float timeToSpotPlayer = 0.5f;
    
    [SerializeField] Light spotlight;
    [SerializeField] float viewDistance;
    [SerializeField] LayerMask viewMask;
    float viewAngle;
    Color originalLightColor;

    Transform player;
    float playerVisibleTimer;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        viewAngle = spotlight.spotAngle;
        originalLightColor = spotlight.color;

        Vector3[] waypoints = new Vector3[pathHolder.childCount];

        for(int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i] = pathHolder.GetChild(i).position;
            waypoints[i] = new Vector3(waypoints[i].x, transform.position.y, waypoints[i].z);
        }

        StartCoroutine(FollowPath(waypoints));
    }

    private void Update()
    {
        if (CanSeePlayer())
        {
            playerVisibleTimer += Time.deltaTime;
        }
        else
        {
            playerVisibleTimer -= Time.deltaTime;
        }

        playerVisibleTimer = Mathf.Clamp(playerVisibleTimer, 0, timeToSpotPlayer);
        spotlight.color = Color.Lerp(originalLightColor, Color.red, playerVisibleTimer / timeToSpotPlayer);

        if(playerVisibleTimer >= timeToSpotPlayer)
        {
            if(OnGuardHasSpottedPlayer != null)
            {
                OnGuardHasSpottedPlayer();
            }
        }
    }

    bool CanSeePlayer()
    {
        if(Vector3.Distance(transform.position, player.position) < viewDistance)
        {
            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            float angleBetweenGuardAndPlayer = Vector3.Angle(transform.forward, directionToPlayer);

            if(angleBetweenGuardAndPlayer < viewAngle / 2f)
            {
                if(!Physics.Linecast(transform.position, player.position, viewMask))
                {
                    return true;
                }
            }
        }
        return false;
    }

    IEnumerator FollowPath(Vector3[] waypoints)
    {
        transform.position = waypoints[0];

        int targetWaypointIndex = 1;
        Vector3 targetWaypoint = waypoints[targetWaypointIndex];
        transform.LookAt(targetWaypoint);

        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetWaypoint, guardSpeed * Time.deltaTime);

            if(transform.position == targetWaypoint)
            {
                targetWaypointIndex = (targetWaypointIndex + 1) % waypoints.Length;
                targetWaypoint = waypoints[targetWaypointIndex];
                yield return new WaitForSeconds(guardPause);
                yield return StartCoroutine(GuardTurn(targetWaypoint));
            }
            yield return null;
        }
    }

    IEnumerator GuardTurn(Vector3 lookTarget)
    {
        Vector3 directionToLookTarget = (lookTarget - transform.position).normalized;
        float targetAngle = 90 - Mathf.Atan2(directionToLookTarget.z, directionToLookTarget.x) * Mathf.Rad2Deg;

        while (Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.y, targetAngle)) > 0.05f)
        {
            float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetAngle, guardTurnTime * Time.deltaTime);
            transform.eulerAngles = Vector3.up * angle;
            yield return null;
        }
    }
    
    private void OnDrawGizmos()
    {
        Vector3 startPosition = pathHolder.GetChild(0).position;
        Vector3 pastPosition = startPosition;
        foreach (Transform waypoint in pathHolder)
        {
            Gizmos.DrawSphere(waypoint.position, 0.5f);
            Gizmos.DrawLine(pastPosition, waypoint.position);
            pastPosition = waypoint.position;
        }
        Gizmos.DrawLine(pastPosition, startPosition);

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * viewDistance);
    }
}
