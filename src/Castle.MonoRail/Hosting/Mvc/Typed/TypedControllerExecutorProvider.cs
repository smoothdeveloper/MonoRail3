namespace Castle.MonoRail.Framework
{
    using System.ComponentModel.Composition;
    using System.Web;
    using System.Web.Routing;
    using Primitives;

    [Export(typeof(ControllerExecutorProvider))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class TypedControllerExecutorProvider : ControllerExecutorProvider
    {
        [Import]
        public ExportFactory<TypedControllerExecutor> ExecutorFactory { get; set; } 

        public override ControllerExecutor CreateExecutor(ControllerMeta meta, RouteData data, HttpContextBase context)
        {
            var executor = ExecutorFactory.CreateExport().Value;

            executor.Meta = meta;
            executor.RouteData = data;

            return executor;
        }
    }
}