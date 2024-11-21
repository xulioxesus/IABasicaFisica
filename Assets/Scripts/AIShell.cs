using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIShell : MonoBehaviour {

    public GameObject explosion; //Prefab para a explosión
    Rigidbody rb; // Para acceder ao RigidBody do GameObject

    /// <summary>
    /// Se a bala colisiona co tanque
    /// </summary>
    /// <param name="col"></param>
    void OnCollisionEnter(Collision col) {

        if (col.gameObject.tag == "tank") {
            
            Debug.Log("Hit tank");
            //Crear unha bala
            GameObject exp = Instantiate(explosion, this.transform.position, Quaternion.identity);

            //Destruir explosión despois de medio segundo
            Destroy(exp, 0.5f);

            //Destruir a bala
            Destroy(this.gameObject);
        }
    }

    void Start() {

        //Obter o compoñente Rigidbody
        rb = GetComponent<Rigidbody>();
    }

    void Update() {

        //Actualízase a dirección da bala co vector de volocidade determinado polo motor de física
        this.transform.forward = rb.linearVelocity;
    }
}
