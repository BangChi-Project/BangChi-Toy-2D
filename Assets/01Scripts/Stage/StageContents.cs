
    using System;
    using System.Collections.Generic;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;
    
    public class StageContents: MonoBehaviour
    {
        // List<StageContent> stageContents = new();
        [SerializeField] GameObject contentParent; // Parent
        [SerializeField] private GameObject conPrefab;
        [SerializeField] private RectTransform generatePoint;

        private void Start()
        {
            // SetContents();
        }

        public void SetContents()
        {
            List<StageData> stageList = GameManager.Instance.StageList.stages;
            foreach (var stage in stageList)
            {
                contentParent.GetComponent<RectTransform>().sizeDelta += new Vector2(400, 0);
                generatePoint.localPosition += new Vector3(400f, 0f, 0f); // Canvas RenderingMode is Camera -> localPosition
                var con = Instantiate(conPrefab, generatePoint.position, Quaternion.identity, contentParent.transform);
                con.GetComponent<StageContent>().Initialize(stage);
            }
        }
    }