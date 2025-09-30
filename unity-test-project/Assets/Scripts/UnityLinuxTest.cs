using UnityEngine;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Unity Linux .NET Test Script
/// Tests various .NET features to ensure compatibility with Unity on Linux
/// </summary>
public class UnityLinuxTest : MonoBehaviour
{
    [Header("Test Configuration")]
    public bool runTestsOnStart = true;
    public bool enableDebugOutput = true;
    
    [Header("Test Results")]
    [SerializeField] private bool allTestsPassed = false;
    [SerializeField] private int totalTests = 0;
    [SerializeField] private int passedTests = 0;
    
    private List<string> testResults = new List<string>();
    
    void Start()
    {
        if (runTestsOnStart)
        {
            RunAllTests();
        }
    }
    
    [ContextMenu("Run All Tests")]
    public void RunAllTests()
    {
        testResults.Clear();
        totalTests = 0;
        passedTests = 0;
        
        Log("=== Unity Linux .NET Compatibility Tests ===");
        Log($"Unity Version: {Application.unityVersion}");
        Log($"Platform: {Application.platform}");
        Log($"System Language: {Application.systemLanguage}");
        Log("");
        
        // Run individual tests
        TestBasicTypes();
        TestCollections();
        TestLinq();
        TestStringManipulation();
        TestMathOperations();
        TestDateTimeOperations();
        TestFileSystem();
        TestUnitySpecific();
        
        // Calculate results
        allTestsPassed = passedTests == totalTests;
        
        Log("");
        Log("=== Test Summary ===");
        Log($"Total Tests: {totalTests}");
        Log($"Passed: {passedTests}");
        Log($"Failed: {totalTests - passedTests}");
        Log($"Success Rate: {(passedTests * 100.0f / totalTests):F1}%");
        Log($"All Tests Passed: {allTestsPassed}");
        
        if (allTestsPassed)
        {
            Log("🎉 Unity .NET integration is working perfectly on Linux!");
        }
        else
        {
            Log("⚠️ Some tests failed. Check the console for details.");
        }
    }
    
    void TestBasicTypes()
    {
        RunTest("Basic Types", () =>
        {
            int intVal = 42;
            float floatVal = 3.14159f;
            string stringVal = "Hello Unity Linux!";
            bool boolVal = true;
            
            return intVal == 42 && 
                   Mathf.Approximately(floatVal, 3.14159f) && 
                   stringVal == "Hello Unity Linux!" && 
                   boolVal == true;
        });
    }
    
    void TestCollections()
    {
        RunTest("Collections (List, Dictionary)", () =>
        {
            var list = new List<int> { 1, 2, 3, 4, 5 };
            var dict = new Dictionary<string, int>
            {
                {"unity", 2025},
                {"linux", 1991},
                {"dotnet", 2016}
            };
            
            return list.Count == 5 && 
                   list.Contains(3) && 
                   dict["unity"] == 2025 &&
                   dict.ContainsKey("linux");
        });
    }
    
    void TestLinq()
    {
        RunTest("LINQ Operations", () =>
        {
            var numbers = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            var evenNumbers = numbers.Where(n => n % 2 == 0).ToList();
            var sum = numbers.Sum();
            var average = numbers.Average();
            
            return evenNumbers.Count == 5 && 
                   sum == 55 && 
                   Mathf.Approximately((float)average, 5.5f);
        });
    }
    
    void TestStringManipulation()
    {
        RunTest("String Operations", () =>
        {
            string original = "Unity on Linux with .NET 8";
            string upper = original.ToUpper();
            string[] parts = original.Split(' ');
            string joined = string.Join("-", parts);
            
            return upper.Contains("UNITY") && 
                   parts.Length == 6 && 
                   joined.Contains("Unity-on-Linux");
        });
    }
    
    void TestMathOperations()
    {
        RunTest("Math Operations", () =>
        {
            float sin45 = Mathf.Sin(45f * Mathf.Deg2Rad);
            float cos0 = Mathf.Cos(0f);
            Vector3 cross = Vector3.Cross(Vector3.right, Vector3.up);
            
            return Mathf.Approximately(sin45, 0.707f, 0.01f) && 
                   Mathf.Approximately(cos0, 1f) && 
                   cross == Vector3.forward;
        });
    }
    
    void TestDateTimeOperations()
    {
        RunTest("DateTime Operations", () =>
        {
            var now = System.DateTime.Now;
            var utcNow = System.DateTime.UtcNow;
            var timeSpan = System.TimeSpan.FromHours(1);
            var future = now.Add(timeSpan);
            
            return now.Year >= 2025 && 
                   future > now && 
                   timeSpan.TotalMinutes == 60;
        });
    }
    
    void TestFileSystem()
    {
        RunTest("File System Access", () =>
        {
            try
            {
                string tempPath = System.IO.Path.GetTempPath();
                string testFile = System.IO.Path.Combine(tempPath, "unity_test.tmp");
                
                // Write and read test
                System.IO.File.WriteAllText(testFile, "Unity Linux Test");
                string content = System.IO.File.ReadAllText(testFile);
                
                // Cleanup
                if (System.IO.File.Exists(testFile))
                    System.IO.File.Delete(testFile);
                
                return content == "Unity Linux Test";
            }
            catch
            {
                return false;
            }
        });
    }
    
    void TestUnitySpecific()
    {
        RunTest("Unity Specific Features", () =>
        {
            // Test GameObject creation
            GameObject testObj = new GameObject("TestObject");
            testObj.transform.position = new Vector3(1, 2, 3);
            
            // Test component access
            bool hasTransform = testObj.GetComponent<Transform>() != null;
            
            // Cleanup
            if (Application.isPlaying)
                DestroyImmediate(testObj);
            else
                DestroyImmediate(testObj);
            
            return hasTransform && testObj.transform.position == new Vector3(1, 2, 3);
        });
    }
    
    void RunTest(string testName, System.Func<bool> testFunction)
    {
        totalTests++;
        try
        {
            bool result = testFunction.Invoke();
            if (result)
            {
                passedTests++;
                Log($"✅ {testName}: PASSED");
            }
            else
            {
                Log($"❌ {testName}: FAILED");
            }
        }
        catch (System.Exception ex)
        {
            Log($"❌ {testName}: ERROR - {ex.Message}");
        }
    }
    
    void Log(string message)
    {
        if (enableDebugOutput)
        {
            Debug.Log($"[UnityLinuxTest] {message}");
        }
        testResults.Add(message);
    }
    
    // Method to get test results programmatically
    public List<string> GetTestResults()
    {
        return new List<string>(testResults);
    }
    
    public bool AreAllTestsPassed()
    {
        return allTestsPassed;
    }
}