

using UnityEngine;



public class MouseAim : MonoBehaviour
{
    // 要控制的骨骼
    public Transform spine;

    // 要Lookat的那个点，把它设置为角色的子物体，然后z轴拉远一点
    public Transform point;

    // 不断调试，获取具体旋转值，使角色正面朝向point
    public Vector3 spineAngle = new Vector3(139.48f, 88.85f, 11.3f);


    // 调试用的小图片
    public Texture2D targetAim;

    // LookAt的点距离屏幕中心最大的距离，也就是限制了脖子转向的最大角度
    public float distance = 300;
    // 鼠标指针所在的位置
    private Vector2 mousePoint;
    // 从哪个中心开始计算，我这里是用了屏幕中心
    private Vector2 center;
    // 图片展示的位置
    private Vector2 aimLoc;

    void Start()
    {
        // 获取中心点位置
        center = new Vector2(Screen.width / 2f, Screen.height / 2f);
    }

    void LateUpdate()
    {
        UpdatePosition();
    }

    private void UpdatePosition()
    {
        // 获取鼠标位置
        mousePoint = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        // 临时变量，如果没有超出distance距离，就用鼠标位置，
        // 否则用center到鼠标位置的单位向量×distance，再加上center坐标，就可以得到从center到鼠标位置被绳子拴住的一个点
        // （可以理解为center为圆心，distance为半径，计算这个点在圆里的坐标）
        // 数学不好还真不好理解- -!!!
        Vector2 temp = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        if (Vector2.Distance(mousePoint, center) > distance)
        {
            temp = (mousePoint - center).normalized * distance + center;
        }
        aimLoc = Camera.main.WorldToViewportPoint(Camera.main.ScreenToWorldPoint(new Vector3(temp.x, temp.y, Camera.main.farClipPlane)));

        if (point != null)
        {
            Vector3 pointPosition = point.localPosition;
            // 此处数值可能要稍作修改，主要取决于aimLoc在x和y的最大值和最小值，还有point的y轴（1.2f）
            pointPosition.x = (aimLoc.x - 0.5f) / 0.24f;
            pointPosition.y = (aimLoc.y - 0.5f) / 0.31f / 1.5f + 1.2f;
            point.localPosition = pointPosition;
            spine.LookAt(point, Vector3.up);
            spine.Rotate(spineAngle);
        }
    }

    // 在屏幕上绘制那个点，方便调试
    void OnGUI()
    {
        GUI.DrawTexture(new Rect(aimLoc.x * Screen.width - 8, Screen.height - (aimLoc.y * Screen.height) - 8, 16, 16), targetAim, ScaleMode.StretchToFill, true, 10.0f);
    }
}
