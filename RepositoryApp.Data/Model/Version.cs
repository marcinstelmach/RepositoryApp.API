﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;
using System.Text;

namespace RepositoryApp.Data.Model
{
    public class Version
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string UniqueName { get; set; }
        public Guid ParentId { get; set; }
        public string Description { get; set; }
        public DateTime CreationDateTime { get; set; }
        public DateTime ModifDateTime { get; set; }
        [ForeignKey("Repository")]
        public Guid RepositoryId { get; set; }
        public Repository Repository { get; set; }
        public IList<File> Files { get; set; }
    }
}