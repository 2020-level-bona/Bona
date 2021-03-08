using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public const int TARGET_WIDTH = 1920;
    public const int TARGET_HEIGHT = 1080;
    const int PIXELS_PER_UNIT = 100;

    Camera cam;

    public Vector2Int backgroundSize {get; private set;} = new Vector2Int(TARGET_WIDTH, TARGET_HEIGHT);
    public Vector2Int cameraSize {get; private set;}

    List<ICameraOperator> cameraOperators;

    void Awake()
    {
        cam = FindObjectOfType<Camera>();

        GameObject composite = GameObject.Find("Composite");
        if (composite != null) {
            SpriteRenderer spriteRenderer = composite.GetComponent<SpriteRenderer>();
            backgroundSize = new Vector2Int(spriteRenderer.sprite.texture.width, spriteRenderer.sprite.texture.height);
            Debug.Log("Background resolution: " + backgroundSize);
        } else {
            Debug.LogWarning($"Composite 오브젝트를 발견하지 못했습니다. 배경 이미지를 가진 Composite 오브젝트를 생성해주세요. 기본 해상도는 {TARGET_WIDTH}x{TARGET_HEIGHT} 으로 설정됩니다.");
        }

        if (backgroundSize.x < TARGET_WIDTH || backgroundSize.y < TARGET_HEIGHT) {
            Debug.LogWarning($"배경의 해상도가 낮습니다! 권장 최저 해상도는 {TARGET_WIDTH}x{TARGET_HEIGHT} 입니다. 배경의 Import Settings > Max Size가 이미지의 해상도보다 낮은지 확인해주세요.");
            cam.orthographicSize = Mathf.Min((float) backgroundSize.y / PIXELS_PER_UNIT / 2f, (float) backgroundSize.x * ((float) TARGET_HEIGHT / TARGET_WIDTH) / PIXELS_PER_UNIT / 2f);
        } else {
            cam.orthographicSize = (float) TARGET_HEIGHT / PIXELS_PER_UNIT / 2f;
        }
        cameraSize = new Vector2Int((int) (cam.orthographicSize * 2f * PIXELS_PER_UNIT * ((float) TARGET_WIDTH / TARGET_HEIGHT)), (int) (cam.orthographicSize * 2f * PIXELS_PER_UNIT));

        cameraOperators = new List<ICameraOperator>();
    }

    void Update()
    {
        Vector2 cameraCenter = Vector2.zero;

        cameraOperators.RemoveAll(x => x.HasDone());

        foreach (ICameraOperator cameraOperator in cameraOperators) {
            cameraOperator.Update();

            if (cameraOperator.type == CameraOperatorType.REPLACE)
                cameraCenter = cameraOperator.GetCameraPosition();
            else
                cameraCenter += cameraOperator.GetCameraPosition();
        }

        // 카메라 오퍼레이터가 존재하지 않으면 카메라 위치를 변경하지 않는다.
        if (cameraOperators.Count == 0)
            return;

        cam.transform.position = new Vector3(cameraCenter.x, cameraCenter.y, cam.transform.position.z);
    }

    // 씬의 바운더리 안에 존재하도록 좌표를 클리핑한 결과를 반환한다.
    public Vector2 ClampCameraPosition(Vector2 cameraCenter) {
        float cameraWidth = cam.orthographicSize * ((float) TARGET_WIDTH / TARGET_HEIGHT) * 2f;
        float cameraHeight = cam.orthographicSize * 2f;

        float worldWidth = (float) backgroundSize.x / PIXELS_PER_UNIT;
        float worldHeight = (float) backgroundSize.y / PIXELS_PER_UNIT;

        cameraCenter.x = Mathf.Clamp(cameraCenter.x, (-worldWidth + cameraWidth) / 2f, (worldWidth - cameraWidth) / 2f);
        cameraCenter.y = Mathf.Clamp(cameraCenter.y, (-worldHeight + cameraHeight) / 2f, (worldHeight - cameraHeight) / 2f);

        return cameraCenter;
    }

    public void AddCameraOperator(ICameraOperator cameraOperator) {
        cameraOperators.Add(cameraOperator);
    }

    public void RemoveCameraOperator(ICameraOperator cameraOperator) {
        cameraOperators.Remove(cameraOperator);
    }
}
