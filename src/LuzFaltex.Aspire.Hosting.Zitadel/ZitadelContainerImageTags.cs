//
//  ZitadelContainerImageTags.cs
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

namespace LuzFaltex.Aspire.Hosting.Zitadel
{
    /// <summary>
    /// Provides a set of constant values for Zitadel's container.
    /// </summary>
    internal static class ZitadelContainerImageTags
    {
        /// <summary>
        /// Gets the registry address.
        /// </summary>
        public const string Registry = "ghcr.io";

        /// <summary>
        /// Gets the image name.
        /// </summary>
        public const string Image = "zitadel/zitadel";
    }
}
