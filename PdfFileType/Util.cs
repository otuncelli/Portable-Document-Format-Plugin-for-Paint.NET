// Copyright 2022 Osman Tunçelli. All rights reserved.
// Use of this source code is governed by GNU General Public License (GPL-2.0) that can be found in the COPYING file.

using System;
using System.IO;
using System.Net.Http;
using System.Runtime;
using System.Threading.Tasks;
using PdfFileTypePlugin.Import;
using System.Drawing;
#if NETCOREAPP && !TEST
using System.Text.Json;
#else
using System.Text.RegularExpressions;
#endif

namespace PdfFileTypePlugin
{
    internal static class Util
    {
        #region Clamp

#if !NETCOREAPP
        private static T Clamp<T>(T value, T min, T max) where T : IComparable
        {
            return value.CompareTo(min) <= 0 ? min : value.CompareTo(max) > 0 ? max : value;
        }
#endif

        public static int Clamp(int value, int min, int max)
        {
#if NETCOREAPP
            return Math.Clamp(value, min, max);
#else
            return Clamp<int>(value, min, max);
#endif
        }

        public static float Clamp(float value, float min, float max)
        {
#if NETCOREAPP
            return Math.Clamp(value, min, max);
#else
            return Clamp<float>(value, min, max);
#endif
        }

        public static decimal Clamp(decimal value, decimal min, decimal max)
        {
#if NETCOREAPP
            return Math.Clamp(value, min, max);
#else
            return Clamp<decimal>(value, min, max);
#endif
        }

        #endregion

        #region Pixel <-> Point Conversions

        public static SizeF PixelsToPoints(Size size, float dpi = 72)
        {
            SizeF device = UI.DPI.Value;
            float width = size.Width * dpi / device.Width;
            float height = size.Height * dpi / device.Height;
            return new SizeF(width, height);
        }

        public static Size PointsToPixels(SizeF sizef, float dpi = 72)
        {
            SizeF device = UI.DPI.Value;
            int width = (int)Math.Round(sizef.Width / (dpi / device.Width));
            int height = (int)Math.Round(sizef.Height / (dpi / device.Height));
            return new Size(width, height);
        }

        #endregion

        #region Other

        public static bool FileExists(string filename, bool def)
        {
            try
            {
                string dir = Path.GetDirectoryName(typeof(PdfFileType).Assembly.Location);
                string path = Path.Combine(dir, filename);
                return File.Exists(path);
            }
            catch
            {
                return def;
            }
        }

        public static MemoryFailPoint CreateMemoryFailPoint(int width, int height, int multiply = 1)
        {
            int bytesPerLayer = checked(width * 4 * height);
            int sizeInMegabytes = Math.Max(bytesPerLayer / (1024 * 1024), 1) * multiply;
            return new MemoryFailPoint(sizeInMegabytes);
        }

        public static unsafe void BufferCopy(IntPtr src, IntPtr dst, long length)
        {
            Buffer.MemoryCopy((void*)src, (void*)dst, length, length);
        }

        public static Lazy<Task<Version>> CheckUpdatesAsync = new Lazy<Task<Version>>(async () =>
        {
            Uri uri = new Uri("https://api.github.com/repos/otuncelli/Portable-Document-Format-Plugin-for-Paint.NET/releases/latest");
            string s = null;
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.UserAgent.ParseAdd(".");
                client.Timeout = TimeSpan.FromSeconds(5);
#if NETCOREAPP && !TEST
                using (Stream stream = await client.GetStreamAsync(uri).ConfigureAwait(false))
                using (JsonDocument json = await JsonDocument.ParseAsync(stream, new JsonDocumentOptions()).ConfigureAwait(false))
                {
                    if (json.RootElement.TryGetProperty("tag_name", out JsonElement element))
                    {
                        s = element.GetString()?.TrimStart('v');
                    }
                }
#else
                string json = await client.GetStringAsync(uri).ConfigureAwait(false);
                Match match = Regex.Match(json, @"""tag_name""\s*:\s*""v([^""]+)", RegexOptions.Multiline);
                if (match.Success)
                {
                    s = match.Groups[1].Value;
                }
#endif
            }

            return Version.Parse(s);
        });

        #endregion
    }
}
