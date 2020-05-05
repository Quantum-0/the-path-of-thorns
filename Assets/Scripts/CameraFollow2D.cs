using UnityEngine;
using UnityEngine.UI;

public class CameraFollow2D : MonoBehaviour
{
    [SerializeField] float damping = 1.5f;
    [SerializeField] Vector2 offset = new Vector2(2f, 1f);
    [SerializeField] bool faceLeft;
	private Transform player;
	private int lastX;

    Camera cam;
    [SerializeField] float newSize;
    bool sizeIsChanged;

    [Space]
    [SerializeField] Vector2 minCord;
    [SerializeField] Vector2 maxCord;

    public int numCristals;
    AudioSource AudioSrs;
    [SerializeField] Text txt;
    [SerializeField] GameObject cristalCanvas;

    void Start ()
    {
        numCristals = 0;
        txt.text = "";
        AudioSrs = GetComponent<AudioSource>();
        cristalCanvas.SetActive(false);

        cam = GetComponent<Camera>();
        offset = new Vector2(Mathf.Abs(offset.x), offset.y);
		FindPlayer(faceLeft);
    }

	public void FindPlayer(bool playerFaceLeft)
	{
		player = GameObject.FindGameObjectWithTag("Player").transform;
		lastX = Mathf.RoundToInt(player.position.x);

        Vector2 pp = player.position;

        /*
        if (pp.x > minCord.x && pp.y > minCord.y && pp.x < maxCord.x && pp.y < maxCord.y)
        {
            Debug.Log("player");

            if (playerFaceLeft)
            {
                transform.position = new Vector3(pp.x - offset.x, pp.y + offset.y, transform.position.z);
            }
            else
            {
                transform.position = new Vector3(pp.x + offset.x, pp.y + offset.y, transform.position.z);
            }
        }*/
	}

	void Update () 
	{
		if(player)
		{
			int currentX = Mathf.RoundToInt(player.position.x);
			if(currentX > lastX) faceLeft = false; else if(currentX < lastX) faceLeft = true;
			lastX = Mathf.RoundToInt(player.position.x);

			Vector3 target;
			if(faceLeft)
			{
				target = new Vector3(player.position.x - offset.x, player.position.y + offset.y, transform.position.z);
			}
			else
			{
				target = new Vector3(player.position.x + offset.x, player.position.y + offset.y, transform.position.z);
			}

            Vector3 currentPosition = Vector3.Lerp(transform.position, target, damping * Time.deltaTime);

            if (currentPosition.x > minCord.x && currentPosition.x < maxCord.x)
            {
                transform.position = new Vector3(currentPosition.x, transform.position.y, transform.position.z);
            }
            if (currentPosition.y > minCord.y && currentPosition.y < maxCord.y)
            {
                transform.position = new Vector3(transform.position.x, currentPosition.y, transform.position.z);
            }

        }

        if (cam.orthographicSize == newSize)
        {
            sizeIsChanged = false;
        }

        if (sizeIsChanged)
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, newSize, Time.deltaTime * 3);
        }
    }

    public void ChangeSize(float _newSize)
    {
        sizeIsChanged = true;
        newSize = _newSize;
    }

    public void addCreistals()
    {
        numCristals += 1;

        cristalCanvas.SetActive(true);

        if (PlayerPrefs.GetInt("sound") > 0)
        {
            AudioSrs.Play();
        }

        txt.text = numCristals.ToString();
    }
}
