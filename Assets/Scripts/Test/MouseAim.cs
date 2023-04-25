

using UnityEngine;



public class MouseAim : MonoBehaviour
{
    // Ҫ���ƵĹ���
    public Transform spine;

    // ҪLookat���Ǹ��㣬��������Ϊ��ɫ�������壬Ȼ��z����Զһ��
    public Transform point;

    // ���ϵ��ԣ���ȡ������תֵ��ʹ��ɫ���泯��point
    public Vector3 spineAngle = new Vector3(139.48f, 88.85f, 11.3f);


    // �����õ�СͼƬ
    public Texture2D targetAim;

    // LookAt�ĵ������Ļ�������ľ��룬Ҳ���������˲���ת������Ƕ�
    public float distance = 300;
    // ���ָ�����ڵ�λ��
    private Vector2 mousePoint;
    // ���ĸ����Ŀ�ʼ���㣬��������������Ļ����
    private Vector2 center;
    // ͼƬչʾ��λ��
    private Vector2 aimLoc;

    void Start()
    {
        // ��ȡ���ĵ�λ��
        center = new Vector2(Screen.width / 2f, Screen.height / 2f);
    }

    void LateUpdate()
    {
        UpdatePosition();
    }

    private void UpdatePosition()
    {
        // ��ȡ���λ��
        mousePoint = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        // ��ʱ���������û�г���distance���룬�������λ�ã�
        // ������center�����λ�õĵ�λ������distance���ټ���center���꣬�Ϳ��Եõ���center�����λ�ñ�����˩ס��һ����
        // ���������ΪcenterΪԲ�ģ�distanceΪ�뾶�������������Բ������꣩
        // ��ѧ���û��治�����- -!!!
        Vector2 temp = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        if (Vector2.Distance(mousePoint, center) > distance)
        {
            temp = (mousePoint - center).normalized * distance + center;
        }
        aimLoc = Camera.main.WorldToViewportPoint(Camera.main.ScreenToWorldPoint(new Vector3(temp.x, temp.y, Camera.main.farClipPlane)));

        if (point != null)
        {
            Vector3 pointPosition = point.localPosition;
            // �˴���ֵ����Ҫ�����޸ģ���Ҫȡ����aimLoc��x��y�����ֵ����Сֵ������point��y�ᣨ1.2f��
            pointPosition.x = (aimLoc.x - 0.5f) / 0.24f;
            pointPosition.y = (aimLoc.y - 0.5f) / 0.31f / 1.5f + 1.2f;
            point.localPosition = pointPosition;
            spine.LookAt(point, Vector3.up);
            spine.Rotate(spineAngle);
        }
    }

    // ����Ļ�ϻ����Ǹ��㣬�������
    void OnGUI()
    {
        GUI.DrawTexture(new Rect(aimLoc.x * Screen.width - 8, Screen.height - (aimLoc.y * Screen.height) - 8, 16, 16), targetAim, ScaleMode.StretchToFill, true, 10.0f);
    }
}
