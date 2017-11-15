using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    public enum BEHAVIOR_STATE { BS_IDLE, BS_PATROLE, BS_CHASE };

    public BEHAVIOR_STATE behavior = BEHAVIOR_STATE.BS_IDLE;

    public Vector3 lastPoint;

    public Transform[] waypoints;

    public int WayPointIndex = 0;


    public AudioSource detection;
    public AudioSource walk;

    [SerializeField]
    private GameObject target;

    private float m_fDifference;
    private Vector3 m_NPCPosition;
    private Vector3 m_WaypointPosition;
    private UnityEngine.AI.NavMeshAgent navAgent;

    void Start()
    {
        navAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();

        navAgent.speed = 1.5f;
        navAgent.angularSpeed = 300.0f;
        navAgent.acceleration = 1.5f;
    }

    void Update()
    {

        if (!walk.isPlaying)
            walk.Play();

        switch (behavior)
        {
            case BEHAVIOR_STATE.BS_IDLE:
                navAgent.destination = lastPoint;
                if (navAgent.remainingDistance < 1.5f)
                {
                    behavior = BEHAVIOR_STATE.BS_PATROLE;
                }
                break;

            case BEHAVIOR_STATE.BS_PATROLE:
                m_NPCPosition = new Vector3(transform.position.x, 0.0f, transform.position.z);
                m_WaypointPosition = new Vector3(waypoints[WayPointIndex].position.x, 0.0f, waypoints[WayPointIndex].position.z);
                m_fDifference = Vector3.Distance(m_NPCPosition, m_WaypointPosition);
                if (m_fDifference < 0.25f)
                {
                    WayPointIndex++;
                    if (WayPointIndex >= waypoints.Length)
                    {
                        WayPointIndex = 0;
                    }
                }

                navAgent.destination = waypoints[WayPointIndex].position;
                break;

            case BEHAVIOR_STATE.BS_CHASE:
                if (target)
                {
                    navAgent.destination = target.transform.position;
                }
                break;

            default:
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            detection.Play();
            target = other.gameObject;
            behavior = BEHAVIOR_STATE.BS_CHASE;
            lastPoint = transform.position;
            navAgent.speed = 20.0f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            target = null;
            behavior = BEHAVIOR_STATE.BS_IDLE;
            navAgent.speed = 10.0f;
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            Debug.Log("hit");
            navAgent.isStopped = true;
        }
    }
}
