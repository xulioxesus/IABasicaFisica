using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireShell : MonoBehaviour {

    public GameObject bullet;
    public GameObject turret;
    public GameObject enemy;
    public Transform turretBase;

    private float speed = 15.0f; // Velocidade da bala
    private float rotSpeed = 5.0f; // Velocidade de rotación do tanque
    private float moveSpeed = 1.0f; // Velocidade lineal do tanque

    static float delayReset = 0.2f; // Tempo entre disparos
    float delay = delayReset; // Tempo ata o seguinte disparo

    /// <summary>
    /// Crea unha bala
    /// </summary>
    void CreateBullet() {

        //Instancia o prefab shell
        GameObject shell = Instantiate(bullet, turret.transform.position, turret.transform.rotation);

        //Establece a velocidade inicial da bala
        shell.GetComponent<Rigidbody>().linearVelocity = speed * turretBase.forward;
    }

    /// <summary>
    /// Rota a torreta no eixe X
    /// </summary>
    /// <returns></returns>
    float? RotateTurret() {

        float? angle = CalculateAngle(true);

        if (angle != null) {

            //Faise a rotación da torreta
            turretBase.localEulerAngles = new Vector3(360.0f - (float)angle, 0.0f, 0.0f);
        }
        return angle;
    }

    /// <summary>
    /// Calcula o ángulo da traxectoria para impactar ao
    /// </summary>
    /// <param name="low">Indica se queremos o ángulo menor</param>
    /// <returns>O ángulo necesario para a traxectoria da bala</returns>
    float? CalculateAngle(bool low) {

        Vector3 targetDir = enemy.transform.position - this.transform.position;
        float y = targetDir.y;
        targetDir.y = 0.0f;
        float x = targetDir.magnitude - 1.0f;
        float gravity = 9.8f;
        float sSqr = speed * speed;
        float underTheSqrRoot = (sSqr * sSqr) - gravity * (gravity * x * x + 2 * y * sSqr);

        if (underTheSqrRoot >= 0.0f) {

            float root = Mathf.Sqrt(underTheSqrRoot);
            float highAngle = sSqr + root;
            float lowAngle = sSqr - root;

            if (low) return (Mathf.Atan2(lowAngle, gravity * x) * Mathf.Rad2Deg);
            else return (Mathf.Atan2(highAngle, gravity * x) * Mathf.Rad2Deg);
        } else
            return null;
    }

    void Update() {

        //Para comprobar se pode disparar novamente
        delay -= Time.deltaTime;

        //Establece a rotación do tanque necesaria para orientarse hacia o player
        //Rota suavemente utilizando Quaternion.Slerp
        //Os Quaternions utilízanse para representar rotacións
        Vector3 direction = (enemy.transform.position - this.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0.0f, direction.z));
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, lookRotation, Time.deltaTime * rotSpeed);


        //Rota a torreta
        float? angle = RotateTurret();

        if (angle != null && delay <= 0.0f) {
            //Se o ángulo da traxectoria é posible e xa pasou o tempo de espera para disparar, dispara
            CreateBullet();
            delay = delayReset;
        } else {
            //Move o tanque cara adiante
            this.transform.Translate(0.0f, 0.0f, Time.deltaTime * moveSpeed);
        }
    }
}
