using UnityEngine;
 public class PositionController : MonoBehaviour
{
    void Start()
    {
       // Debug.Log(WavePositions[1].positions.Length);
    }
    public static PositionController Instance;
    [System.Serializable]
    public class _WavePositions
    {
    public Transform[] positions;
    }

    public _WavePositions[] WavePositions;
    
    public int _index;

    private void Awake()
    {
        Instance = this;
    }

    public Transform GetPosition(int i)
    {
        if (_index == WavePositions[i].positions.Length)
        {
            Debug.LogError("More SpaceShip Than Final Positions");
            _index = 0;
        }
        return WavePositions[i].positions[_index++];
    }
    
}
