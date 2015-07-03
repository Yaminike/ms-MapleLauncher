using System;
using System.Globalization;
using System.IO;

namespace MapleLauncher.Net
{
	public class OutPacket : IDisposable
    {
        public const int DefaultBufferSize = 32;

        private MemoryStream m_stream;
        private bool m_disposed;

        public int Position
        {
            get
            {
                return (int)m_stream.Position;
            }
            set
            {
                if (value <= 0)
                    throw new PacketException("Value less than 1");

                m_stream.Position = value;
            }
        }
        public bool Disposed
        {
            get
            {
                return m_disposed;
            }
        }

        public OutPacket()
        {
            m_stream = new MemoryStream(DefaultBufferSize);
            m_disposed = false;
        }

		public OutPacket(InPacket inPacket)
		{
			m_stream = new MemoryStream(DefaultBufferSize);
			m_disposed = false;
			WriteUShort(inPacket.OperationCode);
			WriteBytes(inPacket.ReadLeftoverBytes());
		}

        public OutPacket(ushort opcode, int size = DefaultBufferSize)
        {
            m_stream = new MemoryStream(size);
            m_disposed = false;
            WriteUShort(opcode);
        }

        //From LittleEndianByteConverter by Shoftee
        private void Append(long value, int byteCount)
        {
            for (int i = 0; i < byteCount; i++)
            {
                m_stream.WriteByte((byte)value);
                value >>= 8;
            }
        }

		private void Reset(int position)
		{
			this.m_stream.Position = position;
		}

        public void WriteBool(bool value)
        {
            ThrowIfDisposed();
            WriteByte(value ? (byte)1 : (byte)0);
        }
        public void WriteByte(byte value = 0)
        {
            ThrowIfDisposed();
            m_stream.WriteByte(value);
        }

		public void WriteSByte(sbyte value = 0)
		{
			ThrowIfDisposed();
			Append(value, 1);
		}

        public void WriteBytes(params byte[] value)
        {
            ThrowIfDisposed();
            m_stream.Write(value, 0, value.Length);
        }
        public void WriteShort(short value = 0)
        {
            ThrowIfDisposed();
            Append(value, 2);
        }
        public void WriteUShort(ushort value = 0)
        {
            ThrowIfDisposed();
            Append(value, 2);
        }
        public void WriteInt(int value = 0)
        {
            ThrowIfDisposed();
            Append(value, 4);
        }
		public void WriteUInt(uint value = 0)
		{
			ThrowIfDisposed();
			Append(value, 4);
		}
		public void SetUInt(int position, uint value)
		{
			int temp = (int)this.Position;
			this.Reset(position);
			this.WriteUInt(value);
			this.Reset(temp);
		}
        public void WriteLong(long value = 0)
        {
            ThrowIfDisposed();
            Append(value, 8);
        }
		public void WriteString(string value)
        {
            ThrowIfDisposed();

            WriteShort((short)value.Length);

            foreach (char c in value)
                WriteByte((byte)c);
        }
		public void WriteString(string value, int length)
		{
			var i = 0;

			for (; i < value.Length & i < length; i++)
			{
				WriteByte((byte)value[i]);
			}

			for (; i < length; i++)
			{
				this.WriteByte();
			}
		}

        public void WriteIntDateTime(DateTime item)
        {
            string time = item.Year.ToString();
            time += item.Month < 10 ? ("0" + item.Month.ToString()) : item.Month.ToString();
            time += item.Day < 10 ? ("0" + item.Day.ToString()) : item.Day.ToString();
            time += item.Hour < 10 ? ("0" + item.Hour.ToString()) : item.Hour.ToString();
            this.WriteInt(int.Parse(time));
        }

        public void WriteLongDateTime(DateTime item)
        {
            this.WriteLong((long)((item.Millisecond * 10000) + 116444592000000000L));
        }

        public void WriteHexString(string input)
        {
            input = input.Replace(" ", "");

            if (input.Length % 2 != 0)
            {
                throw new ArgumentException("Input size is incorrect.");
            }

            for (int i = 0; i < input.Length; i += 2)
            {
                this.WriteByte(byte.Parse(input.Substring(i, 2), NumberStyles.HexNumber));
            }
        }

        public void Skip(int count)
        {
            if (count <= 0)
                throw new PacketException("Value less than 1");

            for (int i = 0; i < count; i++)
                WriteByte();
        }

        public byte[] ToArray()
        {
            ThrowIfDisposed();
            return m_stream.ToArray();
        }

        private void ThrowIfDisposed()
        {
            if (m_disposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }
        }

        public void Dispose()
        {
            m_disposed = true;

            if (m_stream != null)
                m_stream.Dispose();

            m_stream = null;
        }
    }
}
