﻿/*
 *  Copyright (c) 2014-Present, Facebook, Inc.
 *  All rights reserved.
 *
 *  This source code is licensed under the BSD-style license found in the
 *  LICENSE file in the root directory of this source tree. An additional grant
 *  of patent rights can be found in the PATENTS file in the same directory.
 */

using System.Web;
using React.Exceptions;
using React.TinyIoC;
using ServiceStack.Html;
using IHtmlHelper = ServiceStack.Html.HtmlHelper;

#if LEGACYASPNET
namespace React.Web.Mvc
#else
namespace React.Web.SSS.Mvc
#endif
{
    /// <summary>
    /// HTML Helpers for utilising React from an ASP.NET MVC application.
    /// </summary>
    public static class HtmlHelperExtensions
    {

        /// <summary>
        /// Gets the React environment
        /// </summary>
        private static IReactEnvironment Environment
        {
            get
            {
                try
                {
                    return ReactEnvironment.Current;
                }
                catch (TinyIoCResolutionException ex)
                {
                    throw new ReactNotInitialisedException(
						"ReactJS.NET has not been initialised correctly.",
                        ex
                    );
                }
            }
        }

        /// <summary>
        /// Renders the specified React component
        /// </summary>
        /// <typeparam name="T">Type of the props</typeparam>
        /// <param name="htmlHelper">HTML helper</param>
        /// <param name="componentName">Name of the component</param>
        /// <param name="props">Props to initialise the component with</param>
        /// <param name="htmlTag">HTML tag to wrap the component in. Defaults to &lt;div&gt;</param>
        /// <param name="containerId">ID to use for the container HTML tag. Defaults to an auto-generated ID</param>
        /// <param name="clientOnly">Skip rendering server-side and only output client-side initialisation code. Defaults to <c>false</c></param>
        /// <param name="serverOnly">Skip rendering React specific data-attributes during server side rendering. Defaults to <c>false</c></param>
        /// <param name="containerClass">HTML class(es) to set on the container tag</param>
        /// <returns>The component's HTML</returns>
        public static MvcHtmlString React<T>(
            this IHtmlHelper htmlHelper,
            string componentName,
            T props,
            string htmlTag = null,
            string containerId = null,
            bool clientOnly = false,
            bool serverOnly = false,
            string containerClass = null
        )
        {
            var reactComponent = Environment.CreateComponent(componentName, props, containerId, clientOnly);
            if (!string.IsNullOrEmpty(htmlTag))
            {
                reactComponent.ContainerTag = htmlTag;
            }
            if (!string.IsNullOrEmpty(containerClass))
            {
                reactComponent.ContainerClass = containerClass;
            }
            var result = reactComponent.RenderHtml(clientOnly, serverOnly);
            return new MvcHtmlString(result);
        }

        /// <summary>
        /// Renders the specified React component, along with its client-side initialisation code.
        /// Normally you would use the <see cref="React{T}"/> method, but <see cref="ReactWithInit{T}"/>
        /// is useful when rendering self-contained partial views.
        /// </summary>
        /// <typeparam name="T">Type of the props</typeparam>
        /// <param name="htmlHelper">HTML helper</param>
        /// <param name="componentName">Name of the component</param>
        /// <param name="props">Props to initialise the component with</param>
        /// <param name="htmlTag">HTML tag to wrap the component in. Defaults to &lt;div&gt;</param>
        /// <param name="containerId">ID to use for the container HTML tag. Defaults to an auto-generated ID</param>
        /// <param name="clientOnly">Skip rendering server-side and only output client-side initialisation code. Defaults to <c>false</c></param>
        /// <param name="containerClass">HTML class(es) to set on the container tag</param>
        /// <returns>The component's HTML</returns>
        public static IHtmlString ReactWithInit<T>(
            this IHtmlHelper htmlHelper,
            string componentName,
            T props,
            string htmlTag = null,
            string containerId = null,
            bool clientOnly = false,
            string containerClass = null
        )
        {
            var reactComponent = Environment.CreateComponent(componentName, props, containerId, clientOnly);
            if (!string.IsNullOrEmpty(htmlTag))
            {
                reactComponent.ContainerTag = htmlTag;
            }
            if (!string.IsNullOrEmpty(containerClass))
            {
                reactComponent.ContainerClass = containerClass;
            }
            var html = reactComponent.RenderHtml(clientOnly);
			var script = new TagBuilder("script")
			{
				InnerHtml = reactComponent.RenderJavaScript()
			};
            return new HtmlString(html + System.Environment.NewLine + script.ToString());
        }

        /// <summary>
        /// Renders the JavaScript required to initialise all components client-side. This will
        /// attach event handlers to the server-rendered HTML.
        /// </summary>
        /// <returns>JavaScript for all components</returns>
        public static IHtmlString ReactInitJavaScript(this IHtmlHelper htmlHelper, bool clientOnly = false)
        {
            var script = Environment.GetInitJavaScript(clientOnly);
            var tag = new TagBuilder("script")
			{
				InnerHtml = script
			};
			return new HtmlString(tag.ToString());

        }
    }
}
