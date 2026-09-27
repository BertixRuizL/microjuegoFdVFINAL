using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Bullet : MonoBehaviour
{
    int speed = 10;
    float maxLifeTime = 3;
    public Vector3 targetVector;
    public AudioClip hitSound;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // nada más nacer, le damos unos segundos de vida, 
        // lo suficiente para salir de la pantalla       
        Destroy(gameObject, maxLifeTime); 
        
        //para el disparo en sí
        targetVector = Vector3.right; //lo hacemos fijo, sin el transform.right
        Destroy(gameObject, maxLifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        // la bala se mueve en la dirección del jugador al disparar
        transform.Translate(targetVector * speed * Time.deltaTime);
    }
private void OnTriggerEnter(Collider other) {
    if (other.gameObject.tag == "Enemy") {
        Asteroid asteroid = other.GetComponent<Asteroid>();

        AudioSource.PlayClipAtPoint(hitSound, transform.position);

        if (asteroid != null && asteroid.canDivide) {
            DivideAsteroid(asteroid);
        }

        Destroy(other.gameObject);
        Destroy(gameObject);
        Player.SCORE++;
        UpdateScoreText();
    }
}
    private void UpdateScoreText() {
        GameObject go = GameObject.FindGameObjectWithTag("UI");
        go.GetComponent<TMP_Text>().text = "SCORE: " + Player.SCORE;
    }


    private void DivideAsteroid(Asteroid original)
    {
        for (int i = 0; i < 2; i++) {
            GameObject fragment = Instantiate(original.gameObject, original.transform.position, Quaternion.identity);
            fragment.transform.localScale = original.transform.localScale * 0.5f;

            Asteroid fragmentScript = fragment.GetComponent<Asteroid>();
            fragmentScript.canDivide = false; // los dos nuevos asteroides no se vuelven a partir
            fragmentScript.isDestroyed = false;   

            float randomX = Random.Range(-3f, 3f);
            fragmentScript.extraVelocity = new Vector3(randomX, 0f, 0f); //los lanzamos con un pequeñp extra de velocidad
        }
    }
}
