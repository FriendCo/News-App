
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Principal;
using System.Text;

namespace DomainClass
{
    public class PageGroups
    {
        [Key]
        public int GroupID { get; set; }

        [Display(Name="Group Title")]
        [Required(ErrorMessage ="Please Enter {0}")]
        [MaxLength(150)]
        public string Title { get; set; }

        [Display(Name ="Group Name")]
        [Required(ErrorMessage ="Please Enter {0}")]
        [MaxLength(150)]
        public string Name { get; set; }

        //Navigation property
        public virtual List<PagesTB> Pages { get; set; }
    }
}
