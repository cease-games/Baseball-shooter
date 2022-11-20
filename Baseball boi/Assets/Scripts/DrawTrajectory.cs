using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public class DrawTrajectory : MonoBehaviour
{
[SerializeField]
    private LineRenderer _lineRenderer;
    // [SerializeField]
    // [Range(3, 30)]
    private int _lineSegmentCount = 20;
    private List<Vector3> _linePoints = new List<Vector3>();
    #region Singleton

    public static DrawTrajectory Instance;
    private void Awake()
    {
        Instance = this;
    }
    #endregion

    public void UpdateTrajectory(Vector3 forceVector, Rigidbody rigidBody, Vector3 startingPoint)
    {
        Vector3 velocity = (forceVector / rigidBody.mass) * Time.fixedDeltaTime;
        float FlightDuration = (2 * velocity.y) / Physics.gravity.y;
        float stepTime = FlightDuration / _lineSegmentCount;
        _linePoints.Clear();
        _linePoints.Add(startingPoint);
        for (int i = 1; i < _lineSegmentCount; i++)
        {
            float stepTimePassed = stepTime * i; //change in tine
            Vector3 MovementVector = new Vector3(
            velocity.x * stepTimePassed,
            velocity.y * stepTimePassed - 0.5f * Physics.gravity.y * stepTimePassed * stepTimePassed,
            velocity.z * stepTimePassed
            );
            Vector3 NewPointOnLine = -MovementVector + startingPoint;
            _linePoints.Add(NewPointOnLine);
        }
        _lineRenderer.positionCount = _linePoints.Count;
        _lineRenderer.SetPositions(_linePoints.ToArray());
        //http://hyperphysics.phy-astr.gsu.edu/hbase/traj.html#tracon

    }
    public void HideLine()
    {
        _lineRenderer.positionCount = 0;
    }

}
