# Unity Linux Test Project

🎮 A comprehensive test project to verify Unity C# and .NET functionality on Linux systems.

## Overview

This Unity test project is designed to validate that Unity works correctly with .NET 8 on Linux. It includes various test scripts that exercise different aspects of Unity and .NET.

## Test Scripts Included

### 1. `UnityLinuxTest.cs`
Comprehensive compatibility test that verifies:
- ✅ Basic C# types and operations
- ✅ Collections (List, Dictionary, Arrays)
- ✅ LINQ operations and queries
- ✅ String manipulation and formatting
- ✅ Math operations and Unity Math
- ✅ DateTime and TimeSpan operations
- ✅ File system access and I/O
- ✅ Unity-specific features (GameObjects, Components)

### 2. `CubeController.cs`
Interactive controller script that tests:
- ✅ Input handling (keyboard, mouse)
- ✅ Transform operations (movement, rotation)
- ✅ Physics integration (Rigidbody, collision)
- ✅ Unity's Update loop and timing
- ✅ Component access and manipulation

### 3. `PerformanceTest.cs`
Performance benchmarking script that measures:
- ✅ Math operation performance
- ✅ Object instantiation speed
- ✅ Collection operation efficiency
- ✅ String manipulation performance
- ✅ Physics calculation benchmarks
- ✅ Frame rate monitoring

## How to Use

### Option 1: Open in Unity Editor
1. **Open Unity Hub**
2. **Add Project** → Navigate to `unity-test-project` folder
3. **Open** the project
4. **Create a test scene** with the following setup:
   - Add an empty GameObject and attach `UnityLinuxTest.cs`
   - Create a cube and attach `CubeController.cs`
   - Add another empty GameObject with `PerformanceTest.cs`
5. **Run the scene** and check the Console for test results

### Option 2: Quick Test Setup
```bash
# In Unity Editor:
# 1. Create new 3D scene
# 2. Add cube (GameObject → 3D Object → Cube)
# 3. Attach CubeController script to cube
# 4. Create empty GameObject, attach UnityLinuxTest script
# 5. Create another empty GameObject, attach PerformanceTest script
# 6. Press Play
```

### Option 3: Automated Testing
The `UnityLinuxTest` script can run automatically:
1. Set `runTestsOnStart = true` in the inspector
2. Enable `enableDebugOutput = true` for console logging
3. Press Play and watch the Console window

## Expected Results

### ✅ All Tests Pass
If Unity and .NET are working correctly, you should see:
```
[UnityLinuxTest] === Unity Linux .NET Compatibility Tests ===
[UnityLinuxTest] Unity Version: 2022.3.x / 2023.x / 6.x
[UnityLinuxTest] Platform: LinuxPlayer / LinuxEditor
[UnityLinuxTest] ✅ Basic Types: PASSED
[UnityLinuxTest] ✅ Collections (List, Dictionary): PASSED
[UnityLinuxTest] ✅ LINQ Operations: PASSED
[UnityLinuxTest] ✅ String Operations: PASSED
[UnityLinuxTest] ✅ Math Operations: PASSED
[UnityLinuxTest] ✅ DateTime Operations: PASSED
[UnityLinuxTest] ✅ File System Access: PASSED
[UnityLinuxTest] ✅ Unity Specific Features: PASSED
[UnityLinuxTest] === Test Summary ===
[UnityLinuxTest] Total Tests: 8
[UnityLinuxTest] Passed: 8
[UnityLinuxTest] Failed: 0
[UnityLinuxTest] Success Rate: 100.0%
[UnityLinuxTest] All Tests Passed: True
[UnityLinuxTest] 🎉 Unity .NET integration is working perfectly on Linux!
```

### 🎮 Interactive Controls
With the `CubeController`:
- **WASD** - Move the cube
- **Q/E** - Rotate left/right
- **Space** - Jump (if physics enabled)
- **T** - Test input systems
- Watch console for position/physics updates

### 📊 Performance Metrics
The `PerformanceTest` shows:
- FPS counter in top-left corner
- Performance benchmark results in console
- Press **P** to run performance tests manually

## Troubleshooting

### Common Issues

**❌ Compilation Errors**
- Ensure .NET 8 is properly installed
- Check Unity's Player Settings → Configuration → Api Compatibility Level
- Verify all script files are properly placed in `Assets/Scripts/`

**❌ Test Failures**
- Check console for specific error messages
- Verify file system permissions for temp directory access
- Ensure Unity has proper graphics driver support

**❌ Performance Issues**
- Check if running in Debug mode (affects performance)
- Verify graphics drivers are properly installed
- Monitor system resources during tests

**❌ Input Not Working**
- Ensure the scene is focused (click in Game view)
- Check Project Settings → Input Manager
- Verify no modal dialogs are blocking input

### Unity Version Compatibility

This test project is compatible with:
- ✅ **Unity 6.x** (Latest LTS)
- ✅ **Unity 2023.x** (Tech Stream)
- ✅ **Unity 2022.3.x** (LTS)
- ✅ **Unity 2021.3.x** (Previous LTS)

### .NET Compatibility

Tested with:
- ✅ **.NET 8** (Recommended for Unity 6)
- ✅ **.NET Standard 2.1** (Unity default)
- ✅ **.NET Framework 4.8** (Alternative option)

## Project Structure

```
unity-test-project/
├── Assets/
│   └── Scripts/
│       ├── UnityLinuxTest.cs      # Main compatibility test
│       ├── CubeController.cs      # Interactive controller
│       └── PerformanceTest.cs     # Performance benchmarks
├── ProjectSettings/
│   └── ProjectSettings.asset      # Unity project configuration
└── README.md                      # This file
```

## Advanced Usage

### Custom Test Scenarios

You can extend the tests by:

1. **Adding new test methods** to `UnityLinuxTest.cs`:
```csharp
void TestCustomFeature()
{
    RunTest("Custom Feature", () =>
    {
        // Your test logic here
        return true; // or false if test fails
    });
}
```

2. **Modifying performance benchmarks** in `PerformanceTest.cs`
3. **Adding new interactive features** to `CubeController.cs`

### Automated CI/CD Testing

For automated testing in CI/CD pipelines:

```bash
# Unity command line test execution
unity-editor -batchmode -quit -projectPath ./unity-test-project -executeMethod UnityLinuxTest.RunAllTests -logFile test-results.log
```

## Contributing

To improve these test scripts:
1. Fork the repository
2. Add new test cases or improve existing ones
3. Test on different Linux distributions
4. Submit a pull request with your improvements

---

**Made with 🍋 for Unity Linux developers**