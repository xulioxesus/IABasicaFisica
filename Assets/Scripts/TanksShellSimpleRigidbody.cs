using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script para o comportamento dun proxectil controlado por IA.
// - Ao colidir cun obxecto etiquetado como "tank" crea unha explosión e destrúe a bala.
// - Mantén o forward do proxectil segundo a súa velocidade lineal para que apunte na dirección do movemento.
public class TanksShellSimpleRigidbody : MonoBehaviour {

    // Prefab do efecto de explosión a instanciar ao impactar nun tanque.
    public GameObject explosion; 

    // Referencia ao Rigidbody deste proxectil (obtense en Start).
    Rigidbody rb; 

    // Chamado por Unity cando o collider deste obxecto colisiona con outro.
    void OnCollisionEnter(Collision col) {

        // Se o obxecto colidido ten a tag "tank", considerámolo un impacto válido.
        if (col.gameObject.tag == "tank") {
            // Mensaxe de depuración
            Debug.Log("Hit tank");

            // Instanciamos o efecto de explosión na posición actual do proxectil.
            GameObject exp = Instantiate(explosion, this.transform.position, Quaternion.identity);

            // Destruímos o efecto de explosión despois de 0.5 segundos para limpar a escena.
            Destroy(exp, 0.5f);

            // Destruímos o propio proxectil tras o impacto.
            Destroy(this.gameObject);
        }
    }

    void Start() {
        // Obtemos o Rigidbody asociado para ler a velocidade no Update.
        rb = GetComponent<Rigidbody>();
    }

    void Update() {
        // Sempre que haxa un Rigidbody, aliñamos o eixo forward do transform coa velocidade lineal
        // para que o proxectil apunte na dirección do movemento.
        // Nota: se a velocidade é (0,0,0) isto fará que forward sexa vector cero, polo que en casos reais
        // convén comprobalo antes.
        this.transform.forward = rb.linearVelocity;
    }
}
