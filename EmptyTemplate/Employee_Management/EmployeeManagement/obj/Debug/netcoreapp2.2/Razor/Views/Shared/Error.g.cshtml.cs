#pragma checksum "D:\Practise_Home\ASP.NET CORE\EmptyTemplate\Employee_Management\EmployeeManagement\Views\Shared\Error.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "215bb07120cfa2035a1d4d15f2c263d3bece0af8"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_Error), @"mvc.1.0.view", @"/Views/Shared/Error.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Shared/Error.cshtml", typeof(AspNetCore.Views_Shared_Error))]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "D:\Practise_Home\ASP.NET CORE\EmptyTemplate\Employee_Management\EmployeeManagement\Views\_ViewImports.cshtml"
using EmployeeManagement.Model;

#line default
#line hidden
#line 2 "D:\Practise_Home\ASP.NET CORE\EmptyTemplate\Employee_Management\EmployeeManagement\Views\_ViewImports.cshtml"
using EmployeeManagement.ViewModel;

#line default
#line hidden
#line 3 "D:\Practise_Home\ASP.NET CORE\EmptyTemplate\Employee_Management\EmployeeManagement\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"215bb07120cfa2035a1d4d15f2c263d3bece0af8", @"/Views/Shared/Error.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"176821093bf3a713211f2a27487739c3dfcd37ea", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared_Error : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(0, 4, true);
            WriteLiteral("\r\n\r\n");
            EndContext();
#line 3 "D:\Practise_Home\ASP.NET CORE\EmptyTemplate\Employee_Management\EmployeeManagement\Views\Shared\Error.cshtml"
 if (ViewBag.ErrorTitle == null)
{

#line default
#line hidden
            BeginContext(41, 177, true);
            WriteLiteral("<h3>\r\n    an occured while procssing your request.\r\n    the support team is notified and we are working on the fix.\r\n</h3>\r\n<h5>\r\n     Plese contact us on atik@atik.com\r\n</h5>\r\n");
            EndContext();
#line 12 "D:\Practise_Home\ASP.NET CORE\EmptyTemplate\Employee_Management\EmployeeManagement\Views\Shared\Error.cshtml"
}
else
{

#line default
#line hidden
            BeginContext(230, 28, true);
            WriteLiteral("    <h1 class=\"text-danger\">");
            EndContext();
            BeginContext(259, 18, false);
#line 15 "D:\Practise_Home\ASP.NET CORE\EmptyTemplate\Employee_Management\EmployeeManagement\Views\Shared\Error.cshtml"
                       Write(ViewBag.ErrorTitle);

#line default
#line hidden
            EndContext();
            BeginContext(277, 35, true);
            WriteLiteral("</h1>\r\n    <h6 class=\"text-danger\">");
            EndContext();
            BeginContext(313, 20, false);
#line 16 "D:\Practise_Home\ASP.NET CORE\EmptyTemplate\Employee_Management\EmployeeManagement\Views\Shared\Error.cshtml"
                       Write(ViewBag.ErrorMessage);

#line default
#line hidden
            EndContext();
            BeginContext(333, 7, true);
            WriteLiteral("</h6>\r\n");
            EndContext();
#line 17 "D:\Practise_Home\ASP.NET CORE\EmptyTemplate\Employee_Management\EmployeeManagement\Views\Shared\Error.cshtml"
}

#line default
#line hidden
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591