//
//  ZitadelResourceBuilderExtensions.cs
//
//  Author:
//       LuzFaltex Contributors <support@luzfaltex.com>
//
//  Copyright (c) LuzFaltex, LLC.
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Lesser General Public License for more details.
//
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aspire.Hosting;
using Aspire.Hosting.ApplicationModel;

namespace LuzFaltex.Aspire.Hosting.Zitadel
{
    /// <summary>
    /// Provides extension methods for adding Zitadel resources to an <see cref="IDistributedApplicationBuilder"/>.
    /// </summary>
    public static class ZitadelResourceBuilderExtensions
    {
        private const string AdminEnvVarName = "ZITADEL_FIRSTINSTANCE_ORG_HUMAN_USERNAME";
        private const string AdminPasswordEnvVarName = "ZITADEL_FIRSTINSTANCE_ORG_HUMAN_PASSWORD";
        private const int DefaultContainerPort = 8080;

        /// <summary>
        /// Adds a Zitadel container to the application model.
        /// </summary>
        /// <param name="builder">The <see cref="IDistributedApplicationBuilder"/> to modify.</param>
        /// <param name="name">The name of the resource.</param>
        /// <param name="port">The host port that the underlying container is bound to when running locally.</param>
        /// <param name="adminUsername">The parameter used as the admin for the Zitadel resource. If <see langword="null"/> a default value will be used.</param>
        /// <param name="adminPassword">The parameter used as the admin password for the Zitadel resource. If <see langword="null"/> a default password will be used.</param>
        /// <param name="zitadelVersion">The version tag for the package. Defaults to <c>latest</c>.</param>
        /// <returns>A reference to the <see cref="IResourceBuilder{T}"/>.</returns>
        /// <remarks>
        /// The container exposes port 8080 by default.
        /// This version of the package defaults to the <inheritdoc cref="ZitadelContainerImageTags.Image"/> container image.
        /// </remarks>
        /// <example>
        /// Used in application host:
        /// <code lang="csharp">
        /// var zitadel = builder.AddZitadel("zitadel");
        ///
        /// var myService = builder.AddProject?&lt;Projects.MyService&gt;()
        ///                        .WithReference(zitadel);
        /// </code>
        /// </example>
        public static IResourceBuilder<ZitadelResource> AddZitadel
        (
            this IDistributedApplicationBuilder builder,
            string name,
            int? port = null,
            IResourceBuilder<ParameterResource>? adminUsername = null,
            IResourceBuilder<ParameterResource>? adminPassword = null,
            string zitadelVersion = "latest"
        )
        {
            ArgumentNullException.ThrowIfNull(builder);
            ArgumentNullException.ThrowIfNull(name);

            var passwordParameter = adminPassword?.Resource ?? ParameterResourceBuilderExtensions.CreateDefaultPasswordParameter(builder, $"{name}-password");

            var resource = new ZitadelResource(name, adminUsername?.Resource, passwordParameter);

            var zitadel = builder
                .AddResource(resource)
                .WithImage(ZitadelContainerImageTags.Image)
                .WithImageRegistry(ZitadelContainerImageTags.Registry)
                .WithImageTag(zitadelVersion)
                .WithHttpEndpoint(port: port, targetPort: DefaultContainerPort)
                .WithEnvironment(context =>
                {
                    context.EnvironmentVariables[AdminEnvVarName] = resource.AdminReference;
                    context.EnvironmentVariables[AdminPasswordEnvVarName] = resource.AdminPasswordParameter;
                });

            return zitadel;
        }
    }
}
