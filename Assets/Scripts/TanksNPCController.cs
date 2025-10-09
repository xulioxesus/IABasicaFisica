using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Controla un inimigo que apunta e dispara proxectís cunha simple solución balística.
// - Calcula un ángulo de tiro para alcanzar ao `enemy` dependendo da velocidade da bala e da gravidade.
// - Rota a base do inimigo cara ao obxectivo e rota o canón segundo o ángulo calculado.
// - Dispara un prefab `bullet` establecendo a súa velocidade inicial.
public class TanksNPCController : MonoBehaviour {

    // Prefab da bala a instanciar ao disparar.
    public GameObject bullet;

    // Referencia ao obxecto que representa o canón (usado para posicionar a bala).
    public GameObject turret;

    // O obxectivo que queremos alcanzar.
    public GameObject enemy;

    // Transform base do canón (usado para definir a dirección forward do disparo e rotación local).
    public Transform turretBase;

    // Axustes de movemento e tiro
    private float speed = 15.0f; // velocidade inicial da bala (unidades/segundo)
    private float rotSpeed = 5.0f; // velocidade de rotación da unidade cara ao obxectivo
    private float moveSpeed = 1.0f; // velocidade de desprazamento da unidade cando non dispara

    // Control de cadencia de disparo
    static float delayReset = 0.2f; 
    float delay = delayReset; 

    // Instancia o prefab da bala e lle dá a velocidade inicial ao Rigidbody.
    void CreateBullet() {
        GameObject shell = Instantiate(bullet, turret.transform.position, turret.transform.rotation);
        // Asignamos a velocidade lineal inicial multiplicando a dirección forward polo valor speed.
        shell.GetComponent<Rigidbody>().linearVelocity = speed * turretBase.forward;
    }

    // Rota o canón segundo o ángulo balístico calculado. Devolve o ángulo se é posible calculalo.
    float? RotateTurret() {
        float? angle = CalculateAngle(true); // usamos a solución de menor ángulo

        if (angle != null) {
            // Asignamos a rotación local en X para que o canón apunte cara arriba/abaixo
            turretBase.localEulerAngles = new Vector3(360.0f - (float)angle, 0.0f, 0.0f);
        }
        return angle;
    }

    // Calcula o ángulo de disparo (en graos) necesario para alcanzar o obxectivo.
    // Usa a ecuación balística con velocidade inicial `speed` e gravidade fixa.
    // Se non hai solución real (baixo a raíz cadrada negativa), devolve null.
    float? CalculateAngle(bool low) {
        Vector3 targetDir = enemy.transform.position - this.transform.position;
        float y = targetDir.y; // altura relativa
        targetDir.y = 0.0f;
        float x = targetDir.magnitude - 1.0f; // distancia horizontal (axustada substraendo 1)
        float gravity = 9.8f;
        float sSqr = speed * speed;
        float underTheSqrRoot = (sSqr * sSqr) - gravity * (gravity * x * x + 2 * y * sSqr);

        if (underTheSqrRoot >= 0.0f) {
            // Hai solucións reais; calculamos as dúas posibles (alta e baixa)
            float root = Mathf.Sqrt(underTheSqrRoot);
            float highAngle = sSqr + root;
            float lowAngle = sSqr - root;

            if (low) return (Mathf.Atan2(lowAngle, gravity * x) * Mathf.Rad2Deg);
            else return (Mathf.Atan2(highAngle, gravity * x) * Mathf.Rad2Deg);
        } else
            // Non hai solución física para os parámetros dados
            return null;
    }

    void Update() {
        // Actualizamos o temporizador de disparo
        delay -= Time.deltaTime;

        // Rota a unidade (só no eixe Y) cara ao obxectivo para seguir o movemento horizontal
        Vector3 direction = (enemy.transform.position - this.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0.0f, direction.z));
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, lookRotation, Time.deltaTime * rotSpeed);

        // Intentamos obter un ángulo balístico para o canón
        float? angle = RotateTurret();

        // Se temos un ángulo válido e o temporizador permite disparar, creamos a bala
        if (angle != null && delay <= 0.0f) {
            CreateBullet();
            delay = delayReset;
        } else {
            // Se non disparamos, movémonos cara adiante ao ritmo moveSpeed
            this.transform.Translate(0.0f, 0.0f, Time.deltaTime * moveSpeed);
        }
    }
}
