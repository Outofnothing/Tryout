using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ControlToImage;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void Save_Click(object sender, RoutedEventArgs e)
    {
        var grids = GetChildObjects<Grid>(Host);
        for (int i = 0; i < grids.Count; i++)
        {
            var imageBytes = ConvertControlToBytes2(grids[i], 96, 96);
            File.WriteAllBytes($"grid_{i}.png", imageBytes);
        }
    }

    public static byte[] ConvertControlToBytes2(Visual target, double dpiX, double dpiY)
    {
        Rect bounds = VisualTreeHelper.GetDescendantBounds(target);
        RenderTargetBitmap rtb = new RenderTargetBitmap((int)(bounds.Width * dpiX / 96.0),
                                                        (int)(bounds.Height * dpiY / 96.0),
                                                        dpiX,
                                                        dpiY,
                                                        PixelFormats.Pbgra32);
        DrawingVisual dv = new DrawingVisual();
        using (DrawingContext ctx = dv.RenderOpen())
        {
            VisualBrush vb = new VisualBrush(target);
            ctx.DrawRectangle(vb, null, new Rect(new Point(), bounds.Size));
        }
        rtb.Render(dv);// 使用 MemoryStream 来保存图像的字节流
        using (MemoryStream memoryStream = new MemoryStream())
        {
            // 创建一个 PngBitmapEncoder 对象，将 RenderTargetBitmap 编码为 PNG 格式
            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(rtb));
            encoder.Save(memoryStream);

            // 返回字节流
            return memoryStream.ToArray();
        }
    }
    
    
    /// <summary>
     /// 获得指定元素的所有子元素
     /// </summary>
     /// <typeparam name="T"></typeparam>
     /// <param name="obj"></param>
     /// <returns></returns>
    public static List<T> GetChildObjects<T>(DependencyObject obj) where T : FrameworkElement
    {
        List<T> childList = [];
        var count = VisualTreeHelper.GetChildrenCount(obj);

        for (int i = 0; i < count; i++)
        {
            var child = VisualTreeHelper.GetChild(obj, i);

            if (child is T t)
            {
                childList.Add(t);
            }
        }
        return childList;
    }

    public static byte[] ConvertGridToImageByteArray(Grid grid)
    {
        // 确保 Grid 的布局已经完成
        //grid.UpdateLayout();

        // 创建一个 RenderTargetBitmap 对象，指定图像宽度、高度、DPI 和像素格式
        var renderTargetBitmap = new RenderTargetBitmap(
            (int)grid.ActualWidth, // 图像的宽度
            (int)grid.ActualHeight, // 图像的高度
            96, // 水平 DPI
            96, // 垂直 DPI
            System.Windows.Media.PixelFormats.Pbgra32 // 图像的像素格式
        );

        // 将 Grid 渲染到 RenderTargetBitmap 中
        renderTargetBitmap.Render(grid);

        // 使用 MemoryStream 来保存图像的字节流
        using (MemoryStream memoryStream = new MemoryStream())
        {
            // 创建一个 PngBitmapEncoder 对象，将 RenderTargetBitmap 编码为 PNG 格式
            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(renderTargetBitmap));
            encoder.Save(memoryStream);

            // 返回字节流
            return memoryStream.ToArray();
        }
    }
}