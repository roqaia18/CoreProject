#pragma checksum "D:\ITI\Final Final\Final_Project\Final_Project\Final_Project\Views\Orders\EachProduct.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "3ebc0a94d8ca792bd0ed46b98ef6097dca132e45"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Orders_EachProduct), @"mvc.1.0.view", @"/Views/Orders/EachProduct.cshtml")]
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
#nullable restore
#line 1 "D:\ITI\Final Final\Final_Project\Final_Project\Final_Project\Views\_ViewImports.cshtml"
using Final_Project;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\ITI\Final Final\Final_Project\Final_Project\Final_Project\Views\_ViewImports.cshtml"
using Final_Project.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3ebc0a94d8ca792bd0ed46b98ef6097dca132e45", @"/Views/Orders/EachProduct.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c4fb7ef317ae05155a488c52b2e91b7ab94cdc5a", @"/Views/_ViewImports.cshtml")]
    public class Views_Orders_EachProduct : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "D:\ITI\Final Final\Final_Project\Final_Project\Final_Project\Views\Orders\EachProduct.cshtml"
  
    ViewData["Title"] = "EachProduct";
    Layout = "_Layout";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n<h2 class=\"text-center\">");
#nullable restore
#line 7 "D:\ITI\Final Final\Final_Project\Final_Project\Final_Project\Views\Orders\EachProduct.cshtml"
                   Write(ViewBag.CustomerName);

#line default
#line hidden
#nullable disable
            WriteLiteral(" Products</h2>\r\n\r\n<div class=\"container d-flex justify-content-center\">\r\n\r\n    <div class=\"card w-50\">\r\n\r\n        <div class=\"card-body\">\r\n\r\n");
#nullable restore
#line 15 "D:\ITI\Final Final\Final_Project\Final_Project\Final_Project\Views\Orders\EachProduct.cshtml"
             foreach (var item in ViewBag.allProducts)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <div class=\"py-2 my-2\" style=\"border: 1px solid black\">\r\n                    <img");
            BeginWriteAttribute("src", " src=\"", 430, "\"", 455, 1);
#nullable restore
#line 18 "D:\ITI\Final Final\Final_Project\Final_Project\Final_Project\Views\Orders\EachProduct.cshtml"
WriteAttributeValue("", 436, item.Product.Image, 436, 19, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral("\r\n                         class=\"card-img-top img-fluid\" alt=\"Product Image\">\r\n\r\n                    <h5 class=\"card-title\">");
#nullable restore
#line 21 "D:\ITI\Final Final\Final_Project\Final_Project\Final_Project\Views\Orders\EachProduct.cshtml"
                                      Write(item.Product.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h5>\r\n                    <p class=\"card-text\">Quantity: ");
#nullable restore
#line 22 "D:\ITI\Final Final\Final_Project\Final_Project\Final_Project\Views\Orders\EachProduct.cshtml"
                                              Write(item.Quantity);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                </div>\r\n");
#nullable restore
#line 24 "D:\ITI\Final Final\Final_Project\Final_Project\Final_Project\Views\Orders\EachProduct.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </div>\r\n");
            WriteLiteral("\r\n    </div>\r\n\r\n</div>");
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
