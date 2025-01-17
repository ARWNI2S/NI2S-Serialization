using System.Buffers;

namespace ARWNI2S.Serialization.Testing
{
    public interface IOutputBuffer
    {
        ReadOnlySequence<byte> GetReadOnlySequence(int maxSegmentSize);
    }
}