using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script que simula de forma simple o movemento dun proxectil sen usar Rigidbody.
// - Calcula unha aceleración inicial a partir de forza/masa.
// - Aplica drag á velocidade horizontal e unha aceleración vertical por gravidade.
// - Ao colidir con algo etiquetado como "tank" instancia un efecto de explosión e destrúe o proxectil.
public class TanksShellPhysicsSimulated : MonoBehaviour {

    // Prefab do efecto de explosión ao impactar nun tanque.
    public GameObject explosion;

    // Velocidade horizontal do proxectil.
    float speed = 0.0f;

    // Velocidade vertical (Y) do proxectil.
    float ySpeed = 0.0f;

    // Parámetros físicos simulados (non están usando un Rigidbody real).
    float mass = 30.0f;
    float force = 4.0f;
    float drag = 1.0f;
    float gravity = -9.8f;

    // Aceleración vertical efectiva e aceleración inicial calculada.
    float gAccel;
    float acceleration;

    // Detecta colisións; se impacta cun obxecto con tag "tank" fai explosión e destrúe a bala.
    void OnCollisionEnter(Collision col) {
        if (col.gameObject.tag == "tank") {
            GameObject exp = Instantiate(explosion, this.transform.position, Quaternion.identity);
            Destroy(exp, 0.5f);
            Destroy(this.gameObject);
        }
    }

    private void Start() {
        // Calculamos unha aceleración inicial sinxela e a aceleración vertical por gravidade/mass
        acceleration = force / mass;
        speed += acceleration * 1.0f; // aplicamos a aceleración durante 1 segundo (aprox.) ao iniciar
        gAccel = gravity / mass;
    }

    void LateUpdate() {
        // Aplicamos drag á velocidade horizontal (decaemento exponencial aproximado)
        speed *= (1 - Time.deltaTime * drag);

        // Actualizamos a velocidade vertical segundo gAccel
        ySpeed += gAccel * Time.deltaTime;

        // Movemos o transform: (x=0, y=ySpeed, z=speed)
        this.transform.Translate(0.0f, ySpeed, speed);
    }
}
