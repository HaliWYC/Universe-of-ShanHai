using UnityEngine;
using TMPro;

public class PoppingUpDamageText : MonoBehaviour
{
    TextMeshProUGUI text;
    Color textColor;
    public Color damage;
    public Color defense;
    public Color healing;
    public Color strength;
    public Color deStrength;
    public Color shield;
    public Color deShield;
    public Color eliminate;
    public float moveUpSpeed;
    public Vector3 moveUpDir;
    public Vector3 moveDownDir = new Vector3(-0.7f, -1, 0);
    public float disappearTimer; // Time for the text to disappear;
    public float alphaSpeed;// Speed of change in alpha value;
    public float scaleSpeed;// Speed of change in scale value;
    public float directionScale;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (disappearTimer > directionScale * 0.5f)
        {
            //Move Up
            transform.position += moveUpDir * Time.deltaTime;
            moveUpDir += moveUpDir * (Time.deltaTime * moveUpSpeed);
            transform.localScale += Vector3.one * (Time.deltaTime * scaleSpeed);
        }
        else
        {
            //Move Down
            transform.position -= moveDownDir * Time.deltaTime;
            transform.localScale -= Vector3.one * (Time.deltaTime * scaleSpeed);
        }
        //Disappear
        disappearTimer -= Time.deltaTime;
        if (disappearTimer <= 0)
        {
            textColor.a -= Time.deltaTime * alphaSpeed;
            text.color = textColor;
            if (textColor.a < 0)
                Destroy(gameObject);
        }
    }

    public void SetUp(float value, Effect effect)
    {
        float direction = Random.Range(-0.75f, 0.75f);
        switch (effect)
        {
            case DamageEffect:
                text.SetText("- " + " " + value.ToString());
                textColor = damage;
                text.fontSize = 60;
                moveUpDir = new Vector3(direction, 2, 0);
                moveUpSpeed = 20f;
                break;
            case DefenseEffect:
                text.SetText(value.ToString());
                textColor = defense;
                text.fontSize = 60;
                moveUpDir = new Vector3(direction, 2, 0);
                moveUpSpeed = 15f;
                break;
            case HealEffect:
                text.SetText("+" + " " + value.ToString());
                textColor = healing;
                text.fontSize = 60;
                moveUpDir = new Vector3(direction, 2, 0);
                moveUpSpeed = 12f;
                break;
            case StrengthEffect:
                StrengthEffect Strength = effect as StrengthEffect;
                if (Strength.Positive)
                {
                    text.SetText("+ " + (value * 100).ToString() + "%");
                    textColor = strength;
                }
                else
                {
                    text.SetText("- " + (value * 100).ToString() + "%");
                    textColor = deStrength;
                }
                text.fontSize = 60;
                moveUpDir = new Vector3(direction, 2, 0);
                moveUpSpeed = 8f;
                break;
            case ShieldEffect:
                ShieldEffect Shield = effect as ShieldEffect;
                if (Shield.Positive)
                {
                    text.SetText("+ " + (value * 100).ToString() + "%");
                    textColor = shield;
                }
                else
                {
                    text.SetText("- " + (value * 100).ToString() + "%");
                    textColor = deShield;
                }
                text.fontSize = 60;
                moveUpSpeed = 8f;
                moveUpDir = new Vector3(direction, 2, 0);
                break;
            case EliminateEffect:
                text.SetText("Eliminate");
                textColor = eliminate;
                text.fontSize = 60;
                moveUpDir = new Vector3(direction, 2, 0);
                moveUpSpeed = 10f;
                break;
        }
        text.color = textColor;
        disappearTimer = directionScale;
    }

}

