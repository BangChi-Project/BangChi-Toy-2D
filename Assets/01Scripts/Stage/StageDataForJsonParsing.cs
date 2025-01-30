using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// 개별 스포너 데이터
[Serializable]
public class SpawnerData
{
    public int monsterId;
    public float delay;
    public Vector3 position;
}

// 개별 스테이지 데이터 (스테이지 ID + 스포너 리스트)
[Serializable]
public class StageData
{
    public int stageId;
    public string stageName;
    public List<SpawnerData> spawners;
}

// JSON 전체 데이터 구조
[Serializable]
public class StageDataList
{
    public List<StageData> stages; // JSON의 "stages" 키와 일치해야 함
}