using System;

namespace MapleLauncher.Net
{
	public class InPacket
    {
        private readonly byte[] m_buffer;
        private int m_index;

        public int Position
        {
            get
            {
                return m_index;
            }
        }
        public int Available
        {
            get
            {
                return m_buffer.Length - m_index;
            }
        }

        public ushort OperationCode { get; private set; }

        public InPacket(byte[] packet, bool encryped = true)
        {
            m_buffer = packet;
            m_index = 0;

            if (encryped)
            {
                this.OperationCode = this.ReadUShort();
            }
        }

        public void Reset()
        {
            this.m_index = 0;
        }

        private void CheckLength(int length)
        {
            if (m_index + length > m_buffer.Length || length < 0)
                throw new PacketException("Not enough space");
        }

        public byte ReadByte()
        {
            CheckLength(1);
            return m_buffer[m_index++];
        }
        public bool ReadBool()
        {
            return ReadByte() == 1;
        }
        public byte[] ReadBytes(int count)
        {
            CheckLength(count);
            var temp = new byte[count];
            Buffer.BlockCopy(m_buffer, m_index, temp, 0, count);
            m_index += count;
            return temp;
        }
        public unsafe short ReadShort()
        {
            CheckLength(2);

            short value;

            fixed (byte* ptr = m_buffer)
            {
                value = *(short*)(ptr + m_index);
            }

            m_index += 2;

            return value;
        }
        public unsafe ushort ReadUShort()
        {
            CheckLength(2);

            ushort value;

            fixed (byte* ptr = m_buffer)
            {
                value = *(ushort*)(ptr + m_index);
            }

            m_index += 2;

            return value;
        }
        public unsafe int ReadInt()
        {
            CheckLength(4);

            int value;

            fixed (byte* ptr = m_buffer)
            {
                value = *(int*)(ptr + m_index);
            }

            m_index += 4;

            return value;
        }
        public unsafe long ReadLong()
        {
            CheckLength(8);

            long value;

            fixed (byte* ptr = m_buffer)
            {
                value = *(long*)(ptr + m_index);
            }

            m_index += 8;

            return value;
        }

        public string ReadString(int count = -1)
        {
            if (count == -1)
            {
                count = this.ReadShort();
            }

            CheckLength(count);

            char[] final = new char[count];

            for (int i = 0; i < count; i++)
            {
                final[i] = (char)ReadByte();
            }

            return new string(final);
        }

		public byte[] ReadLeftoverBytes()
		{
			return this.ReadBytes(this.Available);
		}

        public void Skip(int count)
        {
            CheckLength(count);
            m_index += count;
        }

        public byte[] ToArray()
        {
            var final = new byte[m_buffer.Length];
            Buffer.BlockCopy(m_buffer, 0, final, 0, m_buffer.Length);
            return final;
        }
    }
}
