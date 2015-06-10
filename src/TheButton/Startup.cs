using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Routing;
using Microsoft.Framework.DependencyInjection;
using Newtonsoft.Json.Linq;

namespace TheButton
{
    public class Startup
    {
        //private static readonly Lazy<List<ButtonPress>> lazyList = new Lazy<List<ButtonPress>>(() => LoadFile());
        private static readonly Lazy<JArray> lazyJson = new Lazy<JArray>(() => LoadFile());

        public static JArray ButtonPressesInstance { get { return lazyJson.Value; } }

        private Startup()
        {
        }

        public Startup(IHostingEnvironment env)
        {
        }

        // This method gets called by a runtime.
        // Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            // Uncomment the following line to add Web API services which makes it easier to port Web API 2 controllers.
            // You will also need to add the Microsoft.AspNet.Mvc.WebApiCompatShim package to the 'dependencies' section of project.json.
            // services.AddWebApiConventions();
        }

        // Configure is called after ConfigureServices is called.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // Configure the HTTP request pipeline.
            app.UseStaticFiles();

            // Add MVC to the request pipeline.
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "Default",
                    template: string.Empty,
                    defaults: new { controller = "Home", action = "Index" });
            });
            // Add the following route for porting Web API 2 controllers.
            // routes.MapWebApiRoute("DefaultApi", "api/{controller}/{id?}");
        }

        private static JArray LoadFile()
        {
            StreamReader reader = File.OpenText("thebutton_presses.csv");
            List<ButtonPress> presses = new List<ButtonPress>();
            JArray a = new JArray();
            string line = null;
            while ((line = reader.ReadLine()) != null)
            {
                if (!line.StartsWith("press"))
                {
                    string[] split = line.Split(',');
                    if (!string.IsNullOrEmpty(split[0]) && !string.IsNullOrEmpty(split[1]) &&
                        !string.IsNullOrEmpty(split[2]) && !string.IsNullOrEmpty(split[3]))
                    {
                        presses.Add(new ButtonPress(split[0], split[1], split[2], split[3]));
                        JObject o = new JObject();
                        ButtonPress press = presses[presses.Count - 1];
                        o["press_time"] = press.PressTime;
                        o["seconds"] = press.Seconds;
                        o["css"] = press.CSS;
                        o["outage_press"] = press.OutagePress;
                        a.Add(o);
                    }
                }
            }
            reader.Dispose();
            return a;
            //return presses;
        }
    }
}
