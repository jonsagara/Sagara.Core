using Microsoft.IO;

namespace Sagara.Core.IO;

/// <summary>
/// Creates a <see cref="RecyclableMemoryStreamManager"/> that the caller can register as a singleton in 
/// a DI framework.
/// </summary>
public static class RecyclableMemoryStreamManagerHelper
{
    /// <summary>
    /// <para>Create a new <see cref="RecyclableMemoryStreamManager"/> instance that can be used by the DI framework.</para>
    /// <para>Uses the same defaults (as of Microsoft.IO.RecyclableMemoryStream v2.3.2) as the <see cref="RecyclableMemoryStreamManager"/> default ctor,
    /// except we cap the max small and large pool free bytes at 12.5 MB and 512 MB, respectively. RMSM leaves them unbounded.</para>
    /// </summary>
    /// <remarks>
    /// <para>See: https://github.com/microsoft/Microsoft.IO.RecyclableMemoryStream</para>
    /// </remarks>
    public static RecyclableMemoryStreamManager Create()
    {
        // 128 KB
        const int blockSizeBytes = 128 * 1024;
        // 1 MB
        const int largeBufferMultipleBytes = 1024 * 1024;
        // 128 MB
        const int maximumBufferSizeBytes = 128 * 1024 * 1024;
        // 12.5 MB
        const int maximumSmallPoolFreeBytes = 100 * blockSizeBytes;
        // 512 MB
        const int maximumLargePoolFreeBytes = maximumBufferSizeBytes * 4;

        return new RecyclableMemoryStreamManager(
            blockSize: blockSizeBytes,
            largeBufferMultiple: largeBufferMultipleBytes,
            maximumBufferSize: maximumBufferSizeBytes,
            maximumSmallPoolFreeBytes: maximumSmallPoolFreeBytes,
            maximumLargePoolFreeBytes: maximumLargePoolFreeBytes
            );
    }
}
