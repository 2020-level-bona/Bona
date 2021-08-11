using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideshowCommand : IActionCommand
{
    string slideshowName;

    public const string Keyword = "SLIDESHOW";
    public bool Blocking => true;
    public int LineNumber {get;}

    public SlideshowCommand(string slideshowName) {
        this.slideshowName = slideshowName;
    }

    public SlideshowCommand(CommandLineParser lineParser) {
        LineNumber = lineParser.lineNumber;
        this.slideshowName = lineParser.GetString(1);
    }

    public IEnumerator GetCoroutine() {
        GameObject slideshowInstance = GameObject.FindObjectOfType<SlideshowRegistry>().CreateSlideshow(slideshowName);
        if (slideshowInstance == null) {
            Debug.LogError($"{slideshowName} 이름을 가진 슬라이드가 존재하지 않습니다.");
            yield return null;
        } else {
            yield return new WaitUntil(() => slideshowInstance == null);
        }
    }
}
