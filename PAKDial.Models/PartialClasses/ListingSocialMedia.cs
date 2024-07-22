using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PAKDial.Domains.DomainModels
{
    public partial class ListingSocialMedia
    {
        [NotMapped]
        public string Name { get; set; }
    }
}
