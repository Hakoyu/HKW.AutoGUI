using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using DesktopCapture.Benchmark;

namespace HKW.AutoGUI.Benchmark;

internal class Program
{
    static void Main(string[] args)
    {
        //var test = BenchmarkRunner.Run<Test>();
        BenchmarkSwitcher
            .FromAssembly(typeof(Test).Assembly)
            .Run(args, new DebugInProcessConfig());
    }
}
