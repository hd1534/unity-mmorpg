using System;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField] float _speed = 10;

    private float _wait_run_ratio = 0;
    bool _moveToDestination = false;
    Vector3 _destinationPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        Manager.Input.KeyAction -= OnKeyboard;
        Manager.Input.KeyAction += OnKeyboard;
        
        Manager.Input.MouseAction -= OnMouseClicked;
        Manager.Input.MouseAction += OnMouseClicked;
    }

    void OnKeyboard() {
        _moveToDestination = false;
        
        if (Input.GetKey(KeyCode.W)) {
            //transform.position += transform.TransformDirection(Vector3.forward * Time.deltaTime * _speed);
            // transform.Translate(Vector3.forward * (Time.deltaTime * _speed));
            
            transform.position += Vector3.forward * (_speed * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 0.2f);
        }
        if (Input.GetKey(KeyCode.S)) {
            //transform.position += transform.TransformDirection(Vector3.back * Time.deltaTime * _speed);
            // transform.Translate(Vector3.back * (Time.deltaTime * _speed));
            
            transform.position += Vector3.back * (_speed * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), 0.2f);
        }
        if (Input.GetKey(KeyCode.A)) {
            //transform.position += transform.TransformDirection(Vector3.left * Time.deltaTime * _speed);
            
            transform.position += Vector3.left * (_speed * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), 0.2f);
        }
        if (Input.GetKey(KeyCode.D)) {
            //transform.position += transform.TransformDirection(Vector3.right * Time.deltaTime * _speed);
            
            transform.position += Vector3.right * (_speed * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), 0.2f);
        }
    }

    void OnMouseClicked(Define.MouseEvent mouseEvent) {
        // if (mouseEvent != Define.MouseEvent.Click) return;
        
        LayerMask mask = LayerMask.GetMask("Wall");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
        Debug.DrawRay(Camera.main.transform.position, ray.direction * 100, Color.red, 1);
        if (Physics.Raycast(ray, out RaycastHit hit, 100, mask)) {
            _destinationPosition = hit.point;
            _moveToDestination = true;
        }
    }
    
    // Update is called once per frame
    void Update() {
        if (_moveToDestination) {
            Vector3 dir = _destinationPosition - transform.position;
            if (dir.magnitude < 0.001f) {
                _moveToDestination = false;
            }
            else {
                transform.position += dir.normalized * Mathf.Clamp(_speed * Time.deltaTime, 0, dir.magnitude);
                transform.LookAt(_destinationPosition);
                
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 10 * Time.deltaTime);
            }
        }
        
        Animator animator = GetComponent<Animator>();
        animator.Play("WAIT_RUN");
        if (_moveToDestination) {
            _wait_run_ratio = Mathf.Lerp(_wait_run_ratio, 1f, 10 * Time.deltaTime);
        }
        else {
            _wait_run_ratio = Mathf.Lerp(_wait_run_ratio, 0f, 10 * Time.deltaTime);
        }
        animator.SetFloat("wait_run_ratio", _wait_run_ratio);
    }
}