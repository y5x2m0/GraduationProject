using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace StuSystem.Models
{
    public static class UlHtml
    {
        /*
         *<ul>
         * <li><a href="/Home/Index">首页</a></li>
         * <li><a href="/Home/New">新闻</a></li>
         * <li><a href="/Home/About">关注</a></li>
         * </ul>
         * @Html.SetUl(new[]{"首页","新闻","关注"})
         *<ul></ul>
         * <li></li>
         * <a href="#"></a>
         * <a href="#">首页</a>
         * <li><a href="#">首页</a></li>
         * List<string,string> itemValue
             */
             /// <summary>
             /// 生成无序列表包含a标签
             /// </summary>
             /// <param name="htmlHelper">处理HTML对象的扩展方法</param>
             /// <param name="itemValue">列表项数组</param>
             /// <returns></returns>
        public static MvcHtmlString SetUl(this HtmlHelper htmlHelper, string[] itemValue)
        {
            TagBuilder tag = new TagBuilder("ul ");
            foreach (var item in itemValue)
            {
                //创建li标签
                TagBuilder liTag = new TagBuilder("li");
                //创建a标签
                TagBuilder aTag = new TagBuilder("a");
                //在a标签上设置属性href="#",向标签添加新特性
                aTag.MergeAttribute("href", "#");
                //a标签之间添加文本
                aTag.SetInnerText(item);
                //通过InnerHtml，给li标签添加 创建的a标签内容（标签、文本）
                liTag.InnerHtml=aTag.ToString();
                //通过InnerHtml，给ul标签累加创建的li标签内容
                tag.InnerHtml += liTag.ToString();
            }
            //MvcHtmlString类的构造方法，构造一个新的标签返回
            return new MvcHtmlString(tag.ToString());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="htmlHelper">处理HTML对象的扩展方法</param>
        /// <param name="itemValue">列表项数组</param>
        /// <param name="ClassName">类名</param>
        /// <returns></returns>
        public static MvcHtmlString SetUl(this HtmlHelper htmlHelper, string[] itemValue,string ClassName)
        {
            TagBuilder tag = new TagBuilder("ul ");
            tag.AddCssClass(ClassName);
            foreach (var item in itemValue)
            {
                //创建li标签
                TagBuilder liTag = new TagBuilder("li");
                //创建a标签
                TagBuilder aTag = new TagBuilder("a");
                //在a标签上设置属性href="#",向标签添加新特性
                aTag.MergeAttribute("href", "#");
                //a标签之间添加文本
                aTag.SetInnerText(item);
                //通过InnerHtml，给li标签添加 创建的a标签内容（标签、文本）
                liTag.InnerHtml = aTag.ToString();
                //通过InnerHtml，给ul标签累加创建的li标签内容
                tag.InnerHtml += liTag.ToString();
            }
            //MvcHtmlString类的构造方法，构造一个新的标签返回
            return new MvcHtmlString(tag.ToString());
        }
    }
}