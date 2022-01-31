// Copyright 2022 Osman Tunçelli. All rights reserved.
// Use of this source code is governed by GNU General Public License (GPL-2.0) that can be found in the COPYING file.

using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Text;
using PaintDotNet;
using PDFiumSharp;
using PDFiumSharp.Types;

namespace PdfFileTypePlugin
{
    internal sealed class LayerProperties
    {
        public const string ParamName = "L";

        public byte Opacity { get; }

        public LayerBlendMode BlendMode { get; }

        public bool IsBackground { get; }

        public bool Visible { get; }

        public string Name { get; }

        public LayerProperties(string name, bool visible, bool isBackground, byte opacity, LayerBlendMode blendMode)
        {
            Name = name;
            Visible = visible;
            IsBackground = isBackground;
            Opacity = opacity;
            BlendMode = blendMode;
        }

        public LayerProperties(Layer layer) : this(layer.Name, layer.Visible, layer.IsBackground, layer.Opacity, layer.BlendMode)
        {
        }

        private byte[] ToBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var gzip = new DeflateStream(ms, CompressionLevel.Fastest))
                using (var bw = new BinaryWriter(gzip))
                {
                    bw.Write(Convert.ToByte(Visible));
                    bw.Write(Convert.ToByte(IsBackground));
                    bw.Write(Opacity);
                    bw.Write((byte)BlendMode);
                    bw.Write(Encoding.Unicode.GetBytes(Name));
                }
                return ms.ToArray();
            }
        }

        public static byte[] CreatePayload(Layer layer)
        {
            return new LayerProperties(layer).ToBytes();
        }

        public bool TryCopyTo(Layer layer)
        {
            try
            {
                layer.Name = Name;
                layer.Visible = Visible;
                layer.IsBackground = IsBackground;
                layer.Opacity = Opacity;
                layer.BlendMode = BlendMode;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool TryLoadFromPageObjectMark(in FPDF_PAGEOBJECTMARK mark, byte[] buf, out LayerProperties props)
        {
            try
            {
                if (PDFium.FPDFPageObjMark_GetParamBlobValue(mark, ParamName, ref buf[0], (uint)buf.Length, out uint out_buflen))
                {
                    bool visible, isBackground;
                    byte opacity;
                    LayerBlendMode blendMode;
                    string name;
                    using (var ms = new MemoryStream(buf, 0, (int)out_buflen))
                    using (var gzip = new DeflateStream(ms, CompressionMode.Decompress))
                    using (var br = new BinaryReader(gzip))
                    {
                        visible = br.ReadBoolean();
                        isBackground = br.ReadBoolean();
                        opacity = br.ReadByte();
                        blendMode = (LayerBlendMode)br.ReadByte();
                        using (var ms1 = new MemoryStream())
                        {
                            int bytesRead;
                            while ((bytesRead = gzip.Read(buf, 0, buf.Length)) > 0)
                            {
                                ms1.Write(buf, 0, bytesRead);
                            }
                            name = Encoding.Unicode.GetString(ms1.ToArray());
                        }
                    }
                    props = new LayerProperties(name, visible, isBackground, opacity, blendMode);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error while loading page object mark: {ex.Message}");
            }
            props = default;
            return false;
        }
    }
}
