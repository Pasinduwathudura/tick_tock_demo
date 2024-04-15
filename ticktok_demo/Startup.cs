/*using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(ticktok_demo.Startup))]

namespace ticktok_demo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {

            ConfigureAuth(app);
        }
    }
}
*/
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(ticktok_demo.Startup))]

namespace ticktok_demo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Enable CORS
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            ConfigureAuth(app);
        }
    }
}
