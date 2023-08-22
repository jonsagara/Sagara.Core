using Microsoft.IO;

namespace Sagara.Core.IO;

public static class RecyclableMemoryStreamManagerHelper
{
    /// <summary>
    /// Create a new <see cref="RecyclableMemoryStreamManager"/> instance that can be used by the DI framework.
    /// </summary>
    /// <remarks>
    /// See: https://github.com/microsoft/Microsoft.IO.RecyclableMemoryStream
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
