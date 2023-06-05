using System.Buffers;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using SixLabors.ImageSharp.Advanced;

namespace HKW.AutoGUI.Native.Windows;

internal static class WinGDI32
{
    /// <summary>
    /// 使用GDI截取屏幕
    /// </summary>
    /// <returns>位图</returns>
    [SupportedOSPlatform(nameof(OSPlatform.Windows))]
    public static System.Drawing.Bitmap CaptureScreen()
    {
        var window = GetDesktopWindow();
        var source = GetWindowDC(window);
        var windowRect = new RECT();
        GetWindowRect(window, ref windowRect);
        int width = windowRect.Right - windowRect.Left;
        int height = windowRect.Bottom - windowRect.Top;
        var hbitmap = CreateCompatibleBitmap(source, width, height);
        var destination = CreateCompatibleDC(source);
        SelectObject(destination, hbitmap);
        BitBlt(destination, 0, 0, width, height, source, 0, 0, 0x00CC0020);
        var bitmap = System.Drawing.Image.FromHbitmap(hbitmap);
        ReleaseDC(window, source);
        DeleteDC(destination);
        DeleteObject(hbitmap);
        return bitmap;
    }

    /// <summary>
    /// 转换至 ImageSharp 的 <see cref="SixLabors.ImageSharp.Image"/>
    /// </summary>
    /// <param name="bitmap">位图</param>
    [SupportedOSPlatform(nameof(OSPlatform.Windows))]
    public static unsafe SixLabors.ImageSharp.Image ToImageSharp(this System.Drawing.Bitmap bitmap)
    {
        int w = bitmap.Width;
        int h = bitmap.Height;
        var fullRect = new System.Drawing.Rectangle(0, 0, w, h);
        var data = bitmap.LockBits(fullRect, ImageLockMode.ReadWrite, bitmap.PixelFormat);
        var image = new Image<Bgr24>(w, h);
        try
        {
            byte* sourcePtrBase = (byte*)data.Scan0;
            long sourceRowByteCount = Math.Abs(data.Stride);
            long destRowByteCount = w * sizeof(Bgra32);
            Configuration configuration = image.GetConfiguration();
            _ = Parallel.For(
                0,
                image.Height,
                y =>
                {
                    using IMemoryOwner<Bgra32> workBuffer =
                        Configuration.Default.MemoryAllocator.Allocate<Bgra32>(w);
                    fixed (Bgra32* destPtr = &MemoryMarshal.GetReference(workBuffer.Memory.Span))
                    {
                        var tempPrt = destPtr;
                        byte* sourceRowPtr = sourcePtrBase + (data.Stride * y);
                        var rows = image.DangerousGetPixelRowMemory(y).Span;
                        Buffer.MemoryCopy(
                            sourceRowPtr,
                            destPtr,
                            destRowByteCount,
                            sourceRowByteCount
                        );
                        PixelOperations<Bgr24>.Instance.FromBgra32(
                            configuration,
                            workBuffer.Memory.Span,
                            rows
                        );
                    }
                }
            );
            //using IMemoryOwner<Bgra32> workBuffer =
            //    Configuration.Default.MemoryAllocator.Allocate<Bgra32>(w);
            //fixed (Bgra32* destPtr = &MemoryMarshal.GetReference(workBuffer.Memory.Span))
            //{
            //    var prt = destPtr;
            //    image.ProcessPixelRows(pixelAccessor =>
            //    {
            //        for (int y = 0; y < pixelAccessor.Height; y++)
            //        {
            //            byte* sourcePtr = sourcePtrBase + (data.Stride * y);
            //            var row = pixelAccessor.GetRowSpan(y);
            //            Buffer.MemoryCopy(sourcePtr, prt, destRowByteCount, sourceRowByteCount);
            //            PixelOperations<Bgr24>.Instance.FromBgra32(
            //                configuration,
            //                workBuffer.Memory.Span[..w],
            //                row
            //            );
            //        }
            //    });
            //    //for (int y = 0; y < image.Height; y++)
            //    //{
            //    //    byte* sourcePtr = sourcePtrBase + (data.Stride * y);
            //    //    var row = image.DangerousGetPixelRowMemory(y).Span;
            //    //    Buffer.MemoryCopy(sourcePtr, prt, destRowByteCount, sourceRowByteCount);
            //    //    PixelOperations<Bgr24>.Instance.FromBgra32(
            //    //        configuration,
            //    //        workBuffer.Memory.Span[..w],
            //    //        row
            //    //    );
            //    //}
            //}
        }
        finally
        {
            bitmap.UnlockBits(data);
        }
        return image;
    }

    [DllImport("GDI32.dll")]
    public static extern bool BitBlt(
        IntPtr hdcDest,
        int nXDest,
        int nYDest,
        int nWidth,
        int nHeight,
        IntPtr hdcSrc,
        int nXSrc,
        int nYSrc,
        int dwRop
    );

    [DllImport("User32.dll")]
    internal static extern IntPtr GetWindowRect(IntPtr hWnd, ref RECT rect);

    [DllImport("GDI32.dll")]
    public static extern IntPtr CreateCompatibleBitmap(IntPtr hdc, int nWidth, int nHeight);

    [DllImport("GDI32.dll")]
    public static extern IntPtr CreateCompatibleDC(IntPtr hdc);

    [DllImport("GDI32.dll")]
    public static extern bool DeleteDC(IntPtr hdc);

    [DllImport("GDI32.dll")]
    public static extern bool DeleteObject(IntPtr hObject);

    [DllImport("GDI32.dll")]
    public static extern int GetDeviceCaps(IntPtr hdc, int nIndex);

    [DllImport("GDI32.dll")]
    public static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);

    [DllImport("User32.dll")]
    public static extern IntPtr GetDesktopWindow();

    [DllImport("User32.dll")]
    public static extern IntPtr GetWindowDC(IntPtr hWnd);

    [DllImport("User32.dll")]
    public static extern IntPtr GetDC(IntPtr hWnd);

    [DllImport("User32.dll")]
    public static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDC);
}
