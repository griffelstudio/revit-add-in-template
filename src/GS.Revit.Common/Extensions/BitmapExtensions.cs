using System.Windows.Media.Imaging;

namespace System.Drawing
{
    public static class BitmapExtensions
    {
        public static BitmapSource ToBitmapSource(this Bitmap bitmap)
        {
            var handle = bitmap.GetHbitmap();
            return Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                handle,
                IntPtr.Zero,
                Windows.Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());
        }
    }
}
