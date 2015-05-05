using System;
using System.IO;
using System.Runtime.Serialization.Json;

namespace SharedDto
{
    public class ProcessSerialization : IDisposable
    {
        private bool _disposed;
        private MemoryStream _memoryStream;
        private StreamReader _streamReader;

        public string SerializeJson(Type objectType, dynamic objectClass)
        {
            var jsonSer = new DataContractJsonSerializer(objectType);
            _memoryStream = new MemoryStream();
            jsonSer.WriteObject(_memoryStream, objectClass);
            _memoryStream.Position = 0;
            _streamReader = new StreamReader(_memoryStream);
            var result = _streamReader.ReadToEnd();
            return result;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (!_disposed)
                {
                    if (_memoryStream != null) _memoryStream.Dispose();
                    if (_streamReader != null) _streamReader.Dispose();
                    _disposed = true;
                }
            }
        }

        public void Dispose()
        {

            GC.SuppressFinalize(this);
        }
    }
}
