using System;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField] float _speed = 10;

    Vector3 _destinationPosition;


    public enum PlayerState {
        Die,
        Moving,
        Idle,
    }

    PlayerState _state = PlayerState.Idle;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        // Manager.Input.KeyAction -= OnKeyboard;
        // Manager.Input.KeyAction += OnKeyboard;
        
        Manager.Input.MouseAction -= OnMouseClicked;
        Manager.Input.MouseAction += OnMouseClicked;
    }
    // Update is called once per frame
    void Update() {
        switch (_state) {
            case PlayerState.Idle:
                UpdateIdle();
                break;
            
            case PlayerState.Moving:
                UpdateMoving();
                break;
            
            case PlayerState.Die:
                break;
        }
        
    }

    private void UpdateIdle() {
        Animator animator = GetComponent<Animator>();
        animator.SetFloat("speed", 0);
    }


    void UpdateMoving() {
        Vector3 dir = _destinationPosition - transform.position;
        if (dir.magnitude < 0.001f) {
            _state = PlayerState.Idle;
        }
        else {
            transform.position += dir.normalized * Mathf.Clamp(_speed * Time.deltaTime, 0, dir.magnitude);
            transform.LookAt(_destinationPosition);
            
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 10 * Time.deltaTime);
        }
        
        Animator animator = GetComponent<Animator>();
        animator.SetFloat("speed", _speed);
    }


    void OnMouseClicked(Define.MouseEvent mouseEvent) {
        if (_state == PlayerState.Die) return;
        
        LayerMask mask = LayerMask.GetMask("Wall");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
        Debug.DrawRay(Camera.main.transform.position, ray.direction * 100, Color.red, 1);
        if (Physics.Raycast(ray, out RaycastHit hit, 100, mask)) {
            _destinationPosition = hit.point;
            _state = PlayerState.Moving;
        }
    }
    
    // void OnKeyboard() {
    //     _moveToDestination = false;
    //     
    //     if (Input.GetKey(KeyCode.W)) {
    //         //transform.position += transform.TransformDirection(Vector3.forward * Time.deltaTime * _speed);
    //         // transform.Translate(Vector3.forward * (Time.deltaTime * _speed));
    //         
    //         transform.position += Vector3.forward * (_speed * Time.deltaTime);
    //         transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 0.2f);
    //     }
    //     if (Input.GetKey(KeyCode.S)) {
    //         //transform.position += transform.TransformDirection(Vector3.back * Time.deltaTime * _speed);
    //         // transform.Translate(Vector3.back * (Time.deltaTime * _speed));
    //         
    //         transform.position += Vector3.back * (_speed * Time.deltaTime);
    //         transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), 0.2f);
    //     }
    //     if (Input.GetKey(KeyCode.A)) {
    //         //transform.position += transform.TransformDirection(Vector3.left * Time.deltaTime * _speed);
    //         
    //         transform.position += Vector3.left * (_speed * Time.deltaTime);
    //         transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), 0.2f);
    //     }
    //     if (Input.GetKey(KeyCode.D)) {
    //         //transform.position += transform.TransformDirection(Vector3.right * Time.deltaTime * _speed);
    //         
    //         transform.position += Vector3.right * (_speed * Time.deltaTime);
    //         transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), 0.2f);
    //     }
    // }
    //
}

