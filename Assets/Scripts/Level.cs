using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
   //this class contains level individual properties.
   // Level Count
   // Number of obstacles ST1 - ST2

   [Header("-Level Properties-")]
   [SerializeField] private int Stage1Obstacles;
   [SerializeField] private int Stage2Obstacles;
   [SerializeField] private List<Transform> obstacles;
   [SerializeField] private ColorController levelColors;
   private List<Vector3> defaultPositions;
   private List<Vector3> defaultRotations;

   private void Awake()
   {
      levelColors.UpdateColors(); //Load colors first.
      LoadDefaultPositions();
      ResetPositionsAndEnability();
   }

   private void Start()
   {
    
   }

   public void LoadDefaultPositions()
   {
      defaultPositions = new List<Vector3>();
      defaultRotations = new List<Vector3>();
      for (int i = 0; i < obstacles.Count; i++)
      {
         defaultPositions.Add(obstacles[i].position);
         defaultRotations.Add(obstacles[i].rotation.eulerAngles);
      }

     
   }


   public int Stage1Obstacles1
   {
      get => Stage1Obstacles;
      set => Stage1Obstacles = value;
   }

   public int Stage2Obstacles1
   {
      get => Stage2Obstacles;
      set => Stage2Obstacles = value;
   }

   public List<Vector3> GetObstaclesPosition()
   {
      return defaultPositions;
   }

   public List<Transform> GetLevelObstacles()
   {
      return obstacles;
   }


   public void ResetPositionsAndEnability()
   {
   
      for (int i = 0; i < obstacles.Count; i++)
      {
         
         
         obstacles[i].position = defaultPositions[i];
         obstacles[i].eulerAngles = defaultRotations[i];
         if (!obstacles[i].gameObject.activeInHierarchy)
         {
            obstacles[i].gameObject.SetActive(true);
         }

         obstacles[i].GetComponent<Rigidbody>().velocity = Vector3.zero;
      }
   }

   public void ResetPhysics(Collider gameAreaCollider,Collider GeneratedMeshCollider)
   {
      foreach (var obs in obstacles)
      {
         
         Physics.IgnoreCollision(obs.GetComponent<Collider>(), gameAreaCollider, false);
         Physics.IgnoreCollision(obs.GetComponent<Collider>(), GeneratedMeshCollider, true);
      }
   }

     
   
}
