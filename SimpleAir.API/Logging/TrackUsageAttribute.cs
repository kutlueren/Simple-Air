using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;

namespace SimpleAir.API.Logging
{
    public class TrackUsageAttribute : ActionFilterAttribute
    {
        private string _product, _layer, _activityName;
        private PerfTracker _tracker;

        public TrackUsageAttribute(string product, string layer, string activityName)
        {
            _product = product;
            _layer = layer;
            _activityName = activityName;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var request = context.HttpContext.Request;
            var activity = $"{request.Path}-{request.Method}";

            var dict = new Dictionary<string, object>();
            foreach (var key in context.RouteData.Values?.Keys)
                dict.Add($"RouteData-{key}", (string)context.RouteData.Values[key]);

            var details = WebHelper.GetWebLogDetail(_product, _layer, activity,
                context.HttpContext, dict);

            _tracker = new PerfTracker(details);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (_tracker != null)
                _tracker.Stop(context.HttpContext.Response);
        }
    }
}