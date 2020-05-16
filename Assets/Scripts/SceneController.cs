using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class SceneController : MonoBehaviour
{
    [SerializeField] private MemoryCard originalCard;
    [SerializeField] private Sprite[] images;
    [SerializeField] private TextMeshPro scoreLabel;

    private const int gridRows = 2;
    private const float offsetX = 2f;
    private const float offsetY = 2.5f;

    private MemoryCard _firstRevealed;
    private MemoryCard _secondRevealed;
    private static int _score = 0;
    private static bool created = false;

    private const string SCORE_LABEL = "Очки: ";

    void Awake()
    {
        if (!created)
        {
            DontDestroyOnLoad(this.gameObject);
            created = true;
        }
    }
    void Start()
    {
        scoreLabel.text = SCORE_LABEL + _score;
        Vector3 startPos = originalCard.transform.position;
        int gridCols = images.Length;
        int[] numbers = shuffleArray(defaultCards(gridCols));
        for (int i = 0; i < gridCols; i++)
        {
            for (int j = 0; j < gridRows; j++)
            {
                MemoryCard card;
                if (i == 0 && j == 0)
                {
                    card = originalCard;
                } else
                {
                    card = Instantiate(originalCard) as MemoryCard;
                }
                int index = j * gridCols + i;
                int id = numbers[index];
                card.setCard(id, images[id]);
                float posX = (offsetX * i) + startPos.x;
                float posY = -(offsetY * j) + startPos.y;
                card.transform.position = new Vector3(posX, posY, startPos.z);
            }
        }
    }

    private int[] shuffleArray(int[] numbers)
    {
        int[] newArray = numbers.Clone() as int[];
        for (int i = 0; i < newArray.Length; i++)
        {
            int tmp = newArray[i];
            int r = Random.Range(i, newArray.Length);
            newArray[i] = newArray[r];
            newArray[r] = tmp;
        }

        return newArray;
    }

    public bool canReveal
    {
        get { return null == _secondRevealed; }
    }

    public void cardRevealed(MemoryCard card)
    {
        if (_firstRevealed == null)
        {
            _firstRevealed = card;
        } else
        {
            _secondRevealed = card;
            StartCoroutine(checkMatch());
        }
    }

    private IEnumerator checkMatch()
    {
        if (_firstRevealed.id == _secondRevealed.id)
        {
            _score++;
            scoreLabel.text = SCORE_LABEL + _score;
        } else
        {
            yield return new WaitForSeconds(.5f);

            _firstRevealed.unreveal();
            _secondRevealed.unreveal();
        }
        _firstRevealed = null;
        _secondRevealed = null;
    }

    public void restart ()
    {
        SceneManager.LoadScene("SampleScene");
    }

    private int[] defaultCards(int countCardTypes)
    {
        int countCards = countCardTypes * 2;
        int[] cards = new int[countCards];
        for (int i=0; i<countCards; i++)
        {
            cards[i] = i / 2;
        }
        

        return cards;
    }
}
