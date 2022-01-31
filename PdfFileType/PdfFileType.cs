// Copyright 2022 Osman Tunçelli. All rights reserved.
// Use of this source code is governed by GNU General Public License (GPL-2.0) that can be found in the COPYING file.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using PaintDotNet;
using PaintDotNet.IndirectUI;
using PaintDotNet.PropertySystem;
using PdfFileTypePlugin.Export;
using PdfFileTypePlugin.Import;
using PdfFileTypePlugin.Localization;

namespace PdfFileTypePlugin
{
    [PluginSupportInfo(typeof(MyPluginSupportInfo))]
    public class PdfFileType : PropertyBasedFileType
    {
        #region Constructors

        public PdfFileType(FileTypeOptions options) : base(GetName("PDF - Portable Document Format"), options)
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
            ControlInfo info = CreateDefaultSaveConfigUI(props)
                .Property(PropertyNames.Quality, StringResources.Quality)
                .Property(PropertyNames.SkipInvisibleLayers, String.Empty, StringResources.SkipInvisibleLayers)
                .Property(PropertyNames.SkipDuplicateLayers, String.Empty, StringResources.SkipDuplicateLayers)
                .Property(PropertyNames.EmbedProperties, String.Empty, StringResources.EmbedLayerAndDocumentProperties)
                .Property(PropertyNames.GitHubLink, String.Format(StringResources.PluginVersion, MyPluginSupportInfo.Instance.Version), StringResources.GitHubLink)
                .Property(PropertyNames.ForumLink, String.Empty, StringResources.ForumLink)
                .Property(PropertyNames.ExportMode, StringResources.Mode, p => p.ValueDisplayNameCallback<ExportMode>(Localize.GetDisplayName))
                .Property(PropertyNames.PdfStandard, StringResources.PDFStandard, p => p.ValueDisplayNameCallback<PdfStandard>(Localize.GetDisplayName));
            return info;
        }

        public sealed override PropertyCollection OnCreateSavePropertyCollection()
        {
            ExportMode[] readOnlyValues = new ExportMode[] { ExportMode.Flattened, ExportMode.Cumulative };
            MyPropertyCollection props = new MyPropertyCollection()
                    .Add(PropertyNames.Quality, 95, 1, 100)
                    .Add(PropertyNames.SkipInvisibleLayers, false)
                    .Add(PropertyNames.SkipDuplicateLayers, false)
                    .Add(PropertyNames.EmbedProperties, false)
                    .Add(PropertyNames.ExportMode, ExportMode.Normal)
                    .Add(PropertyNames.PdfStandard, PdfStandard.None)
                    .Add(PropertyNames.GitHubLink, MyPluginSupportInfo.Instance.WebsiteUri)
                    .Add(PropertyNames.ForumLink, MyPluginSupportInfo.Instance.ForumUri)
                    .WithReadOnlyRule(PropertyNames.EmbedProperties, PropertyNames.ExportMode, readOnlyValues);
            return props;
        }

        #region SaveConfigToken Serialization

        protected sealed override object GetSerializablePortionOfSaveConfigToken(PropertyBasedSaveConfigToken token)
            => token.Properties.Where(IsSerializable).ToDictionary(prop => prop.Name, prop => prop.Value);

        private static bool IsSerializable(Property property)
        {
            if (!(property?.GetOriginalNameValue() is PropertyNames name))
            {
                return false;
            }

            switch (name)
            {
                case PropertyNames.GitHubLink:
                case PropertyNames.ForumLink:
                    return false;
                default:
                    return true;
            }
        }

        protected sealed override PropertyBasedSaveConfigToken GetSaveConfigTokenFromSerializablePortionT(object portion)
        {
            PropertyCollection props1 = CreateSavePropertyCollection();
            PropertyCollection props2 = props1.Clone();
            PropertyBasedSaveConfigToken token = CreateDefaultSaveConfigToken();

            if (!(portion is Dictionary<string, object> dict))
            {
                return token;
            }

            try
            {
                foreach (KeyValuePair<string, object> pair in dict)
                {
                    Property prop = props2[pair.Key];
                    using (prop.InternalUseAsWritable(raiseReadOnlyChangedEvent: false))
                    {
                        prop.Value = IsSerializable(prop) ? pair.Value : prop.DefaultValue;
                    }
                }
            }
            catch
            {
                // fallback to default token
                return token;
            }

            token.Properties.CopyCompatibleValuesFrom(props2, ignoreReadOnlyFlags: true);
            return token;
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
            string assemblyPath = typeof(PdfFileType).Assembly.Location;
            string mydocs = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            bool currentUser = assemblyPath.StartsWith(mydocs);
            return String.Format("{0} Plugin v{1} ({2})", baseName, MyPluginSupportInfo.Instance.Version, currentUser ? "CU" : "AU");
        }

        #endregion
    }
}
