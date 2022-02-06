using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

    [Header("Prefab")]
    public Transform dragonParent;
    [SerializeField]
    private GameObject redDragonPrefab;
    [SerializeField]
    private GameObject greenDragonPrefab;
    [SerializeField]
    private Transform chikenParent;
    [SerializeField]
    private GameObject chikenPrefab;
    [SerializeField]
    private Transform cowParent;
    [SerializeField]
    private GameObject cowPrefab;
    [SerializeField]
    private Transform zoneParent;
    [SerializeField]
    private GameObject foodPrefab;
    [SerializeField]
    private GameObject waterPrefab;
    [SerializeField]
    private GameObject cloudPrefab;

    private readonly int DRAGON_SPAWN_HEIGHT = 7;
    private readonly int ANNIMAL_SPAWN_HEIGHT = 4;
    private readonly int ZONE_SPAWN_HEIGHT = 0;
    private Vector3 v = Vector3.zero;

    private List<an.Annimal> chikenList = new List<an.Annimal>();
    private List<an.Annimal> cowList = new List<an.Annimal>();
    private List<zone.Zone> foodList = new List<zone.Zone>();
    private List<zone.Zone> waterList = new List<zone.Zone>();

    private static SpawnManager instance;
    // Access point
    public static SpawnManager Instance { get => instance;}
    private SpawnManager() { } //block the use of new()
    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
        } else {
            instance = this;
        }

        InvokeRepeating(nameof(SpawnClouds), 0.0f, 2f);
    }

    public List<zone.Zone> GetZonesList() {
        List<zone.Zone> zList = new List<zone.Zone>();
        zList.AddRange(waterList);
        zList.AddRange(foodList);
        return zList;
    }
    public List<an.Annimal> GetAnnimalList() {
        List<an.Annimal> aList = new List<an.Annimal>();
        aList.AddRange(chikenList);
        aList.AddRange(cowList);
        return aList;
    }

    public void SpawnRedDragon() {
        Vector3 loc = Util.GetRandomLoc(DRAGON_SPAWN_HEIGHT);
        GameObject baby = Instantiate(redDragonPrefab, loc, Quaternion.identity);
        baby.transform.parent = dragonParent;
    }
    public void SpawnGreenDragon() {
        Vector3 loc = Util.GetRandomLoc(DRAGON_SPAWN_HEIGHT);
        GameObject baby = Instantiate(greenDragonPrefab, loc, Quaternion.identity);
        baby.transform.parent = dragonParent;
    }
    public void SpawnChicken() {
        Vector3 loc = Util.GetRandomLoc(ANNIMAL_SPAWN_HEIGHT);
        for (int i = 0; i < 3; i++) {
            GameObject baby = Instantiate(chikenPrefab, loc, Quaternion.identity);
            baby.transform.parent = chikenParent;
            chikenList.Add(baby.GetComponents<an.Annimal>()[0]);
        }

    }
    public void SpawnCow() {
        Vector3 loc = Util.GetRandomLoc(ANNIMAL_SPAWN_HEIGHT);
        for (int i = 0; i < 3; i++) {
            GameObject baby = Instantiate(cowPrefab, loc, Quaternion.identity);
            baby.transform.parent = cowParent;
            cowList.Add(baby.GetComponents<an.Annimal>()[0]);
        }
    }
    public void SpawnFood() {
        Vector3 loc = Util.GetRandomLoc(ZONE_SPAWN_HEIGHT, true);
        GameObject baby = Instantiate(foodPrefab, loc, Quaternion.identity);
        baby.transform.parent = zoneParent;
        foodList.Add(baby.GetComponents<zone.Zone>()[0]);
    }
    public void SpawnWater() {
        Vector3 loc = Util.GetRandomLoc(ZONE_SPAWN_HEIGHT, true);
        GameObject baby = Instantiate(waterPrefab, loc, Quaternion.identity);
        baby.transform.parent = zoneParent;
        waterList.Add(baby.GetComponents<zone.Zone>()[0]);
    }


    private void SpawnClouds() {
        float cardinalPoints = Random.Range(0,4);
        float x = 0;
        float z = 0;
        Vector3 velocity = v;
        switch (cardinalPoints) {
            case 0:
                x = Random.Range(-23, 24);
                z = -40;
                velocity = new Vector3(0, 0, 10);
                break;
            case 1:
                x = Random.Range(-23, 24);
                z = 40;
                velocity = new Vector3(0, 0, -10);
                break;
            case 2:
                x = -40;
                z = Random.Range(-23, 24);
                velocity = new Vector3(10, 0,0);
                break;
            case 3:
                x = 40;
                z = Random.Range(-23, 24);
                velocity = new Vector3(-10, 0, 0);
                break;
        }
        GameObject baby = Instantiate(cloudPrefab, new Vector3(x, DRAGON_SPAWN_HEIGHT, z), Quaternion.identity);
        baby.transform.parent = zoneParent;
        baby.GetComponent<Rigidbody>().velocity = velocity;
        Color color = baby.GetComponent<MeshRenderer>().material.color;
        color.a = 0.3f;
        baby.GetComponent<MeshRenderer>().material.color = color;
    }
}
