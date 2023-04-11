using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Centipede : MonoBehaviour
{

    public CentipedeSegment segmentPrefab;
    public Mushroom mushroomPrefab;
    public int size = 12;

    private List<CentipedeSegment> segments = new List<CentipedeSegment>();

    public Sprite[] headSprites;
    public Sprite[] bodySprites;

    public LayerMask collisionMask;
    public BoxCollider2D homeArea;
    public float speed = 1f;

    public ParticleSystem redParticles;
    public ParticleSystem greenParticles;
    public ParticleSystem whiteParticles;

    private int frameCounter = 0;
    private int currentSpriteIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        homeArea = GameObject.Find("HomeArea").GetComponent<BoxCollider2D>();
        speed *= FindObjectOfType<PlayerManager>().GetMultiplier();
        Respawn();
    }

    private void Update()
    {
        frameCounter++;

        if (frameCounter >= 6)
        {
            currentSpriteIndex++;
            if (currentSpriteIndex >= headSprites.Length)
            {
                currentSpriteIndex = 0;
            }
            foreach (CentipedeSegment segment in segments)
            {
                if (segment != null)
                {
                    if (segment.isHead)
                    {
                        segment.GetComponent<SpriteRenderer>().sprite = headSprites[currentSpriteIndex];
                    }
                    else
                    {
                        segment.GetComponent<SpriteRenderer>().sprite = bodySprites[currentSpriteIndex];
                    }
                }
            }
            frameCounter = 0;
        }
    }

    // Update is called once per frame
    private void Respawn()
    {
        foreach(CentipedeSegment segment in segments)
        {
            Destroy(segment.gameObject);
        }

        segments.Clear();

        for (int i = 0; i < size; i++)
        {
            Vector2 position = GridPosition(transform.position) + (Vector2.left * i);
            CentipedeSegment segment = Instantiate(segmentPrefab, position, Quaternion.identity);
            //segment.spriteRenderer.sprite = i == 0 ? headSprites[0] : bodySprites[0];
            segment.centipede = this;
            segments.Add(segment);
        }

        for (int i = 0; i < segments.Count; i++)
        {
            CentipedeSegment segment = segments[i];
            segment.ahead = GetSegmentAt(i - 1);
            segment.behind = GetSegmentAt(i + 1);
        }
    }

    private CentipedeSegment GetSegmentAt(int index)
    {
        if (index >= 0 && index < segments.Count)
        {
            return segments[index];
        } else
        {
            return null;
        }
    }

    public void Remove(CentipedeSegment segment, Vector3 segmentPosition)
    {
        Vector3 position = GridPosition(segment.transform.position);
        Instantiate(mushroomPrefab, position, Quaternion.identity);

        if (segment.ahead != null)
        {
            segment.ahead.behind = null;
        }

        if (segment.behind != null)
        {
            segment.behind.ahead = null;
            segment.behind.spriteRenderer.sprite = headSprites[0];
            segment.behind.UpdateHeadSegment();
        }

        segments.Remove(segment);

        if (segment.isHead)
        {
            FindObjectOfType<ScoreManager>().AddPoints(segment.baseScore * 10);
            FindObjectOfType<ParticleEffectsManager>().PlayParticleSystem(greenParticles, segmentPosition);
            FindObjectOfType<ParticleEffectsManager>().PlayParticleSystem(whiteParticles, segmentPosition);
            FindObjectOfType<ParticleEffectsManager>().PlayParticleSystem(redParticles, segmentPosition);
        } else
        {
            FindObjectOfType<ScoreManager>().AddPoints(segment.baseScore);
            FindObjectOfType<ParticleEffectsManager>().PlayParticleSystem(greenParticles, segmentPosition);
            FindObjectOfType<ParticleEffectsManager>().PlayParticleSystem(whiteParticles, segmentPosition);
        }
        Destroy(segment.gameObject);
        
        if(segments.Count == 0)
        {
            FindObjectOfType<CentipedeSpawner>().Spawn();
            FindObjectOfType<PlayerManager>().wavesCleared++;
            FindObjectOfType<FleaSpawner>().mushroomCountThreshold += 2;
            FindObjectOfType<Achievements>().UpdateCentipedes();
            Destroy(gameObject);
        }
    }

    private Vector2 GridPosition(Vector2 position)
    {
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);
        return position;
    }
}
