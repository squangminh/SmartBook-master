using AdwardSoft.Provider.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace AdwardSoft.Provider.Helper
{
    public static class URLHelper
    {
        public static string RewriteUrl(string url_formart)
        {
            if (url_formart == null) return string.Empty;
            return url_formart.NonUnicode();
        }
        public static string NonUnicode(this string text, bool islower = true)
        {
            string[] arr1 = new string[] { "á", "à", "ả", "ã", "ạ", "â", "ấ", "ầ", "ẩ", "ẫ", "ậ", "ă", "ắ", "ằ", "ẳ", "ẵ", "ặ",
                                            "đ",
                                            "é","è","ẻ","ẽ","ẹ","ê","ế","ề","ể","ễ","ệ",
                                            "í","ì","ỉ","ĩ","ị",
                                            "ó","ò","ỏ","õ","ọ","ô","ố","ồ","ổ","ỗ","ộ","ơ","ớ","ờ","ở","ỡ","ợ",
                                            "ú","ù","ủ","ũ","ụ","ư","ứ","ừ","ử","ữ","ự",
                                            "ý","ỳ","ỷ","ỹ","ỵ",};
            string[] arr2 = new string[] { "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a",
                                            "d",
                                            "e","e","e","e","e","e","e","e","e","e","e",
                                            "i","i","i","i","i",
                                            "o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o",
                                            "u","u","u","u","u","u","u","u","u","u","u",
                                            "y","y","y","y","y",};
            for (int i = 0; i < arr1.Length; i++)
            {
                text = text.Replace(arr1[i], arr2[i]);
                text = text.Replace(arr1[i].ToUpper(), arr2[i].ToUpper());
            }
            text = Regex.Replace(text, @"[^0-9a-zA-Z]+", "-");
            text = Regex.Replace(text, @"\s+", "-");
            return (islower ? text.ToLower() : text);
        }

        #region ApiResources
        public static string ApiResources(this ControllerBase controller)
        {
            return controller.RouteData.Values["Controller"].ToString() + "/" + controller.RouteData.Values["Action"].ToString();
        }
        public static string ApiResources(this ControllerBase controller, string action)
        {
            return controller.RouteData.Values["Controller"].ToString() + "/" + action;
        }
        public static List<Breadcrumb> BreadcrumbLink(params string[] nodes)
        {
            var items = new List<Breadcrumb>();
            if (nodes == null) return items;
            foreach (var node in nodes)
            {
                items.Add(new Breadcrumb { Item = node });
            }
            return items;
        }
        #endregion

    }
}
