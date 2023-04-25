using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test01 : MonoBehaviour
{
    /*
     * 
     * ��ս�������߼��
     * 
     */
    public Transform pointA;
    public Transform pointB;
    public LayerMask layer;
    int hitCount;
    public Transform[] Points; //���߷����
    public Dictionary<int, Vector3> dic_lastPoints = new Dictionary<int, Vector3>(); //����ϸ�λ����Ϣ
    public GameObject particle;//����Ч��
    private void Start()
    {
        if (dic_lastPoints.Count == 0)
        {
            for (int i = 0; i < Points.Length; i++)
            {
                dic_lastPoints.Add(Points[i].GetHashCode(), Points[i].position);
            }
        }
    }
    private void LateUpdate()
    {
        var newA = pointA.position;
        var newB = pointB.position;
        Debug.DrawLine(newA, newB, Color.red, 1f);
        SetPostion(Points);
    }


    void SetPostion(Transform[] points)
    {
        for (int i = 0; i < points.Length; i++)
        {
            var nowPos = points[i];
            dic_lastPoints.TryGetValue(nowPos.GetHashCode(), out Vector3 lastPos);
            //Debug.DrawLine(nowPos.position, lastPos, Color.blue, 1f); ;
            Debug.DrawRay(lastPos, nowPos.position - lastPos, Color.blue, 1f);

            Ray ray = new Ray(lastPos, nowPos.position - lastPos);
            RaycastHit[] raycastHits = new RaycastHit[6];
            Physics.RaycastNonAlloc(ray, raycastHits, Vector3.Distance(lastPos, nowPos.position), layer, QueryTriggerInteraction.Ignore);

            foreach (var item in raycastHits)
            {
                if (item.collider == null) continue;
                //���������к��һЩ�жϺʹ���
                //�����Ѫ֮���,
                //��Ҫע��:��ͬһ֡���λ���һ������
                Debug.Log(item.collider.name);
                if (particle)
                {
                    var go = Instantiate(particle, item.point, Quaternion.identity);
                    Destroy(go, 3f);
                }
                hitCount++;
                break;
            }

            if (nowPos.position != lastPos)
            {
                dic_lastPoints[nowPos.GetHashCode()] = nowPos.position;//�����ϸ�λ����Ϣ
            }
        }
    }
    


    private void OnGUI()
    {
        // ����
        var labelstyle = new GUIStyle();
        labelstyle.fontSize = 32;
        labelstyle.normal.textColor = Color.white;
        int height = 40;
        GUIContent[] contents = new GUIContent[]
        {
               new GUIContent($"hitCount:{hitCount}"),
               new GUIContent($"frameCount:{Time.frameCount }"),
         };
        for (int i = 0; i < contents.Length; i++)
        {
            GUI.Label(new Rect(0, height * i, 180, 80), contents[i], labelstyle);
        }

    }

}
