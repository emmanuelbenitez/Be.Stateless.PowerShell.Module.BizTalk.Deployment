﻿#region Copyright & License

// Copyright © 2012 - 2021 François Chabot
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
// http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

#endregion

using System.Diagnostics.CodeAnalysis;
using System.Management.Automation;
using Be.Stateless.BizTalk.Install.Command;
using Be.Stateless.BizTalk.Install.Command.Extensions;

namespace Be.Stateless.BizTalk.Deployment.Cmdlet.Binding
{
	[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global", Justification = "Cmdlet.")]
	[Cmdlet(VerbsData.Convert, Nouns.ApplicationBinding)]
	[OutputType(typeof(void))]
	public class ConvertApplicationBinding : ApplicationBindingBasedCmdlet
	{
		#region Base Class Member Overrides

		protected override void ProcessRecord()
		{
			WriteInformation($"BizTalk Application {ResolvedApplicationBindingType.FullName} bindings are being converted to XML...", null);
			ApplicationBindingCommandFactory
				.CreateApplicationBindingGenerationCommand(ResolvedApplicationBindingType)
				.Initialize(this)
				.Execute(msg => WriteInformation(msg, null));
			WriteInformation($"BizTalk Application {ResolvedApplicationBindingType.FullName} bindings have been converted to XML.", null);
		}

		#endregion

		[Parameter(Mandatory = true)]
		[ValidateNotNullOrEmpty]
		public string OutputFilePath { get; set; }

		protected internal string ResolvedOutputFilePath => _resolvedOutputFilePath ??= ResolvePowerShellPath(OutputFilePath, nameof(OutputFilePath));

		private string _resolvedOutputFilePath;
	}
}
