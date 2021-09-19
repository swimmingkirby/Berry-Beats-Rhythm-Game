using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoleTransition : MonoBehaviour
{

    [SerializeField] GameObject polePrefab;
    [SerializeField] Transform startingPos;
    [SerializeField] GameObject levelSelectorManager;
    public bool canInstantiate = true;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void StartTransition()
    {
        canInstantiate = false;
        StartCoroutine(PoleTransitionCoroutine());
    }
    private IEnumerator PoleTransitionCoroutine()
    {
        GameObject pole = Instantiate(polePrefab, startingPos);
        yield return new WaitForSeconds(0.15f);
        levelSelectorManager.GetComponent<LevelSelectorManager>().ChangeCurrentLevel();
        yield return new WaitForSeconds(0.15f);
        canInstantiate = true;
        Destroy(pole);
    }

}
