using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
  //看的目标
  [SerializeField] Transform target;
  //target偏移量
  [SerializeField] Vector3 offset;
  //相机移动過渡速度
  [SerializeField] float transitionSpeed = 2.0f;

  //玩家移動在update中執行，相機要跟隨在玩家後，所以用lateupdate
  void LateUpdate()
  {
    //養成好習慣？
    if (target != null)
    {
      Vector3 targetPosition = target.position + offset;
      //相機腳本中：相机要平滑移動到的位置:Lerp(当前位置，目标位置，移动速度)
      transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * transitionSpeed);
    }

  }


}
