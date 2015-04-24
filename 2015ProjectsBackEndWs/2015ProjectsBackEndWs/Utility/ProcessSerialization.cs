using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Web;
using System.Web.Script.Serialization;

namespace _2015ProjectsBackEndWs.Utility
{
    public class ProcessSerialization :IDisposable
    {
        private bool _Disposed = false;
        private MemoryStream _MemoryStream;
        private StreamReader _StreamReader;

        public string SerializeJson(Type objectType, dynamic objectClass)
        {
            string result = string.Empty;
            DataContractJsonSerializer jsonSer = new DataContractJsonSerializer(objectType);
            _MemoryStream = new MemoryStream();            
            jsonSer.WriteObject(_MemoryStream, objectClass);
            _MemoryStream.Position = 0;
            _StreamReader = new StreamReader(_MemoryStream);
            result = _StreamReader.ReadToEnd();
            return result;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (!_Disposed)
                {
                    if (_MemoryStream != null) _MemoryStream.Dispose();
                    if (_StreamReader != null) _StreamReader.Dispose();
                    _Disposed = true;
                } 
            }
        }

        public void Dispose()
        {

            GC.SuppressFinalize(this);
        }
    }
}