using UnityEngine;
using System.Collections;

/// <summary>
/// Performance test script for Unity on Linux
/// Tests various performance-critical operations
/// </summary>
public class PerformanceTest : MonoBehaviour
{
    [Header("Performance Test Settings")]
    public int iterationCount = 10000;
    public bool runOnStart = false;
    
    [Header("Results")]
    [SerializeField] private float lastTestDuration = 0f;
    [SerializeField] private float averageFPS = 0f;
    
    private float fpsSum = 0f;
    private int fpsCount = 0;
    
    void Start()
    {
        if (runOnStart)
        {
            StartCoroutine(RunPerformanceTests());
        }
        
        // Start FPS monitoring
        InvokeRepeating(nameof(MonitorFPS), 1f, 1f);
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            StartCoroutine(RunPerformanceTests());
        }
    }
    
    [ContextMenu("Run Performance Tests")]
    public void RunPerformanceTestsMenu()
    {
        StartCoroutine(RunPerformanceTests());
    }
    
    IEnumerator RunPerformanceTests()
    {
        Debug.Log("=== Performance Tests Starting ===");
        Debug.Log("Press 'P' to run performance tests anytime");
        
        yield return StartCoroutine(TestMathOperations());
        yield return StartCoroutine(TestObjectInstantiation());
        yield return StartCoroutine(TestCollectionOperations());
        yield return StartCoroutine(TestStringOperations());
        yield return StartCoroutine(TestPhysicsOperations());
        
        Debug.Log("=== Performance Tests Complete ===");
        Debug.Log($"Average FPS during tests: {averageFPS:F1}");
    }
    
    IEnumerator TestMathOperations()
    {
        Debug.Log("Testing math operations performance...");
        
        float startTime = Time.realtimeSinceStartup;
        
        for (int i = 0; i < iterationCount; i++)
        {
            // Complex math operations
            float result = Mathf.Sin(i) * Mathf.Cos(i) + Mathf.Sqrt(i * 0.1f);
            Vector3 v1 = new Vector3(i, i * 0.5f, i * 0.25f);
            Vector3 v2 = new Vector3(i * 0.1f, i * 0.2f, i * 0.3f);
            Vector3 cross = Vector3.Cross(v1, v2);
            float dot = Vector3.Dot(v1, v2);
            
            // Prevent optimization
            if (result > float.MaxValue) Debug.Log("Impossible");
            
            if (i % 1000 == 0) yield return null; // Yield every 1000 iterations
        }
        
        float duration = Time.realtimeSinceStartup - startTime;
        Debug.Log($"Math Operations: {iterationCount} iterations in {duration:F3}s ({iterationCount/duration:F0} ops/sec)");
        lastTestDuration = duration;
    }
    
    IEnumerator TestObjectInstantiation()
    {
        Debug.Log("Testing object instantiation performance...");
        
        GameObject prefab = GameObject.CreatePrimitive(PrimitiveType.Cube);
        prefab.SetActive(false);
        
        float startTime = Time.realtimeSinceStartup;
        var objects = new GameObject[iterationCount / 100]; // Reduce count for instantiation
        
        for (int i = 0; i < objects.Length; i++)
        {
            objects[i] = Instantiate(prefab);
            objects[i].transform.position = Random.insideUnitSphere * 100f;
            
            if (i % 10 == 0) yield return null;
        }
        
        float duration = Time.realtimeSinceStartup - startTime;
        Debug.Log($"Object Instantiation: {objects.Length} objects in {duration:F3}s ({objects.Length/duration:F0} objects/sec)");
        
        // Cleanup
        foreach (var obj in objects)
        {
            if (obj != null) DestroyImmediate(obj);
        }
        DestroyImmediate(prefab);
    }
    
    IEnumerator TestCollectionOperations()
    {
        Debug.Log("Testing collection operations performance...");
        
        float startTime = Time.realtimeSinceStartup;
        
        var list = new System.Collections.Generic.List<int>();
        var dict = new System.Collections.Generic.Dictionary<int, string>();
        
        for (int i = 0; i < iterationCount; i++)
        {
            // List operations
            list.Add(i);
            if (list.Count > 100)
            {
                list.RemoveAt(0);
            }
            
            // Dictionary operations
            dict[i % 1000] = $"Item_{i}";
            
            if (i % 1000 == 0) yield return null;
        }
        
        float duration = Time.realtimeSinceStartup - startTime;
        Debug.Log($"Collection Operations: {iterationCount} operations in {duration:F3}s ({iterationCount/duration:F0} ops/sec)");
    }
    
    IEnumerator TestStringOperations()
    {
        Debug.Log("Testing string operations performance...");
        
        float startTime = Time.realtimeSinceStartup;
        
        for (int i = 0; i < iterationCount / 10; i++) // Reduce for string ops
        {
            string test = $"Unity Linux Test {i}";
            string upper = test.ToUpper();
            string[] parts = test.Split(' ');
            string joined = string.Join("-", parts);
            
            // String manipulation
            if (joined.Contains("Unity") && upper.Length > 0)
            {
                // Prevent optimization
            }
            
            if (i % 100 == 0) yield return null;
        }
        
        float duration = Time.realtimeSinceStartup - startTime;
        Debug.Log($"String Operations: {iterationCount/10} operations in {duration:F3}s ({(iterationCount/10)/duration:F0} ops/sec)");
    }
    
    IEnumerator TestPhysicsOperations()
    {
        Debug.Log("Testing physics operations performance...");
        
        float startTime = Time.realtimeSinceStartup;
        
        for (int i = 0; i < iterationCount / 100; i++)
        {
            // Physics calculations
            Vector3 origin = new Vector3(i * 0.1f, 10f, i * 0.1f);
            Vector3 direction = Vector3.down;
            
            // Raycast test (without actually casting)
            Ray ray = new Ray(origin, direction);
            
            // Physics math
            float gravity = Physics.gravity.magnitude;
            float fallTime = Mathf.Sqrt(2 * 10f / gravity);
            Vector3 landingPos = origin + direction * (0.5f * gravity * fallTime * fallTime);
            
            if (i % 10 == 0) yield return null;
        }
        
        float duration = Time.realtimeSinceStartup - startTime;
        Debug.Log($"Physics Operations: {iterationCount/100} operations in {duration:F3}s ({(iterationCount/100)/duration:F0} ops/sec)");
    }
    
    void MonitorFPS()
    {
        float currentFPS = 1f / Time.deltaTime;
        fpsSum += currentFPS;
        fpsCount++;
        averageFPS = fpsSum / fpsCount;
        
        // Reset every 60 samples to prevent overflow
        if (fpsCount >= 60)
        {
            fpsSum = averageFPS;
            fpsCount = 1;
        }
    }
    
    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 300, 200));
        GUILayout.Label($"FPS: {(1f / Time.deltaTime):F1}");
        GUILayout.Label($"Average FPS: {averageFPS:F1}");
        GUILayout.Label($"Last Test Duration: {lastTestDuration:F3}s");
        GUILayout.Label("Press 'P' for Performance Tests");
        GUILayout.EndArea();
    }
}