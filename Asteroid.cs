using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float speed = 4f;
    float maxLifeTime = 8f; 
    Vector3 targetVector;
    public bool isDestroyed = false;
    public bool canDivide = true; //solo pueden dividirse los asteroides primerizos
    public Vector3 extraVelocity = Vector3.zero; //empujón extra al nacer de la colisión del asteroide primerizo

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        targetVector = Vector3.down; //siempre hacia abajo
        Destroy(gameObject, maxLifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate((targetVector * speed + extraVelocity) * Time.deltaTime);
    }
}
