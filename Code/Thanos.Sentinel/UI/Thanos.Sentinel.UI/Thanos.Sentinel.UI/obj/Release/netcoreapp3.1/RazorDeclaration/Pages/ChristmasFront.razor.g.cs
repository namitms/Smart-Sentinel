// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

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
#line 3 "C:\Users\namit\Documents\GitHub\Smart-Sentinel\Code\Thanos.Sentinel\UI\Thanos.Sentinel.UI\Thanos.Sentinel.UI\Pages\ChristmasFront.razor"
using Thanos.Sentinel.UI.Data;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\namit\Documents\GitHub\Smart-Sentinel\Code\Thanos.Sentinel\UI\Thanos.Sentinel.UI\Thanos.Sentinel.UI\Pages\ChristmasFront.razor"
using Microsoft.AspNetCore.WebUtilities;

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/christmasfront")]
    public partial class ChristmasFront : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 42 "C:\Users\namit\Documents\GitHub\Smart-Sentinel\Code\Thanos.Sentinel\UI\Thanos.Sentinel.UI\Thanos.Sentinel.UI\Pages\ChristmasFront.razor"
       

    private string message;
    private List<string> blogs = null;


    protected override async Task OnInitializedAsync()
    {
        if (state.IsLoggedIn == false)
        {
            NavManager.NavigateTo("/");
        }

        blogs = await blogService.GetLoops("front");
    }

    private void Wave(string flow)
    {
        message = blogService.GetJson(flow).Result;
        //message = flow + " # " + DateTime.Now.ToString();
    }

    private void Clear()
    {
        blogService.Clear("front");
    }

#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private State state { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private NavigationManager NavManager { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private BlogService blogService { get; set; }
    }
}
#pragma warning restore 1591
