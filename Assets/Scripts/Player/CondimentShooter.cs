using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class CondimentShooter : MonoBehaviour
{
    //Reference Variables
    private PlayerController playerController = null;
    private TextMeshProUGUI condimentDisplay = null;

    //Configuration Parameters
    [Header("Shooter Parameters")]
    [SerializeField] float initialPause = 10f;
    [SerializeField] float minimumPause = 0f;
    [SerializeField] float maximumPause = 0f;
    [SerializeField] float minimumDuration = 0f;
    [SerializeField] float maximumDuration = 0f;

    [Header("Projectiles")]
    [SerializeField] Condiment[] condimentArray = null;
    [SerializeField] Transform projectileParent = null;

    //State Variables
    private Condiment currentCondiment = null;

    //Random Shooting Variables
    private float pauseTicker = 0f;
    private float durationTicker = 0f;
    private Coroutine shooterCoroutine = null;

    //Internal Methods
    private void Awake() {
        FindPlayerController();
        FindCondimentDisplay();
        CheckCondimentArray();
    }

    private void FindPlayerController() {
        playerController = gameObject.GetComponent<PlayerController>();
        if (!playerController) {
            Debug.LogWarning("No Player Controller Component Found On Player");
            enabled = false;
        }
    }

    private void FindCondimentDisplay() {
        condimentDisplay = GameObject.FindGameObjectWithTag("CondimentDisplay").GetComponent<TextMeshProUGUI>();
    }

    private void CheckCondimentArray() {
        if (condimentArray == null || condimentArray.Length == 0) {
            Debug.LogWarning("No Condiments Assigned to Array in Shooter Script");
            enabled = false;
        }
    }

    private void Start() {
        PickRandomCondiment();
        InitializeShootPause();
    }

    private void PickRandomCondiment() {
        int index = Random.Range(0, condimentArray.Length);
        currentCondiment = condimentArray[index];
        DisplayCurrentCondiment();
    }

    private void InitializeShootPause() {
        pauseTicker = initialPause;
    }

    private void Update()
    {
        ShootRandomly();
    }
    
    private void ShootRandomly() {
        if (durationTicker > 0f) {
            durationTicker -= Time.deltaTime;
            if (durationTicker < 0f) {
                StopCoroutine(shooterCoroutine);
                shooterCoroutine = null;
            }
        } else {
            if (pauseTicker > 0f) {
                pauseTicker -= Time.deltaTime;
            } else {
                ChooseRandomShootValues();
                shooterCoroutine = StartCoroutine(ShootCurrentCondiment());
            }
        }
    }

    private void ChooseRandomShootValues() {
        pauseTicker = Random.Range(minimumPause, maximumPause);
        durationTicker = Random.Range(minimumDuration, maximumDuration);
    }

    private IEnumerator ShootCurrentCondiment() {
        GameObject condimentPrefab = currentCondiment.GetCondimentPrefab();
        while (durationTicker > 0f) {
            GameObject projectile = Instantiate(condimentPrefab, transform.position, Quaternion.identity) as GameObject;
            if (projectileParent) {
                projectile.transform.SetParent(projectileParent);
            }
            Condiment condiment = projectile.GetComponent<Condiment>();
            if (condiment) {
                condiment.SetMoveDirection(playerController.GetMouseDirection());
            } else {
                Debug.LogError("Spawned Projectile Does Not Have Condiment Component");
            }
            yield return new WaitForSeconds(currentCondiment.GetShootDelay());
        }
    }

<<<<<<< HEAD
    //Public Methods
    public void SwitchCondimentsRandomly() {
        PickRandomCondiment();
=======
    private void DisplayCurrentCondiment() {
        if (condimentDisplay) {
            condimentDisplay.text = currentCondiment.gameObject.name.ToUpper();
            condimentDisplay.color = currentCondiment.GetCondimentPrefab().GetComponent<Condiment>().GetCondimentColor();
        }
    }

    //Public Methods
    public void SwitchCondiments(int index) {
        currentCondiment = condimentArray[index];
        DisplayCurrentCondiment();
>>>>>>> dracoBranch
    }
}
