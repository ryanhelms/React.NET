/*
 *  Copyright (c) 2015, Facebook, Inc.
 *  All rights reserved.
 *
 *  This source code is licensed under the BSD-style license found in the
 *  LICENSE file in the root directory of this source tree. An additional grant
 *  of patent rights can be found in the PATENTS file in the same directory.
 */

using System;
using React.TinyIoC;

namespace React.Sample.ConsoleApp
{
	class Program
	{
		static void Main(string[] args)
		{
			Initialize();

			ReactSiteConfiguration.Configuration
				.SetReuseJavaScriptEngines(false)
				.AddScript("Sample.jsx");

			var environment = ReactEnvironment.Current;
			var component = environment.CreateComponent("HelloWorld", new { name = "Daniel" });
			// renderServerOnly omits the data-reactid attributes
			var html = component.RenderHtml(renderServerOnly: true);

			Console.WriteLine(html);
			Console.ReadKey();
		}

		private static void Initialize()
		{
			Initializer.Initialize(registration => registration.AsSingleton());
			var container = React.AssemblyRegistration.Container;

			container.Register<ICache, NullCache>();
			container.Register<IFileSystem, SimpleFileSystem>();
		}
	}
}
