using System;
using System.Collections.Generic;
using System.Text;

namespace BufferManager
{
    class BufferManager
    {
        int mBuffercapacity = 0;
        int mBufferSize = 0;
        int mBufferRemainderSize = 0;
        int mMaxPakcetSize = 0;
        private byte[] mBuffer = null;
        private byte[] mBufferTemp = null;
        private int mWritePos = 0;
        private int mReadPos = 0;

        public bool Init(int bufferSize, int maxPacketSize)
        {
            if (maxPacketSize > bufferSize)
                return false;
            
            mBuffercapacity = bufferSize;
            mBufferRemainderSize = bufferSize;
            mMaxPakcetSize = maxPacketSize;
            mBufferSize = 0;
            mWritePos = 0;
            mReadPos = 0;
            mBuffer = new byte[bufferSize];
            mBufferTemp = new byte[bufferSize];
            return true;
        }
        public bool Write(byte[] src, int srcOffset, int count)
        {
            if (count > mBufferRemainderSize)
                return false;
            if (count <= 0)
                return false;

            Buffer.BlockCopy(src, srcOffset, mBuffer, mWritePos, count);
            mWritePos += count;
            mBufferRemainderSize -= count;
            mBufferSize += count;
            
            if(IsFull() == false)
            {
                LeftShiftBuffer();
            }
            return true;
        }
        public bool Read(byte[] dest, int destOffset, int count, bool delete = true)
        {
            if (count > mBufferSize)
                return false;
            if (count <= 0)
                return false;
            if (mReadPos + count > mWritePos)
                return false;

            Buffer.BlockCopy(mBuffer, mReadPos, dest, destOffset, count);
            
            if(delete)
            {
                mReadPos += count;
                mBufferSize -= count;
            }
            if (IsFull() == false)
            {
                LeftShiftBuffer();
            }
            return true;
        }
        public int Capacity()
        {
            return mBuffercapacity;
        }
        public int Size()
        {
            return mBufferSize;
        }
        private bool IsOver()
        {
            if (mMaxPakcetSize > mBufferRemainderSize + mReadPos)
                return true;
            else
                return false;
        }
        private bool IsFull()
        {
            if (mMaxPakcetSize > mBufferRemainderSize + mReadPos)
                return true;
            else
                return false;
        }
        private void LeftShiftBuffer()
        {
            if(mReadPos > 0)
            {
                Buffer.BlockCopy(mBuffer, mReadPos, mBufferTemp, 0, mBufferSize);
                Buffer.BlockCopy(mBufferTemp, 0, mBuffer, 0, mBufferSize);
                mBufferRemainderSize += mReadPos;
                mReadPos = 0;
                mWritePos = mBufferSize;
            }
        }
    }
}
