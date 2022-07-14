namespace NAPS2.Tools.Project.Verification;

public static class ExeSetupVerifier
{
    public static void Verify(Platform platform, string version, bool verbose)
    {
        if (!ProjectHelper.RequireElevation()) return;
        
        ExeInstaller.Install(platform, version, verbose);
        Verifier.RunVerificationTests(ProjectHelper.GetInstallationFolder(platform), verbose);

        var exePath = ProjectHelper.GetPackagePath("exe", platform, version);
        Console.WriteLine(verbose ? $"Verified exe installer: {exePath}" : "Done.");
    }
}