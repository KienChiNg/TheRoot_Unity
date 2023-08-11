using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] private MoveCam MoveCam;
    [SerializeField] private ManagerGame managerGame;
    public void OpenGate(int indPort, bool stateAnim)
    {
        // if((gate.GetComponentsInChildren<Transform>().Length - 1 )/ 3 < indPort + 1) 
        //     return;
        // Debug.Log(gate.transform.childCount + " " + indPort);
        if (transform.childCount <= indPort)//Quá số lượng cổng có sẵn
            return;
        Transform ros0 = transform.GetChild(indPort).GetChild(0).transform;
        Transform ros1 = transform.GetChild(indPort).GetChild(1).transform;
        // Debug.Log(ros0.rotation.y * Mathf.Rad2Deg);
        if (!stateAnim)
        {
            Vector3 rot1 = ros0.rotation.eulerAngles;
            Vector3 rot2 = ros0.rotation.eulerAngles;
            ros0.rotation = Quaternion.Euler(new Vector3(rot1.x, rot1.y - 90, rot1.z));
            ros1.rotation = Quaternion.Euler(new Vector3(rot2.x, rot2.y - 90, rot2.z));
        }
        else
        {
            StartCoroutine(MoveToPosCam(indPort, ros0, ros1));
        }
    }
    IEnumerator MoveToPosCam(int ind, Transform ros0, Transform ros1)
    {
        MoveCam.SwitchCam(ind);
        managerGame.PauseGame = true;
        yield return new WaitForSeconds(MoveCam.BlendTime);
        StartCoroutine(RotationPort(ros0, true));
        StartCoroutine(RotationPort(ros1, false));
        StartCoroutine(ReturnMainCam());
        
    }
    IEnumerator ReturnMainCam(){
        yield return new WaitForSeconds(1.5f);
        MoveCam.SwitchCam(-1);
        managerGame.PauseGame = false;
    }
    IEnumerator RotationPort(Transform ros0, bool state)
    {
        int deg = state ? -90 : 90;
        float time = 0;
        Vector3 rot = ros0.rotation.eulerAngles;
        rot = new Vector3(rot.x, rot.y + deg, rot.z);
        while (time < 1)
        {
            ros0.rotation = Quaternion.Slerp(ros0.rotation, Quaternion.Euler(rot), time);
            time += 0.3f * Time.deltaTime;
            yield return null;
        }
    }
}
