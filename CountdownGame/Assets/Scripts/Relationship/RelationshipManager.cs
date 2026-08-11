namespace Relationship
{
    using UnityEngine;
    using System.Collections;
    using UnityEngine.UI;
    using TMPro;

    public class RelationshipManager : MonoBehaviour
    {
        private GameManager gameManager;

        [Header("Affection")]
        [SerializeField] private int maxAffection = 500;//Max Bar
        [SerializeField] public int currentAffection;


        [Header("Decay")]
        [SerializeField] private int decreaseOverTime = 1;
        [SerializeField] private float decreaseIntervalChange = 1; // how much faster it gets
        [SerializeField] private float decreaseInterval = 1f;
        private float baseDecreaseInterval;
        [SerializeField] private float decreaseDecreaseInterval = 5f;
        private float waitTime;

        [Header("UI")] 
        [SerializeField] private Slider affectionBar;
        [SerializeField] private TMP_Text affectionNumber;

        private bool partnerInteract = false;
        private Coroutine messageCoroutine;

        private void Start()
        {
            currentAffection = maxAffection;
            affectionNumber.SetText( maxAffection.ToString());
  
            affectionBar.maxValue = maxAffection;
            affectionBar.value = currentAffection;

            GameObject GameController = GameObject.FindGameObjectWithTag("GameManager");
            gameManager = GameController.GetComponent<GameManager>();

            baseDecreaseInterval = decreaseInterval;
        }

        private void Update()
        {
            if (currentAffection <= 0)
            {
                gameManager.GameOver();
            }
        }

        private IEnumerator AffectionDecayRoutine()
        {
            while (currentAffection > 0 && !partnerInteract)
            {
                yield return new WaitForSeconds(decreaseInterval);
                ModifyAffection(-decreaseOverTime);
            }
        }

        private IEnumerator DecayAccelerationRoutine()
        {
            while (currentAffection > 0 && !partnerInteract)
            {
                yield return new WaitForSeconds(decreaseDecreaseInterval);
                decreaseInterval -= decreaseIntervalChange;
            }
        }

        public void StartLoseAffection()
        {
            waitTime = 0f;
            partnerInteract = false;
            if (messageCoroutine == null)
            {
                messageCoroutine = StartCoroutine(AffectionDecayRoutine());
                StartCoroutine(DecayAccelerationRoutine());
            }
        }

        public void StopLoseAffection()
        {
            waitTime = 0;
            partnerInteract = true;
            decreaseInterval = baseDecreaseInterval;
            if (messageCoroutine != null)
            {
                StopCoroutine(messageCoroutine);
                StopCoroutine(DecayAccelerationRoutine());
                messageCoroutine = null;
            }
        }

        public void ModifyAffection(int amount)
        {
            currentAffection = Mathf.Clamp(currentAffection + amount, 0, maxAffection);
        
            affectionBar.value = currentAffection;
            affectionNumber.SetText(currentAffection.ToString());
            //Debug.Log("Current Affection:" + currentAffection);
        }


    }
}