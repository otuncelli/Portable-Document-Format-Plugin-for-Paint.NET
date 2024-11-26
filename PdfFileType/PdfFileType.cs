// Copyright 2022 Osman Tunçelli. All rights reserved.
// Use of this source code is governed by GNU General Public License (GPL-2.0) that can be found in the COPYING file.

using System;
using System.IO;
using System.Linq;
using System.Threading;
using PaintDotNet;
using PaintDotNet.IndirectUI;
using PaintDotNet.IndirectUI.Extensions;
using PaintDotNet.PropertySystem;
using PdfFileTypePlugin.Export;
using PdfFileTypePlugin.Import;
using PdfFileTypePlugin.Localization;

namespace PdfFileTypePlugin;

[PluginSupportInfo(typeof(MyPluginSupportInfo))]
public class PdfFileType : PropertyBasedFileType
{
    #region Constructors

    public PdfFileType(FileTypeOptions options) : base(GetName("PDF"), options)
    {
    }

    protected PdfFileType(string name, FileTypeOptions options) : base(name, options)
    {
    }

    #endregion

    #region Load

    protected sealed override Document OnLoad(Stream input) => PdfImport.Load(input);

    #endregion

    #region Save

    public sealed override ControlInfo OnCreateSaveConfigUI(PropertyCollection props)
    {
        Ensure.IsNotNull(props, nameof(props));
        PropertyControlInfoCollection pcic = new PropertyControlInfoCollection(props);
        pcic.Configure(PropertyNames.Quality, SR.Quality)
            .Configure(PropertyNames.SkipInvisibleLayers, string.Empty, SR.SkipInvisibleLayers)
            .Configure(PropertyNames.SkipDuplicateLayers, string.Empty, SR.SkipDuplicateLayers)
            .Configure(PropertyNames.EmbedProperties, string.Empty, SR.EmbedLayerAndDocumentProperties)
            .Configure(PropertyNames.GitHubLink, string.Format(SR.PluginVersion, MyPluginSupportInfo.Instance.Version), SR.GitHubLink)
            .Configure(PropertyNames.ForumLink, string.Empty, SR.ForumLink)
            .Configure(PropertyNames.ExportMode, SR.Mode, p => p.ValueDisplayNameCallback<ExportMode>(Localize.GetDisplayName))
            .Configure(PropertyNames.PdfStandard, SR.PDFStandard, p => p.ValueDisplayNameCallback<PdfStandard>(Localize.GetDisplayName));
        return pcic.CreatePanelControlInfo();
    }

    public sealed override PropertyCollection OnCreateSavePropertyCollection()
    {
        ExportMode[] readOnlyValues = new ExportMode[] { ExportMode.Flattened, ExportMode.Cumulative };
        MyPropertyCollection props = new MyPropertyCollection()
                .AddInt32(PropertyNames.Quality, 95, 1, 100)
                .AddBoolean(PropertyNames.SkipInvisibleLayers, false)
                .AddBoolean(PropertyNames.SkipDuplicateLayers, false)
                .AddBoolean(PropertyNames.EmbedProperties, false)
                .AddStaticListChoice(PropertyNames.ExportMode, ExportMode.Normal)
                .AddStaticListChoice(PropertyNames.PdfStandard, PdfStandard.None)
                .AddUri(PropertyNames.GitHubLink, MyPluginSupportInfo.Instance.WebsiteUri)
                .AddUri(PropertyNames.ForumLink, MyPluginSupportInfo.Instance.ForumUri)
                .WithReadOnlyRule(PropertyNames.EmbedProperties, PropertyNames.ExportMode, readOnlyValues);
        return props;
    }

    #region SaveConfigToken Serialization

    protected sealed override object GetSerializablePortionOfSaveConfigToken(PropertyBasedSaveConfigToken token)
        => token.Properties.Where(IsSerializable).Select(prop => Pair.Create(prop.Name, prop.Value)).ToArray();

    private static bool IsSerializable(Property property)
    {
        switch (property.Name)
        {
            case nameof(PropertyNames.GitHubLink):
            case nameof(PropertyNames.ForumLink):
                return false;
            default:
                return Enum.IsDefined(typeof(PropertyNames), property.Name);
        }
    }

    #endregion

    protected override void OnSaveT(Document input, Stream output, PropertyBasedSaveConfigToken token, Surface scratchSurface, ProgressEventHandler progressCallback)
    {
#if !DEBUG
        if (UI.SaveConfigDialog != null) { return; }
#endif
        PdfExporter exporter = PdfExporter.Create(input, output, token, scratchSurface, progressCallback);
        exporter.Export(CancellationToken.None);
    }

    #endregion

    #region Static Helpers

    protected static string GetName(string baseName)
    {
        return $"{baseName} Plugin";
    }

    #endregion
}
