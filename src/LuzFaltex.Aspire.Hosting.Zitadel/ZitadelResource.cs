//
//  ZitadelResource.cs
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
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Aspire.Hosting;
using Aspire.Hosting.ApplicationModel;

namespace LuzFaltex.Aspire.Hosting.Zitadel
{
    /// <summary>
    /// A resource that represents a Zitadel resource.
    /// </summary>
    /// <param name="name">The name of the resource.</param>
    /// <param name="admin">A parameter that contains the Zitadel admin, or <see langword="null"/> to  use the default value <c>"admin"</c>.</param>
    /// <param name="adminPassword">A parameter that contains the Zitadel admin password.</param>
    public sealed class ZitadelResource(string name, ParameterResource? admin, ParameterResource adminPassword)
        : ContainerResource(ThrowIfNull(name)), IResourceWithServiceDiscovery
    {
        private const string DefaultAdmin = "admin";

        /// <summary>
        /// Gets the parameter that contains the Zitadel admin.
        /// </summary>
        public ParameterResource? AdminUserNameParameter { get; } = admin;

        /// <summary>
        /// Gets a reference expression comprised of the admin username.
        /// </summary>
        internal ReferenceExpression AdminReference =>
            AdminUserNameParameter is not null
            ? ReferenceExpression.Create($"{AdminUserNameParameter}")
            : ReferenceExpression.Create($"{DefaultAdmin}");

        /// <summary>
        /// Gets a parameter resource for the admin password.
        /// </summary>
        public ParameterResource AdminPasswordParameter { get; } = adminPassword;

        private static T ThrowIfNull<T>([NotNull] T? argument, [CallerArgumentExpression(nameof(argument))] string? paramName = null)
            => argument ?? throw new ArgumentNullException(paramName);
    }
}
