using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSlider : MonoBehaviour
{
    [Tooltip("Draw here your sprites.")]
    [SerializeField]
    private List<Sprite> spritesToSlide = new List<Sprite>();

    [Tooltip("Do you want the sprites to switch randomly?")]
    [SerializeField]
    private bool slideSpritesRandomly = true;

    [Tooltip("How much time must remain between each slides?")]
    [SerializeField]
    private float sliderDelay = 10.5f;

    [Tooltip("Only one of the two components must have this checked.")]
    [SerializeField]
    private bool isTheActiveSlideComponent = false;

    [Tooltip("Set this serializable only if isTheSliderComponent is checked.")]
    [SerializeField]
    private Animator spriteSliderAnimator = null;

    [Tooltip("Set this serializable only if isTheSliderComponent is checked.")]
    [SerializeField]
    private GameObject inactiveSpriteSliderGameObject = null;

    private SpriteRenderer spriteRenderer;
    private SpriteSlider inactiveSpriteSlider;
    private int curIndex;
    private float lastReset;
    private bool fadesOut;
    private bool hasChanged;
    private bool sliderIsPaused;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (isTheActiveSlideComponent)
        {
            inactiveSpriteSlider = inactiveSpriteSliderGameObject.GetComponentInChildren<SpriteSlider>();

            if (inactiveSpriteSlider == null)
            {
                Debug.Log("The SpriteSlider in the serializable was not found! Please fix this!");
            }
        }

        if (spriteRenderer == null)
        {
            Debug.Log("The sprite component wasn't found! Please fix this!");
        }
    }

    private void Start()
    {
        curIndex = -1;
        fadesOut = true;
        sliderIsPaused = false;

        ResetSliderDelay();
        UpdateSlide();
    }

    private void Update()
    {
        if (!sliderIsPaused && SliderDelayPassed())
        {
            sliderIsPaused = true;
            Slide();
        }
    }

    private void ChangeSlideIndex()
    {
        if (spriteRenderer != null)
        {
            if (slideSpritesRandomly)
            {
                int lastIndex = curIndex;
                curIndex = (int)Random.Range(0, spritesToSlide.Count);

                while (curIndex == lastIndex)
                {
                    curIndex = (int)Random.Range(0, spritesToSlide.Count);
                }
            }
            else
            {
                ++curIndex;
                if (curIndex > spritesToSlide.Count)
                {
                    curIndex = 0;
                }
            }
        }
    }

    public void UpdateSlide()
    {
        ChangeSlideIndex();
        spriteRenderer.sprite = spritesToSlide.ToArray()[curIndex];
    }

    private bool SliderDelayPassed()
    {
        return (lastReset + sliderDelay) <= Time.time;
    }

    private void ResetSliderDelay()
    {
        lastReset = Time.time;
        sliderIsPaused = false;
    }

    private void Slide()
    {        
        if (isTheActiveSlideComponent)
        {
            spriteSliderAnimator.SetBool("CanFadeOut", fadesOut == true);
            spriteSliderAnimator.SetBool("CanFadeIn", fadesOut == false);
            fadesOut = !fadesOut;
        }
    }

    public void NotifyCanChange()
    {
        CleanAnimator();
        UpdateSlide();
        ResetSliderDelay();
    }

    public void NotifyTwinSlider()
    {
        CleanAnimator();
        inactiveSpriteSlider.UpdateSlide();
        ResetSliderDelay();
    }

    private void CleanAnimator()
    {
        spriteSliderAnimator.SetBool("CanFadeIn", false);
        spriteSliderAnimator.SetBool("CanFadeOut", false);
    }
}
