using Microsoft.AspNetCore.Rewrite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdwardSoft.Web.Inside.TagHelpers
{
    public class ConfigUrl
    {
        public static RewriteOptions rewrite()
        {
            return new RewriteOptions()
                .AddRewrite(@"dang-nhap", "User/Login", true)
                .AddRewrite(@"dang-xuat", "User/Logout", true)
                .AddRewrite(@"quen-mat-khau", "User/ForgotPassword", true)
                .AddRewrite(@"thanh-vien", "User/Index", true)
                .AddRewrite(@"khong-co-quyen-truy-cap", "/Account/AccessDenied?ReturnUrl=$1", true)
                .AddRewrite(@"vi-tri", "Position/Index", true)
                .AddRewrite(@"trang-loi", "Home/Error", true)
                .AddRewrite(@"trang-chu", "Home/Index", true)
                .AddRewrite(@"thong-tin-ca-nhan", "Home/Profile", true)
                .AddRewrite(@"phong-ban", "Department/Index", true)
                .AddRewrite(@"phieu-yeu-cau", "IssueForm/Index", true)
                .AddRewrite(@"trang-thai-xu-ly-yeu-cau", "IssueStatus/Index", true)
                .AddRewrite(@"loai-yeu-cau", "IssueType/Index", true)
                .AddRewrite(@"lich-trinh", "Schedule/Index", true)
                .AddRewrite(@"quyen-truy-cap", "Role/Index", true)
                .AddRewrite(@"chi-tiet-quyen", "Permission/Index", true)
                .AddRewrite(@"to-chuc", "Organizational/Index", true)
                .AddRewrite(@"ke-hoach-dao-tao-phe-duyet", "TrainingPlan/_FormStatus", true)
                .AddRewrite(@"ke-hoach-dao-tao", "TrainingPlan/Index", true)
                .AddRewrite(@"chi-tiet-nhan-vien/(\d+)", "Employee/EmployeeForm?id=$1", true)
                .AddRewrite(@"nhan-vien", "Employee/Index", true)
                .AddRewrite(@"danh-sach-KPI", "KPI/Index", true)
                .AddRewrite(@"bieu-mau-KPI", "KPIForm/Index", true)
                .AddRewrite(@"danh-sach-KPI-theo-phong-ban&chuc-vu", "KPIFormPosition/Index", true);
        }
    }
}
