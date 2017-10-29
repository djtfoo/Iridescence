using UnityEngine;
using System.Collections.Generic;
using System.Reflection;
using System;
using System.Xml.Serialization;

[XmlRoot(ElementName = "EnemyBehaviour")]
public class EnemyBehaviour {

    private GameObject thisEnemy;   // this enemy

    private NewPathfinder pathfinder;

    private float timer = 0f;
    private float updateTime = 0.2f;

    // for Chloris
    private bool toSpawn = false;

    //private bool isMoving = false;  // whether to move or not
    List<Vector3> pathWaypoints = new List<Vector3>();  // storing the waypoints of a path
    private Vector3 velocity = Vector3.zero;
    //private Vector3 destination;    // to move to

    [XmlElement(ElementName = "methodName")]
    public string methodName;

    [XmlArray("parameters")]
    [XmlArrayItem("ObjectArrayItem")]
    public ObjectArrayItem[] parameters;

    // non-XML variables
    public object[] methodParams;   // will be populated with data from ObjectArrayItem[]

    public void AddEnemy(GameObject enemy)
    {
        Array.Resize(ref methodParams, methodParams.Length + 1);
        methodParams[methodParams.Length - 1] = enemy.transform.parent.gameObject;

        thisEnemy = enemy.transform.parent.gameObject;
        pathfinder = enemy.GetComponent<NewPathfinder>();
    }

    public void Update()
    {
        timer += Time.deltaTime;

        //Debug.Log(methodParams[0].GetType());

        //Get method information
        MethodInfo method = GetType().GetMethod(methodName);    // from this class, EnemyBehaviour

        if (method != null)
        {
            //Invoke the method
            method.Invoke(this, methodParams);
        }

        if (velocity != Vector3.zero)
        {
            // check for reach waypoint destination
            Vector3 dirCheck = pathWaypoints[0] - thisEnemy.transform.position;
            // check by knowing whether "overshot" the path
            float cosAngle = dirCheck.x * velocity.x + dirCheck.y * velocity.y;
            if (cosAngle < 0f)
            {
                ReachedWaypoint();
            }

            // move because still got somewhere to move to
            if (pathWaypoints.Count != 0)
            {
                // move towards destination
                thisEnemy.transform.position += velocity * Time.deltaTime;
            }
        }

    }

    private void ReachedWaypoint()
    {
        pathWaypoints.RemoveAt(0);
        if (pathWaypoints.Count == 0) {
            velocity = Vector3.zero;
            pathWaypoints.Clear();
        }
        else
            SetVelocity((pathWaypoints[0] - thisEnemy.transform.position).normalized);
    }

    public void ChasePlayer(float speed, GameObject enemy)
    {
        // every cycle, check position against player
        if (timer >= updateTime)
        {
            // if player is near enough, begin chase
            Vector3 playerPos = PlayerAction.instance.transform.position;
            Vector3 distToPlayer = (playerPos - enemy.transform.position) * 0.8f;

            float sqrMag = distToPlayer.sqrMagnitude;

            if (sqrMag > 1 && sqrMag < 10)  // 3*3
            {
                // empty path
                pathWaypoints.Clear();

                // go after player
                pathfinder.CalculatePath(playerPos, ref pathWaypoints);
                //pathWaypoints.Add(enemy.transform.position + distToPlayer);
                //isMoving = true;
                SetVelocity(distToPlayer.normalized);

                timer = 0f;
            }
        }
    }

    public void ApolloBehaviour(float attackTime, GameObject enemy)
    {
        if (timer >= attackTime)
        {
            // begin attack animation
            pathfinder.transform.GetComponent<SpriteAnimator>().ChangeAnimation("Attack", false);

            // reset timer
            timer = 0f;
        }
    }

    public void ChlorisBehaviour(float spawnTime, GameObject enemy)
    {
        // every few moments, walk around
        //if (pathWaypoints.Count == 0) {
        //
        //    Vector3 vel = new Vector3(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), 0f).normalized;
        //    Vector3 destination = enemy.transform.position + vel;
        //    pathWaypoints.Add(destination);
        //    SetVelocity(vel);
        //}

        if (toSpawn)
        {
            if (pathfinder.transform.GetComponent<SpriteAnimator>().animationComplete)
            {
                // spawn enemies
                for (int i = 0; i < 2; ++i)
                {
                    GameObject spawn = GameObject.Instantiate(pathfinder.GetComponent<EnemyData>().toSpawn);

                    Vector3 vel = new Vector3((float)GameProgressManager.instance.rand.NextDouble(), (float)GameProgressManager.instance.rand.NextDouble(), 0f).normalized;
                    spawn.transform.position = enemy.transform.position + vel;
                }

                pathfinder.transform.GetComponent<SpriteAnimator>().animationComplete = false;
                toSpawn = false;
            }
        }

        if (timer >= spawnTime)
        {
            // begin attack animation
            pathfinder.transform.GetComponent<SpriteAnimator>().ChangeAnimation("Attack", false);
            toSpawn = true;

            timer = 0f;
        }
    }

    private void SetVelocity(Vector3 vel)
    {
        velocity = vel;
    }

}
