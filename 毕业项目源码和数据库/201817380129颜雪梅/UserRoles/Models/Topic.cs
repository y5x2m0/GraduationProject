//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace UserRoles.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public partial class Topic
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Topic()
        {
            this.Detail = new HashSet<Detail>();
        }
    
        public int TopicID { get; set; }
        [Display(Name = "题目描述")]
        public string TopicExplain { get; set; }
        [Display(Name = "该题分值")]
        public int TopicScore { get; set; }
        [Display(Name = "题目类型")]
        public int TopicType { get; set; }
        [Display(Name = "A选项")]
        public string TopicA { get; set; }
        [Display(Name = "B选项")]
        public string TopicB { get; set; }
        [Display(Name = "C选项")]
        public string TopicC { get; set; }
        [Display(Name = "D选项")]
        public string TopicD { get; set; }
        [Display(Name = "排序")]
        public int TopicSort { get; set; }
        [Display(Name = "正确答案")]
        public string TopicAnswer { get; set; }
        [Display(Name = "所属试卷外键")]
        public int PaperID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Detail> Detail { get; set; }
        public virtual Paper Paper { get; set; }
    }
}
