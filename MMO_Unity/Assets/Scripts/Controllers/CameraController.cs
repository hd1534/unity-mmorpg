using UnityEngine;

public class CameraController : MonoBehaviour {
    [SerializeField]
    Define.CameraMode _mode = Define.CameraMode.QuarterView;
    
    [SerializeField]
    Vector3 _delta = new Vector3(0.0f, 6, -5);
    
    [SerializeField]
    GameObject _player = null;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {

        if (_mode == Define.CameraMode.QuarterView) {
            if (Physics.Raycast(_player.transform.position, _delta, out RaycastHit hit, _delta.magnitude,
                    LayerMask.GetMask("Wall"))) {
                transform.position = hit.point;
            }
            else {
                transform.position = _player.transform.position + _delta;
            }
            
            transform.LookAt(_player.transform);
        }
    }
}
