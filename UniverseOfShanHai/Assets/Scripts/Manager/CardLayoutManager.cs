using System.Collections.Generic;
using UnityEngine;

public class CardLayoutManager : Singleton<CardLayoutManager>
{
    public bool isHorizontal;
    public float maxWidth=7f;
    public float cardSpacing = 2f;
    public float angleBetweenCard = 7f;
    public float maxAngle = 30f;
    public float radius = 17f;
    public Vector3 centerPoint;

    [SerializeField]private List<Vector3> cardPositions = new();
    [SerializeField]private List<Quaternion> cardRotations = new();

    protected override void Awake() 
    {
        base.Awake();
        centerPoint = isHorizontal ? Vector3.up*-4.5f: Vector3.up*-21.5f;

    }
    public List<CardTransform> GetCardTransform(int totalCards)
    {
        List<CardTransform> cardTransforms = new();
        CalculatePosition(totalCards, isHorizontal);
        for(int index = 0; index < cardPositions.Count; index++)
        {
            cardTransforms.Add(new CardTransform(cardPositions[index], cardRotations[index]));
           
        }
        return cardTransforms;
    }
    public void CalculatePosition(int numOfCards, bool horizontal)
    {
        cardPositions.Clear();
        cardRotations.Clear();
        if(horizontal)
        {
            float currentWidth = cardSpacing * (numOfCards - 1);
            float totalWidth = Mathf.Min(currentWidth, maxWidth);

            float currentSpacing = totalWidth>0? totalWidth / (numOfCards - 1) : 0;

            for(int i = 0; i < numOfCards; i++)
            {
                float xPos = 0-(totalWidth / 2) + i * currentSpacing;
                var pos = new Vector3(xPos, centerPoint.y, 0);
                var rotation = Quaternion.identity;

                cardPositions.Add(pos);
                cardRotations.Add(rotation);
            }
        }
        else
        {
            float currentAngle = angleBetweenCard * (numOfCards - 1);
            float totalAngle = Mathf.Min(currentAngle, maxAngle);
            float currentSpacing = totalAngle>0? totalAngle / (numOfCards - 1) : 0;
            for(int i =0; i<numOfCards;i++)
            {
                var pos = FanCardPosition(totalAngle/2 - i * currentSpacing);
                var rotation = Quaternion.Euler(0, 0, totalAngle/2 - i * currentSpacing);
                cardPositions.Add(pos);
                cardRotations.Add(rotation);
            }
        }
    }

    private Vector3 FanCardPosition(float angle)
    {
        return new Vector3
        (
            centerPoint.x - Mathf.Sin(Mathf.Deg2Rad * angle) * radius, 
            centerPoint.y + Mathf.Cos(Mathf.Deg2Rad * angle) * radius,
            0
        );
    }
}
