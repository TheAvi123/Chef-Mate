using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CutsceneManager : MonoBehaviour
{
    public GameObject Panel1;
    public GameObject Panel2;
    public GameObject Panel3;
    public GameObject Panel4;
    public GameObject Panel5;
    public GameObject Panel6;
    public GameObject Panel7;
    public GameObject Panel8;
    public GameObject Panel9;
    public GameObject Panel10;
    public float TimeBetweenPanels;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SwapPanels());
    }

    IEnumerator SwapPanels()
    {
        Panel1.SetActive(true);
        yield return new WaitForSeconds(TimeBetweenPanels);
        Panel1.SetActive(false);
        Panel2.SetActive(true);
        yield return new WaitForSeconds(TimeBetweenPanels);
        Panel2.SetActive(false);
        Panel3.SetActive(true);
        yield return new WaitForSeconds(TimeBetweenPanels);
        Panel3.SetActive(false);
        Panel4.SetActive(true);
        yield return new WaitForSeconds(TimeBetweenPanels);
        Panel4.SetActive(false);
        Panel5.SetActive(true);
        yield return new WaitForSeconds(TimeBetweenPanels);
        Panel5.SetActive(false);
        Panel6.SetActive(true);
        yield return new WaitForSeconds(TimeBetweenPanels);
        Panel6.SetActive(false);
        Panel7.SetActive(true);
        yield return new WaitForSeconds(TimeBetweenPanels);
        Panel7.SetActive(false);
        Panel8.SetActive(true);
        yield return new WaitForSeconds(TimeBetweenPanels);
        Panel8.SetActive(false);
        Panel9.SetActive(true);
        yield return new WaitForSeconds(TimeBetweenPanels);
        Panel9.SetActive(false);
        Panel10.SetActive(true);
        yield return new WaitForSeconds(TimeBetweenPanels);

        SceneManager.LoadScene("PlayScene");
    }
}
