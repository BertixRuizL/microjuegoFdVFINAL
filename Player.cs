using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    float xBorderLimit;
    float yBorderLimit;

     //para poder llamar luego al componente de la nave
    private Rigidbody rigidBody;
    private AudioSource audioSource;
    Vector2 thrustDirection; // z=0

    float rotationSpeed = 90f;
    float thrustForce = 2f;
    public GameObject bulletPrefab;
    public Transform bulletSpawner;
    public AudioClip shootSound;

    public static int SCORE = 0;
    public GameObject gameOver;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //calculamos los limites de la pantalla a traves de la cámara
        Camera cam = Camera.main;
        yBorderLimit = cam.orthographicSize;
        xBorderLimit = yBorderLimit * cam.aspect;

        //para aplicar fuerzas sobre la nave
        rigidBody = GetComponent<Rigidbody>();

        audioSource = GetComponent<AudioSource>();

        Time.timeScale = 1f;
        gameOver.SetActive(false);

        SCORE = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //entradas por teclado
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime; 
        float thrust = Input.GetAxis("Vertical") * thrustForce + Time.deltaTime;

        
        thrustDirection = transform.right; //por defecto eje x +
        transform.Rotate(Vector3.forward, -rotation);

        //aplicamos fuerza al rigidbody en la dirección de empuje, por la fuerza leída
        rigidBody.AddForce(thrust * thrustDirection * thrustForce);

        //detectamos disparos
        if (Input.GetButtonDown("Jump"))
        {
            Instantiate(bulletPrefab, bulletSpawner.position, bulletSpawner.rotation);
            audioSource.PlayOneShot(shootSound);
        }

        //limites infinitos
        var newPos = transform.position;

        if (newPos.x > xBorderLimit)
            newPos.x = -xBorderLimit + 1;
        else if (newPos.x < -xBorderLimit)
            newPos.x = xBorderLimit - 1;
        else if (newPos.y > yBorderLimit)
            newPos.y = -yBorderLimit + 1;
        else if (newPos.y < -yBorderLimit)
            newPos.y = yBorderLimit - 1;

        transform.position = newPos;

        //mientras estemos en GameOver, solo leemos el input del espacio para el reinicio
        if (gameOver.activeSelf) {
            if (Input.GetKeyDown(KeyCode.Space)) {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            return;
        }
        
    }

    private void OnCollisionEnter(Collision collision){
        if(collision.gameObject.tag=="Enemy"){
            GameOver();
        }
        else{
            Debug.Log("Colisión con otro objeto");
        }
    }

    private void GameOver() {
        gameOver.SetActive(true);
        Time.timeScale = 0f; //congelamos todo aunque se siga leyendo unput desde Update()
    }
}
