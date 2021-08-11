using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideshowRegistry : MonoBehaviour
{
    public Slideshow[] registry;

    public GameObject CreateSlideshow(string name) {
        foreach (Slideshow slideshow in registry) {
            if (slideshow.slideshowName == name) {
                return Instantiate(slideshow.gameObject);
            }
        }
        return null;
    }
}
