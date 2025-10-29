using UnityEngine;

public class Platform_Gioco1 : MonoBehaviour
{

    public SpriteRenderer renderer;


    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    private void Awake()
    {
        Debug.Log("Inizializzato");

        renderer = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Update");
    }
}
