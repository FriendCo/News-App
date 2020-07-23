using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Principal;
using System.Text;

namespace DomainClass
{
    public class PagesTB
    {
        [Key]
        public int PageID { get; set; }

        [Display(Name ="Title")]
        [Required(ErrorMessage ="Please Enter {0}")]
        [MaxLength(150)]
        public string Title { get; set; }

        [Display(Name = "Short Description")]
        [Required(ErrorMessage = "Please Enter {0}")]
        [MaxLength(800)]
        [DataType(DataType.MultilineText)]
        public string ShortDescription { get; set; }

        [Display(Name = "Text")]
        [Required(ErrorMessage = "Please Enter {0}")]
        [DataType(DataType.MultilineText)]
        public string Text { get; set; }

        [Display(Name ="Image")]
        [MaxLength(250)]
        public string ImageName { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }

        [Required]
        public int Visist { get; set; }

        [Required]
        public int Like { get; set; }

        //Relationships
        [Required]
        public int GroupID { get; set; }

        //Navigation Property
        public virtual PageGroups PageGroups { get; set; }
    }
}
