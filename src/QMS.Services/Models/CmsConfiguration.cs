﻿using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QMS.Services.Models
{
    /// <summary>
    /// Defines the location of the Cms Configuration
    /// </summary>
    public class CmsConfigLocation
    {
        public Uri Uri { get; set; }
    }

    /// <summary>
    /// Holds all CMS Configuration (schemas)
    /// </summary>
    public class CmsConfiguration
    {
        public List<string> Languages { get; set; }

        public List<EntityGroupConfiguration> EntityGroups { get; set; } = new List<EntityGroupConfiguration>();

        public IEnumerable<SchemaLocation> Entities => EntityGroups.SelectMany(x => x.Entities);

        public bool IsInitialized => EntityGroups.SelectMany(x => x.Entities).Any();
    }

    public class EntityGroupConfiguration
    {
        public string Name { get; set; }
        public int Order { get; set; }

        public List<SchemaLocation> Entities { get; set; } = new List<SchemaLocation>();

    }

    public class SchemaLocation
    {
        public string Name { get; set; }
        public string Key { get; set; }
        public Uri Uri { get; set; }
        /// <summary>
        /// Indicates there can be only one instance
        /// TODO: Replace with Min / Max option like JsonSchema?
        /// </summary>
        public bool IsSingleton { get; set; }
        public string Schema { get; set; }

        public List<ListViewProperty> ListViewProperties { get; set; }

    }

    public class ListViewProperty
    {
        public string Key { get; set; }
        public string DisplayName { get; set; }
    }
}