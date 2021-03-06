﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //UI使用する
using GoogleARCore.Examples.HelloAR; //名前空間を取得する
 
 public class UIcontroller : MonoBehaviour
 {
   GameObject AR; //HelloARcontrollerを入れる変数
   GameObject distance; //UIを入れる
   
   HelloARController script;　//HelloARControllerが入る変数
   // Start is called before the first frame update
   void Start()
   {
      this.distance = GameObject.Find("Distance");
      this.AR = GameObject.Find("HelloAR Controller");
      script = AR.GetComponent<HelloARController>();
      //GameObjectのHelloAR Controllerの中にあるHelloARController.csを取得して変数に格納
   
   }
   // Update is called once per frame
   void Update()
   {
      //HelloARController.csから座標を取得
      List<GameObject> pos_list = script.list_toggle_;
      if ( pos_list.Count < 2 ) {
         this.distance.GetComponent<Text>().text = "";
         return;
      }

      Vector3 p1 = pos_list[pos_list.Count - 2].transform.position;
      Vector3 p2 = pos_list[pos_list.Count - 1].transform.position;

      //長さを計算
      float length = (p1 - p2).magnitude;
      //UIのdistanceを変更
      this.distance.GetComponent<Text>().text = (length * 100f).ToString("F2") + "cm";
   }
}