using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerActions : MonoBehaviour
{
    [SerializeField] private Gun gun;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
           
    }

    public void OnAttack() { 
        Debug.Log("Attacking on Titan");
        gun.Fire();
    }

}
