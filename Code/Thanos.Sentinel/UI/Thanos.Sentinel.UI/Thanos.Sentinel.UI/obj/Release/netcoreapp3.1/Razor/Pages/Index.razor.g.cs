#pragma checksum "C:\Users\namit\Documents\GitHub\Smart-Sentinel\Code\Thanos.Sentinel\UI\Thanos.Sentinel.UI\Thanos.Sentinel.UI\Pages\Index.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "c0d29acf12373f5a8f79593139b12ac74731c4ea"
// <auto-generated/>
#pragma warning disable 1591
namespace Thanos.Sentinel.UI.Pages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "C:\Users\namit\Documents\GitHub\Smart-Sentinel\Code\Thanos.Sentinel\UI\Thanos.Sentinel.UI\Thanos.Sentinel.UI\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\namit\Documents\GitHub\Smart-Sentinel\Code\Thanos.Sentinel\UI\Thanos.Sentinel.UI\Thanos.Sentinel.UI\_Imports.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\namit\Documents\GitHub\Smart-Sentinel\Code\Thanos.Sentinel\UI\Thanos.Sentinel.UI\Thanos.Sentinel.UI\_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\namit\Documents\GitHub\Smart-Sentinel\Code\Thanos.Sentinel\UI\Thanos.Sentinel.UI\Thanos.Sentinel.UI\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\namit\Documents\GitHub\Smart-Sentinel\Code\Thanos.Sentinel\UI\Thanos.Sentinel.UI\Thanos.Sentinel.UI\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\namit\Documents\GitHub\Smart-Sentinel\Code\Thanos.Sentinel\UI\Thanos.Sentinel.UI\Thanos.Sentinel.UI\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Users\namit\Documents\GitHub\Smart-Sentinel\Code\Thanos.Sentinel\UI\Thanos.Sentinel.UI\Thanos.Sentinel.UI\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\Users\namit\Documents\GitHub\Smart-Sentinel\Code\Thanos.Sentinel\UI\Thanos.Sentinel.UI\Thanos.Sentinel.UI\_Imports.razor"
using Thanos.Sentinel.UI;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "C:\Users\namit\Documents\GitHub\Smart-Sentinel\Code\Thanos.Sentinel\UI\Thanos.Sentinel.UI\Thanos.Sentinel.UI\_Imports.razor"
using Thanos.Sentinel.UI.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\namit\Documents\GitHub\Smart-Sentinel\Code\Thanos.Sentinel\UI\Thanos.Sentinel.UI\Thanos.Sentinel.UI\Pages\Index.razor"
using Microsoft.AspNetCore.WebUtilities;

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/")]
    public partial class Index : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.AddMarkupContent(0, "<h1>Welcome to Sentinel 🤖</h1>\r\n\r\n\r\n\r\n");
            __builder.OpenElement(1, "table");
            __builder.AddAttribute(2, "style", "height:auto; width:auto");
            __builder.OpenElement(3, "tbody");
            __builder.OpenElement(4, "tr");
            __builder.OpenElement(5, "td");
            __builder.OpenElement(6, "p");
            __builder.AddAttribute(7, "style", "color:red;");
            __builder.AddContent(8, 
#nullable restore
#line 14 "C:\Users\namit\Documents\GitHub\Smart-Sentinel\Code\Thanos.Sentinel\UI\Thanos.Sentinel.UI\Thanos.Sentinel.UI\Pages\Index.razor"
                                        errormessagee

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(9, "\r\n        ");
            __builder.OpenElement(10, "tr");
            __builder.OpenElement(11, "td");
            __builder.OpenElement(12, "input");
            __builder.AddAttribute(13, "hidden", 
#nullable restore
#line 19 "C:\Users\namit\Documents\GitHub\Smart-Sentinel\Code\Thanos.Sentinel\UI\Thanos.Sentinel.UI\Thanos.Sentinel.UI\Pages\Index.razor"
                                 IsDisabled

#line default
#line hidden
#nullable disable
            );
            __builder.AddAttribute(14, "style", "width: 350px; background-color: white; color: darkblue");
            __builder.AddAttribute(15, "class", "btn btn-primary");
            __builder.AddAttribute(16, "value", Microsoft.AspNetCore.Components.BindConverter.FormatValue(
#nullable restore
#line 19 "C:\Users\namit\Documents\GitHub\Smart-Sentinel\Code\Thanos.Sentinel\UI\Thanos.Sentinel.UI\Thanos.Sentinel.UI\Pages\Index.razor"
                                                                                                                                            key

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(17, "onchange", Microsoft.AspNetCore.Components.EventCallback.Factory.CreateBinder(this, __value => key = __value, key));
            __builder.SetUpdatesAttributeName("value");
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(18, "\r\n            ");
            __builder.OpenElement(19, "td");
            __builder.OpenElement(20, "button");
            __builder.AddAttribute(21, "class", "btn btn-primary");
            __builder.AddAttribute(22, "hidden", 
#nullable restore
#line 22 "C:\Users\namit\Documents\GitHub\Smart-Sentinel\Code\Thanos.Sentinel\UI\Thanos.Sentinel.UI\Thanos.Sentinel.UI\Pages\Index.razor"
                                                         IsDisabled

#line default
#line hidden
#nullable disable
            );
            __builder.AddAttribute(23, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 22 "C:\Users\namit\Documents\GitHub\Smart-Sentinel\Code\Thanos.Sentinel\UI\Thanos.Sentinel.UI\Thanos.Sentinel.UI\Pages\Index.razor"
                                                                               () => Login()

#line default
#line hidden
#nullable disable
            ));
            __builder.AddContent(24, "Login");
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(25, "\r\n<img style=\"height:420px;\" src=\"https://media0.giphy.com/media/t7y1lOOXQ43zW/source.gif\" alt=\"Jarvis\">");
        }
        #pragma warning restore 1998
#nullable restore
#line 29 "C:\Users\namit\Documents\GitHub\Smart-Sentinel\Code\Thanos.Sentinel\UI\Thanos.Sentinel.UI\Thanos.Sentinel.UI\Pages\Index.razor"
       
    string errormessagee = null;
    int currentCount = 0;
    bool IsDisabled = false;
    string key = null;

    protected override void OnInitialized()
    {

        if (state.IsLoggedIn == false)
        {
            IsDisabled = false;
            var uri = NavManager.ToAbsoluteUri(NavManager.Uri);
            if (QueryHelpers.ParseQuery(uri.Query).TryGetValue("Key", out var _initialCount))
            {
                if (_initialCount != "spirit")
                {
                    errormessagee = "Please login";
                }
                else
                {
                    state.IsLoggedIn = true;
                    IsDisabled = true;
                }
            }
            else
            {
                errormessagee = "Please login";
            }

        }
        else
        {
            IsDisabled = true;
        }
    }

    void Login()
    {
        if (key != "spirit")
        {
            errormessagee = "Wrong Key";
        }
        else
        {
            state.IsLoggedIn = true;

            IsDisabled = true;
            errormessagee = "";
        }
    }


#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private State state { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private NavigationManager NavManager { get; set; }
    }
}
#pragma warning restore 1591
