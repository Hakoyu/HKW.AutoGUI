using System;
using System.Buffers;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using HKW.AutoGUI;
using HKW.AutoGUI.AutoGUI;
using HKW.AutoGUI.Native.Windows;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Advanced;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.PixelFormats;

namespace DesktopCapture.Benchmark;

[Orderer(SummaryOrderPolicy.Method)]
[MemoryDiagnoser]
public class Test
{
    [GlobalSetup]
    public void Initialize()
    {
        //byte[,] differences = image1.GrayValues.GetDifferences(image2.GrayValues);
        //int numberOfPixels = differences.GetLength(0) * differences.GetLength(1);
        //float diffPixels = differences.All().Count(b => b > threshold);
        //return diffPixels / numberOfPixels;
        //sr_ll = Enumerable
        //    .Range(0, 100)
        //    .Select(i => new List<int>(Enumerable.Range(0, 10)))
        //    .ToList();
        //sr_cl = new(
        //    Enumerable
        //        .Range(0, 100)
        //        .Select(i => new List<int>(Enumerable.Range(0, 10).ToList()))
        //        .ToList()
        //);
        //foreach (int i in Enumerable.Range(0, 100))
        //{
        //    //sr_listList.Add(new List<int>(Enumerable.Range(0, 10)));
        //    //sr_dic.Add(i, i);
        //    s_list.Add(i);
        //}
        //ol1 = new(Enumerable.Range(0, 1000));
        //ol2 = new(Enumerable.Range(0, 1000).ToList());
        //s_list = new(Enumerable.Range(0, 1000));
        //s_set = s_list.ToHashSet();
        //s_array = s_list.ToArray();
        //s_collection = new(s_list);
        _bitmap = _dd.GetLatestFrame(out _);
    }

    static string _file = @"C:\Users\HKW\Desktop\1.bmp";

    static DesktopDuplicator _dd = new();
    static Bitmap _bitmap;

    [Benchmark]
    [IterationCount(10)]
    public object? Test1()
    {
        return _bitmap.ToImageSharp();
    }
}
