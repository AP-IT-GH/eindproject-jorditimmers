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
    private float maxEpisodeLength = 200f; // Maximum episode length
    public float maxSpeed = 20f;

    void Start()
    {
        rBody = GetComponent<Rigidbody>();
        rBody.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        Rigidbody targetRb = target.GetComponent<Rigidbody>();
        targetRb.constraints = RigidbodyConstraints.FreezeAll;
    }

    public override void OnEpisodeBegin()
{
    this.transform.localPosition = new Vector3(-56.55f, -1.9f, 17.55f);

    bool validPosition = false;
    while (!validPosition)
    {
        targetPosition = new Vector3(Random.Range(-88f, -43f), -1.44f, Random.Range(-27, 19f));
        validPosition = CheckIfValidPosition(targetPosition, target.transform.localScale);
    }

    target.transform.localPosition = targetPosition;
    episodeTime = 0f;
}

private bool CheckIfValidPosition(Vector3 position, Vector3 size)
{
    // Increase the size of the box by the desired margin
    Vector3 expandedSize = size + Vector3.one/2;

    // Check if the position is inside another object
    Collider[] colliders = Physics.OverlapBox(position, expandedSize / 2);
    if (colliders.Length == 0)
    {
        // The position is valid if there are no colliders in the box
        return true;
    }
    else
    {
        return false;
    }
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
void FixedUpdate()
{
    rBody.velocity = Vector3.ClampMagnitude(rBody.velocity, maxSpeed);
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