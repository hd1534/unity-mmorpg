using System;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    private void OnCollisionEnter(Collision other) {
        Debug.Log($"{other.gameObject.name} collided");
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log($"{other.gameObject.name} triggerd");

        AudioSource audio = GetComponent<AudioSource>();
        Manager.Sound.Play("UnityChan/univ0001");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        // RaycastHit hit;
        // Debug.DrawRay(transform.position + Vector3.up, transform.forward * 10f, Color.red);
        // if (Physics.Raycast(transform.position + Vector3.up, Vector3.forward, out hit, 10)) {
        //     Debug.Log($"{hit.collider.gameObject.name} detected");
        // }
        
        // Screen 기준 좌표
        // Input.mousePosition;
        
        // Viewport 좌표
        // Debug.Log(Camera.main!.ScreenToViewportPoint(Input.mousePosition));
        
        // ScreenToWorldPoint
        // 수동
        // if (Input.GetMouseButtonDown(0)) {
        //     Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
        //     Vector3 direction = (mousePos - Camera.main.transform.position).normalized;
        //     
        //     Debug.DrawRay(Camera.main.transform.position, direction * 100, Color.red, 1);
        //     if (Physics.Raycast(Camera.main.transform.position, direction, out RaycastHit hit, 100)) {
        //         Debug.Log($"{hit.collider.gameObject.name} collided");
        //     }
        // }
        
        // 자동
        if (Input.GetMouseButtonDown(0)) {
            LayerMask mask = LayerMask.GetMask("Monster") | LayerMask.GetMask("Wall");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            Debug.DrawRay(Camera.main.transform.position, ray.direction * 100, Color.red, 1);
            if (Physics.Raycast(ray, out RaycastHit hit, 100, mask)) {
                Debug.Log($"{hit.collider.gameObject.name} collided");
            }
        }
        
    }
}
