using Owin;
using Nancy;

namespace Samples.Nancy
{
    public class Startup
    {
        public static void Configuration(IAppBuilder app)
        {
            string rootpath = app.Properties["node.rootpath"] as string;

            app.UseNancy(options => options.Bootstrapper = new NodeBootstrapper(rootpath));
        }
    }

    public class NodeRootPathProvider : IRootPathProvider
    {
        private string rootpath;

        public NodeRootPathProvider(string rootpath)
        {
            this.rootpath = rootpath;
        }

        public string GetRootPath()
        {
            return this.rootpath;
        }
    }

    public class NodeBootstrapper : DefaultNancyBootstrapper
    {
        private string rootpath;

        public NodeBootstrapper(string rootpath)
            : base()
        {
            this.rootpath = rootpath;
        }

        protected override IRootPathProvider RootPathProvider
        {
            get { return new NodeRootPathProvider(this.rootpath); }
        }
    }
}
