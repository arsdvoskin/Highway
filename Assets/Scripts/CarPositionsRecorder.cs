using System.Collections.Generic;
using UnityEngine;

public struct CarPositionData
{
    public Vector3 position;
    public Quaternion rotation;
    public float time;

    public CarPositionData(Vector3 pos, Quaternion rot, float t)
    {
        position = pos;
        rotation = rot;
        time = t;
    }
}

public class CarPositionsRecorder : MonoBehaviour
{
    [field: SerializeField]
    public float PositionsRecordInterval { get; private set; } = 0.2f;

    private List<CarPositionData> carPositions = new List<CarPositionData>();
    private float localTimer = 0f;

    private void Awake()
    {
        enabled = false;
    }

    private void Start()
    {
        RaceController.OnBeforeFinish += RecordCarPosition;
        RaceController.OnNewRecord += SaveCarPositions;
    }

    private void Update()
    {
        localTimer += Time.deltaTime;

        if (localTimer >= PositionsRecordInterval)
        {
            RecordCarPosition();
            localTimer = 0f;
        }
    }

    private void RecordCarPosition()
    {
        carPositions.Add(new CarPositionData(transform.position, transform.rotation, RaceController.Timer));
    }

    private void SaveCarPositions()
    {
        ES3.Save<List<CarPositionData>>("BestTimeCarPositions", carPositions);
    }
}