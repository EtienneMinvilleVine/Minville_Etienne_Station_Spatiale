using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SanteJoueur : MonoBehaviour
{
    [Header("Gestion de la santé")]
    public float niveauOxygene = 100;
    public float niveauOxygeneMax = 100;
    public float perteOxygene = 0.1f;

    [Header("Sons")]
    [SerializeField] AudioClip sonMort;
    [SerializeField] AudioClip sonOxygene;
    [SerializeField] AudioClip sonBlesse;

    [Header("Composants")]
    ControlesJoueur controles;
    Animator animator;
    AudioSource audiosource;


    void Start()
    {
        controles = GetComponent<ControlesJoueur>();
        animator = GetComponent<Animator>();
        audiosource = GetComponent<AudioSource>();
    }

    void Update()
    {
    //    niveauOxygene -= perteOxygene * Time.deltaTime;
    RecevoirDegats(perteOxygene * Time.deltaTime);
    }
    public void RecevoirDegats(float degats)
    {
        niveauOxygene -= degats;
        // if (niveauOxygene < 0)
        // {
        //     niveauOxygene = 0;
        // }
        niveauOxygene = Mathf.Clamp(niveauOxygene, 0, niveauOxygeneMax);
        if (niveauOxygene <= 0 && controles.etatPerso != EtatPerso.MORT)
        {
            StartCoroutine(Mourir());
        }
    }

    public void Soigner(int soins)
    {
       niveauOxygene += soins;
       niveauOxygene = Mathf.Clamp(niveauOxygene, 0, niveauOxygeneMax);
    }


    public IEnumerator Mourir()
    {
        
        // Changer l'etat du personnage
        controles.etatPerso = EtatPerso.MORT;
        // Declencher un son de fin
        // Declencher l'animation de mort
        animator.SetTrigger("mort");
        // Quand l'animation est terminee, redemarrer le jeu
        yield return new WaitForSeconds(3);
        controles.RedemarrerPartie();
    }
}
