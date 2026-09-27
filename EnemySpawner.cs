using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject asteroidPrefab;   
    float spawnRatePerMinute = 30f;   
    float spawnRateIncrement = 1f;     
    float xBorderLimit;            
    float yBorderLimit;           
    float spawnNext = 0f; //cuándo toca el próximo spawn

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Camera cam = Camera.main;
        yBorderLimit = cam.orthographicSize;
        xBorderLimit = yBorderLimit * cam.aspect;
    }

    // Update is called once per frame
    void Update()
    {
                // instanciamos enemigos sólo si ha pasado tiempo suficiente desde el último.
                if (Time.time > spawnNext){          
                    
                    // indicamos cuándo podremos volver a instanciar otro enemigo
                    spawnNext = Time.time + 60 / spawnRatePerMinute; 
                    // sube la dificultad con cada spawn aumentando los asteroides por minuto
                    spawnRatePerMinute += spawnRateIncrement;   

                    //guardo un punto aleatorio entre las esquinas superiores de la pantalla
                    var rand = Random.Range(-xBorderLimit, xBorderLimit);        
                    var spawnPosition = new Vector2(rand, yBorderLimit); 

                    // instancip el asteroide en dicho punto, y con el ángulo random
                    Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity);
                }
        
    }
}
