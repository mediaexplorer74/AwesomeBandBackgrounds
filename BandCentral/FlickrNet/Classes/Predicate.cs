﻿using System.Xml;

namespace FlickrNet
{
    /// <summary>
    /// A machine tag predicate. "namespace:predicate=value".
    /// </summary>
    public sealed class Predicate : IFlickrParsable
    {
        /// <summary>
        /// The name the predicate
        /// </summary>
        public string PredicateName { get; set; }

        /// <summary>
        /// The usage of the predicate.
        /// </summary>
        public int Usage { get; set; }

        /// <summary>
        /// The number of distinct namespaces the predicate applies to.
        /// </summary>
        public int Namespaces { get; set; }

        #region IFlickrParsable Members

        void IFlickrParsable.Load(XmlReader reader)
        {
            while (reader.MoveToNextAttribute())
            {
                switch (reader.LocalName)
                {
                    case "usage":
                        Usage = reader.ReadContentAsInt();
                        break;
                    case "namespaces":
                        Namespaces = reader.ReadContentAsInt();
                        break;
                }
            }

            reader.Read();

            if (reader.NodeType == XmlNodeType.Text)
                PredicateName = reader.ReadContentAsString();

            reader.Read();
        }

        #endregion
    }
}