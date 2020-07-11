using UnityEngine;
using System.Collections;

public class CondimentShooter : MonoBehaviour
{
    //Configuration Parameters
    [Header("Shooter Parameters")]
    [SerializeField] float minimumPause;
    [SerializeField] float maximumPause;
    [SerializeField] float minimumDuration;
    [SerializeField] float maximumDuration;

    [Header("Projectiles")]
    [SerializeField] Condiment[] condimentArray = null;
    [SerializeField] Transform projectileParent = null;

    //State Variables
    private Condiment currentCondiment = null;

    //Random Shooting Variables
    private float pauseTicker = 0f;
    private float durationTicker = 0f;
    private Coroutine shooterCoroutine = null;

    //Mouse Tracking Variables
    private float mouseXPosition, mouseYPosition;
    private Vector2 mouseDirection;
    private float lookAngle = 0;

    //Internal Methods
    private void Awake() {
        CheckCondimentArray();
    }

    private void CheckCondimentArray() {
        if (condimentArray == null || condimentArray.Length == 0) {
            Debug.LogWarning("No Condiments Assigned to Array in Shooter Script");
            enabled = false;
        }
    }

    private void Start() {
        InitializeToRandomCondiment();
    }

    private void InitializeToRandomCondiment() {
        int index = Random.Range(0, condimentArray.Length - 1);
        currentCondiment = condimentArray[index];
    }

    private void Update()
    {
        UpdateLookAngle();
        LookAtMouse();
        ShootRandomly();
    }

    private void UpdateLookAngle() {
        mouseXPosition = Input.mousePosition.x;
        mouseYPosition = Input.mousePosition.y;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(new Vector2(mouseXPosition, mouseYPosition));
        mouseDirection = (mousePosition - (Vector2) transform.position).normalized;
        lookAngle = (Mathf.Atan2(mouseDirection.y, mouseDirection.x) * Mathf.Rad2Deg) - 90;
    }

    private void LookAtMouse() {
        transform.rotation = Quaternion.AngleAxis(lookAngle, Vector3.forward);
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
                condiment.SetMoveDirection(mouseDirection);
            } else {
                Debug.LogError("Spawned Projectile Does Not Have Condiment Component");
            }
            yield return new WaitForSeconds(currentCondiment.GetShootDelay());
        }
    }
}
