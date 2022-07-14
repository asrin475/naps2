﻿using CommandLine;
using NAPS2.Tools.Localization;
using NAPS2.Tools.Project;
using NAPS2.Tools.Project.Packaging;
using NAPS2.Tools.Project.Verification;

namespace NAPS2.Tools;

public static class Program
{
    public static void Main(string[] args) =>
        Parser.Default
            .ParseArguments<CleanOptions, BuildOptions, TestOptions, PackageOptions, InstallOptions, VerifyOptions,
                TemplatesOptions, LanguageOptions>(args).MapResult(
                (CleanOptions opts) => CleanCommand.Run(opts),
                (BuildOptions opts) => BuildCommand.Run(opts),
                (TestOptions opts) => TestCommand.Run(opts),
                (PackageOptions opts) => PackageCommand.Run(opts),
                (InstallOptions opts) => InstallCommand.Run(opts),
                (VerifyOptions opts) => VerifyCommand.Run(opts),
                (TemplatesOptions opts) => TemplatesCommand.Run(opts),
                (LanguageOptions opts) => LanguageCommand.Run(opts),
                errors => 1);
}