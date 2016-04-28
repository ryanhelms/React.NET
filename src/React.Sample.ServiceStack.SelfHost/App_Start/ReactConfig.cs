/*
 *  Copyright (c) 2014-Present, Facebook, Inc.
 *  All rights reserved.
 *
 *  This source code is licensed under the BSD-style license found in the
 *  LICENSE file in the root directory of this source tree. An additional grant 
 *  of patent rights can be found in the PATENTS file in the same directory.
 */

using React.TinyIoC;
using ServiceStack;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(React.Sample.SSS.SelfHost.ReactConfig), "Configure")]

namespace React.Sample.SSS.SelfHost
{
    public static class ReactConfig
    {
        public static void Configure()
        {
            var container = React.AssemblyRegistration.Container;

            container.Register<ICache, NullCache>();
            container.Register<IFileSystem, SimpleFileSystem>().AsSingleton();

            InitializeReact(container);
        }

        private static void InitializeReact(TinyIoCContainer container)
        {
            Initializer.Initialize(registration => registration.AsSingleton());
            
            ReactSiteConfiguration.Configuration
                .SetReuseJavaScriptEngines(false)
                .AddJsxScripts("~/../".MapHostAbsolutePath());
        }
    }
}