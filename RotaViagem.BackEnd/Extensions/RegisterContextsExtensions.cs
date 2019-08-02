using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection {
    public static class RegisterContextsExtensions {
        public static void AddContexts (this IServiceCollection services, Assembly assembly) {

            var types = assembly.GetTypes ();

            types.Where (t => !t.IsAbstract &&
                    t.Name.EndsWith ("Context", StringComparison.InvariantCultureIgnoreCase) &&
                    t.GetInterfaces ().Length == 1 &&
                    t.GetInterfaces () [0].Name.EndsWith ("Context", StringComparison.InvariantCultureIgnoreCase))
                .ToList ()
                .ForEach (t => {
                    services.AddSingleton (t.GetInterfaces () [0], t);
                });
        }
    }
}