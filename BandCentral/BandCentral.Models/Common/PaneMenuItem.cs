﻿using System;
using CommonHelpers.Common;

namespace BandCentral.Models.Common
{
    public class PaneMenuItem : BindableBase
    {
        private string glyphIcon;
        private string label;
        private Type destinationType;
        private Uri destinationUri;
        private object arguments = null;

        public string GlyphIcon
        {
            get => glyphIcon;
            set => SetProperty(ref glyphIcon, value);
        }

        public string Label
        {
            get => label;
            set => SetProperty(ref label, value);
        }

        public Type DestinationType
        {
            get => destinationType;
            set => SetProperty(ref destinationType, value);
        }

        public Uri DestinationUri
        {
            get => destinationUri;
            set => SetProperty(ref destinationUri, value);
        }
        
        public object Arguments
        {
            get => arguments;
            set => SetProperty(ref arguments, value);
        }
    }
}
