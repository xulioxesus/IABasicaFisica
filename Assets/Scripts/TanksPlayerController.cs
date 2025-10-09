using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Controla o movemento dun tanque simple: translación e rotación da base,
// control do canón e disparo de proxectís.
// - Move con os eixes de Input: "Vertical" para adiante/atrás e "Horizontal" para xirar.
// - As teclas T/G inclinan o canón arredor do seu eixo local.
// - B dispara instanciando o prefab `bulletObj` na posición e rotación de `gun`.
public class TanksPlayerController : MonoBehaviour
{
    // Velocidade de translación en unidades por segundo (adiante/atrás).
    public float speed = 10.0f;

    // Velocidade de rotación en graos por segundo (xiro da base do tanque).
    public float rotationSpeed = 100.0f;

    // Transform que representa o punto/objeto a rotar cando se move o canón (turret pivot).
    public Transform transGun;

    // Transform usado como punto de orixe para instanciar as bala (barrel end).
    public Transform gun;

    // Prefab da bala a instanciar ao disparar.
    public GameObject bulletObj;

    void Update()
    {
        // Lemos os eixes de Input estándar de Unity (configurados en Edit > Project Settings > Input):
        // "Vertical" normalmente está ligado a W/S ou frechas arriba/abaixo; devolve [-1,1].
        // "Horizontal" normalmente está ligado a A/D ou frechas esquerda/dereita; devolve [-1,1].
        float translation = Input.GetAxis("Vertical") * speed;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;

        // Multiplicamos por Time.deltaTime para facer o movemento independente do FPS
        // (speed e rotationSpeed están en unidades/segundo e graos/segundo respectivamente).
        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;

        // Movemos o transform local ao longo do eixo Z (adiante/atrás) segundo `translation`.
        transform.Translate(0, 0, translation);

        // Rotamos ao redor do eixo Y local (xiro da base do tanque).
        transform.Rotate(0, rotation, 0);

        // Control do canón: T inclina cara abaixo, G inclina cara arriba.
        // RotateAround usa transGun.position como punto de rotación e transGun.right
        // como eixo local (x do transform do canón).
        if (Input.GetKey(KeyCode.T))
        {
            transGun.RotateAround(transGun.position, transGun.right, -20.0f * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.G))
        {
            transGun.RotateAround(transGun.position, transGun.right, 20.0f * Time.deltaTime);
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            // Disparo: instanciamos unha bala na posición e rotación de `gun`.
            Instantiate(bulletObj, gun.position, gun.rotation);
        }
    }
}
