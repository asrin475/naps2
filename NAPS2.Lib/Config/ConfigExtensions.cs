﻿using NAPS2.Ocr;
using NAPS2.Scan;

namespace NAPS2.Config;

public static class ConfigExtensions
{
    public static OcrParams DefaultOcrParams(this Naps2Config config)
    {
        if (!config.Get(c => c.EnableOcr))
        {
            return OcrParams.Empty;
        }
        return new OcrParams(
            config.Get(c => c.OcrLanguageCode),
            MapOcrMode(config.Get(c => c.OcrMode)),
            config.Get(c => c.OcrTimeoutInSeconds));
    }

    private static OcrMode MapOcrMode(LocalizedOcrMode ocrMode)
    {
        return ocrMode switch
        {
            LocalizedOcrMode.Fast => OcrMode.Fast,
            LocalizedOcrMode.FastWithPreProcess => OcrMode.FastWithPreProcess,
            LocalizedOcrMode.Best => OcrMode.BestWithPreProcess,
            LocalizedOcrMode.BestWithPreProcess => OcrMode.BestWithPreProcess,
            _ => OcrMode.Default
        };
    }

    public static OcrParams OcrAfterScanningParams(this Naps2Config config)
    {
        if (!config.Get(c => c.OcrAfterScanning))
        {
            return OcrParams.Empty;
        }
        return config.DefaultOcrParams();
    }

    public static ScanProfile DefaultProfileSettings(this Naps2Config config)
    {
        return config.Get(c => c.DefaultProfileSettings) ??
               new ScanProfile { Version = ScanProfile.CURRENT_VERSION };
    }
}