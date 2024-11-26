// Copyright 2022 Osman Tunçelli. All rights reserved.
// Use of this source code is governed by GNU General Public License (GPL-2.0) that can be found in the COPYING file.

using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using PaintDotNet;
using PDFiumSharp;
using PDFiumSharp.Types;

namespace PdfFileTypePlugin;

internal sealed class DocumentProperties
{
    public const string MarkName = "PDN3";
    public const string ParamName = "D";

    public float DpuX { get; }

    public float DpuY { get; }

    public MeasurementUnit Unit { get; }

    public DocumentProperties(float dpux, float dpuy, MeasurementUnit unit)
    {
        const float min = (float)Document.MinimumDpu;
        const float max = (float)Document.MaximumDpu;
        DpuX = Math.Clamp(dpux, min, max);
        DpuY = Math.Clamp(dpuy, min, max);
        Unit = unit;
    }

    private byte[] ToBytes()
    {
        using var ms = new MemoryStream();
        using (var bw = new BinaryWriter(ms))
        {
            bw.Write(DpuX);
            bw.Write(DpuY);
            bw.Write((byte)Unit);
            bw.Flush();
        }
        return ms.ToArray();
    }

    public static byte[] CreatePayload(Document document)
    {
        float dpux = (float)Math.Round(document.DpuX, 3, MidpointRounding.AwayFromZero);
        float dpuy = (float)Math.Round(document.DpuY, 3, MidpointRounding.AwayFromZero);
        MeasurementUnit unit = document.DpuUnit;
        return new DocumentProperties(dpux, dpuy, unit).ToBytes();
    }

    public void CopyTo(Document document)
    {
        try
        {
            document.DpuX = DpuX;
            document.DpuY = DpuY;
            document.DpuUnit = Unit;
        }
        catch
        {
            document.DpuX = 96.0;
            document.DpuY = 96.0;
            document.DpuUnit = MeasurementUnit.Inch;
        }
    }

    public static bool TryLoadFromPageObjectMark(in FPDF_PAGEOBJECTMARK mark, byte[] buf, [NotNullWhen(true)] out DocumentProperties? props)
    {
        try
        {
            if (PDFium.FPDFPageObjMark_GetParamBlobValue(mark, ParamName, ref buf[0], (uint)buf.Length, out uint out_buflen))
            {
                float dpux, dpuy;
                MeasurementUnit unit;

                using (var ms = new MemoryStream(buf, 0, (int)out_buflen))
                using (var br = new BinaryReader(ms))
                {
                    dpux = br.ReadSingle();
                    dpuy = br.ReadSingle();
                    unit = (MeasurementUnit)br.ReadByte();
                }

                props = new DocumentProperties(dpux, dpuy, unit);
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
