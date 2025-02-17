using ARWNI2S.Serialization.Buffers;
using ARWNI2S.Serialization.Codecs;
using ARWNI2S.Serialization.GeneratedCodeHelpers;
using ARWNI2S.Serialization.WireProtocol;
using System.Buffers;

namespace ARWNI2S.Serialization.Serializers
{
    /// <summary>
    /// Serializer for types which are abstract and therefore cannot be instantiated themselves, such as abstract classes and interface types.
    /// </summary>
    public class AbstractTypeSerializer<TField> : AbstractTypeSerializer, IFieldCodec<TField>, IBaseCodec<TField> where TField : class
    {
        public AbstractTypeSerializer() : base(typeof(TField)) { }

        public void WriteField<TBufferWriter>(ref Writer<TBufferWriter> writer, uint fieldIdDelta, Type expectedType, TField value) where TBufferWriter : IBufferWriter<byte>
            => base.WriteField(ref writer, fieldIdDelta, expectedType, value);

        public new TField ReadValue<TInput>(ref Reader<TInput> reader, Field field) => (TField)base.ReadValue(ref reader, field);

        public virtual void Serialize<TBufferWriter>(ref Writer<TBufferWriter> writer, TField instance) where TBufferWriter : IBufferWriter<byte> { }

        public virtual void Deserialize<TReaderInput>(ref Reader<TReaderInput> reader, TField instance) => reader.ConsumeEndBaseOrEndObject();
    }

    // without the class type constraint
    internal sealed class AbstractTypeSerializerWrapper<TField> : AbstractTypeSerializer, IFieldCodec<TField>
    {
        public AbstractTypeSerializerWrapper() : base(typeof(TField)) { }

        public void WriteField<TBufferWriter>(ref Writer<TBufferWriter> writer, uint fieldIdDelta, Type expectedType, TField value) where TBufferWriter : IBufferWriter<byte>
            => base.WriteField(ref writer, fieldIdDelta, expectedType, value);

        public new TField ReadValue<TInput>(ref Reader<TInput> reader, Field field) => (TField)base.ReadValue(ref reader, field);
    }

    public class AbstractTypeSerializer : IFieldCodec
    {
        private readonly Type _fieldType;

        internal AbstractTypeSerializer(Type fieldType) => _fieldType = fieldType;

        public void WriteField<TBufferWriter>(ref Writer<TBufferWriter> writer, uint fieldIdDelta, Type expectedType, object value) where TBufferWriter : IBufferWriter<byte>
        {
            if (value is null)
            {
                ReferenceCodec.WriteNullReference(ref writer, fieldIdDelta);
                return;
            }

            var specificSerializer = writer.Session.CodecProvider.GetCodec(value.GetType());
            specificSerializer.WriteField(ref writer, fieldIdDelta, expectedType, value);
        }

        public object ReadValue<TInput>(ref Reader<TInput> reader, Field field)
        {
            if (field.IsReference)
                return ReferenceCodec.ReadReference(ref reader, field.FieldType ?? _fieldType);

            var fieldType = field.FieldType;
            if (fieldType is null)
                ThrowMissingFieldType();

            var specificSerializer = reader.Session.CodecProvider.GetCodec(fieldType);
            return specificSerializer.ReadValue(ref reader, field);
        }

        private void ThrowMissingFieldType() => throw new FieldTypeMissingException(_fieldType);
    }
}