﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RepositoryApp.Data.Model
{
    public class File
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDateTime { get; set; }
        public string ContentType { get; set; }
        public string Path { get; set; }
        public bool Overrided { get; set; }
        [ForeignKey("Version")]
        public Guid VersionId { get; set; }
        public Version Version { get; set; }
    }
}