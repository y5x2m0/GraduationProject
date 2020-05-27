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
    public partial class Teacher
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Teacher()
        {
            this.Answer = new HashSet<Answer>();
        }
    
        public int TeacherID { get; set; }
        [Required(ErrorMessage ="姓名是必填项")]
        public string TeacherName { get; set; }
        [Required(ErrorMessage = "账号是必填项")]
        [Display(Name = "账号")]
        
        public string TeacherLoginName { get; set; }
        [Required(ErrorMessage = "密码是必填项")]
        [Display(Name = "密码")]
        [DataType(DataType.Password)]
        [RegularExpression("[A-Za-z0-9]{6,12}", ErrorMessage = "密码输入的格式不正确")]
        [StringLength(40)]
        public string TeacherLoginPwd { get; set; }
        public string TeacherPhone { get; set; }
        public string TeacherEmail { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Answer> Answer { get; set; }
    }
}
