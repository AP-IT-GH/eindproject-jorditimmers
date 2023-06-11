using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using UnityEngine;

public class ZombieAgent : Agent
{
    private Rigidbody rBody;
    public GameObject target;
    private Vector3 targetPosition;
    private float episodeTime;
    private float maxEpisodeLength = 100f; // Maximum episode length
    private float maxMapDistance = Mathf.Sqrt(65*65 + 61*61); // Max distance in the map

    void Start()
    {
        rBody = GetComponent<Rigidbody>();
        rBody.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    public override void OnEpisodeBegin()
    {
        this.transform.localPosition = new Vector3(Random.Range(-20, 45), -5.347855f, Random.Range(-16, 45));
        targetPosition = new Vector3(Random.Range(-20, 45), -5.347855f, Random.Range(-16, 45));
        target.transform.localPosition = targetPosition;
        episodeTime = 0f;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(targetPosition);
        sensor.AddObservation(episodeTime);
        sensor.AddObservation(transform.InverseTransformDirection(rBody.velocity));
    }

    public override void OnActionReceived(ActionBuffers actions)
{
    // Move the agent
    var dirToGo = Vector3.zero;
    var rotateDir = Vector3.zero;

    var action = actions.DiscreteActions[0];
    switch (action)
    {
        case 0:
            dirToGo = transform.forward * 1f;
            break;
        case 1:
            dirToGo = transform.forward * -1f;
            break;
        case 2:
            rotateDir = transform.up * 1f;
            break;
        case 3:
            rotateDir = transform.up * -1f;
            break;
    }
    transform.Rotate(rotateDir, Time.deltaTime * 200f);
    rBody.AddForce(dirToGo * 2f, ForceMode.VelocityChange);

    // Update the episode time
    episodeTime += Time.deltaTime;

    // Check if the episode has taken too long
    if (episodeTime > maxEpisodeLength) 
    {
        EndEpisode();
    }
}


    void OnCollisionEnter(Collision collision)
{
    // Check if the agent hit a wall
    if(collision.gameObject.tag == "target")
    {
        float scaledReward = Mathf.Max(0.5f, (maxEpisodeLength - episodeTime) / maxEpisodeLength * 2f);
        AddReward(scaledReward);
        EndEpisode();
    }
}



    public override void Heuristic(in ActionBuffers actionsOut)
{
    var discreteActionsOut = actionsOut.DiscreteActions;
    discreteActionsOut.Clear();

    if (Input.GetKey(KeyCode.W))
    {
        discreteActionsOut[0] = 0;
    }
    else if (Input.GetKey(KeyCode.S))
    {
        discreteActionsOut[0] = 1;
    }
    else if (Input.GetKey(KeyCode.D))
    {
        discreteActionsOut[0] = 2;
    }
    else if (Input.GetKey(KeyCode.A))
    {
        discreteActionsOut[0] = 3;
    }
}
}
